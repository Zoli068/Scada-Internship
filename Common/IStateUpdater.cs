using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ICommunication
{
    /// <summary>
    /// Contains a method that allows us to subscribe to an <see cref="IStateHandler{T}.StateChanged"/> event
    /// </summary>
    public interface IStateChangedHandler
    {
        /// <summary>
        /// The method that we need to subscribe to an <see cref="IStateHandler{T}.StateChanged"/> event
        /// </summary>
        void StateChangedHandler();
    }
}
