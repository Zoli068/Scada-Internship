using Common.Command;
using Common.IPointsDataBase;
using Common.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Slave.CommandHandler.Commands
{
    public class ReadDiscreteInputsCommand:IMessageDataCommand<IModbusData>
    {
        private IPointsDataBase pointsDataBase;

        public ReadDiscreteInputsCommand(IPointsDataBase pointsDataBase)
        {
            this.pointsDataBase = pointsDataBase;
        }

        public IModbusData Execute(IModbusData modbusData)
        {
            ModbusReadDiscreteInputsRequest data = modbusData as ModbusReadDiscreteInputsRequest;

            if (data.QuantityOfInputs <1 || data.QuantityOfInputs > 2000)
            {
                throw new ValueOutOfIntervalException();
            }

            byte byteCount = (byte)(data.QuantityOfInputs / 8);

            if (data.QuantityOfInputs % 8 != 0)
            {
                byteCount += 1;
            }

            byte[] bytes = new byte[byteCount];
            ushort address;
            int byteIndex;
            int bitPosition;

            for (ushort i = 0; i < data.QuantityOfInputs; i++)
            {
                byteIndex = i / 8;
                bitPosition = i % 8;
                address = (ushort)(data.StartingAddress + i);

                if (pointsDataBase.ReadDiscreteValue(address, PointsType.DISCRETE_INPUTS) != 0)
                {
                    bytes[byteIndex] |= (byte)(1 << bitPosition);
                }
            }

            return new ModbusReadDiscreteInputsResponse(byteCount, bytes);
        }
    }
}
