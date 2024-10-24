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
    public class CommunicationHandlerOptions : ICommunicationHandlerOptions
    {
        private readonly SecurityMode securityMode;

        public CommunicationHandlerOptions(SecurityMode securityMode)
        {
            this.securityMode = securityMode;
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
