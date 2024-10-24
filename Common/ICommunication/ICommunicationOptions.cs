using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ICommunication
{
    /// <summary>
    /// Interface which contains the important values for a communication
    /// </summary>
    public interface ICommunicationOptions
    {
        /// <summary>
        /// Indicates the communication type which will be used.
        /// </summary>
        CommunicationType CommunicationType { get; }
    }
}
