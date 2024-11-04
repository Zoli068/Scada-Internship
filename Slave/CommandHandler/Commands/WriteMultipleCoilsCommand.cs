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
    public class WriteMultipleCoilsCommand : IMessageDataCommand<IModbusData>
    {
        private IPointsDataBase pointsDataBase;

        public WriteMultipleCoilsCommand(IPointsDataBase pointsDataBase)
        {
            this.pointsDataBase = pointsDataBase;
        }

        public IModbusData Execute(IModbusData data)
        {
            ModbusWriteMultipleCoilsRequest request = data as ModbusWriteMultipleCoilsRequest;


            //to continue
            return null;
        }
    }
}
