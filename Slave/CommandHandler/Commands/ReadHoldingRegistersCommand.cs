using Common.Command;
using Common.IPointsDataBase;
using Common.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slave.CommandHandler.Commands
{

    public class ReadHoldingRegistersCommand:IMessageDataCommand<IModbusData>
    {
        private IPointsDataBase pointsDataBase;
    
        public ReadHoldingRegistersCommand(IPointsDataBase pointsDataBase)
        {
            this.pointsDataBase = pointsDataBase;
        }
    
        
       public IModbusData Execute(IModbusData modbusData)
        {
            ModbusReadHoldingRegistersRequest request=modbusData as ModbusReadHoldingRegistersRequest;

            if(request.QuantityOfRegisters<1 || request.QuantityOfRegisters > 125)
            {
                throw new ValueOutOfIntervalException();
            }

            byte byteCount =(byte)( 2 * request.QuantityOfRegisters);
            short[] bytes =new short[byteCount];
            ushort address;

            for(int i=0;i<request.QuantityOfRegisters; i++)
            {
                address = (ushort)(request.StartingAddress + i);
                bytes[i] = pointsDataBase.ReadRegisterValue(address, PointsType.HOLDING_REGISTERS);
            }

            return new ModbusReadHoldingRegistersResponse(byteCount, bytes);
        }
    }
}
