using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Master
{
    public class AbstractSecureCommunication
    {
        private X509Certificate2 certificate=null;

        public AbstractSecureCommunication() { }
    
        public void SecureStream(Stream stream) 
        {
            SslStream sslStream = new SslStream(stream, false);

            if (certificate == null)
            {
                certificate =LoadCertificate();
            }

            sslStream.AuthenticateAsClient("localhost", new X509CertificateCollection {certificate}, false);
        }

        private X509Certificate2 LoadCertificate()
        {
            string thumbprint = ConfigurationManager.AppSettings["CA_ThumbPrint"];

            using (var store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
            {
                store.Open(OpenFlags.ReadOnly);

                var certs = store.Certificates.Find(X509FindType.FindByThumbprint, thumbprint, false);

                return certs[0];
            }
        }
    }
}
