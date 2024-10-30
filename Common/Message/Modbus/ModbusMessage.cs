using Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message.Modbus
{
    public class ModbusMessage : IMessage
    {
        private IMessageData messageData;
        private IMessageHeader messageHeader;

        public ModbusMessage() { }

        public ModbusMessage(IMessageData messageData, IMessageHeader messageHeader)
        {
            this.messageData = messageData;
            this.messageHeader = messageHeader;
        }

        [Order(1)]
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

        [Order(2)]
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
