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

        public bool Equal(Velocity other)
        {
            return other.X == this.X && other.Y == this.Y && other.Z == this.Z;
        }


        public override string ToString()
        {
            return $"{X,4},{Y,4},{Z,4}";
        }

    }
}
