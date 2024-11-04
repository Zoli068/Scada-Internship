﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Command
{
    /// <summary>
    /// The exception that is thrown when the value of an atribute is out of the definied interval
    /// </summary>
    public class ValueOutOfIntervalException : Exception
    {
        /// <summary>
        /// The exception that is thrown when the value of an atribute is out of the definied interval
        /// </summary>  
        public ValueOutOfIntervalException() : base("Value is out from the interval") { }

        /// <summary>
        /// The exception that is thrown when the value of an atribute is out of the definied interval
        /// </summary>
        public ValueOutOfIntervalException(string message) : base(message) { }
    }
}
