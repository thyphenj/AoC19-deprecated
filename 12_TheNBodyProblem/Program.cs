using System;
using System.Collections.Generic;

namespace _12_TheNBodyProblem
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Advent Of Code - Day 12\n\n");

            Part1(0);
            Part2(0);
        }

        static void Part1(int dataSet)
        {
            Console.WriteLine($"Part One\n========\n");
            var data = new Data(dataSet);
            Planet[] planets = data.Planets;

            for (int i = 0; i < 1000; i++)
            {
                Step(planets);
            }

            int totalEnergy = 0;
            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine($"{planets[i].ToString()} ---> {planets[i].Energy()}");
                totalEnergy += planets[i].Energy();
            }
            Console.WriteLine($"\nTotal Energy {totalEnergy}\n\n----------------------\n\n");
        }

        static void Part2(int dataSet)
        {
            Console.WriteLine($"Part Two\n========\n");
            var data = new Data(dataSet);
            Planet[] planets = data.Planets;
            long? A = null, B = null, C = null;

            long iStep = 0;
            while (iStep <= 10000000 && (!A.HasValue || !B.HasValue || !C.HasValue))
            {
                if (++iStep % 100000 == 0) Console.Write($"{iStep,10}\r");
                Step(planets);

                if (!A.HasValue && planets[0].Vel.X == 0 && planets[1].Vel.X == 0 && planets[2].Vel.X == 0 && planets[3].Vel.X == 0)
                    A = iStep * 2;
                if (!B.HasValue && planets[0].Vel.Y == 0 && planets[1].Vel.Y == 0 && planets[2].Vel.Y == 0 && planets[3].Vel.Y == 0)
                    B = iStep * 2;
                if (!C.HasValue && planets[0].Vel.Z == 0 && planets[1].Vel.Z == 0 && planets[2].Vel.Z == 0 && planets[3].Vel.Z == 0)
                    C = iStep * 2;
            }
            Console.WriteLine($"X={A} Y={B} Z={C}");
            Console.WriteLine(LCM(A.Value,B.Value,C.Value));
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
        static long LCM(long A, long B, long C)
        {
            long retval = 1;
            long[] primes = { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47,
             53,     59,     61,     67,     71,
     73,     79,     83,     89,     97,    101,    103,    107,    109,    113,
    127,    131,    137,    139,    149,    151,    157,    163,    167,    173,
    179,    181,    191,    193,    197,    199,    211,    223,    227,    229,
    233,    239,    241,    251,    257,    263,    269,    271,    277,    281,
    283,    293,    307,    311,    313,    317,    331,    337,    347,    349,
    353,    359,    367,    373,    379,    383,    389,    397,    401,    409,
    419,    421,    431,    433,    439,    443,    449,    457,    461,    463,
    467,    479,    487,    491,    499,    503,    509,    521,    523,    541 };
            List<long> factors = new List<long>(); ;

            foreach (long p in primes)
            {
                while (A % p == 0 || B % p == 0 || C % p == 0)
                {
                    factors.Add(p);
                    if (A % p == 0) A /= p;
                    if (B % p == 0) B /= p;
                    if (C % p == 0) C /= p;
                }
            }
            foreach (var a in factors)
                retval *= a;

            return retval;
        }
    }
}
