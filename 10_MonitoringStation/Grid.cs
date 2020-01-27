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

            PrintMap();
        }

        public void Scan()
        {
            for (int y = 0; y < Height; y++)
            {
                string display = "";

                for (int x = 0; x < Width; x++)
                {
                    if (Map[x, y] == '#' || Map[x, y] == 'X')
                    {
                        ScannerDirections[x, y] = GetScannerDirections(x, y);

                        HitCount[x, y] = CountScannerHits(x, y);

                        display += $"{HitCount[x, y],1}";
                    }
                    else
                    {
                        HitCount[x, y] = 0;

                        display += $".";
                    }

                }
                //Console.WriteLine(display);
            }
        }

        public void Results()
        {
            int max = 0;
            int maxX = 0; int maxY = 0;

            {
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
                Console.WriteLine($"\nPart 1 result : max = {max} at ({maxX},{maxY})\n\n");
            }

            {
                var laser = ScannerDirections[maxX, maxY];

                int i = 0;
                int numHits = 0;

                // *** Get first hit
                laser[i].Hittable = false;
                numHits++;
                //Console.WriteLine($"{i,3} ({maxX - laser[i].DeltaX},{maxY - laser[i].DeltaY})");

                while (numHits < 200)
                {
                    // *** Increment and skip identical direction
                    if (++i == laser.Count)
                    {
                        i = 0;
                    }
                    else
                    {
                        while (i > 0 && laser[i].Equal(laser[i - 1]))
                        {
                            if (++i == laser.Count)
                                i = 0;
                        }
                    }

                    // *** increment to next hittable
                    while (!laser[i].Hittable)
                    {
                        if (++i == laser.Count)
                            i = 0;
                    }
                    numHits++;
                    laser[i].Hittable = false;
                    //Console.WriteLine($"{i,3} ({maxX-laser[i].DeltaX},{maxY-laser[i].DeltaY})");
                }
                Console.WriteLine($"\nPart 2 result :  ({maxX-laser[i].DeltaX},{maxY-laser[i].DeltaY})");
            }
        }

        // ---------------------------------------------------------------------
        // --- Private functions
        private int CountScannerHits(int x, int y)
        {
            int count = 1;
            for (int i = 1; i < ScannerDirections[x, y].Count; i++)
            {
                if (ScannerDirections[x, y][i].NotEqual(ScannerDirections[x, y][i - 1]))
                    count++;
            }

            return count;
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
                    if ((Map[x1, y1] == '#' || Map[x1, y1] == 'X') && !(xDelta == 0 && yDelta == 0))
                    {
                        retDirections.Add(new Direction(xDelta, yDelta));
                    }
                }
            }

            retDirections = retDirections.OrderBy(q=>q.Distance).OrderBy(p => p.Angle).OrderBy(o => o.Quadrant).ToList<Direction>();

            return retDirections;
        }

        private (int, int) Simplify(int a, int b)
        {
            int x = a;
            int y = b;

            if (x == 0) { return (0, Math.Sign(y)); }
            if (y == 0) { return (Math.Sign(x), 0); }

            foreach (int n in new int[] { 2, 3, 5, 7, 11 })
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

        private void PrintMap()
        {
            // Print out the map
            Console.WriteLine($"Original Map\n");

            for (int y = 0; y < Height; y++)
            {
                string str = "";
                for (int x = 0; x < Width; x++)
                {
                    str += Map[x, y];
                }
                Console.WriteLine(str);
            }
            Console.WriteLine($"\n");
        }
    }
}
