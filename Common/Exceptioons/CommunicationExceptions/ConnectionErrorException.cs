using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptioons.CommunicationExceptions
{
    /// <summary>
    /// The exception that is thrown when trying to use a connection, but an error happened
    /// </summary>
    public class ConnectionErrorException:Exception
    {
        /// <summary>
        /// The exception that is thrown when trying to use a connection, but an error happened
        /// </summary>
        public ConnectionErrorException():base("An error happened while using the connection") { }

        /// <summary>
        /// The exception that is thrown when trying to use a connection, but an error happened
        /// </summary>
        public ConnectionErrorException(string message):base(message) { }
    }
}
