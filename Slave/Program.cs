﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Exceptioons.CommunicationExceptions;
using Common.Exceptioons.SecureExceptions;
using Slave.Communication;

namespace Slave
{
    public class Program
    {
        static void Main(string[] args)
        {

            TcpCommunicationOptions options = new TcpCommunicationOptions(IPAddress.Loopback, 8000, CommunicationType.TCP,8192);
            CommunicationHandlerOptions communicationHandlerOptions = new CommunicationHandlerOptions(SecurityMode.SECURE);

            CommunicationHandler communicationHandler;
            try
            {
                 communicationHandler=new CommunicationHandler(communicationHandlerOptions,options);
            }
            catch (Exception ex) when (ex is  ListeningNotSuccessedException)
            {
                Console.WriteLine("The server was not able to start");
                Console.WriteLine(ex.Message);
                Console.ReadKey();
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unknown error happened");
                Console.WriteLine(ex.Message);
                Console.ReadKey();
                return;
            }

            while (true)
            {

            }
        }
    }
}
