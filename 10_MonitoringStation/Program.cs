using System;
using System.IO;

namespace _10_MonitoringStation
{
    class Program
    {
        public static char[] grid;
        public static int width;
        public static int height;

        static void Main()
        {
            string programText;

            programText = File.ReadAllText("data.txt");

            populateGrid(programText);

            printGrid();

            iterateGrid();
        }

        private static void iterateGrid()
        {
            int[] values = new int[grid.Length];

            for (int pos = 0; pos < grid.Length; pos++)
            {
                if (grid[pos] == '.')
                    values[pos] = 0;
                else
                {
                    values[pos] = scanFrom(pos);
                }
            }


            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Console.Write($"{values[loc(x, y)],2}");
                }
                Console.WriteLine();
            }
        }

        private static int scanFrom(int pos)
        {
            return 1;
        }
        private static void populateGrid(string programText)
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

                    grid[loc(x, y)] = ch;
                    x++;
                }
                y++;
            }

        }

        public static void printGrid()
        {
            // Print out the grid
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Console.Write(grid[loc(x, y)]);
                }
                Console.WriteLine();
            }
        }

        public static int loc(int x, int y)
        {
            return x + y * width;
        }
    }
}
