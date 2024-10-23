﻿using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Master.TcpCommunication
{
    /// <summary>
    /// Contains all the values for a TCP Communicaiton
    /// </summary>
    public class TcpCommunicationOptions : ITcpCommunicationOptions
    {
        private readonly int timeOut;
        private readonly int bufferSize;
        private readonly int portNumber;
        private readonly IPAddress address;
        private readonly int reconnectInterval;
        private readonly SecurityMode securityMode;
        private readonly CommunicationType communicationType;

        /// <summary>
        /// Initializes a new instance of the <see cref="TcpCommunicationOptions"/> class
        /// </summary>
        /// <param name="address">IP Address of the server</param>
        /// <param name="portNumber">Port number of the server</param>
        /// <param name="communicationType">Indicates the communication type which will be used</param>
        /// <param name="securityMode">Indicates the security of the communication</param>
        /// <param name="reconnectInterval">Time period between two connection attempt</param>
        /// <param name="timeOut">Time period after which the current command will be interrupted</param>
        /// <param name="bufferSize">Size of the connection buffer in bytes</param>
        public TcpCommunicationOptions(IPAddress address, int portNumber, CommunicationType communicationType, SecurityMode securityMode, int reconnectInterval, int timeOut, int bufferSize)
        {
            this.address = address;
            this.portNumber = portNumber;
            this.communicationType = communicationType;
            this.securityMode = securityMode;
            this.reconnectInterval = reconnectInterval;
            this.timeOut = timeOut;
            this.bufferSize = bufferSize;
            this.bufferSize = bufferSize;
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

        public int ReconnectInterval
        {
            get
            {
                return reconnectInterval;
            }
        }

        public int TimeOut
        {
            get
            {
                return timeOut;
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
