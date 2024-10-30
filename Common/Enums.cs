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

    public enum MessageType : short
    {
        TCPModbus,
    }
}
