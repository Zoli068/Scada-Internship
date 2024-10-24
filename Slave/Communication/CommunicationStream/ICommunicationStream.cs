using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slave.Communication
{
    /// <summary>
    /// Describes all the required methods for a communication stream, also implements the <see cref="IStateHandler{T}"/>
    /// </summary>
    public interface ICommunicationStream : IDisposable { 
        /// <summary>
        /// Async Listening for connection
        /// </summary>
        /// <returns>Task object, which is representing the async listening</returns>
        Task Accept();

        /// <summary>
        /// Disconnects the accepted client
        /// </summary>
        void Disconnect();

        /// <summary>
        /// Async sending the bytes to the server
        /// </summary>
        /// <returns>Task object, which is representing the async byte sending</returns>
        Task Send(byte[] data);

        /// <summary>
        /// Async reciving the bytes from the server
        /// </summary>
        /// <returns>Task object, which is representing the async byte reciving</returns>
        Task<byte[]> Receive();

        /// <summary>
        /// For accessing the <see cref="Stream"/> object
        /// </summary>
        Stream Stream { get; set; }
    }
}
