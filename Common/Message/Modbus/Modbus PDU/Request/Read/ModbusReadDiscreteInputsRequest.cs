using Common.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message
{
    public class ModbusReadDiscreteInputsRequest : IModbusReadDiscreteInputsRequest
    {
        private short startingAddress;
        private short quantityOfInputs;

        public ModbusReadDiscreteInputsRequest() { }

        public ModbusReadDiscreteInputsRequest(short startingAddress, short quantityOfInputs)
        {
            this.startingAddress = startingAddress;
            this.quantityOfInputs = quantityOfInputs;
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
        public short QuantityOfInputs
        {
            get
            {
                return quantityOfInputs;
            }
            set
            {
                quantityOfInputs = value;
            }
        }
    }
}
