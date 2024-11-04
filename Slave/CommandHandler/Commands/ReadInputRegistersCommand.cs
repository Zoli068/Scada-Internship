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
    public class ReadInputRegistersCommand:IMessageDataCommand<IModbusData>
    {
        private IPointsDataBase pointsDataBase;

        public ReadInputRegistersCommand(IPointsDataBase pointsDataBase)
        {
            this.pointsDataBase = pointsDataBase;
        } 
            
        public IModbusData Execute(IModbusData data)
        {
            ModbusReadInputRegistersRequest request= data as ModbusReadInputRegistersRequest;

            if(request.QuantityOfInputRegisters < 1 || request.QuantityOfInputRegisters > 125)
            {
                throw new ValueOutOfIntervalException();
            }

            byte byteCount = (byte)(2 * request.QuantityOfInputRegisters);
            short[] bytes= new short[byteCount];
            ushort address;

            for(int i=0;i< request.QuantityOfInputRegisters; i++)
            {
                address=(ushort)(request.StartingAddress+i);
                bytes[i]=pointsDataBase.ReadRegisterValue(address,PointsType.DISCRETE_INPUTS);
            }

            return new ModbusReadInputRegistersResponse(byteCount, bytes);
        }
    }
}
