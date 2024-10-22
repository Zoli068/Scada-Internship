using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slave.Communication
{
    public interface ICommunicationStream:IDisposable,IStateHandler<CommunicationState>
    {
        Task Listening();

        void Disconnect();

        Task Send(byte[] data);

        Task<byte[]> Receive();
    }
}
