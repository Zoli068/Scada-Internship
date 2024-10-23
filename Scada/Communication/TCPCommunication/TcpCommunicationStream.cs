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
using System.Net.Security;

namespace Master.TcpCommunication
{
    /// <summary>
    /// The <see cref="TcpCommunicationStream"/> responsible for the TCP Communication
    /// </summary>
    public class TcpCommunicationStream :AbstractCommunicationStateHandler, ICommunicationStream, IReconnectStream, ISecureCommunication
    {
        private Stream stream;
        private TcpClient client;
        private ITcpCommunicationOptions options;
        private SecureCommunication secureCommunication=null;

        /// <summary>
        /// Initializes a new instance of the <see cref="TcpCommunicationStream"/> class
        /// </summary>
        /// <param name="options"><see cref="TcpCommunicationOptions"/> which holds all the option values</param>
        public TcpCommunicationStream(ICommunicationOptions options)
        { 
            this.options=options as ITcpCommunicationOptions;
            client = new TcpClient();
        }

        /// <summary>
        /// Async Connect to the server
        /// </summary>
        /// <returns>Task object, which is representing the async Connect</returns>
        public async Task Connect()
        {
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

        /// <summary>
        /// Check the security level of the communication and, if needed, make it secure
        /// </summary>
        public void MakeSecure()
        {
            if (options.SecurityMode == SecurityMode.SECURE && !(stream is SslStream))
            {
                if(secureCommunication == null)
                {
                    secureCommunication=new SecureCommunication();
                }

                stream=secureCommunication.SecureStream(stream);
            }
        }

        /// <summary>
        /// Disconnecting from the server, and closing the stream
        /// </summary>
        public void Disconnect()
        {
            stream.Close();
            client.Close();
            ChangeState(CommunicationState.DISCONNECTED);
        }

        /// <summary>
        /// Async reciving the bytes from the server
        /// </summary>
        /// <returns>Task object, which is representing the async byte reciving</returns>
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
                Array.Copy(recvData,data,readedBytes);
            }

            return data;
        }

        public void Reconnect()
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

        #region Dispose
        private bool disposedValue;

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
