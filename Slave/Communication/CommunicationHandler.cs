using Common;
using Common.ICommunication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slave.Communication
{
    /// <summary>
    /// The <see cref="CommunicationHandler"/> class handling the communication (<see cref="ICommunicationStream"/>), creates the specified object from 
    /// the recived bytes, also converting our specified object to a byte array to be able to send it
    /// </summary>
    public class CommunicationHandler
    {
        public TcpCommunicationStream communicationStream;

        public CommunicationHandler(ICommunicationOptions communication)
        {

            if (communication.CommunicationType == CommunicationType.TCP)
            {
                communicationStream = new TcpCommunicationStream(communication as ITcpCommunicationOptions);
            
            }
        }
    }
}
