using System;
using System.Collections.Generic;
using System.Text;

namespace _00_Interfaces
{
    interface IDataSpace
    {
        long Read();
        void Write(long value);
    }
}
