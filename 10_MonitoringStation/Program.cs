using System;
using System.IO;
using System.Collections.Generic;

namespace _10_MonitoringStation
{
    class Program
    {
        public static char[] grid;
        public static int width;
        public static int height;
        public static SortedSet<(int, int)> direction;

        static void Main()
        {
            string programText;

            programText = File.ReadAllText("data.txt");
            //programText = ".#..#\n" + ".....\n" + "#####\n" + "....#\n" + "...##";

            PopulateGrid(programText);

            PrintGrid();

            IterateGrid();
        }

        private static void IterateGrid()
        {
            int[] values = new int[grid.Length];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (grid[Loc(x, y)] == '.')
                        values[Loc(x, y)] = 0;
                    else
                        values[Loc(x, y)] = ScanFrom(x, y);
                }
            }

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Console.Write($"{values[Loc(x, y)],2}");
                }
                Console.WriteLine();
            }
        }

        private static int ScanFrom(int x0, int y0)
        {
            Console.WriteLine($"\n\n*****from {x0},{y0}");

            direction = new SortedSet<(int, int)>();

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
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

            foreach (var (x, y) in direction)
            {
                int factor = 1;
                bool notFound = true;

                while (notFound && x * factor < width && y * factor < width)
                {
                    if (grid[Loc(x * factor, y * factor)] == '#')
                    {
                        notFound = false;
                        Console.WriteLine($"**hit at {x*factor},{y*factor}");
                    }
                }
            }

            return 1;
        }
        public static (int, int) HCF(int a, int b)
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
        private static void PopulateGrid(string programText)
        {
            width = programText.IndexOf('\n');
            height = programText.Length / width;

            Array.Resize(ref grid, width * height);

            int y = 0;
            int x;

            foreach (var row in programText.Split('\n'))
            {
                x = 0;
                foreach (var ch in row)
                {

                    grid[Loc(x, y)] = ch;
                    x++;
                }
                y++;
            }

        }

        public static void PrintGrid()
        {
            // Print out the grid
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Console.Write(grid[Loc(x, y)]);
                }
                Console.WriteLine();
            }
        }

        public static int Loc(int x, int y)
        {
            return x + y * width;
        }
    }
}
