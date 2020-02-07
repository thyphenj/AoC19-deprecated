using System;
using System.Collections.Generic;

namespace _12_TheNBodyProblem
{
    class Program
    {
        public static Planet[] planets;

        static void Main()
        {
            Part1(0);
            Part2(0);
        }

        static void Part1(int dataSet)
        {
            var data = new Data(dataSet);
            planets = data.Planets;

            for (int i = 0; i < 1000; i++)
            {
                Step();
                if (planets[0].Pos.X == 119) Console.WriteLine($"{i,4} ==> {CurrentState()}");
            }


            Console.WriteLine($"\n\n");

            int totalEnergy = 0;
            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine($"{planets[i].ToString()} ---> {planets[i].Energy()}");
                totalEnergy += planets[i].Energy();
            }
            Console.WriteLine($"\n\nTotal Energy {totalEnergy}");
        }

        static void Part2(int dataSet)
        {
            var data = new Data(dataSet);
            planets = data.Planets;
            int prev = 0;
            for (int i = 0; i < 1000000000; i++)
            {
                Step();
                if ( i % 100000 == 0 ) Console.Write($"{i}\r");
                if (planets[0].Pos.Equal(new Position(119,0,27) ))
                {
                    Console.WriteLine($"{i,4} ==> {i-prev}");
                    prev = i;
                }
            }
        }

        static void Step()
        {
            for (int i = 0; i < 3; i++)
                for (int j = i + 1; j < 4; j++)
                    planets[i].Gravity(planets[j]);

            for (int i = 0; i < 4; i++)
                planets[i].Velocity();
        }

        static string CurrentState()
        {
            string retval = "";

            for (int i = 0; i < 4; i++)
            {
                retval += planets[i].ToString();
            }
            return retval;
        }
    }
}
