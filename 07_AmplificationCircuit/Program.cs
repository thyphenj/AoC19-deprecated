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

            //answer = Part1(programText);


            answer = Part2(programText);

        }
        //-------------------------------------------------------------------------------------------------------
        static int Part1(string programText)
        {
            ProgramCode programCode = new ProgramCode(programText);

            int max = 0;

            var perms = new int[] { 0, 1, 2, 3, 4 }.GetPermutations();

            foreach (var phaseList in perms)
            {
                string phaseString = string.Join(" ", phaseList);

                int input = 0;
                int output = 0;

                foreach (var phase in phaseList)
                {
                    if (programCode.Run(input, out output, phase))
                        break;
                    else
                        input = output;
                }
                if (output > max)
                {
                    max = output;
                    Console.WriteLine($"{phaseString} - {output}");
                }
            }
            return max;
        }
        //-------------------------------------------------------------------------------------------------------
        static int Part2(string programText)
        {
            ProgramCode ampA = new ProgramCode(programText);
            ProgramCode ampB = new ProgramCode(programText);
            ProgramCode ampC = new ProgramCode(programText);
            ProgramCode ampD = new ProgramCode(programText);
            ProgramCode ampE = new ProgramCode(programText);

            int max = 0;

            var perms = new int[] { 5, 6, 7, 8, 9 }.GetPermutations();

            foreach (var phases in perms)
            {
                string phaseString = string.Join(" ", phases);
                int[] loc = phases.ToArray();

                int transValue = 0;

                bool completed = false;
                if (!completed) completed = ampA.Run(transValue, out transValue, loc[0]);
                if (!completed) completed = ampB.Run(transValue, out transValue, loc[1]);
                if (!completed) completed = ampC.Run(transValue, out transValue, loc[2]);
                if (!completed) completed = ampD.Run(transValue, out transValue, loc[3]);
                if (!completed) completed = ampE.Run(transValue, out transValue, loc[4]);

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
                    Console.WriteLine($"{phaseString} - {transValue}");
                }
                if (completed) break;
            }
            return max;
        }
    }
}


