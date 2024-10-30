using Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message
{
    public class ModbusWriteMultipleRegistersRequest : IModbusWriteMultipleRegistersRequest
    {
        private short startingAddress;
        private short quantityOfRegisters;
        private short byteCount;
        private short[] registerValue;

        public ModbusWriteMultipleRegistersRequest() { }

        public ModbusWriteMultipleRegistersRequest(short startingAddress, short quantityOfRegisters, short byteCount, short[] registerValue, short startingAddres)
        {
            this.startingAddress = startingAddress;
            this.quantityOfRegisters = quantityOfRegisters;
            this.byteCount = byteCount;
            this.registerValue = registerValue;

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
        public short QuantityOfRegisters
        {
            get
            {
                return quantityOfRegisters;
            }
            set
            {
                quantityOfRegisters = value;
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
        public short[] RegisterValue
        {
            get
            {
                return registerValue;
            }
            set
            {
                registerValue = value;
            }
        }
    }
}
