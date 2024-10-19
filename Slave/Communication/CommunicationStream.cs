﻿using Common;
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
    public class CommunicationStream
    {
        #region Atributes

        private Stream stream;
        private bool disposedValue;
        private ConnectionState state;

        #endregion

        #region Constructors

        public CommunicationStream()
        {
            CreateStream();
        }

        #endregion

        #region Methods

        //for each new kind of streamCommunication just need to use a constructor overloading
        private void CreateStream( )
        {
            Socket socket = new Socket(SocketType.Stream, ProtocolType.Tcp);

            //Config for server values

            IPEndPoint endPoint = new IPEndPoint(IPAddress.Loopback, 8000);

            socket.Bind(endPoint);
           
            socket.Blocking = true;
            socket.Listen(1);

            Socket master = socket.Accept();

            state = ConnectionState.CONNECTED;

            stream = new NetworkStream(master);
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

        }

        public byte[] RecvBytes(int numberOfBytes)
        {
            //first we need to read the header, and then check in the header, the
            //all length and like that we will know how much data we will need
            //or just do both of those stuff here

            //Maybe adding a plus param for the position of the length value

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
