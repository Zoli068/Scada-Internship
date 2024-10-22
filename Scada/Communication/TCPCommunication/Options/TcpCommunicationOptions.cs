using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Master.TcpCommunication
{
    public class TcpCommunicationOptions : ITcpCommunicationOptions
    {
        #region Attributes

        private IPAddress address;
        private CommunicationType communicationType;
        private SecurityMode securityMode;
        private int portNumber;
        private int reconnectInterval;
        private int timeOut;
        private int bufferSize;

        #endregion

        #region Constructor

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

        #endregion

        #region Properties

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

        #endregion
    }
}
