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

        public void Deserialize(byte[] data, ref int startIndex)
        {
            functionCode = (FunctionCode)data[startIndex++];
            Type type=null;
    
            if(ModbusFunctionDictionary.TypeMap.TryGetValue(functionCode, out type))
            {
                this.data = Activator.CreateInstance(type) as IModbusData;
                this.data.Deserialize(data, ref startIndex);
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        public byte[] Serialize()
        {
            throw new NotImplementedException();
        }

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
