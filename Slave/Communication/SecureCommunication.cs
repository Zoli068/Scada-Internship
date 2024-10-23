using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Slave.Communication
{
    public class SecureCommunication
    {
        private X509Certificate2 certificate = null;

        public SecureCommunication()
        {
            certificate = LoadCertificate();
        }

        public Stream SecureStream(Stream stream)
        {
            SslStream sslStream = new SslStream(stream, false, new RemoteCertificateValidationCallback(ValidateClientCertificate), null);

            if (certificate == null)
            {
                certificate = LoadCertificate();
            }

            sslStream.AuthenticateAsServer(certificate, true,SslProtocols.Tls12, false);

            return sslStream;
        }

        private X509Certificate2 LoadCertificate()
        {
            string thumbprint = ConfigurationManager.AppSettings["CA_ThumbPrint"];

            using (var store = new X509Store(StoreName.CertificateAuthority, StoreLocation.LocalMachine))
            {
                store.Open(OpenFlags.ReadOnly);

                var certs = store.Certificates.Find(X509FindType.FindByThumbprint, thumbprint, false);

                return certs[0];
            }
        }


        public bool ValidateClientCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            //if no error then can be trusted
            if (sslPolicyErrors == SslPolicyErrors.None)
            {
                return true;
            }

            X509Certificate2 cert = certificate as X509Certificate2;

            //self signed certificate
            if (cert != null && cert.Issuer == cert.Subject)
            {
                X509Store trustedRoot = new X509Store(StoreName.Root, StoreLocation.LocalMachine);

                trustedRoot.Open(OpenFlags.ReadOnly);

                X509Certificate2Collection trustedCertificates = trustedRoot.Certificates;

                foreach (X509Certificate2 trustedCert in trustedCertificates)
                {
                    if (trustedCert.Thumbprint == cert.Thumbprint)
                    {
                        trustedRoot.Close();
                        return true;
                    }
                }

                trustedRoot.Close();

                return false;
            }

            //checking for CA inside a chain
            if (chain != null && chain.ChainStatus.Length > 0)
            {
                foreach (var status in chain.ChainStatus)
                {
                    if (status.Status != X509ChainStatusFlags.NoError)
                    {
                        return false;
                    }
                }
                return true;
            }

            return false;
        }
    }
}
