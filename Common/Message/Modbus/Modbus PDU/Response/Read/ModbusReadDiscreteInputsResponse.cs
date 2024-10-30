using Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message
{
    public class ModbusReadDiscreteInputsResponse : IModbusReadDiscreteInputsResponse
    {
        private byte byteCount;
        private byte[] inputStatus;

        public ModbusReadDiscreteInputsResponse() { }

        public ModbusReadDiscreteInputsResponse(byte byteCount, byte[] inputStatus)
        {
            this.byteCount = byteCount;
            this.inputStatus= inputStatus;
        }

        [Order(1)]
        public byte ByteCount
        {
            get
            {
                return byteCount;
            }
            set
            {
                byteCount = value;
            }
        }

        [Order(2)]
        public byte[] InputStatus
        {
            get
            {
                return inputStatus;
            }
            set
            {
                inputStatus = value;
            }
        }
    }
}
