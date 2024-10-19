using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Master.TcpCommunication
{
    public class TCPConfiguration : ITcpCommunicationOptions
    {
        #region Attributes

        private IPAddress address;
        private CommunicationType communicationType;
        private SecurityMode securityMode;
        private int lengthAttributePosition = 7;
        private int portNumber;

        #endregion

        #region Constructor

        public TCPConfiguration(IPAddress address, int portNumber, CommunicationType communicationType, SecurityMode securityMode)
        {
            this.address = address;
            this.portNumber= portNumber;
            this.communicationType = communicationType;
            this.securityMode = securityMode;

            //if(SecurityMode) bcs we will have our length attribute in a different place
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

        public int LengthAttributePosition
        {
            get
            {
                return lengthAttributePosition;
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
