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
            int answer;

            programText = File.ReadAllText("data.txt");
            //programText = "3,15,3,16,1002,16,10,16,1,16,15,15,4,15,99,0,0";
            //programText = "3,23,3,24,1002,24,10,24,1002,23,-1,23,101,5,23,23,1,24,23,23,4,23,99,0,0";
            //programText = "3,31,3,32,1002,32,10,32,1001,31,-2,31,1007,31,0,33,1002,33,7,33,1,33,31,31,1,32,31,31,4,31,99,0,0,0";
            //programText = "3,26,1001,26,-4,26,3,27,1002,27,2,27,1,27,26,27,4,27,1001,28,-1,28,1005,28,6,99,0,0,5";

            answer = Part1(programText);

            Console.WriteLine(answer);
            //answer = Part2(programText);

        }
        //-------------------------------------------------------------------------------------------------------
        static int Part1(string programText)
        {
            int max = 0;

            var perms = new int[] { 0, 1, 2, 3, 4 }.GetPermutations();

            foreach (var perm in perms)
            {
                ProgramCode ampA = new ProgramCode("ampA", programText);
                ProgramCode ampB = new ProgramCode("ampB", programText);
                ProgramCode ampC = new ProgramCode("ampC", programText);
                ProgramCode ampD = new ProgramCode("ampD", programText);
                ProgramCode ampE = new ProgramCode("ampE", programText);

                string phaseString = string.Join(" ", perm);
                int[] phases = perm.ToArray();

                int transValue = 0;

                bool completed = false;
                completed = ampA.Run(transValue, out transValue, phases[0]);
                completed = ampB.Run(transValue, out transValue, phases[1]);
                completed = ampC.Run(transValue, out transValue, phases[2]);
                completed = ampD.Run(transValue, out transValue, phases[3]);
                completed = ampE.Run(transValue, out transValue, phases[4]);

                if (transValue > max)
                {
                    max = transValue;
                    Console.WriteLine($"{phaseString} - {transValue}");
                }
            }
            return max;
        }
        //-------------------------------------------------------------------------------------------------------
        static int Part2(string programText)
        {
            ProgramCode ampA = new ProgramCode("ampA", programText);
            ProgramCode ampB = new ProgramCode("ampB", programText);
            ProgramCode ampC = new ProgramCode("ampC", programText);
            ProgramCode ampD = new ProgramCode("ampD", programText);
            ProgramCode ampE = new ProgramCode("ampE", programText);

            int max = 0;

            var perms = new int[] { 5, 6, 7, 8, 9 }.GetPermutations();

            foreach (var perm in perms)
            {
                string phaseString = string.Join(" ", perm);
                int[] phases = perm.ToArray();

                int transValue = 0;

                bool completed = false;
                completed = ampA.Run(transValue, out transValue, phases[0]);
                completed = ampB.Run(transValue, out transValue, phases[1]);
                completed = ampC.Run(transValue, out transValue, phases[2]);
                completed = ampD.Run(transValue, out transValue, phases[3]);
                completed = ampE.Run(transValue, out transValue, phases[4]);

                while (!completed)
                {
                    if (!completed) completed = ampA.Run(transValue, out transValue);
                    if (!completed) completed = ampB.Run(transValue, out transValue);
                    if (!completed) completed = ampC.Run(transValue, out transValue);
                    if (!completed) completed = ampD.Run(transValue, out transValue);
                    if (!completed) completed = ampE.Run(transValue, out transValue);
                }

                if (transValue > max)
                {
                    max = transValue;
                    Console.WriteLine($"******************************************{phaseString} - {transValue}");
                }
                if (completed) break;
            }
            return max;
        }
    }
}


