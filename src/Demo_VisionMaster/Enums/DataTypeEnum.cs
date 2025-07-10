using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_VisionMaster.Enums
{
    public class DataTypeEnum
    {
        public enum ModbusDataType 
        {
            shortType = 0,
            ushortType = 4,
            intType = 8,
            uintType = 12,
            longType = 16,
            ulongType = 20,
            floatType = 24,
            doubleType = 28,
            CoiType = 32,
            DiscreteType = 32,
            stringType = 100,

        }
    }
}
