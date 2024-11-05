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
    public class WriteSingleCoilCommand:IMessageDataCommand<IModbusData>
    {
        private IPointsDataBase pointsDataBase;

        public WriteSingleCoilCommand(IPointsDataBase pointsDataBase) 
        { 
            this.pointsDataBase= pointsDataBase;
        }

        public IModbusData Execute(IModbusData data)
        {
            ModbusWriteSingleCoilRequest request = data as ModbusWriteSingleCoilRequest;

            if((request.OutputValue != 0) || (request.OutputValue != 65280))
            {
                throw new ValueOutOfIntervalException();
            }

            if(!(pointsDataBase.CheckAddress(request.OutputAddress)))
            {
                throw new InvalidAddressException();
            }

           if(request.OutputValue == 0)
           {
                pointsDataBase.WriteDiscreteValue(request.OutputAddress, PointsType.COILS, 0);
           }
           else
           {
                pointsDataBase.WriteDiscreteValue(request.OutputAddress, PointsType.COILS, 1);
           }

            return new ModbusWriteSingleCoilResponse(request.OutputAddress, request.OutputValue);
        }
    }
}
