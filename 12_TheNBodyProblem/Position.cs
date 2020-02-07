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

        public bool Equal ( Position other)
        {
            return other.X == this.X && other.Y == this.Y && other.Z == this.Z;
        }

        public override string ToString()
        {
            return $"{X,4},{Y,4},{Z,4}";
        }
    }
}
