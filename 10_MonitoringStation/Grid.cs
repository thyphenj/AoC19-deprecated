using System;
using System.Collections.Generic;
using System.Text;

namespace _10_MonitoringStation
{
    class Grid
    {
        public int Width;
        public int Height;
        public char[] theGrid;
        public int[] theValues;

        public Grid(string programText)
        {
            Width = programText.IndexOf('\n');
            Height = programText.Length / Width;

            theGrid = new char[Loc(Width, Height)];
            theValues = new int[Loc(Width, Height)];

            int y = 0;
            int x;

            foreach (var row in programText.Split('\n'))
            {
                x = 0;
                foreach (var ch in row)
                {

                    theGrid[Loc(x, y)] = ch;
                    x++;
                }
                y++;
            }
        }

        public void Print()
        {
            // Print out the grid
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Console.Write(theGrid[Loc(x, y)]);
                }
                Console.WriteLine();
            }
        }

        public void Iterate()
        {

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (theGrid[Loc(x, y)] == '.')
                        theValues[Loc(x, y)] = 0;
                    else
                        theValues[Loc(x, y)] = ScanFrom(x, y);
                }
            }
        }

        private int ScanFrom(int x0, int y0)
        {
            HashSet<(int, int)> direction = new HashSet<(int, int)>();

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    int yDelta = y0 - y;
                    int xDelta = x0 - x;
                    if (xDelta != 0 || yDelta != 0)
                    {
                        (xDelta, yDelta) = HCF(xDelta, yDelta);

                        direction.Add((xDelta, yDelta));
                    }
                }
            }

            int hits = 0;
            foreach (var (xDelta, yDelta) in direction)
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
                        if (theGrid[Loc(x, y)] == '#')
                        {
                            found = true;
                            hits++;
                        }
                    }                
                    else
                        edge = true;
                }
            }

            return hits; ;
        }
        public (int, int) HCF(int a, int b)
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

        private int Loc(int x, int y)
        {
            return x + y * Width;
        }

        public void Results()
        {
            int max = 0;
            int i=0; int j=0;
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (theValues[Loc(x, y)] == 0)
                        Console.Write("    ");
                    else
                    {
                        Console.Write($"{theValues[Loc(x, y)],4}");
                        if (theValues[Loc(x, y)] > max)
                        {
                            max = theValues[Loc(x, y)];
                            i = x;
                            j = y;
                        }
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine($"\n\nmax = {max} at ({i},{j})");
        }
    }
}
