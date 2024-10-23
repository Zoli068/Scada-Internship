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
    /// <summary>
    /// The <see cref="CommunicationHandler"/> class handling the communication (<see cref="ICommunicationStream"/>), creates the specified object from 
    /// the recived bytes, also converting our specified object to a byte array to be able to send it
    /// </summary>
    public class CommunicationHandler
    {
        public ICommunicationStream communicationStream;

        public CommunicationHandler(ICommunicationOptions options)
        {
            if (options.CommunicationType == CommunicationType.TCP)
            {
                communicationStream = new TcpCommunicationStream(options as ITcpCommunicationOptions);

                communicationStream.StateChanged += stateGotChangedSoDoSomething;
                communicationStream.Connect();
            }
        }

        private void stateGotChangedSoDoSomething()
        {
            Console.WriteLine("Changed state to " + communicationStream.State);
        }
    }
}
