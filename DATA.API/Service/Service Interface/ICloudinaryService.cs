using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace E_BOOK.API.Service.Service_Interface
{
    public interface ICloudinaryService
    {
        Task<ImageUploadResult> UploadPhoto(IFormFile file, object id);
    }
}
