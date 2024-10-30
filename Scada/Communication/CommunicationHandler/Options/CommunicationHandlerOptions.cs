using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.Communication
{
    /// <summary>
    /// Contains all the option values for a <see cref="Master.CommunicationHandler"/> class
    /// </summary>
    public class CommunicationHandlerOptions : ICommunicationHandlerOptions
    {
        private readonly int reconnectInterval;
        private readonly SecurityMode securityMode;
        private readonly MessageType messageType;

        public CommunicationHandlerOptions(int reconnectInterval,SecurityMode securityMode, MessageType messageType)
        {
            this.reconnectInterval = reconnectInterval;
            this.securityMode = securityMode;
            this.messageType = messageType;
        }

        public int ReconnectInterval{
            get
            {
                return reconnectInterval;
            }
        }

        public SecurityMode SecurityMode
        {
            get
            {
                return securityMode;
            }
        }

        public MessageType MessageType
        {
            get
            {
                return messageType;
            }
        }
    }
}
