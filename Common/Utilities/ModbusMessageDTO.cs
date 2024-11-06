using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utilities
{
    public class ModbusMessageDTO
    {
        public ModbusMessageDTO() { }

        public ushort Address { get; set; }

        public ushort Quantity { get; set; }

        public byte[] ByteArray {  get; set; }

        public short[] ShortArray { get; set; }

        public short UShortValue {  get; set; }

        public short ShortValue {  get; set; }

        public byte ByteValue {  get; set; }

    }
}
