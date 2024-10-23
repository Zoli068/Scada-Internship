using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.Communication
{
    /// <summary>
    /// Describes all the required methods for a communication stream, also implements the <see cref="IStateHandler{T}"/>
    /// </summary>
    public interface ICommunicationStream:IDisposable,IStateHandler<CommunicationState>
    {
        /// <summary>
        /// Async Connect to the server
        /// </summary>
        /// <returns>Task object, which is representing the async Connect</returns>
        Task Connect();

        /// <summary>
        /// Disconnecting from the server, and closing the stream
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
    }
}
