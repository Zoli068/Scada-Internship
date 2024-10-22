using Common;
using Common.ICommunication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Slave.Communication
{
    public interface ITcpCommunicationOptions : ICommunicationOptions
    {
        IPAddress Address { get; }

        int PortNumber { get; }

        int BufferSize { get; }
    }
}
