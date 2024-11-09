using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message
{
    public class ModbusWriteFileRecordRequest : IModbusWriteFileRecordRequest
    {
        private byte requestDataLength;
        private byte[] referenceType;
        private ushort[] fileNumber;
        private ushort[] recordNumber;
        private ushort[] recordLength;
        private ushort[][] recordData;

        public ModbusWriteFileRecordRequest() { }

        public ModbusWriteFileRecordRequest(byte requestDataLength, byte[] referenceType, ushort[] fileNumber, ushort[] recordNumber, ushort[] recordLength, ushort[][] recordData)
        {
            this.requestDataLength = requestDataLength;
            this.referenceType = referenceType;
            this.fileNumber = fileNumber;
            this.recordNumber = recordNumber;
            this.recordLength = recordLength;
            this.recordData = recordData;
        }

        public void Deserialize(byte[] data, ref int startIndex)
        {
            throw new NotImplementedException();
        }

        public byte[] Serialize()
        {
            throw new NotImplementedException();
        }


        public byte RequestDataLength
        {
            get
            {
                return requestDataLength;
            }
            set
            {
                requestDataLength= value;
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
                referenceType= value;
            }
        }

        public ushort[] FileNumber
        {
            get
            {
                return fileNumber;
            }
            set
            {
                fileNumber= value;
            }
        }

        public ushort[] RecordNumber
        {
            get
            {
                return recordNumber;
            }
            set
            {
                recordNumber= value;
            }
        }

        public ushort[] RecordLength
        {
            get
            {
                return recordLength;
            }
            set
            {
                recordLength= value;
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
                recordData= value;
            }
        }
    }
}
