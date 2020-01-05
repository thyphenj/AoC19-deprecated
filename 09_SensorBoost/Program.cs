using System;
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

            Part1(programText);

            Console.WriteLine();

        }
        //-------------------------------------------------------------------------------------------------------

        static int Part1(string programText)
        {
            int max = 0;

            IntCode amp = new IntCode(programText,false);

            var IO = new Queue(1);

            amp.Run(0, IO);

            return max;
        }

        //-------------------------------------------------------------------------------------------------------
        static long Part2(string programText)
        {
            long max = 0;

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
