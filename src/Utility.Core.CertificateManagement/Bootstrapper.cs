using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Utility.Core.CertificateManagement
{
    [ExcludeFromCodeCoverage]
    public static class Bootstrapper
    {
        /// <summary>
        /// Registration method and get keys in appsettings needed for using the package
        /// </summary>
        /// <param name="services"></param>
        /// <param name="_configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddCertificateFileServices(this IServiceCollection services, IConfiguration _configuration)
        {

            services.AddScoped<IS3, S3>();
            services.AddScoped<IFileP12, FileP12>();
            SetValues(_configuration);


            return services;
        }

        private static void SetValues(IConfiguration _configuration)
        {
            S3Credentials.Section = _configuration.GetSection("CertificateManagement").Key;
            S3Credentials.AccessKey = _configuration.GetSection("CertificateManagement:AccessKey").Value;
            S3Credentials.AccessSecret = _configuration.GetSection("CertificateManagement:AccessSecret").Value;
            S3Credentials.S3BucketName = _configuration.GetSection("CertificateManagement:S3BucketName").Value;
            S3Credentials.Region = _configuration.GetSection("CertificateManagement:Region").Value;
            S3Credentials.SessionToken = _configuration.GetSection("CertificateManagement:SessionToken").Value;

            S3Credentials.Validate();
        }
    }
}
