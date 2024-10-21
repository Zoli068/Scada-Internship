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
            CreateStream();
        }

        #endregion

        #region Methods

        //for each new kind of streamCommunication just need to use a constructor overloading
        private void CreateStream()
        {
            Socket socket = new Socket(SocketType.Stream, ProtocolType.Tcp);

            //Config for server values

            IPEndPoint endPoint = new IPEndPoint(options.Address, options.PortNumber);

            socket.Bind(endPoint);
           
            socket.Blocking = true;
            socket.Listen(1);

            Socket master = socket.Accept();


            stream = new NetworkStream(master);
        }

        public void SecureStream()
        {

        }

        public void ConnectionRestart()
        {

        }

        public void SendBytes(byte[] bytesToSend)
        {


        }

        public byte[] RecvBytes()
        {
            return null;
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

        public void Listening()
        {
            throw new NotImplementedException();
        }

        public void Disconnect()
        {
            throw new NotImplementedException();
        }

        public void Send(byte[] data)
        {
            throw new NotImplementedException();
        }

        public void Receive(byte[] data)
        {
            throw new NotImplementedException();
        }

        public void ChangeState(CommunicationState newState)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
