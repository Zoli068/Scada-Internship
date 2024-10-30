using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message
{
    public interface IMessage
    {
        IMessageData MessageData { get; set; }

        IMessageHeader MessageHeader { get; set; }
    }
}
