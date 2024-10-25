using Common;
using Common.ICommunication;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Master.Communication
{

    /// <summary>
    /// Interface which describes the important values for TCP communication
    /// </summary>
    public interface ITcpCommunicationOptions: ICommunicationOptions
    {
        /// <summary>
        /// IP Address of the server
        /// </summary>
        IPAddress Address { get; }

        /// <summary>
        /// Port number of the server
        /// </summary>
        int PortNumber { get; }

        /// <summary>
        /// Time period after which the current command will be interrupted
        /// </summary>
        int TimeOut { get; }

        /// <summary>
        /// Size of the connection buffer in bytes
        /// </summary>
        int BufferSize { get; }
    }
}
