﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.Message.MessageHistory { 
    /// <summary>
    /// The exception that is thrown when can't find the messagedata by id
    /// </summary>
    public class MessageDataNotFoundException : Exception
    {
        /// <summary>
        /// The exception that is thrown when can't find the messagedata by id
        /// </summary>
        public MessageDataNotFoundException() : base("Can't find the specified messageData") { }

        /// <summary>
        /// The exception that is thrown when can't find the messagedata by id
        /// </summary>
        public MessageDataNotFoundException(string message) : base(message) { }
    }
}
