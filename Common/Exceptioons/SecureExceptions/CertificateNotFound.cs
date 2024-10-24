using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptioons.SecureExceptions
{
    public class CertificateNotFound:Exception
    {
        public CertificateNotFound():base("Cant' find the certificate with the definied thumbprint. Please check the thumbprint, and check if the certificate is installed") { }
   
        public CertificateNotFound(string message) : base(message) { }
    }
}
