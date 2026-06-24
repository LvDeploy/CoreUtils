using System;

namespace Utility.Core.CertificateManagement
{
    public static class S3Credentials
    {
        public static string? AccessKey { get; internal set; }
        public static string? AccessSecret { get; internal set; }
        public static string? S3BucketName { get; internal set; }
        public static string Region { get; internal set; }
        public static string? SessionToken { get; internal set; }
        public static string? Section { get; internal set; }

        public static void Validate()
        {

            string enviroment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            if (string.IsNullOrEmpty(Section))
                throw new Exception("Null or empty section - CoreCertificateManagement");

            if (string.IsNullOrEmpty(Region))
                throw new Exception("Null or empty property - Region");

            if (string.IsNullOrEmpty(S3BucketName))
                throw new Exception("Null or empty property - S3BucketName");

            if (enviroment.ToUpper().Equals("DEV"))
            {
                if (string.IsNullOrEmpty(AccessKey))
                    throw new Exception("Null or empty property - AccessKey");

                if (string.IsNullOrEmpty(AccessSecret))
                    throw new Exception("Null or empty property - AccessSecret");

                if (string.IsNullOrEmpty(SessionToken))
                    throw new Exception("Null or empty property - SessionToken");

            }
        }
    }
}
