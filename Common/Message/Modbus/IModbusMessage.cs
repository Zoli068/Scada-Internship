using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message.Modbus
{
    public interface IModbusRequestMessage : IMessage
    {
        //IMessageHeader -> IModbusHeader

        //IMessageData -> IModbusPDU
    }
}
