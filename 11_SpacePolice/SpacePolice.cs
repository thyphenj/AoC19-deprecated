using System;
using System.IO;

namespace _11_SpacePolice
{
    class SpacePolice
    {
        static void Main()
        {
            Part1();

            Part2();
        }

        //-------------------------------------------------------------------------------------------------------

        static void Part1()
        {
            string programText;

            programText = File.ReadAllText(@"Text\intCode.txt");

            IntCodeComputer computer = new IntCodeComputer(programText);

            var dataSpace = new DataSpace(0);

            computer.Run(dataSpace);

            dataSpace.Part1();
        }
        //-------------------------------------------------------------------------------------------------------

        static void Part2()
        {
            string programText;

            programText = File.ReadAllText(@"Text\intCode.txt");

            IntCodeComputer computer = new IntCodeComputer(programText);

            var dataSpace = new DataSpace(1);

            computer.Run(dataSpace);

            dataSpace.Part2();
        }
    }
}
