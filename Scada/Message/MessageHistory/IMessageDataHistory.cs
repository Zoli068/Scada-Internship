using Common.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.Message
{
    public interface IMessageDataHistory
    {
        void AddMessageData(IMessageData messageData,ushort messageId);

        IMessageData GetMessageData(ushort  messageId);
    }
}
