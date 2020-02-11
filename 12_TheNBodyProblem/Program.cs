using System;
using System.Collections.Generic;

namespace _12_TheNBodyProblem
{
    class Program
    {
        static void Main()
        {
            Part1(1);
            Part2(1);
        }

        static void Part1(int dataSet)
        {
            var data = new Data(dataSet);
            Planet[] planets = data.Planets;

            for (int i = 0; i < 1000; i++)
            {
                Step(planets);
                if (planets[0].Pos.X == 119) Console.WriteLine($"{i,4} ==> {CurrentState(planets)}");
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
            Planet[] planets = data.Planets;

            for (long iStep = 1; iStep <= 100000000000; iStep++)
            {
                if (iStep % 100000 == 0) Console.Write($"{iStep,10}\r");
                Step(planets);
                for(int i = 0; i < 4; i++)
                {
                    if (planets[i].AtStart)
                        Console.WriteLine($"{iStep,10} - {i} - {planets[i]}");
                }
            }
        }

        static void Step(Planet[] planets)
        {
            for (int A = 0; A < 3; A++)
                for (int B = A + 1; B < 4; B++)
                    planets[A].Gravity(planets[B]);

            for (int A = 0; A < 4; A++)
            {
                planets[A].Velocity();
            }
        }


        static string CurrentState(Planet[] planets)
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
