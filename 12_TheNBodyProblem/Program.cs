using System;
using System.Collections.Generic;

namespace _12_TheNBodyProblem
{
    class Program
    {
        public static Planet[] planets;

        static void Main()
        {
            Part2();
        }

        //static void Part1()
        //{
        //    var data = new Data();
        //    planets = data.Planets;

        //    for (int i = 0; i < 1000; i++)
        //        Step();

        //    int totalEnergy = 0;
        //    for (int i = 0; i < 4; i++)
        //    {
        //        Console.WriteLine($"{planets[i].ToString()} ---> {planets[i].Energy()}");
        //        totalEnergy += planets[i].Energy();
        //    }
        //    Console.WriteLine($"\n\nTotal Energy {totalEnergy}");
        //}

        static void Part2()
        {
            Dictionary<string, int> results = new Dictionary<string, int>();

            var data = new Data(1);
            planets = data.Planets;

            int i = 0;
            while (results.TryAdd(CurrentState(), i++))
            {
                Step();
                if (i % 100000 == 0) 
                    Console.Write($"{i}\r");
            }
            Console.WriteLine($"\n{results[CurrentState()]}\n");
            Console.WriteLine($"\n{i,4} - {CurrentState().Replace(" ", "")}");
            Console.ReadLine();
        }

        static void Step()
        {
            for (int i = 0; i < 3; i++)
                for (int j = i + 1; j < 4; j++)
                    Gravity(planets[i], planets[j]); ;

            for (int i = 0; i < 4; i++)
                Velocity(planets[i]);
        }

        static void Gravity(Planet a, Planet b)
        {
            if (b.Pos.X > a.Pos.X)
            {
                a.Vel.X++;
                b.Vel.X--;
            }
            if (b.Pos.X < a.Pos.X)
            {
                a.Vel.X--;
                b.Vel.X++;
            }
            if (b.Pos.Y > a.Pos.Y)
            {
                a.Vel.Y++;
                b.Vel.Y--;
            }
            if (b.Pos.Y < a.Pos.Y)
            {
                a.Vel.Y--;
                b.Vel.Y++;
            }
            if (b.Pos.Z > a.Pos.Z)
            {
                a.Vel.Z++;
                b.Vel.Z--;
            }
            if (b.Pos.Z < a.Pos.Z)
            {
                a.Vel.Z--;
                b.Vel.Z++;
            }
        }

        static void Velocity(Planet a)
        {
            a.Pos.X += a.Vel.X;
            a.Pos.Y += a.Vel.Y;
            a.Pos.Z += a.Vel.Z;
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
