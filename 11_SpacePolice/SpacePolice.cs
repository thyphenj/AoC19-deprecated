using System;
using System.IO;

namespace _11_SpacePolice
{
    class SpacePolice
    {
        static void Main()
        {
            string programText;
             
            programText = File.ReadAllText(@"Text\intCode.txt");

            Console.WriteLine(Part1(programText));
            
        }
        //-------------------------------------------------------------------------------------------------------

        static long Part1(string programText)
        {
            IntCodeComputer computer = new IntCodeComputer(programText);

            var dataSpace = new DataSpace();

            computer.Run(dataSpace);

            return dataSpace.Result();
        }
    }
}
