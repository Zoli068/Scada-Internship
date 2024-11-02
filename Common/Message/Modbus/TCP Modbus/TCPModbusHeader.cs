using Common.Message.Modbus;
using Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message
{
    public class TCPModbusHeader : ITCPModbusHeader 
    {
        private short transactionID;
        private short protocolID=0x0000;
        private short length;
        private byte unitID;

        public TCPModbusHeader() { }

        public TCPModbusHeader(short transactionID, short protocolID, short length, byte unitID)
        {
            this.transactionID = transactionID;
            this.protocolID = protocolID;
            this.length = length;
            this.unitID = unitID;
        }

        public void Deserialize(byte[] data,ref int startIndex)
        {
            ByteValueConverter.GetValue(out transactionID, data,ref startIndex);
            ByteValueConverter.GetValue(out protocolID, data,ref startIndex);
            ByteValueConverter.GetValue(out length, data,ref startIndex);
            ByteValueConverter.GetValue(out unitID, data,ref startIndex);
        }

        public byte[] Serialize()
        {
            List<byte> bytes = new List<byte>(7);

            bytes.AddRange(ByteValueConverter.ExtractBytes(transactionID));
            bytes.AddRange(ByteValueConverter.ExtractBytes(protocolID));
            bytes.AddRange(ByteValueConverter.ExtractBytes(length));
            bytes.AddRange(ByteValueConverter.ExtractBytes(unitID));

            return bytes.ToArray();
        }

        public short TransactionID
        {
            get
            {
                return transactionID;
            }
            set
            {
               transactionID = value;
            }
        }

        public short ProtocolID
        {
            get
            {
                return protocolID;
            }
            set
            {
                protocolID = value;
            }
        }

        public short Length
        {
            get
            {
                return length;
            }
            set
            {
                length= value;
            }
        }

        public byte UnitID
        {
            get
            {
                return unitID;
            }
            set
            {
                unitID= value;
            }
        }
    }
}
