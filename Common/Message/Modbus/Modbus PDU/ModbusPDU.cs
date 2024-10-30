using Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message
{
    public class ModbusPDU : IModbusPDU
    {
        private FunctionCode functionCode;
        private IModbusData data;

        public ModbusPDU() { }

        public ModbusPDU(FunctionCode functionCode, IModbusData data)
        {
            this.functionCode = functionCode;
            this.data = data;
        }

        [Order(1)]
        public FunctionCode FunctionCode
        {
            get
            {
                return functionCode;
            }
            set
            {
                functionCode = value;
            }
        }

        [Order(2)]
        public IModbusData Data{
            get
            {
                return data;
            }
            set
            {
                data = value;
            }
        }
    }
}
