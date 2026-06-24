using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Utility.Core.CertificateManagement
{
    public class FileP12 : IFileP12
    {
        private readonly IS3 _s3;
        public FileP12(IS3 s3)
        {
            _s3 = s3;
        }

        public async Task<X509Certificate2Collection> GetAllCertificates(string fileName, string passwordFile)
        {
            try
            {
                var fileStream = await _s3.DownloadFileAsync(fileName);

                if (fileStream is null)
                {
                    Log.Write($"{nameof(GetAllCertificates)} An error occurred while download file. FileName - {fileName}");
                    throw new Exception("An error occurred while download file.");
                }

                var byteFile = GetByteFile(fileStream);

                if (byteFile is null)
                {
                    Log.Write($"{nameof(GetAllCertificates)}, An error occurred while reading bytes from file. FileName - {fileName}");
                    throw new Exception("An error occurred while reading bytes from file.");
                }

                var result = GetCertificatesFromFile(byteFile, passwordFile);

                if (result is null)
                {
                    Log.Write($"{nameof(GetCertificatesFromFile)} An error occurred while getting the certificates file. FileName - {fileName}");
                    throw new Exception("An error occurred while get certificates file.");
                }

                return result;
            }
            catch (Exception exc)
            {
                Log.Write($"{nameof(GetAllCertificates)} An error occurred while retrieving the certificate file. FileName - {fileName} Exception - {exc.Message}");
                throw new Exception(exc.Message);
            }
        }


        public async Task<X509Certificate2> GetCertificateBySerialNumber(string serialNumber, string fileName, string passwordFile)
        {
            try
            {
                var fileStream = await _s3.DownloadFileAsync(fileName);

                if (fileStream is null)
                {
                    Log.Write($"{nameof(GetCertificateBySerialNumber)} An error occurred while download file. FileName - {fileName}");
                    throw new Exception("An error occurred while download file.");
                }

                var byteFile = GetByteFile(fileStream);

                if (byteFile is null)
                {
                    Log.Write($"{nameof(GetCertificateBySerialNumber)}, An error occurred while reading bytes from file. FileName - {fileName}");
                    throw new Exception("An error occurred while reading bytes from file.");
                }

                var result = GetCertificatesFromFileBySerialNumber(byteFile, serialNumber, passwordFile);

                if (result is null)
                {
                    Log.Write($"{nameof(GetCertificatesFromFileBySerialNumber)} An error occurred while getting the certificate by serial number. FileName - {fileName}");
                    throw new Exception("An error occurred while get certificate by name.");
                }

                return result;
            }
            catch (Exception exc)
            {
                Log.Write($"{nameof(GetCertificateBySerialNumber)} An error occurred while retrieving the certificate file. FileName - {fileName} Exception - {exc.Message}");
                throw new Exception(exc.Message);
            }
        }

        private byte[] GetByteFile(Stream fileStream)
        {
            try
            {
                byte[] fileByte = new byte[1];

                if (fileStream != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        fileStream.CopyTo(ms);
                        fileByte = ms.ToArray();
                    }
                }

                return fileByte;
            }
            catch (Exception exc)
            {
                Log.Write($"{nameof(GetByteFile)} An error occurred while reading bytes from file. {exc.Message}");
                throw new Exception(exc.ToString());
            }
        }

        private X509Certificate2Collection GetCertificatesFromFile(byte[] byteFile, string passwordFile)
        {
            var certificatesFileCollection = new X509Certificate2Collection();

            try
            {
                certificatesFileCollection.Import(byteFile, passwordFile, X509KeyStorageFlags.DefaultKeySet);
                return certificatesFileCollection;

            }
            catch (Exception exc)
            {
                Log.Write($"{ nameof(GetCertificatesFromFile)} An error occurred while importing the file into the certificate collection. {exc.Message}");
                throw new Exception(exc.ToString());
            }
        }

        private X509Certificate2 GetCertificatesFromFileBySerialNumber(byte[] byteFile, string serialNumber, string passwordFile)
        {
            var certificatesFileCollection = new X509Certificate2Collection();

            try
            {
                certificatesFileCollection.Import(byteFile, passwordFile, X509KeyStorageFlags.DefaultKeySet);
                return certificatesFileCollection.OfType<X509Certificate2>().FirstOrDefault(x => x.SerialNumber.Equals(serialNumber));

            }
            catch (Exception exc)
            {
                Log.Write($"{nameof(GetCertificatesFromFileBySerialNumber)} An error occurred when searching certificate by serial number. {exc.Message}");
                throw new Exception(exc.ToString());
            }
        }
    }
}
