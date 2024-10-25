using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// The exception that is thrown when trying to establish a connection, but it was unsuccessfull
    /// </summary>
    public class UnsuccessfullConnectionException:Exception
    {
        /// <summary>
        /// The exception that is thrown when trying to establish a connection, but it was unsuccessfull
        /// </summary>
        public UnsuccessfullConnectionException():base("Can't connect to the server") { }

        /// <summary>
        /// The exception that is thrown when trying to establish a connection, but it was unsuccessfull
        /// </summary>
        public UnsuccessfullConnectionException(string message) : base(message) { }
    }
}
