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

namespace Slave
{
    public class Program
    {
        static void Main(string[] args)
        {

            TcpCommunicationOptions options = new TcpCommunicationOptions(IPAddress.Loopback, 8000, CommunicationType.TCP,8192);
            CommunicationHandlerOptions communicationHandlerOptions = new CommunicationHandlerOptions(SecurityMode.SECURE,MessageType.TCPModbus);

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

            //TESTS
            SerializationHandler serializationHandler = new SerializationHandler();

            ModbusMessage modbusMessage = new ModbusMessage();
            modbusMessage.MessageHeader = new TCPModbusHeader(2, 2, 9, 1);
            modbusMessage.MessageData = new ModbusPDU(FunctionCode.ReadInputRegisters, null);
            (modbusMessage.MessageData as ModbusPDU).Data = new ModbusReadInputRegistersResponse(3, new short[3] { 12, 34, 56 });

            List<byte> tosendbytes = new List<byte>();

            tosendbytes.AddRange(serializationHandler.SerializeToBytes(modbusMessage.MessageHeader));
            tosendbytes.AddRange(serializationHandler.SerializeToBytes(modbusMessage.MessageData));
            tosendbytes.AddRange(serializationHandler.SerializeToBytes((modbusMessage.MessageData as ModbusPDU).Data));

            //Header length not good wroking!,good value working,not supported functioncode working.
            communicationHandler.dataToSend.Add(tosendbytes.ToArray());
            while (true)
            {
            }
        }
    }
}
