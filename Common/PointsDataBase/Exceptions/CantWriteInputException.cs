﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.PointsDataBase
{
    /// <summary>
    /// The exception that is thrown when trying to write on an Input Point
    /// </summary>
    public class CantWriteInputException : Exception
    {
        /// <summary>
        /// The exception that is thrown when trying to write on an Input Point
        /// </summary>  
        public CantWriteInputException() : base("Can't write into an Input point") { }

        /// <summary>
        /// The exception that is thrown when trying to write on an Input Point
        /// </summary>
        public CantWriteInputException(string message) : base(message) { }
    }
 }
