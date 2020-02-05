using System;
using System.Collections.Generic;
using System.Text;

namespace _12_TheNBodyProblem
{
    class Planet
    {
        public Position Pos;
        public Velocity Vel;

        public Planet(int x, int y, int z)
        {
            Pos = new Position(x, y, z);
            Vel = new Velocity();
        }

        public override string ToString()
        {
            string xx = $"{Pos.ToString()},{Vel.ToString()}";

            int i = 0;
            int next = 0;
            string theStr = "";

            while (i < xx.Length)
            {
                char ch = xx[i];
                int a;
                if (ch >= '0' && ch <= '9')
                    a = int.Parse(ch.ToString());
                else if (ch == '-')
                    a = 10;
                else if (ch == ',')
                    a = 11;
                else
                    a = 12;

                next = next * 16 + a;
                i++;
                if ( i % 4 == 0)
                {
                    theStr += (char)next;

                    next = 0;
                }
            }
            return theStr;
        }

        public int Pot()
        {
            return Math.Abs(Pos.X) + Math.Abs(Pos.Y) + Math.Abs(Pos.Z);
        }

        public int Kin()
        {
            return Math.Abs(Vel.X) + Math.Abs(Vel.Y) + Math.Abs(Vel.Z);
        }

        public int Energy()
        {
            return this.Pot() * this.Kin();
        }
    }
}
