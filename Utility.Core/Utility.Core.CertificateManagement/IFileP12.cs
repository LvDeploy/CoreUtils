using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Utility.Core.CertificateManagement
{
    public interface IFileP12
    {
        /// <summary>
        /// Method that returns a collection of certificates present in the P12 file
        /// </summary>
        /// <param name="fileName">Enter the name of the file with the complete path example: folder/file_name.extension (is case sensitive)</param>
        /// <param name="passwordFile">Enter the password for the file</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        Task<X509Certificate2Collection> GetAllCertificates(string fileName, string passwordFile);

        /// <summary>
        /// Method that returns only a certificate present in the P12 file according to the certificate name 
        /// </summary>
        /// <param name="serialNumber">Enter the serial number of the certificate to be retrieved</param>
        /// <param name="fileName">Enter the name of the file with the complete path example: folder/file_name.extension</param>
        /// <param name="passwordFile">Enter the password for the file</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        Task<X509Certificate2> GetCertificateBySerialNumber(string serialNumber, string fileName, string passwordFile);
    }
}
