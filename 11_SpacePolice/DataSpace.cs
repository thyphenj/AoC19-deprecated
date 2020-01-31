using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;

namespace _11_SpacePolice
{
    class DataSpace
    {
        public Dictionary<Point, (long colour, int visits)> queue = new Dictionary<Point, (long, int)>();

        public int X;
        public int Y;
        public int Colour;

        private int Direction;

        private long? Paint = null;

        public DataSpace(int initColour)
        {
            X = 0;
            Y = 0;
            Colour = initColour;
            Direction = 0;

            queue.Add(new Point(X, Y), (Colour, 0));
        }

        public long Read()
        {
            (long colour, int visits) val;

            if (queue.TryGetValue(new Point(X, Y), out val))
            {
                return val.colour;
            }
            else
            {
                queue.Add(new Point(X, Y), (0, 0));
                return 0;
            }
        }

        // *** We need to BUILD this from separate writes
        public void Write(long val)
        {
            if (!Paint.HasValue)
                Paint = val;
            else
            {
                var zz = queue[new Point(X, Y)];
                zz.visits++;
                zz.colour = Paint.Value;
                Paint = null;
                queue[new Point(X, Y)] = zz;

                Direction = (Direction + (val == 0 ? 3 : 1)) % 4;

                switch (Direction)
                {
                    case 0:
                        Y++;
                        break;
                    case 1:
                        X++;
                        break;
                    case 2:
                        Y--;
                        break;
                    case 3:
                        X--;
                        break;
                    default:
                        break;
                }
            }
        }

        public void Part1()
        {
            Console.WriteLine($"There are {queue.Count} cells used!\n\n");
            Console.WriteLine($"--------------------------\n\n");
        }

        public void Part2()
        {
            int minX = 0;
            int maxX = 0;
            int minY = 0;
            int maxY = 0;

            foreach (var xx in queue)
            {
                if (xx.Key.X < minX) minX = xx.Key.X;
                if (xx.Key.X > maxX) maxX = xx.Key.X;
                if (xx.Key.Y < minY) minY = xx.Key.Y;
                if (xx.Key.Y > maxY) maxY = xx.Key.Y;
            }

            long[,] result = new long[1+maxX - minX, 1+maxY - minY];

            foreach (var xx in queue)
            {
                result[xx.Key.X - minX, xx.Key.Y - minY] = xx.Value.colour % 2;
            }

            for (int y = maxY - minY; y >= 0; y--)
            {
                for (int x = 0; x <= maxX - minX; x++)
                    Console.Write(result[x,y]== 1 ? 'x':' ');
                Console.WriteLine();
            }

        }
    }
}
