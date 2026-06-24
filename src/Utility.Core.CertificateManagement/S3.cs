using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Utility.Core.CertificateManagement
{
    public class S3 : IS3
    {
        private readonly AmazonS3Client _client;
        public S3()
        {
            _client = GetClientAWS();
        }
        public async Task<Stream> DownloadFileAsync(string fileName)
        {
            try
            {
                var fileTransferUtility = new TransferUtility(_client);
                var fileStream = await fileTransferUtility.OpenStreamAsync(S3Credentials.S3BucketName, fileName);

                fileTransferUtility.Dispose();

                return fileStream;
            }
            catch (AmazonS3Exception ex)
            {
                Log.Write($"{nameof(DownloadFileAsync)}, Error: {ex.Message} - {ex.InnerException} - {ex.StackTrace}");
                throw new Exception("File not found");
            }
            catch (Exception ex)
            {
                Log.Write($"{nameof(DownloadFileAsync)},Error: {ex.Message} - {ex.InnerException} - {ex.StackTrace}");
                throw new Exception("Download file failed.");
            }

        }

        private AmazonS3Client GetClientAWS()
        {
            try
            {
                string enviroment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

                if (enviroment.ToUpper().Equals("DEV"))
                {
                    var config = new AmazonS3Config
                    {
                        RegionEndpoint = RegionEndpoint.GetBySystemName(S3Credentials.Region)

                    };
                    config.SetWebProxy(System.Net.WebRequest.DefaultWebProxy);
                    return new AmazonS3Client(S3Credentials.AccessKey, S3Credentials.AccessSecret, S3Credentials.SessionToken, config);
                }

                return new AmazonS3Client(RegionEndpoint.GetBySystemName(S3Credentials.Region));
            }
            catch(AmazonS3Exception ex) 
            {
                Log.Write($"{nameof(GetClientAWS)},Error: {ex.Message} - {ex.InnerException} - {ex.StackTrace}");
                throw new Exception("Get client aws failed.");
            }

        }
    }
}
