using System;
using System.Collections.Generic;
using System.Text;

namespace _11_SpacePolice
{
    class DataSpace
    {
        public List<long> queue = new List<long>();

 
        public DataSpace()
        {

        }
        public long Read()
        {
            if (queue.Count > 0)
            {
                var retval = queue[0];
                queue.Remove(queue[0]);
                return retval;
            }
            else
            {
                return 0;
            }
        }
        public void Write(long val)
        {
            queue.Add(val);
        }
        public long Result()
        {
            return queue[0];
        }
    }
}
