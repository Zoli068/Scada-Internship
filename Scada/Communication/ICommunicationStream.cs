using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.Communication
{
    public interface ICommunicationStream:IDisposable,IStateHandler<CommunicationState>
    {
        void Connect();

        void Disconnect();
    
        void Send(byte[] data);

        void Receive(byte[] data);

    }
}
