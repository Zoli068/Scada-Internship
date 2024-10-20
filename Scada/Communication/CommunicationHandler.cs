using Common;
using Master.TcpCommunication;
using Scada.Communication;
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
        //CHANGE BACK TO PRIVATE, JUST FOR TESTING!!!!
        public ICommunicationStream communicationStream;

        public CommunicationHandler(ICommunicationOptions options)
        {

            if (options.CommunicationType == CommunicationType.TCP)
            {
                communicationStream = new TcpCommunicationStream(options as ITcpCommunicationOptions);
            }

            //MessageBytesConverter(The logic for converting a type of the messagepackets to network<->host order
            //Because we wont want to CommunicationStream care about the format of the messages
            //just to recv/send

            //the MessageBytesConverter will be the Network-Host order converter
            //Plus it will reCreate that kind of the message Object
        }
    }
}
