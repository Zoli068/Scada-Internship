using Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message
{
    public class ModbusReadFileRecordResponse:IModbusReadFileRecordResponse
    {
        private byte responseDataLength;
        private byte[] fileResponseLength;
        private byte[] referenceType;
        private ushort[][] recordData;

        public ModbusReadFileRecordResponse() { }

        public ModbusReadFileRecordResponse(byte responseDataLength, byte[] fileResponseLength, byte[] referenceType, ushort[][] recordData)
        {
            this.responseDataLength = responseDataLength;
            this.fileResponseLength = fileResponseLength;
            this.referenceType = referenceType;
            this.recordData = recordData;
        }

        public void Deserialize(byte[] data, ref int startIndex)
        {
            ByteValueConverter.GetValue(out responseDataLength, data, ref startIndex);

            List<byte> tempFileResponseLength= new List<byte>();
            List<byte> tempReferenceType= new List<byte>();
            List <List<ushort>> tempRecordData = new List<List<ushort>> ();
            List<ushort> shortHelper;

            byte byteTemp;
            ushort ushortTemp;

            byte bytesLeftToRead=responseDataLength;
            int i = 0;
            while (bytesLeftToRead > 0)
            {

                ByteValueConverter.GetValue(out byteTemp, data, ref startIndex);
                tempFileResponseLength.Add(byteTemp);

                bytesLeftToRead = (byte)(bytesLeftToRead - (byteTemp + 1));

                ByteValueConverter.GetValue(out byteTemp, data, ref startIndex);
                tempReferenceType.Add(byteTemp);


                shortHelper=new List<ushort>();

                for(int j=0;j < ((tempFileResponseLength.ElementAt(i)-1) / 2); j++)
                {
                    ByteValueConverter.GetValue(out ushortTemp, data, ref startIndex);
                    shortHelper.Add(ushortTemp);
                }
                tempRecordData.Add(shortHelper);
                i++;
            }

            fileResponseLength=tempFileResponseLength.ToArray();
            referenceType=tempReferenceType.ToArray();

            recordData = new ushort[tempRecordData.Count][];

            for(int j=0;j<tempRecordData.Count;j++)
            {
                recordData[j] = tempRecordData[j].ToArray();
            }

        }

        public byte[] Serialize()
        {
            List<byte> bytes= new List<byte>();
            bytes.Add(responseDataLength);

            int temp;

            for(int i=0;i<fileResponseLength.Length;i++)
            {
                bytes.Add(fileResponseLength[i]);
                temp = (fileResponseLength[i] - 1)/2;
                bytes.Add(referenceType[i]);
                
                for(int j = 0; j < temp; j++)
                {
                    bytes.AddRange(ByteValueConverter.ExtractBytes(recordData[i][j]));
                }

            }

            return bytes.ToArray();
        }

        public byte ResponseDataLength
        {
            get
            {
                return responseDataLength;
            }
            set
            {
                responseDataLength = value;
            }
        }

        public byte[] FileResponseLength
        {
            get
            {
                return fileResponseLength;
            }
            set
            {
                fileResponseLength = value;
            }
        }

        public byte[] ReferenceType
        {
            get
            {
                return referenceType;
            }
            set
            {
                referenceType = value;
            }
        }

        public ushort[][] RecordData
        {
            get
            {
                return recordData;
            }
            set
            {
                recordData = value;
            }
        }
    }
}
