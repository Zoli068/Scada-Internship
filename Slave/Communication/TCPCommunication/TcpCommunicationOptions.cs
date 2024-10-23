using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Slave.Communication.TCPCommunication
{
    /// <summary>
    /// Contains all the values for a TCP Communicaiton
    /// </summary>
    public class TcpCommunicationOptions : ITcpCommunicationOptions
    {
        private IPAddress address;
        private CommunicationType communicationType;
        private SecurityMode securityMode;
        private int portNumber;
        private int bufferSize;

        /// <summary>
        /// Initializes a new instance of the <see cref="TcpCommunicationOptions"/> class
        /// </summary>
        /// <param name="address">IP Address of the server</param>
        /// <param name="portNumber">Port number of the server</param>
        /// <param name="communicationType">Indicates the communication type which will be used</param>
        /// <param name="securityMode">Indicates the security of the communication</param>
        /// <param name="bufferSize">Size of the connection buffer in bytes</param>
        public TcpCommunicationOptions(IPAddress address, int portNumber, CommunicationType communicationType, SecurityMode securityMode,int bufferSize)
        {
            this.address = address;
            this.portNumber = portNumber;
            this.communicationType = communicationType;
            this.securityMode = securityMode;
            this.bufferSize= bufferSize;
        }

        public IPAddress Address
        {
            get
            {
                return address;
            }
        }

        public CommunicationType CommunicationType
        {
            get
            {
                return communicationType;
            }
        }

        public SecurityMode SecurityMode
        {
            get
            {
                return securityMode;
            }
        }

        public int PortNumber
        {
            get
            {
                return portNumber;
            }
        }

        public int BufferSize
        {
            get
            {
                return bufferSize;
            }
        }
    }
}
