using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.IPointsDataBase
{
    public interface IPointsDataBase
    {
        void WriteRegisterValue(ushort address,PointsType pointType ,short value);

        void WriteDiscreteValue(ushort address, PointsType pointType, byte value);

        short ReadRegisterValue(ushort address, PointsType pointType);
        
        byte ReadDiscreteValue(ushort address, PointsType pointType);
    }
}
