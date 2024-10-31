using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slave.Communication
{
    /// <summary>
    /// Describes all the must have values for the <see cref="CommunicationHandler"/> class
    /// </summary>
    public interface ICommunicationHandlerOptions
    {
        /// <summary>
        /// Indicates the security of the communication
        /// </summary>
        SecurityMode SecurityMode { get; }

        /// <summary>
        /// Indicates the type of the message
        /// </summary>
        MessageType MessageType { get; }
    }
}
