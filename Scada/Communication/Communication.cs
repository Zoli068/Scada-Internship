using Common;
using Common.Communication;
using Common.ICommunication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Master.Communication
{
    public class Communication : ICommunication
    {
        public event Action<byte[]> BytesRecived;
        private CommunicationHandler communicationHandler;

        public Communication(ICommunicationOptions options,ICommunicationHandlerOptions handlerOptions)
        {
           communicationHandler = new CommunicationHandler(handlerOptions, options, BytesRecived);
        }

        private void RaiseBytesRecvied(byte[] bytes)
        {
            if (BytesRecived != null)
            {
                BytesRecived.Invoke(bytes);
            }
        }

        public void SendBytes(byte[] bytes)
        {
            communicationHandler.dataToSend.Add(bytes);
        }

        public void Dispose()
        {
            communicationHandler.Dispose();
        }
    }
}
