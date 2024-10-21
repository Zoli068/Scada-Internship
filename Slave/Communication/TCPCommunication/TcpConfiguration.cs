using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Slave.Communication.TCPCommunication
{
    public class TCPCommunicationOptions : ITcpCommunicationOptions
    {
        #region Attributes

        private IPAddress address;
        private CommunicationType communicationType;
        private SecurityMode securityMode;
        private int portNumber;

        #endregion

        #region Constructor

        public TCPCommunicationOptions(IPAddress address, int portNumber, CommunicationType communicationType, SecurityMode securityMode)
        {
            this.address = address;
            this.portNumber = portNumber;
            this.communicationType = communicationType;
            this.securityMode = securityMode;
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

        #endregion
    }
}
