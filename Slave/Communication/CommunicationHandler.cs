using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slave.Communication
{
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
