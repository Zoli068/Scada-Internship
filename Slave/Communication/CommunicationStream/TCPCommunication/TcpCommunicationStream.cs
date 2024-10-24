using Common;
using Common.ICommunication;
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
    public class TcpCommunicationStream :ICommunicationStream
    {
        private Stream stream;
        private TcpClient tcpClient;
        private TcpListener tcpListener;
        private ITcpCommunicationOptions options;

        /// <summary>
        /// Initializes a new instance of the <see cref="TcpCommunicationStream"/> class
        /// </summary>
        /// <param name="options"><see cref="TcpCommunicationOptions"/> which holds all the option values</param
        public TcpCommunicationStream(ITcpCommunicationOptions options)
        {
            this.options= options;
            IPEndPoint endPoint = new IPEndPoint(options.Address, options.PortNumber);
            tcpListener = new TcpListener(endPoint);
        }

        /// <summary>
        /// Async Listening for connection
        /// </summary>
        /// <returns>Task object, which is representing the async listening</returns>
        public async Task Accept()
        {
            tcpListener.Start(2);
            if(tcpClient==null || tcpClient.Connected)
            {
                tcpClient = await tcpListener.AcceptTcpClientAsync();
                stream = tcpClient.GetStream();
            }

        }

        /// <summary>
        /// Disconnects the accepted client
        /// </summary>
        public void Disconnect()
        {
            stream.Close();
            tcpClient.Close();
        }

        /// <summary>
        /// Async sending the bytes to the server
        /// </summary>
        /// <returns>Task object, which is representing the async byte sending</returns>
        public async Task Send(byte[] data)
        {
            if (stream != null && tcpClient.Connected)
            {
                await stream.WriteAsync(data, 0, data.Count());
            }
        }

        /// <summary>
        /// Async reciving the bytes from the client
        /// </summary>
        /// <returns>Task object, which is representing the async byte reciving</returns>
        public async Task<byte[]> Receive()
        {
            byte[] recvData = new byte[options.BufferSize];
            int readedBytes = 0;

            if (stream != null && tcpClient.Connected)
            {
                readedBytes = await stream.ReadAsync(recvData, 0, options.BufferSize);
            }

            byte[] data = new byte[readedBytes];

            if (readedBytes > 0)
            {
                Array.Copy(recvData, data, readedBytes);
            }

            return data;
        }

        public Stream Stream
        {
            get
            {
                return stream;
            }

            set
            {
                stream= value;
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
