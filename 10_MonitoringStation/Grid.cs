using System;
using System.Collections.Generic;

namespace _10_MonitoringStation
{
    class Grid
    {
        public int Width;
        public int Height;
        public char[] Map;
        public int[] Value;

        public HashSet<(int, int)>[] ScannerDirections;

        public Grid(Data data)
        {
            string programText = data.Retrieve();

            Width = programText.IndexOf('\n');
            Height = programText.Length / Width;

            Map = new char[Loc(Width, Height)];
            Value = new int[Loc(Width, Height)];
            ScannerDirections = new HashSet<(int, int)>[Loc(Width, Height)];

            int x;
            int y = 0;

            foreach (var row in programText.Split('\n'))
            {
                x = 0;
                foreach (var ch in row)
                {
                    Map[Loc(x, y)] = ch;
                    x++;
                }
                y++;
            }

            Print();
        }

        public void Scanner()
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (Map[Loc(x, y)] == '.')
                        Value[Loc(x, y)] = 0;
                    else
                        Value[Loc(x, y)] = CountScannerHits(x, y);
                }
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
                        if (Value[Loc(x, y)] != 0)
                        {
                            if (Value[Loc(x, y)] > max)
                            {
                                max = Value[Loc(x, y)];
                                maxX = x;
                                maxY = y;
                            }
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
            ScannerDirections[Loc(x0, y0)] = GetScannerDirections(x0, y0);

            int hits = 0;
            foreach (var (xDelta, yDelta) in ScannerDirections[Loc(x0, y0)])
            {
                int factor = 0;
                int x;
                int y;

                bool found = false;
                bool edge = false;

                while (!found && !edge)
                {
                    factor++;

                    x = x0 - xDelta * factor;
                    y = y0 - yDelta * factor;

                    if (x >= 0 && y >= 0 && x < Width && y < Height)
                    {
                        if (Map[Loc(x, y)] == '#')
                        {
                            found = true;
                            hits++;
                        }
                    }
                    else
                        edge = true;
                }
            }
            return hits;
        }

         private HashSet<(int, int)> GetScannerDirections(int x0, int y0)
        {
            HashSet<(int, int)> retDirections = new HashSet<(int, int)>();
            List<Direction> dirs = new List<Direction>();

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    int yDelta = y0 - y;
                    int xDelta = x0 - x;
                    if (xDelta != 0 || yDelta != 0)
                    {
                        (xDelta, yDelta) = Simplify(xDelta, yDelta);

                        retDirections.Add((xDelta, yDelta));

                        Direction dd = new Direction(xDelta, yDelta);
                        dirs.Add(dd);
                    }
                }
            }
            return retDirections;
        }
        private int Loc(int x, int y)
        {
            return x + y * Width;
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
                    str += Map[Loc(x, y)];
                }
                Console.WriteLine(str);
            }
        }
    }
}
