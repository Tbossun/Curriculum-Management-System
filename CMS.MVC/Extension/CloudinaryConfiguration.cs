using CloudinaryDotNet;
using CMS.MVC.Models;

namespace CMS.MVC.Extension
{
    public static class CloudinaryConfiguration
    {
        public static void AddCloudinaryExtension(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(provider =>
            {
                var cloudinarySettings = configuration.GetSection("CloudinarySettings").Get<CloudinarySettings>();
                return new Cloudinary(new Account(cloudinarySettings.CloudName, cloudinarySettings.ApiKey, cloudinarySettings.ApiSecret));
            });
        }
    }

}
