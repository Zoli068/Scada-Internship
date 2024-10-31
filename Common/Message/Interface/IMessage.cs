using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message
{
    /// <summary>
    /// Describes the attributes of every message types
    /// </summary>
    public interface IMessage
    {
        /// <summary>
        /// The attribute which holds the header of the message
        /// </summary>
        IMessageHeader MessageHeader { get; set; }

        /// <summary>
        /// The attribute which holds the data of the message
        /// </summary>
        IMessageData MessageData { get; set; }
    }
}
