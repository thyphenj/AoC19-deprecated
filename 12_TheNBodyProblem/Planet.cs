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
        public Planet(string str)
        {
            int[] arr = new int[3];
            int i = 0;
            foreach (var a in str.Split(','))
                arr[i++] = Convert.ToInt32(a);

            Pos = new Position(arr[0], arr[1], arr[2]);
            Vel = new Velocity();
        }

        public override string ToString()
        {
            return $"{Pos.ToString()} : {Vel.ToString()}";
        }

        public int Pot ()
        {
            return Math.Abs(Pos.X) + Math.Abs(Pos.Y) + Math.Abs(Pos.Z);
        }

        public int Ken ()
        {
            return Math.Abs(Vel.X) + Math.Abs(Vel.Y) + Math.Abs(Vel.Z);
        }

        public int Energy ()
        {
            return this.Pot() * this.Ken();
        }
    }
}
