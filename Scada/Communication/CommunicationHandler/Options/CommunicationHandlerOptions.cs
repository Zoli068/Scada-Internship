using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.Communication
{
    /// <summary>
    /// Describes all the must have values for the <see cref="Master.CommunicationHandler"/> class
    /// </summary>
    public class CommunicationHandlerOptions : ICommunicationHandlerOptions
    {
        private readonly int reconnectInterval;
        private readonly SecurityMode securityMode;

        public CommunicationHandlerOptions(int reconnectInterval,SecurityMode securityMode)
        {
            this.reconnectInterval = reconnectInterval;
            this.securityMode = securityMode;
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
    }
}
