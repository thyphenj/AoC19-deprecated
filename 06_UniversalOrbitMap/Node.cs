using System;
using System.Collections.Generic;
using System.Text;

namespace _06_UniversalOrbitMap
{
    class Node
    {
        public Orig theData;
        public List<Orig> onwards = new List<Orig>();

        public Node ( Orig dat)
        {
            theData = dat;
        }
    }
}
