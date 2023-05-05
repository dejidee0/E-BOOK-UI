using E_BOOK.API.Repository;
using E_BOOK.API.Repository.Repository_Interface;
using E_BOOK.API.Service;
using E_BOOK.API.Service.Service_Interface;

namespace E_BOOK.API.Extension
{
    public static class RegisterServices
    {
        public static void ServiceConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IGenerateJwt, GenerateJwt>();
            services.AddScoped<ICloudinaryService, CloudinaryService>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IE_BookRepository, E_BookRepository>();
        }
    }
}
