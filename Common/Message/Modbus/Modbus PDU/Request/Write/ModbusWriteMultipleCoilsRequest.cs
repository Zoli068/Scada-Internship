using Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message
{
    public class ModbusWriteMultipleCoilsRequest : IModbusWriteMultipleCoilsRequest
    {
        private short startingAddress;
        private short quantityOfOutputs;
        private short byteCount;
        private short[] outputsValue;

        public ModbusWriteMultipleCoilsRequest() { }

        public ModbusWriteMultipleCoilsRequest(short startingAddress, short quantityOfOutputs, short byteCount, short[] outputsValue)
        {
            this.startingAddress = startingAddress;
            this.quantityOfOutputs = quantityOfOutputs;
            this.byteCount = byteCount;
            this.outputsValue = outputsValue;
        }

        public void Deserialize(byte[] data, ref int startIndex)
        {
            ByteValueConverter.GetValue(out startingAddress, data, ref startIndex);
            ByteValueConverter.GetValue(out quantityOfOutputs, data, ref startIndex);
            ByteValueConverter.GetValue(out byteCount, data, ref startIndex);

            outputsValue = new short[byteCount];

            for(int i=0;i<byteCount;i++)
            {
                ByteValueConverter.GetValue(out outputsValue[i], data, ref startIndex);
            }
        }

        public byte[] Serialize()
        {
            throw new NotImplementedException();
        }

        public short StartingAddress
        {
            get
            {
                return startingAddress; 
            }
            set
            {
                startingAddress = value;
            }
        }

        public short QuantityOfOutputs
        {
            get
            {
                return quantityOfOutputs;
            }
            set
            {
                quantityOfOutputs = value;
            }
        }

        public short ByteCount
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

        public short[] OutputsValue
        {
            get
            {
                return outputsValue;
            }
            set
            {
                outputsValue = value;
            }
        }

    }
}
