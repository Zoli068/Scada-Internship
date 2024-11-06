using Common;
using Common.Command;
using Common.Message;
using Common.Message.Exceptions;
using Common.Message.Modbus;
using Common.Serialization;
using Common.Utilities;
using Master.CommandHandler;
using Master.Message;
using Master.Message.MessageHistory;
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
        private ushort transactionIdentificator = 0;
        private ModbusMessageDataHistory messageDataHistory;
        private IResponseMessageDataHandler responseMessageDataHandler;
        
        public TCPModbusMessageHandler(Action<byte[]> sendBytes, IResponseMessageDataHandler responseMessageDataHandler)
        {
            this.sendBytes = sendBytes;
            messageDataHistory = new ModbusMessageDataHistory();
            this.responseMessageDataHandler=responseMessageDataHandler;
        }

        public void ProcessBytes(byte[] data)
        {
            ModbusMessage modbusMessage=null;

            try
            {
                modbusMessage = Serialization.CreateMessageObject<ModbusMessage>(data);

                if(data.Length -7 == (modbusMessage.MessageHeader as TCPModbusHeader).Length)
                {                     
                    IMessageData response = messageDataHistory.GetMessageData(((TCPModbusHeader)modbusMessage.MessageHeader).TransactionID);
                    responseMessageDataHandler.ProcessMessageData(response, modbusMessage.MessageData);      
                }
            }
            catch (Exception) 
            {
                return;
            }
        }

        public void SendMessage(IMessageData messageData)
        {
            try
            {
                List<byte> bytes = new List<byte>();

                byte[] messageDataSerialized = messageData.Serialize();
                TCPModbusHeader header = new TCPModbusHeader(transactionIdentificator, 0, (ushort)messageDataSerialized.Length, 255);

                bytes.AddRange(header.Serialize());
                bytes.AddRange(messageDataSerialized);

                messageDataHistory.AddMessageData(messageData, transactionIdentificator);
                sendBytes(bytes.ToArray());
                transactionIdentificator++;
            }
            catch { }
        }
        
    }
}
