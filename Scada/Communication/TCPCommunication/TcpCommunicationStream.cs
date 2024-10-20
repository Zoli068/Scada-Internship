using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Scada.Communication;
using Common;


namespace Master.TcpCommunication
{
    public class TcpCommunicationStream: ICommunicationStream
    {
        #region Atributes

        private Stream stream;
        ITcpCommunicationOptions options;
        private bool disposedValue;
        private ConnectionState state;

        #endregion

        #region Constructors

        public TcpCommunicationStream(ITcpCommunicationOptions options)
        {
            this.options = options;
            CreateStream();

            if (options.SecurityMode == SecurityMode.SECURE)
            {
                //TODO
                SecureStream();
            }
        }

        #endregion

        #region Methods

        private void CreateStream()
        { 
            Socket socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(options.Address, options.PortNumber);
            state = ConnectionState.CONNECTED;
            stream = new NetworkStream(socket);
        }

        private void SecureStream()
        {

        }

        public void ConnectionRestart()
        {
            //stream.Close();
            //if(CommOption.Type==tcp) then options as ITcpConnection 
            //stream=createStream
        }

        public void SendBytes(byte[] bytesToSend)
        {
            int currentlySent = 0;

            //maybe a plus check for the state of the connection
            stream.Write(bytesToSend, currentlySent, bytesToSend.Length - currentlySent);
            stream.Flush();

        }

        public byte[] RecvBytes()
        {
            int numberOfBytes = (options as ITcpCommunicationOptions).LengthAttributePosition;
            //like that we will know where to find the length attribute


            int numberOfReceivedBytes = 0;
            byte[] retval = new byte[numberOfBytes];
            int numOfReceived;

            while (numberOfReceivedBytes < numberOfBytes)
            {
                numOfReceived = 0;

                //again check for the state

                numOfReceived = stream.Read(retval, numberOfReceivedBytes, (int)numberOfBytes - numberOfReceivedBytes);

                if (numOfReceived > 0)
                {
                    numberOfReceivedBytes += numOfReceived;
                }
            }
            return retval;
        }

        #endregion

        #region Properties

        public Stream Stream
        {
            get { return stream; }
        }



        public ConnectionState State
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
