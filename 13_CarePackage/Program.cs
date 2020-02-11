using System;
using System.IO;

namespace _13_CarePackage
{
    class Program
    {
        static void Main(string[] args)
        {
            //Part1();
            Part2();
        }
        static void Part1()
        {
            string programText;

            programText = File.ReadAllText(@"Text\intCode.txt");

            IntCodeComputer computer = new IntCodeComputer(programText);

            var dataSpace = new DataSpace();

            computer.Run(dataSpace);

            Console.WriteLine($"Count = {dataSpace.GetCount()}");

            Console.WriteLine(dataSpace.GetString());
        }
        static void Part2()
        {
            string programText;

            programText = File.ReadAllText(@"Text\intCode.txt");
            programText = "2" + programText.Substring(1);

            IntCodeComputer computer = new IntCodeComputer(programText);

            var dataSpace = new DataSpace();

            computer.Run(dataSpace);

            Console.WriteLine(dataSpace.Screen);
        }
    }
}
