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
        private short protocolID;
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

        [Order(1)]
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

        [Order(2)]
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

        [Order(3)]
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

        [Order(4)]
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
