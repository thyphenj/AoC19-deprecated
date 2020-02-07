using System;

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
            return xx;
        }

        public void Gravity(Planet that)
        {
            if (this.Pos.X < that.Pos.X)
            {
                this.Vel.X++;
                that.Vel.X--;
            }
            if (this.Pos.X > that.Pos.X)
            {
                this.Vel.X--;
                that.Vel.X++;
            }
            if (this.Pos.Y < that.Pos.Y)
            {
                this.Vel.Y++;
                that.Vel.Y--;
            }
            if (this.Pos.Y > that.Pos.Y)
            {
                this.Vel.Y--;
                that.Vel.Y++;
            }
            if (this.Pos.Z < that.Pos.Z)
            {
                this.Vel.Z++;
                that.Vel.Z--;
            }
            if (this.Pos.Z > that.Pos.Z)
            {
                this.Vel.Z--;
                that.Vel.Z++;
            }
        }

        public void Velocity()
        {
            this.Pos.X += this.Vel.X;
            this.Pos.Y += this.Vel.Y;
            this.Pos.Z += this.Vel.Z;
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
