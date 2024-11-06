using Common.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Master.Message.MessageHistory
{
    public class ModbusMessageDataHistory : IMessageDataHistory
    {
        private Dictionary<ushort, IMessageData> messageDataHistory= new Dictionary<ushort, IMessageData>();

        public void AddMessageData(IMessageData messageData, ushort messageId)
        {
            if (!messageDataHistory.ContainsKey(messageId))
            {
                messageDataHistory[messageId] = messageData;
            }
            else
            {
                throw new MessageDataIdentificatorCollisionException();
            }
        }

        public IMessageData GetMessageData(ushort messageId)
        {
            IMessageData messageData;
            if (messageDataHistory.TryGetValue(messageId, out messageData))
            {
                messageDataHistory.Remove(messageId);
            }
            else
            {
                throw new MessageDataNotFoundException();
            }

            return messageData;
        }
    }
}
