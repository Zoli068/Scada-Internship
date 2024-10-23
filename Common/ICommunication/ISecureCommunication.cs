using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ICommunication
{
    /// <summary>
    /// Contains the method which will check the security level of the communication and, if needed, make it secure
    /// </summary>
    public interface ISecureCommunication
    {
        /// <summary>
        /// Check the security level of the communication and, if needed, make it secure
        /// </summary>
        void MakeSecure();
    }
}
