using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message.Modbus
{
    public interface ITCPModbusHeader:IMessageHeader
    {
        short TransactionID { get; set; }

        short ProtocolID { get; set; }

        short Length { get; set; }

        byte UnitID { get; set; }
    }
}
