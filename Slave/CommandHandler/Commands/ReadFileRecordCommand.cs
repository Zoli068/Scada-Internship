using Common.Command;
using Common.FileRecord;
using Common.Message;
using Common.PointsDataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slave.CommandHandler
{
    public class ReadFileRecordCommand : IMessageDataCommand<IModbusData>
    {
        private IFileRecord fileRecord;

        public ReadFileRecordCommand(IFileRecord fileRecord)
        {
            this.fileRecord = fileRecord;
        }

        public IModbusData Execute(IModbusData data)
        {
            ModbusReadFileRecordRequest request = data as ModbusReadFileRecordRequest;

            if(request.ByteCount > 0xf5 || request.ByteCount <0x07)
            {
                throw new ValueOutOfIntervalException();
            }

            ModbusReadFileRecordResponse response=new ModbusReadFileRecordResponse();

            int numOfGroups = request.ReferenceType.Length;
            response.FileResponseLength=new byte[numOfGroups];
            response.ReferenceType=new byte[numOfGroups];
            response.RecordData = new short[numOfGroups][];

            int datasize = 0;
            short[] temp;
            try
            {
                for (int i = 0; i < request.ReferenceType.Length; i++)
                {
                    if(!fileRecord.CheckValues(request.ReferenceType[i], request.FileNumber[i], request.RecordNumber[i], request.RecordLength[i]))
                    {
                        throw new Exception();
                    }
                }

                for (int i = 0; i < request.ReferenceType.Length; i++)
                {
                    temp = fileRecord.ReadFileRecord(request.FileNumber[i], request.RecordNumber[i], request.RecordLength[i]);
                    response.RecordData[i] = temp;
                    datasize += temp.Length * 2;    //recordData size
                }
            }
            catch (Exception)
            {
                throw new InvalidAddressException();
            }

            response.ResponseDataLength = (byte)(1 + response.FileResponseLength.Length + response.ReferenceType.Length + datasize);

            return response;
        }
    }
}
