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
    public class ReadCoilsCommand : IMessageDataCommand<IModbusData>
    {
        private IPointsDataBase pointsDataBase;

        public ReadCoilsCommand(IPointsDataBase pointsDataBase)
        {
            this.pointsDataBase = pointsDataBase;
        }

        public IModbusData Execute(IModbusData modbusData)
        {
            ModbusReadCoilsRequest data= modbusData as ModbusReadCoilsRequest;

            if (data.QuantityOfCoils < 1 || data.QuantityOfCoils > 2000)
            {
                throw new ValueOutOfIntervalException();
            }

            byte byteCount = (byte)(data.QuantityOfCoils / 8);

            if (data.QuantityOfCoils % 8 != 0)
            {
                byteCount += 1;
            }

            byte[] bytes = new byte[byteCount];
            ushort address;
            int byteIndex;
            int bitPosition;

            for (ushort i = 0; i < data.QuantityOfCoils; i++)
            {
                byteIndex = i / 8;
                bitPosition = i % 8;
                address = (ushort)(data.StartingAddress + i);

                if (pointsDataBase.ReadDiscreteValue(address,PointsType.COILS) != 0)
                {
                    bytes[byteIndex] |= (byte)(1 << bitPosition);
                }
            }

            return new ModbusReadCoilsResponse(byteCount, bytes);
        }
    }
}
