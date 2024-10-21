using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public interface IStateHandler<T>
    {
        event Action StateChanged;

        void ChangeState(T newState);

        T State { get; }
    }
}
