using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// Enumeration of the possible communication type
    /// </summary>
    public enum CommunicationType: short
    {
        TCP,
        SERIAL
    }

    /// <summary>
    /// Enumeration of the possible security modes
    /// </summary>
    public enum SecurityMode: short
    {
        SECURE,
        INSECURE
    }

    /// <summary>
    /// Enumeration of the possible communication state
    /// </summary>
    public enum CommunicationState: short
    {
        CLOSED,
        UNSUCCESSFULL_CONNECTION,
        CONNECTED,
        DISCONNECTED
    }

    /// <summary>
    /// Enumeration of the possible auto reconnect modes
    /// </summary>
    public enum AutoReconnect : short
    {
        ON,
        OFF
    }

    public enum FunctionCode : byte
    {
        ReadCoils = 0x01,
        ReadDiscreteInputs = 0x02,
        ReadHoldingRegisters = 0x03,
        ReadInputRegisters = 0x04,
        WriteSingleCoil = 0x05,
        WriteSingleRegister = 0x06,
        WriteMultipleCoils = 0x0F,
        WriteMultipleRegisters = 0x10,
    }
}
