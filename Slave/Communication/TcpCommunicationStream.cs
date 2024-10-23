using Common;
using Common.ICommunication;
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
    public class TcpCommunicationStream :AbstractCommunicationStateHandler, ICommunicationStream,ISecureCommunication
    {
        private Stream stream;
        private TcpClient tcpClient;
        private TcpListener tcpListener;
        private ITcpCommunicationOptions options;
        private SecureCommunication secureCommunication=null;

        /// <summary>
        /// Initializes a new instance of the <see cref="TcpCommunicationStream"/> class
        /// </summary>
        /// <param name="options"><see cref="TcpCommunicationOptions"/> which holds all the option values</param
        public TcpCommunicationStream(ITcpCommunicationOptions options)
        {
            this.options= options;
            IPEndPoint endPoint = new IPEndPoint(options.Address, options.PortNumber);
            tcpListener = new TcpListener(endPoint);
            tcpListener.Start(1);
        }

        /// <summary>
        /// Async Listening for connection
        /// </summary>
        /// <returns>Task object, which is representing the async listening</returns>
        public async Task Listening()
        {
            tcpClient = await tcpListener.AcceptTcpClientAsync();
            stream = tcpClient.GetStream();
            MakeSecure();
            ChangeState(CommunicationState.CONNECTED);
        }

        /// <summary>
        /// Check the security level of the communication and, if needed, make it secure
        /// </summary>
        public void MakeSecure()
        {
            if (options.SecurityMode == SecurityMode.SECURE)
            {
                if (secureCommunication == null)
                {
                    secureCommunication = new SecureCommunication();
                }

                stream=secureCommunication.SecureStream(stream);
            }
        }

        /// <summary>
        /// Disconnects the accepted client
        /// </summary>
        public void Disconnect()
        {
            stream.Close();
            tcpClient.Close();
            ChangeState(CommunicationState.DISCONNECTED);
        }

        public void ConnectionRestart()
        {
            //TODO
        }

        /// <summary>
        /// Async sending the bytes to the server
        /// </summary>
        /// <returns>Task object, which is representing the async byte sending</returns>
        public async Task Send(byte[] data)
        {
            if (stream != null && state == CommunicationState.CONNECTED)
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

            if (stream != null && state == CommunicationState.CONNECTED)
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
