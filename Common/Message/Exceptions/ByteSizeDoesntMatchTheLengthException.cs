using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message.Exceptions
{
    /// <summary>
    /// The exception that is thrown when there is more byte available then the Length property describes it
    /// </summary>
    public class ByteSizeDoesntMatchTheLengthException : Exception
    {
        /// <summary>
        /// The exception that is thrown when there is more byte available then the Length property describes it
        /// </summary>
        public ByteSizeDoesntMatchTheLengthException() : base("Bytes are missing and can't create a message object") { }

        /// <summary>
        /// The exception that is thrown when there is more byte available then the Length property describes it
        /// </summary>
        public ByteSizeDoesntMatchTheLengthException(string message) : base(message) { }
    }
}
