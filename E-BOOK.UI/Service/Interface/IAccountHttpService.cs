using DATA.DTO;
using MODEL.DTO;
using MODEL.Entity;

namespace E_BOOK.UI.Service.Interface
{
    public interface IAccountHttpService
    {
        Task<string> LoginAsyc(SignInModel signInModel);
        Task<string> UploadProfielPic(IFormFile formFile);
        Task<bool> SignUpAsync(SignUp signUp);
        Task<bool> ConfirmAccount(ConfirmEmailDTO confirmEmailDTO);
        Task<bool> ForgotPassword(string email);
        Task<bool> ResetPassword(ResetPassword reset);
    }
}
