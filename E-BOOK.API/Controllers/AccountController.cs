using DATA.DTO;
using E_BOOK.API.Repository.Repository_Interface;
using E_BOOK.API.Service;
using E_BOOK.API.Service.Service_Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MODEL.DTO;
using MODEL.Entity;
using System.ComponentModel.DataAnnotations;

namespace E_BOOK.API.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IEmailService _emailService;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IAccountRepository accountRepository, IEmailService emailService, UserManager<AppUser> userManager, ILogger<AccountController> logger)
        {
            _accountRepository = accountRepository;
            _emailService = emailService;
            _userManager = userManager;
            _logger = logger;
        }
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUp signUp)
        {
            try
            {

                var result = await _accountRepository.SignUpAsync(signUp);
                if (result != null)
                {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(result);
                    var callbackUrl2 = $"{HttpContext.Request.Scheme}://localhost:7226/confirm_email?email={result.Email}&token={token}";
                    var message = new Message(new string[] { result.Email }, "Confirmation email Link", $"Please confirm your account by clicking this link: <a href='{callbackUrl2}'>link</a>");
                    _emailService.SendEmail(message);
                    return Ok(new { Message = $"Account was created Successfully and Confirm Email Link was sent to {result.Email} successfully", StatusCode = StatusCodes.Status200OK });

                }
                return Unauthorized(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing sign up request");
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] SignInModel signInModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = await _accountRepository.LoginAsync(signInModel);
                if (string.IsNullOrEmpty(result))
                {
                    return Unauthorized(result);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing login request");
                return Unauthorized(ex.Message);
            }

        }
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([Required] string Email)
        {
            var tokenGen = await _accountRepository.ForgotPassword(Email);
            var message = new Message(new string[] { Email }, "Reset Password Code", $"<p>Your reset password code is below<p><br/><h1>{tokenGen}</h1><br/> <p>Please use it in your reset password page</p>");
            _emailService.SendEmail(message);
            return Ok(new { Message = $"Account Password Reset Code was sent to {Email} successfully", StatusCode = StatusCodes.Status200OK });
        }
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPassword resetPassword)
        {
            try
            {
                var resetAccountPassword = await _accountRepository.ResetPassword(resetPassword);
                if (resetAccountPassword != null)
                {
                    return Ok(new { Email = resetAccountPassword.Email, NewPassword = resetAccountPassword.Password });
                }
                return BadRequest("Reset password not successful");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing reset password request");
                return BadRequest(ex.Message);
            }

        }
        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(ConfirmEmailDTO confirmEmailDTO)
        {
            try
            {
                var result = await _accountRepository.ConfirmEmail(confirmEmailDTO.token, confirmEmailDTO.email);
                if (result)
                {
                    return Ok(new { Success = true, Message = "Email Confirm Successfully", StatusCode = StatusCodes.Status200OK });
                }

                return StatusCode(StatusCodes.Status400BadRequest, "Error, Wrong token");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing confirm email request");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error, email not Varied");
            }
        }
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
        [HttpGet("search/email")]
        public async Task<IActionResult> FindUserbyEmail(string email)
        {
            try
            {

                var user = await _accountRepository.FindUserByEmailAsync(email);
                if (user != null)
                {
                    var result = new DisplayFindUserDTO();
                    result.FirstName = user.FirstName;
                    result.Email = user.Email;
                    result.ProfilePicture = user.ProfilePicture;
                    result.PhoneNumber = user.PhoneNumber;
                    result.UserName = user.UserName;
                    result.Id = user.Id;
                    result.LastName = user.LastName;
                    result.Gender = user.Gender;
                    return Ok(result);
                }
                return NotFound("User not found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing finding user by email request");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error finding user by email from the database");
            }
        }
        [HttpGet("all")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
        public async Task<IActionResult> GetallUser(int pageNumber, int perPageSize)
        {
            try
            {
                var alluser = await _accountRepository.GetAllUser(pageNumber, perPageSize);
                if (alluser != null)
                {
                    return Ok(alluser);
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error getting all user from the database");

            }
        }
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
        [HttpGet("search/id")]
        public async Task<IActionResult> FindUserbyId(string Id)
        {
            try
            {

                var user = await _accountRepository.FindUserByIdAsync(Id);
                if (user != null)
                {
                    var result = new DisplayFindUserDTO();
                    result.FirstName = user.LastName;
                    result.Email = user.Email;
                    result.ProfilePicture = user.ProfilePicture;
                    result.PhoneNumber = user.PhoneNumber;
                    result.UserName = user.UserName;
                    result.Id = user.Id;
                    result.LastName = user.LastName;
                    result.Gender = user.Gender;
                    return Ok(result);
                }
                return NotFound("User not found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing finding user by id request");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error finding user by id from the database");
            }
        }
        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
        [HttpPatch("update_role")]
        public async Task<IActionResult> UpdateRoleAsync(string email, string role)
        {
            try
            {
                var UpdateRole = await _accountRepository.AddRoleAsync(email, role);
                if (UpdateRole)
                {
                    return Ok(new { Message = "Role updated successfully", StatusCode = StatusCodes.Status200OK });
                }
                return StatusCode(StatusCodes.Status500InternalServerError, "Error  Image to the database");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing role request for user");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error  Image to the database");
            }
        }
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPut("update/{email}")]
        public async Task<IActionResult> UpdateUser(string email, UpdateUserDTO updateDetails)
        {
            try
            {
                var user = await _accountRepository.UpdateUserInfo(email, updateDetails);
                if (user)
                {
                    return NoContent();
                }
                return BadRequest("Fail to update database");
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "An error occurred while processing update for user details request");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating user to the database");
            }
        }
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPatch("upload")]
        public async Task<IActionResult> UploadProfilePictureAsync(IFormFile file, string email)
        {
            try
            {
                var result = await _accountRepository.UploadProfilePic(file, email);
                return Ok(new
                {
                    PublicId = result.PublicId,
                    Url = result.SecureUrl.ToString(),
                    Status = "Uploaded Succefully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing photo upload for user request");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Uploading Image to the database");
            }
        }
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpDelete("delete/{email}")]
        public async Task<IActionResult> DeleteUser(string email)
        {
            try
            {
                var result = await _accountRepository.DeleteUserByEmail(email);
                if (result)
                {
                    return NoContent();
                }
                return BadRequest("User not deleted successfully");
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "An error occurred while processing delete for user request");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting user from the database");
            }
        }

    }
}
