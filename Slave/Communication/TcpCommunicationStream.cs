using Common;
using Slave.Communication.TCPCommunication;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Slave.Communication
{
    public class TcpCommunicationStream : ICommunicationStream
    {
        #region Atributes

        private TcpListener tcpListener;
        private TcpClient tcpClient;
        private Stream stream;
        private bool disposedValue;
        private ITcpCommunicationOptions options;
        private CommunicationState state;

        public event Action StateChanged;

        #endregion

        #region Constructors

        public TcpCommunicationStream(ITcpCommunicationOptions options)
        {
            this.options= options;
            IPEndPoint endPoint = new IPEndPoint(options.Address, options.PortNumber);
            tcpListener = new TcpListener(endPoint);
        }

        #endregion

        #region Methods

        //for each new kind of streamCommunication just need to use a constructor overloading
        public void Listening()
        {
            tcpListener.Start(1);

            tcpClient = tcpListener.AcceptTcpClient();

            stream = tcpClient.GetStream();
        }

        public void Disconnect()
        {
            stream.Close();
            tcpClient.Close();
        }


        public void SecureStream()
        {

        }

        public void ConnectionRestart()
        {

        }

        public async Task Send(byte[] data)
        {
            if (stream != null && state == CommunicationState.CONNECTED)
            {
                await stream.WriteAsync(data, 0, data.Count());
            }
        }

        public async Task<byte[]> Receive()
        {
            byte[] recvData=new byte[options.BufferSize];

            if(stream!=null && state == CommunicationState.CONNECTED)
            {
               await stream.ReadAsync(recvData, 0, options.BufferSize);
            }

            return null;
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

        #endregion

        #region Properties

        public CommunicationState State
        {
            get
            {
                return state;
            }
        }

        #endregion

        #region Dispose

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    stream.Close();
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
