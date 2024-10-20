using Common;
using Master.TcpCommunication;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scada.Communication
{
    public  interface ICommunicationStream:IDisposable
    {

        void SendBytes(byte[] bytesToSend);

        byte[] RecvBytes();

        //???Idk it have to be accessable or not
        Stream Stream { get; }

        ConnectionState State { get; }

        void ConnectionRestart();
    }
}
