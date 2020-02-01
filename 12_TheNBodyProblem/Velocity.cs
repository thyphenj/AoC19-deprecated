using System;
using System.Collections.Generic;
using System.Text;

namespace _12_TheNBodyProblem
{
    class Velocity
    {
        public int X;
        public int Y;
        public int Z;

        public Velocity ()
        {
            X = 0;
            Y = 0;
            Z = 0;
        }
        public override string ToString()
        {
            return $"({X,3},{Y,3},{Z,3})";
        }

    }
}
