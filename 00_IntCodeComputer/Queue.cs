using System;
using System.Collections.Generic;
using System.Text;

namespace IntCodeComputer
{
    class Queue
    {
        public List<long> queue = new List<long>();

        public Queue(int seed)
        {
            queue.Add(seed);
        }
        public Queue()
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
    }
}
