using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Serialization
{
    public interface ISerialize
    {
        byte[] Serialize();
    }
}
