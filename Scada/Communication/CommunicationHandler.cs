using Common;
using Master.TcpCommunication;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Master
{
    public class CommunicationHandler
    {

        //CHANGE BACK TO PRIV, JUST FOR TESTING!!!!
        public CommunicationStream communicationStream;

        public CommunicationHandler(ICommunicationOptions communication) {

            if (communication.CommunicationType == CommunicationType.TCP)
            {
                communicationStream=new CommunicationStream(communication as ITcpCommunicationOptions);
            }
        }
    }
}
