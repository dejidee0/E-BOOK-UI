using CloudinaryDotNet.Actions;
using DATA.DTO;
using E_BOOK.API.Repository.Repository_Interface;
using E_BOOK.API.Service.Service_Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MODEL.DTO;
using MODEL.Entity;
using static System.Reflection.Metadata.BlobBuilder;

namespace E_BOOK.API.Repository
{
    public class AccountRepository : IAccountRepository
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IGenerateJwt _generateJwt;
        private readonly ICloudinaryService _cloudinaryService;

        public AccountRepository(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IConfiguration configuration, RoleManager<IdentityRole> roleManager, IGenerateJwt generateJwt, ICloudinaryService cloudinaryService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration ?? throw new ArgumentNullException();
            _roleManager = roleManager;
            _generateJwt = generateJwt;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<AppUser> SignUpAsync(SignUp signUp)
        {
            var existUser = await _userManager.FindByEmailAsync(signUp.Email);
            if (existUser != null)
            {
                throw new Exception("User already exists");
            }

            if (await _roleManager.RoleExistsAsync("USER"))
            {
                var user = new AppUser()
                {
                    FirstName = signUp.FirstName,
                    LastName = signUp.LastName,
                    Email = signUp.Email,
                    UserName = signUp.Email,
                    PhoneNumber = signUp.PhoneNumber,
                    ProfilePicture = string.Empty,
                    Gender = signUp.Gender,

                };
                var result = await _userManager.CreateAsync(user, signUp.Password);
                if (!result.Succeeded)
                {

                    throw new Exception("User failed to create");
                }
                await _userManager.AddToRoleAsync(user, "USER");

                return user;
            }
            else
            {
                throw new Exception("This role does not exist");
            }

        }
        public async Task<AppUser> FindUserByEmailAsync(string email)
        {
            var result = await _userManager.FindByEmailAsync(email);
            return result;
        }
        public async Task<AppUser> FindUserByIdAsync(string id)
        {
            var result = await _userManager.FindByIdAsync(id);
            return result;
        }
        public async Task<string> LoginAsync(SignInModel signIn)
        {

            var user = await _userManager.FindByEmailAsync(signIn.Email);
            if (user == null)
            {
                throw new Exception("Email does not exist");
            }
            var checkConfirm = await _userManager.IsEmailConfirmedAsync(user);
            if (!checkConfirm)
            {
                throw new Exception("Email not Confirm, Please Confirm ur Email");

            }
            if (!await _userManager.CheckPasswordAsync(user, signIn.Password))
            {
                throw new Exception("Invalid password");
            }

            var generateToken = await _generateJwt.GenerateToken(user, signIn.Email);
            if (await _userManager.CheckPasswordAsync(user, signIn.Password))
            {
                return generateToken;
            }
            throw new Exception("Registration not successful");
        }
        public async Task<ImageUploadResult> UploadProfilePic(IFormFile file, string email)
        {
            var findUser = await _userManager.FindByEmailAsync(email);
            if (findUser == null)
            {
                throw new Exception("Account to upload Profile Picture for not found");
            }
            var uploadImageResult = await _cloudinaryService.UploadPhoto(file, findUser);
            if (uploadImageResult == null)
            {
                throw new Exception("Image not uploaded successfully");
            }
            findUser.ProfilePicture = uploadImageResult.Url.ToString();
            var updateUserImg = await _userManager.UpdateAsync(findUser);
            if (updateUserImg.Succeeded)
            {
                return uploadImageResult;
            }
            throw new Exception("Contact details not updated successfully");
        }
        public async Task<bool> AddRoleAsync(string email, string Role)
        {
            var existingUser = await _userManager.FindByEmailAsync(email);
            if (existingUser == null)
            {
                throw new Exception("The user to update role for not exist");
            }
            var existingRoles = await _userManager.GetRolesAsync(existingUser);
            var RemoveExistingRole = await _userManager.RemoveFromRolesAsync(existingUser, existingRoles);
            if (await _roleManager.RoleExistsAsync(Role))
            {
                var AddRole = _userManager.AddToRoleAsync(existingUser, Role);
                if (AddRole.IsCompletedSuccessfully)
                {

                    return true;
                }
                throw new Exception("Error Updating Role");
            }
            throw new Exception("These row does not exist");
        }
        public async Task<string> ForgotPassword(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                if (token != null)
                {
                    return token;
                }
                throw new Exception("Token not generated");
            }
            throw new Exception("User email reset password not found");
        }
        public async Task<bool> ConfirmEmail(string token, string email)
        {

            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    return true;
                }
                throw new Exception("token not valid");
            }
            throw new Exception("User not available");
        }
        public async Task<ResetPassword> ResetPassword(ResetPassword resetPassword)
        {
            var user = await _userManager.FindByEmailAsync(resetPassword.Email);
            var result = await _userManager.ResetPasswordAsync(user, resetPassword.Token, resetPassword.Password);
            if (result.Succeeded)
            {
                return resetPassword;
            }
            throw new Exception("Reset Password not Successfully");
        }
        public async Task<bool> UpdateUserInfo(string email, UpdateUserDTO user)
        {
            var findUser = await FindUserByEmailAsync(email);
            if (findUser != null)
            {
                findUser.Email = user.Email;
                findUser.FirstName = user.FirstName;
                findUser.LastName = user.LastName;
                findUser.UserName = user.UserName;
                findUser.PhoneNumber = user.PhoneNumber;
                findUser.Gender = user.Gender;
                var result = await _userManager.UpdateAsync(findUser);

                if (result.Succeeded)
                {
                    return true;
                }
                return false;
            }
            throw new Exception("User to Update not available");

        }
        public async Task<bool> DeleteUserByEmail(string email)
        {
            var findUserToDelete = await FindUserByEmailAsync(email);
            if (findUserToDelete != null)
            {
                var result = await _userManager.DeleteAsync(findUserToDelete);
                if (result.Succeeded)
                {
                    return true;
                }
                return false;
            }
            throw new Exception("User to delete not available");
        }
        
        public async Task<PaginatedUser> GetAllUser(int pageNumber, int perPageSize)
        {
            var getAllUser =  _userManager.Users;
            pageNumber = pageNumber < 1 ? 1 : pageNumber;
            perPageSize = perPageSize < 1 ? 5 : perPageSize;
            var totalCount = getAllUser.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / perPageSize);
            var paginated = await getAllUser.Skip((pageNumber - 1) * perPageSize).Take(perPageSize).ToListAsync();
            var DisplayUser = new List<DisplayFindUserDTO>();
            foreach (var item in paginated)
            {
               DisplayUser.Add(new DisplayFindUserDTO 
               {
                   UserName = item.UserName,
                Email = item.Email,
                FirstName = item.FirstName,
                PhoneNumber = item.PhoneNumber,
                ProfilePicture = item.ProfilePicture,
                LastName = item.LastName,
                Gender = item.Gender,
                Id = item.Id
            });
            }
            var result = new PaginatedUser
            {
                CurrentPage = pageNumber,
                PageSize = perPageSize,
                TotalPages = totalPages,
                User = DisplayUser 
            };
            return result;
        }
    }
}

