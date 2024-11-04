using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Common.Exceptioons.CommunicationExceptions;
using Common.Exceptioons.SecureExceptions;
using Common.Message.Modbus;
using Common.Message;
using Slave.Communication;
using Common.Utilities;
using Common.Communication;
using Common.ICommunication;
using Common.Command;
using Slave.CommandHandler;
using Common.IPointsDataBase;
using Common.PointsDataBase;

namespace Slave
{
    public class Program
    {
        static void Main(string[] args)
        {

            ICommunicationOptions options = new TcpCommunicationOptions(IPAddress.Loopback, 8000, CommunicationType.TCP,8192);
            ICommunicationHandlerOptions communicationHandlerOptions = new CommunicationHandlerOptions(SecurityMode.SECURE,MessageType.TCPModbus);
            ICommunication communication;

            IPointsDataBase pointsDataBase = new PointsDataBase();

            IMessageHandler messageHandler;
            IMessageDataHandler messageDataHandler=null;

            try
            {
                communication = new Communication.Communication(options, communicationHandlerOptions);

                messageDataHandler = new ModbusMessageDataHandler(pointsDataBase);

                messageHandler = new TCPModbusMessageHandler(communication.SendBytes,messageDataHandler);
                communication.BytesRecived += messageHandler.ProcessBytes;

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


            //testing the commandHandler
            ((Communication.Communication)communication).RaiseBytesRecvied(new byte[] {0,1,0,0,0,5,5,2,39,17, 0,9});

            while (true)
            {
            }
        }
    }
}
