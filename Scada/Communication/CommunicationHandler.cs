using Common;
using Common.ICommunication;
using Master.Communication;
using Master.TcpCommunication;
using System;
using System.Collections.Generic;
using System.Data;
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
        public ICommunicationStream communicationStream;

        public CommunicationHandler(ICommunicationOptions options)
        {
            if (options.CommunicationType == CommunicationType.TCP)
            {
                communicationStream = new TcpCommunicationStream(options as ITcpCommunicationOptions);

                //Dev purpose, but later with that event we can stop proccess ...      
                communicationStream.StateChanged += stateGotChangedSoDoSomething;
                communicationStream.Connect();
            }
        }

        //like this we can stop a thread from trying sending out messages...
        private void stateGotChangedSoDoSomething()
        {
            Console.WriteLine("Changed state to " + communicationStream.State);
        }
    }
}
