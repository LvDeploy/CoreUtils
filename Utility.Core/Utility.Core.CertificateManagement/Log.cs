using System;

namespace Utility.Core.CertificateManagement
{
    internal static class Log
    {
        public static void Write(string message)
        {
            Console.WriteLine($"[CertificateManagement]: {message}");
        }
    }
}
