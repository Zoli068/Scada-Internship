using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Message
{
    public interface IMessageHandler
    {
        Queue<byte[]> ByteMessagesQueue { get;}

        AutoResetEvent MessageAvailableForSend { get; }

        void CreateMessageObject(byte[] data);

        void CreateByteArrayFromMessage(IMessage message);
    }
}
