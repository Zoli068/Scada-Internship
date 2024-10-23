using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ICommunication
{
    /// <summary>
    /// Describes the <see cref="Reconnect"/> method, which will allow us to reconnect to the server
    /// </summary>
    public interface IReconnectStream
    {
        /// <summary>
        /// After the connection got interrupted, or we just disconnected, will allow us to reconnect
        /// </summary>
        void Reconnect();
    }
}
