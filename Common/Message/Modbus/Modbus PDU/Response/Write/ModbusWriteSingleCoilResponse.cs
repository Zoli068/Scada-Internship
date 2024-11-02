using Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message
{
    public class ModbusWriteSingleCoilResponse : IModbusWriteSingleCoilResponse
    {
        private short outputAddress;
        private short outputValue;
        
        public ModbusWriteSingleCoilResponse() { }

        public ModbusWriteSingleCoilResponse(short outputAddress, short outputValue)
        {
            this.outputAddress = outputAddress;
            this.outputValue = outputValue;
        }

        public void Deserialize(byte[] data, ref int startIndex)
        {
            ByteValueConverter.GetValue(out outputAddress, data, ref startIndex);
            ByteValueConverter.GetValue(out outputValue, data, ref startIndex);
        }

        public byte[] Serialize()
        {
            throw new NotImplementedException();
        }

        public short OutputAddress
        {
            get
            {
                return outputAddress;
            }
            set
            {
                outputAddress = value;
            }
        }

        public short OutputValue
        {
            get
            {
                return outputValue;
            }
            set
            {
                outputValue= value;
            }
        }
    }
}
