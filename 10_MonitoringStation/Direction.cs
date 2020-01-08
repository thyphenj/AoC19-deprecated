using System;
using System.Collections.Generic;
using System.Text;

namespace _10_MonitoringStation
{
    class Direction
    {
        public int X;
        public int Y;
        public ushort Quadrant;
        public float LaserOrder;

        public Direction (int x, int y)
        {
            X = x;
            Y = y;

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
        public override string ToString()
        {
            return$"{(Quad)Quadrant} {LaserOrder:0.0000}  ({X} {Y})";
        }
    }
}
