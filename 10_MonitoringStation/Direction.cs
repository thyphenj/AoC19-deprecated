using System;
using System.Linq;

namespace _10_MonitoringStation
{
    class Direction
    {
        public int DeltaX;
        public int DeltaY;
        public Double Distance;
        public ushort Quadrant;
        public float Angle;
        public bool Hittable;

        public Direction (int x, int y)
        {
            Hittable = true;

            DeltaX = x;
            DeltaY = y;

            Distance = Math.Sqrt((x * x) +  (y * y));
            if ( DeltaX <= 0 && DeltaY > 0)
            {
                Quadrant = (int)Quad.Q1;
                Angle = Math.Abs((float)DeltaX / (float)DeltaY);
            }
            if ( DeltaX < 0 && DeltaY <= 0)
            {
                Quadrant = (int)Quad.Q2;
                Angle = Math.Abs((float)DeltaY / (float)DeltaX);
            }
            if ( DeltaX >= 0 && DeltaY < 0)
            {
                Quadrant = (int)Quad.Q3;
                Angle = Math.Abs((float)DeltaX / (float)DeltaY);
            }
            if ( DeltaX > 0 && DeltaY >= 0)
            {
                Quadrant = (int)Quad.Q4;
                Angle = Math.Abs((float)DeltaY / (float)DeltaX);
            }
        }

        public bool Equal ( Direction other)
        {
            return (this.Quadrant == other.Quadrant && this.Angle == other.Angle);
        }
        public bool NotEqual(Direction other)
        {
            return (this.Quadrant != other.Quadrant || this.Angle != other.Angle);
        }
        public override string ToString()
        {
            return$"{(Quad)Quadrant} {Angle:0.0000}  ({DeltaX} {DeltaY})";
        }
    }
}
