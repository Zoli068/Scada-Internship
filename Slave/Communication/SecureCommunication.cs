﻿using System;
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
    /// <summary>
    /// SecureCommunication provides possibility to secure a <see cref="Stream"/> with TLS1.2 with x509 authentication
    /// </summary>
    public class SecureCommunication
    {
        /// <summary>
        /// Server Certificate
        /// </summary>
        private X509Certificate2 certificate = null;

        public SecureCommunication()
        {
            certificate = LoadCertificate();
        }

        /// <summary>
        /// This method from a <paramref name="stream"/> object creats a <see cref="SslStream"/> object
        /// </summary>
        /// <param name="stream"></param>
        /// <returns><see cref="SslStream"/> stream</returns>
        public Stream SecureStream(Stream stream)
        {
            SslStream sslStream = new SslStream(stream, false, new RemoteCertificateValidationCallback(ValidateClientCertificate), null);

            if(certificate == null)
            {
                certificate = LoadCertificate();
            }

            sslStream.AuthenticateAsServer(certificate, true,SslProtocols.Tls12, false);

            return sslStream;
        }

        /// <summary>
        /// Loads our certificate from the <see cref="X509Store"/> by the certificate thumbprint which is placed inside the app configuration
        /// </summary>
        /// <returns>Certificate which will be used for authentication</returns>
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

        /// <summary>
        ///  Method will determine the certificate validation, used inside the <see cref="SslStream.AuthenticateAsServer()"/> method
        /// </summary>
        /// <returns>The status of certificate</returns>
        public bool ValidateClientCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == SslPolicyErrors.None)
            {
                return true;
            }

            X509Certificate2 cert = certificate as X509Certificate2;

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