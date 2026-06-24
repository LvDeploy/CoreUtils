using System.IO;
using System.Threading.Tasks;

namespace Utility.Core.CertificateManagement
{
    public interface IS3
    {
        Task<Stream> DownloadFileAsync(string fileName);
    }
}
