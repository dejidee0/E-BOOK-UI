using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using E_BOOK.API.Service.Service_Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace E_BOOK.API.Service
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly IConfiguration _configuration;

        public CloudinaryService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<ImageUploadResult> UploadPhoto(IFormFile file, object id)
        {
            if (file == null || file.Length == 0)
            {
                throw new Exception("No Image Uploaded");
            }
            var cloudinary = new Cloudinary(new Account(_configuration["CloudinarySettings:CloudName"], _configuration["CloudinarySettings:ApiKey"], _configuration["CloudinarySettings:ApiSecret"]));
            using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, stream),
                PublicId = $"{id}"
            };
            var result = await cloudinary.UploadAsync(uploadParams);
            if (result != null)
            {
                return result;
            }
            throw new Exception("Image Fail to Upload");
        }
    }
}
