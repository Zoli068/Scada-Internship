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
    public class TcpCommunicationStream :AbstractCommunicationStateHandler, ICommunicationStream
    {
        #region Atributes

        private Stream stream;
        private TcpClient tcpClient;
        private TcpListener tcpListener;
        private ITcpCommunicationOptions options;

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

        public async Task Listening()
        {
            tcpListener.Start(1);

            tcpClient = await tcpListener.AcceptTcpClientAsync();

            stream = tcpClient.GetStream();
            ChangeState(CommunicationState.CONNECTED);
        }

        public void Disconnect()
        {
            stream.Close();
            tcpClient.Close();
            ChangeState(CommunicationState.DISCONNECTED);
        }


        public void SecureStream()
        {
            //TODO
        }

        public void ConnectionRestart()
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

        public async Task<byte[]> Receive()
        {
            byte[] recvData = new byte[options.BufferSize];
            int readedBytes = 0;

            if (stream != null && state == CommunicationState.CONNECTED)
            {
                readedBytes = await stream.ReadAsync(recvData, 0, options.BufferSize);
            }

            byte[] data = new byte[readedBytes];

            if (readedBytes > 0)
            {
                //like that we will send the bytes + the info of num of recv bytes.
                Array.Copy(recvData, data, readedBytes);
            }

            return data;
        }

        #endregion

        #region Dispose
        private bool disposedValue;

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
