﻿using Common;
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
            IMessageData messageDataToSend;

            try
            {
                modbusMessage = Serialization.CreateMessageObject<ModbusMessage>(data);

                if (data.Length - 7 == (modbusMessage.MessageHeader as TCPModbusHeader).Length)
                {
                    messageDataToSend=messageDataHandler.ProcessMessageData(modbusMessage.MessageData);
                    SendMessage(messageDataToSend);
                    modbusMessage = null;
                }
            }
            catch (Exception)
            {
                return;    
            }
        }

        private void SendMessage(IMessageData messageData)
        {
            List<byte> dataToSend = new List<byte>();

            byte[] messsageDataSerialized=messageData.Serialize();

            TCPModbusHeader header = new TCPModbusHeader();
            header.ProtocolID = 0;
            header.TransactionID = (modbusMessage.MessageHeader as TCPModbusHeader).TransactionID;
            header.UnitID = (modbusMessage.MessageHeader as TCPModbusHeader).UnitID;
            header.Length = (byte)messsageDataSerialized.Length;

            dataToSend.AddRange(header.Serialize());
            dataToSend.AddRange(messsageDataSerialized);

            sendBytes(dataToSend.ToArray());
        }
    }
}