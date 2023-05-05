using CloudinaryDotNet.Actions;
using DATA.DTO;
using Microsoft.AspNetCore.Http;
using MODEL.DTO;
using MODEL.Entity;
using System.ComponentModel.DataAnnotations;

namespace E_BOOK.API.Repository.Repository_Interface
{
    public interface IAccountRepository
    {
        Task<AppUser> SignUpAsync(SignUp signUp);
        Task<string> LoginAsync(SignInModel signIn);
        Task<ImageUploadResult> UploadProfilePic(IFormFile file, string email);
        Task<bool> AddRoleAsync(string email, string Role);
        Task<string> ForgotPassword([Required] string email);
        Task<bool> ConfirmEmail(string token, string email);
        Task<ResetPassword> ResetPassword(ResetPassword resetPassword);
        Task<AppUser> FindUserByEmailAsync(string email);
        Task<AppUser> FindUserByIdAsync(string id);
        Task<bool> UpdateUserInfo(string email, UpdateUserDTO user);
        Task<bool> DeleteUserByEmail(string email);


    }
}
