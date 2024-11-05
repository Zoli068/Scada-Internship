using Common.Command;
using Common.IPointsDataBase;
using Common.Message;
using Common.PointsDataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slave.CommandHandler.Commands
{
    public class WriteSingleRegisterCommand:IMessageDataCommand<IModbusData>
    {

        private IPointsDataBase pointsDataBase;

        public WriteSingleRegisterCommand(IPointsDataBase pointsDataBase)
        {
            this.pointsDataBase = pointsDataBase;
        }

        public IModbusData Execute(IModbusData data)
        {
            ModbusWriteSingleRegisterRequest request=data as ModbusWriteSingleRegisterRequest;

            if (!(pointsDataBase.CheckAddress(request.RegisterAddress)))
            {
                throw new InvalidAddressException();
            }

            pointsDataBase.WriteRegisterValue(request.RegisterAddress, PointsType.HOLDING_REGISTERS, request.RegisterValue);

            return new ModbusWriteSingleRegisterResponse(request.RegisterAddress,request.RegisterValue);
        }
    }
}
