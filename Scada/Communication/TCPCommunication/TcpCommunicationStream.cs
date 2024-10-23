using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.ICommunication;
using Master.Communication;

namespace Master.TcpCommunication
{
    public class TcpCommunicationStream :AbstractCommunicationStateHandler, ICommunicationStream, IReconnectStream, ISecureCommunication
    {
        private Stream stream;
        private TcpClient client;
        private ITcpCommunicationOptions options;
        private SecureCommunication secureCommunication=null;

        public TcpCommunicationStream(ICommunicationOptions options)
        { 
            this.options=options as ITcpCommunicationOptions;
            state = CommunicationState.CLOSED;
        }

        public async Task Connect()
        {
            client = new TcpClient();

            await client.ConnectAsync(options.Address, options.PortNumber);

            if (client.Connected)
            {
                stream = client.GetStream();
                MakeSecure();
                ChangeState(CommunicationState.CONNECTED);
            }
            else
            {
                ChangeState(CommunicationState.UNSUCCESSFULL_CONNECTION);
            }
        }

        public void MakeSecure()
        {
            if (options.SecurityMode == SecurityMode.SECURE)
            {
                if(secureCommunication == null)
                {
                    secureCommunication=new SecureCommunication();
                }

                stream=secureCommunication.SecureStream(stream);
            }
        }

        public void Disconnect()
        {
            stream.Close();
            client.Close();
            ChangeState(CommunicationState.DISCONNECTED);
        }

        public async Task<byte[]> Receive()
        {
            byte[] recvData = new byte[options.BufferSize];
            int readedBytes = 0;

            if (stream != null && state == CommunicationState.CONNECTED)
            {
                readedBytes=await stream.ReadAsync(recvData, 0, options.BufferSize);
            }

            byte[] data= new byte[readedBytes];
            
            if(readedBytes > 0)
            {
                //like that we will send the bytes + the info of num of recv bytes.
                Array.Copy(recvData,data,readedBytes);
            }

            return data;
        }

        public void Reconnect()
        {
            //TODO
        }

        public async Task Send(byte[] data)
        {
            if (stream != null && state == CommunicationState.CONNECTED)
            {
                await stream.WriteAsync(data, 0, data.Count());
            }
        }

        #region Dispose
        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    stream.Close();
                    client.Close();
                    ChangeState(CommunicationState.CLOSED);
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }



        #endregion

    }
}
