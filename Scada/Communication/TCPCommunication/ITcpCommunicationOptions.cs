using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Master.TcpCommunication
{
    public interface ITcpCommunicationOptions: ICommunicationOptions
    {
        IPAddress Address { get; }

        int PortNumber { get; }

    }
}
