using Common;
using Common.Message;
using Common.Message.Exceptions;
using Common.Message.Modbus;
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
        private BlockingCollection<byte[]> dataToProcess;

        public void CreateByteArrayFromMessage(IMessage message)
        {
            throw new NotImplementedException();
        }

        public void CreateMessageObject(byte[] data)
        {
            throw new NotImplementedException();
        }
    }
}
