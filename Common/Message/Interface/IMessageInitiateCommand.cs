using Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message
{
    public interface IMessageInitiateCommand<T,T2>
    {
        T2 InitiateMessage(T messageDTO);
    }
}
