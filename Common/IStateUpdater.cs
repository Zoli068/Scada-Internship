using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.ICommunication
{
    public interface IStateUpdater<T>
    {
        //"delegate" for the function
        void UpdateState(T state);
    }
}
