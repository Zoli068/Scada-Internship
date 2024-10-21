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

        #endregion

        #region Constructor

        public TcpCommunicationOptions(IPAddress address, int portNumber, CommunicationType communicationType, SecurityMode securityMode, int reconnectInterval)
        {
            this.address = address;
            this.portNumber = portNumber;
            this.communicationType = communicationType;
            this.securityMode = securityMode;
            this.reconnectInterval = reconnectInterval;
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

        #endregion
    }
}
