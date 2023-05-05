using MODEL.Entity;

namespace E_BOOK.API.Service.Service_Interface
{
    public interface IGenerateJwt
    {
        Task<string> GenerateToken(AppUser user, string Email);
    }
}
