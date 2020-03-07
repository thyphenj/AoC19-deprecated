using System;
using System.IO;

namespace _15_OxygenSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Part1();
        }
        static void Part1()
        {
            string programText;

            programText = File.ReadAllText(@"Text\intCode.txt");

            IntCodeComputer computer = new IntCodeComputer(programText);

            var dataSpace = new DataSpace();

            computer.Run(dataSpace);
        }
    }
}
