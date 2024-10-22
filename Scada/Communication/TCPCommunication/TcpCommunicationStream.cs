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
    public class TcpCommunicationStream : ICommunicationStream, IReconnectStream, ISecureCommunication
    {
        private Stream stream;
        private TcpClient client;
        private bool disposedValue;
        private CommunicationState state;
        private ITcpCommunicationOptions options;

        public event Action StateChanged;

        public TcpCommunicationStream(ICommunicationOptions options) 
        { 
            this.options=options as ITcpCommunicationOptions;
            client = new TcpClient();
            state = CommunicationState.CLOSED;
        }

        public void ChangeState(CommunicationState newState)
        {
            if (state != newState)
            {
                state = newState;

                if (StateChanged != null)
                {
                    StateChanged();
                }
            }
        }

        public void Connect()
        {
            client.ConnectAsync(options.Address, options.PortNumber).ContinueWith(t => 
            {
                if (client.Connected)
                {
                    ChangeState(CommunicationState.CONNECTED);
                    stream = client.GetStream();
                }
                else
                {
                    ChangeState(CommunicationState.UNSUCCESSFULL_CONNECTION);
                }

            }).Wait(options.TimeOut);
        }

        public void Disconnect()
        {
            stream.Close();
            client.Close();

            ChangeState(CommunicationState.DISCONNECTED);
        }

        public void MakeSecure()
        {
            throw new NotImplementedException();
        }

        public async  Task<byte[]> Receive()
        {
            byte[] recvData = new byte[options.BufferSize];
            int readedBytes = 0;

            if (stream != null && state == CommunicationState.CONNECTED)
            {
                readedBytes=await stream.ReadAsync(recvData, 0, options.BufferSize);
            }

            //return the data, but not all the empty bytes, where didn't got written anything
            return null;
        }

        public void Reconnect()
        {
            throw new NotImplementedException();
        }

        public async Task Send(byte[] data)
        {
            await stream.WriteAsync(data,0,data.Count());
        }

        public CommunicationState State
        {
            get
            {
                return state;
            }
        }

        #region Dispose

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    stream.Close();
                    client.Close();
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
