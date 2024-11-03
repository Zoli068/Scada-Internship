using Common;
using Common.Message;
using Common.Message.Exceptions;
using Common.Message.Modbus;
using Common.Serialization;
using Common.Utilities;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Slave.Communication
{
    public class TCPModbusMessageHandler : IMessageHandler
    {
        private Action<byte[]> sendBytes;

        public TCPModbusMessageHandler(Action<byte[]> sendBytes)
        {
            this.sendBytes = sendBytes;
        }

        public void ProcessBytes(byte[] data)
        {
            ModbusMessage modbusMessage = null;

            try
            {
                modbusMessage = Serialization.CreateMessageObject<ModbusMessage>(data);

                if (data.Length - 7 == (modbusMessage.MessageHeader as TCPModbusHeader).Length)
                {
                    //commandhandler.handle(messagPDU);
                }
                else
                {
                    //commandHandle.handle(ErrorInTheMessage)
                }
            }
            catch (Exception ex)
            {
                //errorhandling //NotSupportedException ex||
            }
        }

        public void SendMessage(IMessage message)
        {
            sendBytes(Serialization.ExtractMessageBytes<ModbusMessage>(message as ModbusMessage));
        }
    }
}
