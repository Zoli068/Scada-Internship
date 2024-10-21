using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Common.ICommunication
{
    public interface IStateUpdater<T>
    {
        void UpdateState(T state);
    }
}
