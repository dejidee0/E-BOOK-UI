using DATA.DTO;
using MODEL.DTO;
using MODEL.Entity;

namespace E_BOOK.UI.Service.Interface
{
    public interface IAccountHttpService
    {
        

        Task<string> LoginAsyc(SignInModel signInModel);
        Task<string> UploadProfielPic(IFormFile formFile, string email);
        Task<bool> SignUpAsync(SignUp signUp);
        Task<bool> ConfirmAccount(ConfirmEmailDTO confirmEmailDTO);
        Task<bool> ForgotPassword(string email);
        Task<bool> UpdateUserRole(string email, string role);
        Task<bool> ResetPassword(ResetPassword reset);
        Task<DisplayFindUserDTO> FindUserByAsyc(string email);
        Task<bool> DeleteUserByEmailAsyc(string email);
        Task<bool> UpdateUserByEmailAsyc(string email, UpdateUserDTO updateUserDTO);
        Task<PaginatedUser> GetAllUserAsyc(int pageNumber, int perPageSize);
        
    }
}
