﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utilities
{

    /// <summary>
    /// The exception that is thrown when we don't got enough bytes
    /// </summary>
    public class NotEnoughBytesException : Exception
    {
        /// <summary>
        /// The exception that is thrown when we don't got enough bytes
        /// </summary>
        public NotEnoughBytesException() : base("The method doesn't got enough bytes for finishing his job") { }

        /// <summary>
        /// The exception that is thrown when we don't got enough bytes
        /// </summary>
        public NotEnoughBytesException(string message) : base(message) { }
    }
}
