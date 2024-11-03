using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Communication
{
    public interface ICommunication
    {
         event Action<byte[]> BytesRecived;

         void SendBytes(byte[] bytes);

         void Dispose();
    }
}
