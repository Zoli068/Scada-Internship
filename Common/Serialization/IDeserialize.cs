using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Serialization
{
    public interface IDeserialize
    {
        void Deserialize(byte[] data, ref int startIndex);
    }
}
