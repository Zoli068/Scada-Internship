using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// Abstract class with implemented <see cref="IStateHandler{T}"/>, need to be inherited if a class want to use the <see cref="IStateHandler{T}"/>
    /// </summary>
    public abstract class AbstractCommunicationStateHandler : IStateHandler<CommunicationState>
    {
        public CommunicationState state=CommunicationState.CLOSED;
        public event Action StateChanged;

        /// <summary>
        /// Method to change the managed state
        /// </summary>
        /// <param name="newState">The new value for the state</param>
        public void ChangeState(CommunicationState newState)
        {
            if (state != newState)
            {
                state = newState;

                if (StateChanged != null)
                {
                    StateChanged();
                }
            }
        }

        public CommunicationState State
        {
            get
            {
                return state;
            }
        }
    }
}
