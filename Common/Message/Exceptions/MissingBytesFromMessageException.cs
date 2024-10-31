using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message
{
    /// <summary>
    /// The exception that is thrown when there is no enough byte for creating the message object
    /// </summary>
    public class MissingBytesFromMessageException:Exception
    {
        /// <summary>
        /// The exception that is thrown when there is no enough byte for creating the message object
        /// </summary>
        public MissingBytesFromMessageException() : base("Bytes are missing and can't create a message object") { }

        /// <summary>
        /// The exception that is thrown when there is no enough byte for creating the message object
        /// </summary>
        public MissingBytesFromMessageException(string message) : base(message) { }
    }
}
