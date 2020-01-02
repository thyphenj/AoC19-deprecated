using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

namespace _07_AmplificationCircuit
{
    class Program
    {
        static void Main()
        {
            string programText;
 
            programText = File.ReadAllText("data.txt");
            //programText = "3,15,3,16,1002,16,10,16,1,16,15,15,4,15,99,0,0";
            //programText = "3,23,3,24,1002,24,10,24,1002,23,-1,23,101,5,23,23,1,24,23,23,4,23,99,0,0";
            //programText = "3,31,3,32,1002,32,10,32,1001,31,-2,31,1007,31,0,33,1002,33,7,33,1,33,31,31,1,32,31,31,4,31,99,0,0,0";
            //programText = "3,26,1001,26,-4,26,3,27,1002,27,2,27,1,27,26,27,4,27,1001,28,-1,28,1005,28,6,99,0,0,5";

            //Console.WriteLine(Part1(programText));
            Console.WriteLine(Part2(programText));

        }
        //-------------------------------------------------------------------------------------------------------

        static int Part1(string programText)
        {
            int max = 0;

            var perms = new int[] { 0, 1, 2, 3, 4 }.GetPermutations();

            foreach (var perm in perms)
            {
                int[] phases = perm.ToArray();

                IntCode[] amps = new IntCode[5];

                for (int i = 0; i < 5; i++)
                    amps[i] = new IntCode(programText);

                var IO = new Queue(0);

                for (int i = 0; i < 5; i++)
                    amps[i].Run(phases[i], IO);

                //Console.WriteLine(IO.queue.Last());
                //Console.WriteLine();

                if (IO.queue.Last() > max)
                    max = IO.queue.Last();

            }
            return max;
        }

        //-------------------------------------------------------------------------------------------------------
        static int Part2(string programText)
        {
            int max = 0;

            var perms = new int[] { 5, 6, 7, 8, 9 }.GetPermutations();

            foreach (var perm in perms)
            {
                int[] phases = perm.ToArray();

                IntCode[] amps = new IntCode[5];

                for (int i = 0; i < 5; i++)
                    amps[i] = new IntCode(programText);

                var IO = new Queue(0);

                bool halted = false;
                while (!halted)
                    for (int i = 0; i < 5; i++)
                        halted |= amps[i].Run(phases[i], IO);

                Console.WriteLine(IO.queue.Last());
                Console.WriteLine();

                if (IO.queue.Last() > max)
                    max = IO.queue.Last();

            }
            return max;
        }
    }
}


