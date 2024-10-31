using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utilities { 
    /// <summary>
    /// The exception that is thrown when an we got an error in the process of serialization/deserialization
    /// </summary>
    public class SerializationException : Exception
    {
        /// <summary>
        /// The exception that is thrown when trying to use a connection, but that connection doesn't exists
        /// </summary>
        public SerializationException() : base("There is no existing connection") { }

        /// <summary>
        /// The exception that is thrown when trying to use a connection, but that connection doesn't exists
        /// </summary>
        public SerializationException(string message) : base(message) { }
    }
}
