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
            state = newState;

            if (StateChanged != null)
            {
                StateChanged();
            }
        }

        public void Connect()
        {
            //Little TODO here
            //if (!client.ConnectAsync("remotehost", remotePort).Wait(1000)) for timeout trying also like that we get back a bool for indication it was successfull or not
            //or EndConnect();
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

            });

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

        public void Receive(byte[] data)
        {
            throw new NotImplementedException();
        }

        public void Reconnect()
        {
            throw new NotImplementedException();
        }

        public void Send(byte[] data)
        {
            throw new NotImplementedException();
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
