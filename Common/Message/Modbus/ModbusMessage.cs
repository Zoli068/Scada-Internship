using Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message.Modbus
{
    /// <summary>
    /// The implementation of a Modbus Message
    /// </summary>
    public class TCPModbusMessage : IMessage
    {
        private IMessageData messageData;
        private IMessageHeader messageHeader;

        public TCPModbusMessage() 
        {
            messageHeader = new TCPModbusHeader();
            messageData = new ModbusPDU();
        }

        public TCPModbusMessage(IMessageData messageData, IMessageHeader messageHeader)
        {
            this.messageData = messageData;
            this.messageHeader = messageHeader;
        }

        public void Deserialize(byte[] data,ref int startIndex)
        {
               messageHeader.Deserialize(data,ref startIndex);
               messageData.Deserialize(data,ref startIndex);
        }

        public byte[] Serialize()
        {
            List<byte> bytes = new List<byte>();

            bytes.AddRange(messageHeader.Serialize());
            bytes.AddRange(messageData.Serialize());

            return bytes.ToArray();
        }

        public IMessageData MessageData
        {
            get
            {
                return messageData;
            }
            set
            {
                messageData = value;
            }
        }

        public IMessageHeader MessageHeader
        {
            get
            {
                return messageHeader;
            }
            set
            {
                messageHeader = value;
            }
        }


    }
}
