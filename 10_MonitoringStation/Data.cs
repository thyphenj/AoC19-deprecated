﻿using System.IO;

namespace _10_MonitoringStation
{
    class Data
    {
        private readonly string theString = "";

        public Data() => theString = returnData(0);

        public Data(int i) => theString = returnData(i);

        public int Width => theString.IndexOf('\n');
        public int Height => theString.Length / Width;

        public char[,] Retrieve()
        {
            char[,] theArray = new char[Width, Height];

            int y = 0;
            foreach (var row in theString.Split('\n'))
            {
                int x = 0;
                foreach (var ch in row)
                {
                    theArray[x, y] = ch;
                    x++;
                }
                y++;
            }

            return theArray;
        }

        private string returnData(int index)
        {
            return index switch
            {
                0 => File.ReadAllText("data.txt"),

                1 => ".#..#" + "\n" +                   // ---- should be 8 at (3,4)
                     "....." + "\n" +
                     "#####" + "\n" +
                     "....#" + "\n" +
                     "...##",

                2 => "......#.#." + "\n" +              // ---- should be 33 at (5,8) 
                     "#..#.#...." + "\n" +
                     "..#######." + "\n" +
                     ".#.#.###.." + "\n" +
                     ".#..#....." + "\n" +
                     "..#....#.#" + "\n" +
                     "#..#....#." + "\n" +
                     ".##.#..###" + "\n" +
                     "##...#..#." + "\n" +
                     ".#....####",

                3 => "#.#...#.#." + "\n" +              // --- should be 35 at (1,2)
                     ".###....#." + "\n" +
                     ".#....#..." + "\n" +
                     "##.#.#.#.#" + "\n" +
                     "....#.#.#." + "\n" +
                     ".##..###.#" + "\n" +
                     "..#...##.." + "\n" +
                     "..##....##" + "\n" +
                     "......#..." + "\n" +
                     ".####.###.",

                4 => ".#..##.###...#######" + "\n" +    // --- should be 210 at (11,13)
                     "##.############..##." + "\n" +
                     ".#.######.########.#" + "\n" +
                     ".###.#######.####.#." + "\n" +
                     "#####.##.#.##.###.##" + "\n" +
                     "..#####..#.#########" + "\n" +
                     "####################" + "\n" +
                     "#.####....###.#.#.##" + "\n" +
                     "##.#################" + "\n" +
                     "#####.##.###..####.." + "\n" +
                     "..######..##.#######" + "\n" +
                     "####.##.####...##..#" + "\n" +
                     ".#####..#.######.###" + "\n" +
                     "##...#.##########..." + "\n" +
                     "#.##########.#######" + "\n" +
                     ".####.#.###.###.#.##" + "\n" +
                     "....##.##.###..#####" + "\n" +
                     ".#.#.###########.###" + "\n" +
                     "#.#.#.#####.####.###" + "\n" +
                     "###.##.####.##.#..##",

                _ => "",                                //-- TOTALLY INVALID !!!!
            };
        }

    }
}