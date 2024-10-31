using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utilities
{
    /// <summary>
    /// The exception that is thrown when we got a not supported type
    /// </summary>
    public class UnsupportedTypeException : Exception
    {
        /// <summary>
        /// The exception that is thrown when we got a not supported type
        /// </summary>
        public UnsupportedTypeException() : base("Not supported type") { }

        /// <summary>
        /// The exception that is thrown when we got a not supported type
        /// </summary>
        public UnsupportedTypeException(string message) : base(message) { }
    }
}
