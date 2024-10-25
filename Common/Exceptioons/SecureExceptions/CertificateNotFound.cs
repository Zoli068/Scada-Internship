using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptioons.SecureExceptions
{
    /// <summary>
    /// The exception that is thrown when can't find the specified certificate 
    /// </summary>
    public class AuthenticationFailedException:Exception
    {
        /// <summary>
        /// The exception that is thrown when can't find the specified certificate 
        /// </summary>
        public AuthenticationFailedException():base("Authentication failed while creating a secure stream") { }

        /// <summary>
        /// The exception that is thrown when can't find the specified certificate 
        /// </summary>
        public AuthenticationFailedException(string message) : base(message) { }
    }
}
