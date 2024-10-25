﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.CommunicationExceptions
{
    /// <summary>
    /// The exception that is thrown when trying to establish a connection, but the connection already exists
    /// </summary>
    public class ConnectionAlreadyExisting:Exception
    {
        /// <summary>
        /// The exception that is thrown when trying to establish a connection, but the connection already exists
        /// </summary>  
        public ConnectionAlreadyExisting():base("Connection already got established") { }

        /// <summary>
        /// The exception that is thrown when trying to establish a connection, but the connection already exists
        /// </summary>
        public ConnectionAlreadyExisting(string message) : base(message) { }
    }
}
