using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public enum CommunicationType: short
    {
        TCP,
        SERIAL
    }

    public enum SecurityMode: short
    {
        SECURE,
        INSECURE
    }

    public enum CommunicationState: short
    {
        CLOSED,
        UNSUCCESSFULL_CONNECTION,
        CONNECTED,
        DISCONNECTED
    }
}
