using System;
using System.Linq;

namespace _09_SensorBoost
{
    class Program
    {
        static void Main()
        {
            string programText;

            //programText = File.ReadAllText("data.txt");
            programText = "109,1,204,-1,1001,100,1,100,1008,100,16,101,1006,101,0,99";

            Part1(programText);


        }
        //-------------------------------------------------------------------------------------------------------

        static int Part1(string programText)
        {
            int max = 0;

            IntCode amp = new IntCode(programText,false);

            var IO = new Queue(0);

            amp.Run(0, IO);

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
