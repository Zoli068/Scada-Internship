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

namespace Master.Communication
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
            ModbusMessage modbusMessage=null;

            try
            {
                modbusMessage = Serialization.CreateMessageObject<ModbusMessage>(data);

                if(data.Length -7 == (modbusMessage.MessageData as TCPModbusHeader).Length)
                {
                    //commandhandler.handle(messagPDU);
                }
                else
                {
                    //commandHandle.handle(ErrorInTheMessage)
                }
            }
            catch (Exception) 
            {
                //errorhandling //NotSupportedException ex||
            }
        }

        private void SendMessage(IMessageData messageData)
        {
            try
            {
                //creating header
                //adding the messageData
              //  sendBytes(Serialization.ExtractMessageBytes<ModbusMessage>(message as ModbusMessage));
            }
            catch
            {
                ///todo
            }
        }
    }
}
