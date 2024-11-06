using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message.Modbus
{
    public interface ITCPModbusHeader:IMessageHeader
    {
        ushort TransactionID { get; set; }

        ushort ProtocolID { get; set; }

        ushort Length { get; set; }

        byte UnitID { get; set; }
    }
}
