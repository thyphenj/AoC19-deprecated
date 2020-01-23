using System;
using System.Collections.Generic;
using System.Linq;

namespace _10_MonitoringStation
{
    class Grid
    {
        public int Width;
        public int Height;
        public char[,] Map;
        public int[,] HitCount;

        public List<Direction>[,] ScannerDirections;

        public Grid(Data data)
        {
            Width = data.Width;
            Height = data.Height;

            Map = data.Retrieve();

            HitCount = new int[Width, Height];
            ScannerDirections = new List<Direction>[Width, Height];

            Print();
        }

        public void Scan()
        {
            for (int y = 0; y < Height; y++)
            {
                string display = "";

                for (int x = 0; x < Width; x++)
                {
                    bool foundOne = Map[x, y] == '#';

                    HitCount[x, y] = foundOne ? CountScannerHits(x, y) : 0;

                    display += (foundOne ? $"{HitCount[x, y],1}" : " ");
                }
                Console.WriteLine(display);
            }
        }

        public void Results(int part)
        {
            if (part == 1)
            {
                int max = 0;
                int maxX = 0; int maxY = 0;
                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        if (HitCount[x, y] > max)
                        {
                            max = HitCount[x, y];
                            maxX = x;
                            maxY = y;
                        }
                    }
                }
                Console.WriteLine($"\nmax = {max} at ({maxX},{maxY})");
            }

            if (part == 2)
            {

            }
        }

        // ---------------------------------------------------------------------
        // --- Private functions
        private int CountScannerHits(int x0, int y0)
        {
            ScannerDirections[x0, y0] = GetScannerDirections(x0, y0);

            return ScannerDirections[x0,y0].Count;
        }

        private List<Direction> GetScannerDirections(int x0, int y0)
        {
            List<Direction> retDirections = new List<Direction>();

            for (int y1 = 0; y1 < Height; y1++)
            {
                for (int x1 = 0; x1 < Width; x1++)
                {
                    int yDelta = y0 - y1;
                    int xDelta = x0 - x1;
                    if (Map[x1, y1] == '#' && !(xDelta == 0 && yDelta == 0))
                    {
                        retDirections.Add(new Direction(xDelta, yDelta));
                    }
                }
            }

            retDirections = retDirections.OrderBy(p => p.LaserOrder).OrderBy(o => o.Quadrant).ToList<Direction>();

            int i = retDirections.Count - 1;
            while (i > 0)
            {
                if (retDirections[i].Equal(retDirections[i - 1]))
                    retDirections.RemoveAt(i);
                i--;
            }

            return retDirections;
        }

        private (int, int) Simplify(int a, int b)
        {
            int x = a;
            int y = b;

            if (x == 0) { return (0, Math.Sign(y)); }
            if (y == 0) { return (Math.Sign(x), 0); }

            foreach (int n in new int[] { 2, 3, 5, 7, 11, 13, 17, 19, 21, 23, 29, 31 })
            {
                if (Math.Abs(x) >= n && Math.Abs(y) >= n)
                    while (x % n == 0 && y % n == 0)
                    {
                        x /= n;
                        y /= n;
                    }
            }
            return (x, y);
        }

        private void Print()
        {
            // Print out the map
            for (int y = 0; y < Height; y++)
            {
                string str = "";
                for (int x = 0; x < Width; x++)
                {
                    str += Map[x, y];
                }
                Console.WriteLine(str);
            }
        }
    }
}
