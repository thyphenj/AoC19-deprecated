using System;
using System.Linq;

namespace _10_MonitoringStation
{
    class Direction
    {
        public int X;
        public int Y;
        public ushort Quadrant;
        public float LaserOrder;
        public bool Hittable;

        public Direction (int x, int y)
        {
            X = x;
            Y = y;
            Hittable = true;

            if ( X <= 0 && Y > 0)
            {
                Quadrant = (int)Quad.Q1;
                LaserOrder = Math.Abs((float)X / (float)Y);
            }
            if ( X < 0 && Y <= 0)
            {
                Quadrant = (int)Quad.Q2;
                LaserOrder = Math.Abs((float)Y / (float)X);
            }
            if ( X >= 0 && Y < 0)
            {
                Quadrant = (int)Quad.Q3;
                LaserOrder = Math.Abs((float)X / (float)Y);
            }
            if ( X > 0 && Y >= 0)
            {
                Quadrant = (int)Quad.Q4;
                LaserOrder = Math.Abs((float)Y / (float)X);
            }
        }

        public bool Equal ( Direction other)
        {
            return (this.Quadrant == other.Quadrant && this.LaserOrder == other.LaserOrder);
        }
        public override string ToString()
        {
            return$"{(Quad)Quadrant} {LaserOrder:0.0000}  ({X} {Y})";
        }
    }
}
