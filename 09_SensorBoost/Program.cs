﻿using System;
using System.Linq;
using System.IO;

namespace _09_SensorBoost
{
    class Program
    {
        static void Main()
        {
            string programText;

            programText = File.ReadAllText("data.txt");
            //programText = "109,1,204,-1,1001,100,1,100,1008,100,16,101,1006,101,0,99";
            //programText = "1102,34915192,34915192,7,4,7,99,0";
            //programText = "104,1125899906842624,99";

            Console.WriteLine(Part1(programText));
            Console.WriteLine(Part2(programText));

        }
        //-------------------------------------------------------------------------------------------------------

        static long Part1(string programText)
        {
            IntCode amp = new IntCode(programText,false);

            var IO = new Queue(1);

            amp.Run(0, IO);

            return IO.queue[0];
        }

        //-------------------------------------------------------------------------------------------------------
        static long Part2(string programText)
        {
            IntCode amp = new IntCode(programText, false);

            var IO = new Queue(2);

            amp.Run(0, IO);

            return IO.queue[0];
        }
    }
}
