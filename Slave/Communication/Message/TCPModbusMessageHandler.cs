using Common;
using Common.Command;
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
        private IMessageDataHandler messageDataHandler;
        private ModbusMessage modbusMessage;

        public TCPModbusMessageHandler(Action<byte[]> sendBytes, IMessageDataHandler messageDataHandler)
        {
            this.sendBytes = sendBytes;
            this.messageDataHandler = messageDataHandler;
        }

        public void ProcessBytes(byte[] data)
        {
            try
            {
                modbusMessage = Serialization.CreateMessageObject<ModbusMessage>(data);

                if (data.Length - 7 == (modbusMessage.MessageHeader as TCPModbusHeader).Length)
                {
                    SendMessage(messageDataHandler.ProcessMessageData(modbusMessage.MessageData));
                }
                else
                {
                    //comandHandler create Error MEssage! or no response, have to check the modbus spec
                }
            }
            catch (Exception)
            {
                //errorhandling //NotSupportedException ex||
            }
        }

        private void SendMessage(IMessageData messageData)
        {
            //creating header
            //adding the messageData
            //sendBytes(Serialization.ExtractMessageBytes<ModbusMessage>(message as ModbusMessage));
        }
    }
}
