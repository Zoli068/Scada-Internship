using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public abstract class AbstractCommunicationStateHandler : IStateHandler<CommunicationState>
    {
        public CommunicationState state;
        public event Action StateChanged;
        
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
