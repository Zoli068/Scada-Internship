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

        [Order(1)]
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

        [Order(2)]
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

        [Order(3)]
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

        [Order(4)]
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
