using System;
using System.Collections.Generic;
using System.Text;

namespace _09_SensorBoost
{
    class Queue
    {
        public List<int> queue = new List<int>();

        public Queue(int seed)
        {
            queue.Add(seed);
        }
        public Queue()
        {

        }
        public int Read()
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
        public void Write(int val)
        {
            queue.Add(val);
        }
    }
}
