﻿using System;
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
            //string programText = File.ReadAllText("data.txt");
            string programText = "3,15,3,16,1002,16,10,16,1,16,15,15,4,15,99,0,0";
            //string programText = "3,23,3,24,1002,24,10,24,1002,23,-1,23,101,5,23,23,1,24,23,23,4,23,99,0,0";
            //string programText = "3,31,3,32,1002,32,10,32,1001,31,-2,31,1007,31,0,33,1002,33,7,33,1,33,31,31,1,32,31,31,4,31,99,0,0,0";

            int ans;

            ProgramCode programCode = new ProgramCode(programText);

            //ans = Part1(programCode);

//            Console.WriteLine ($"***********************{ans}");




            programText = "3,26,1001,26,-4,26,3,27,1002,27,2,27,1,27,26,27,4,27,1001,28,-1,28,1005,28,6,99,0,0,5";
            
            programCode = new ProgramCode(programText);

            ans = Part2(programCode);

        }
        //-------------------------------------------------------------------------------------------------------
        static int Part1(ProgramCode programCode)
        {
            int max = 0;

            var perms = new int[] { 0, 1, 2, 3, 4 }.GetPermutations();

            foreach (var phases in perms)
            {
                string phaseString = string.Join(" ", phases);

                int input = 0;
                int output = 0;

                foreach (var phase in phases)
                {
                    if (programCode.Run(input, phase, out output))
                        break;
                    else
                        input = output;

                    if (output > max)
                    {
                        max = output;
                        Console.WriteLine($"{phaseString} - {output}");
                    }
                }
            }
            return max;
        }
        //-------------------------------------------------------------------------------------------------------
        static int Part2(ProgramCode programCode)
        {
            int max = 0;

            //int[] vals = { 0, 1, 2, 3, 4 };
            int[] vals = { 5, 6, 7, 8, 9 };
            var perms = vals.GetPermutations();


            foreach (var phases in perms)
            {

                string phaseString = string.Join(" ", phases);

                int input = 0;
                int output = 0;

                bool completed = false;
                while (!completed)
                {
                    foreach (var phase in phases)
                    {
                        if (programCode.Run(input, phase, out output))
                        {
                            completed = true;
                            break;
                        }
                        else
                            input = output;
                    }

                    if (output > max)
                    {
                        max = output;
                        Console.WriteLine($"{phaseString} - {output}");
                    }
                    if (completed) break;
                }
            }
            return max;
        }
    }
}


