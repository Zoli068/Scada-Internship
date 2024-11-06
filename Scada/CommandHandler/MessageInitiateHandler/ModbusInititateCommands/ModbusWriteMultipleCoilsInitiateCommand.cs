using Common.IPointsDataBase;
using Common.Message;
using Common.PointsDataBase;
using Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Master.CommandHandler.MessageInitiateHandler
{
    public class ModbusWriteMultipleCoilsInitiateCommand : IMessageInitiateCommand<ModbusMessageDTO, IModbusData>
    {
        public IModbusData InitiateMessage(ModbusMessageDTO messageDTO)
        {
            ModbusWriteMultipleCoilsRequest req=new ModbusWriteMultipleCoilsRequest();

            if (messageDTO.Address < 0 || messageDTO.Quantity < 0 || messageDTO.Quantity > 1968 || messageDTO.ByteArray.Length< messageDTO.Quantity)
            {
                throw new MessageDTOBadValuesExceptioncs();
            }

            req.StartingAddress = messageDTO.Address;
            req.QuantityOfOutputs = messageDTO.Quantity;

            byte byteCount = (byte)(req.QuantityOfOutputs / 8);
            
            if((req.QuantityOfOutputs % 8) > 0)
            {
                byteCount++;
            }

            req.ByteCount = byteCount;
            req.OutputsValue = new byte[byteCount];

            for (ushort i = 0; i < messageDTO.ByteArray.Length; i++)
            {
                int byteIndex = i / 8;
                int bitPosition = i % 8;

                req.OutputsValue[byteIndex] |= (byte)(1 << bitPosition);           
            }

            return req;
        }
    }
}
