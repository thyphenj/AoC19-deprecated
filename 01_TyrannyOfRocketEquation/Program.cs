using System;
using System.IO;

namespace AoC_01_TyrannyOfRocketEquation
{
    class Program
    {
        static void Main()
        {
            //string[] theData = { "12", "14", "1969", "100756" };
            //string[] theData = { "100756" };
            string[] theData = File.ReadAllLines("data.txt");

            Part1(theData);

            Part2(theData);

        }
        static void Part1(string[] theData)
        {
            long sum = 0;

            foreach (var line in theData)
            {
                long num = Int64.Parse(line);
                long val = num / 3 - 2;
                sum += val;
            }

            Console.WriteLine(sum);
        }
        static void Part2 (string[] theData)
        {
            long sum = 0;

            foreach (var line in theData)
            {
                long val= Int64.Parse(line);

                val = val / 3 - 2;
                sum += val;

                val = val / 3 - 2;
                while ( val  > 0)
                {
                    sum += val;
                    val = val / 3 - 2;
                }
            }

            Console.WriteLine(sum);
        }
    }
}
