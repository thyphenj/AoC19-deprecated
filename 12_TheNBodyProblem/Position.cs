using System;
using System.Collections.Generic;
using System.Text;

namespace _12_TheNBodyProblem
{
    class Position
    {
        public int X;
        public int Y;
        public int Z;

        public Position (int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public override string ToString()
        {
            return $"({X,3},{Y,3},{Z,3})";
        }
    }
}
