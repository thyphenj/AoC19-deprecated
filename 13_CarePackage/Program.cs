using System;
using System.IO;

namespace _13_CarePackage
{
    class Program
    {
        static void Main(string[] args)
        {
            string programText;

            programText = File.ReadAllText(@"Text\intCode.txt");

            IntCodeComputer computer = new IntCodeComputer(programText);

            var dataSpace = new DataSpace();

            computer.Run(dataSpace);

            Console.WriteLine( $"Count = {dataSpace.GetCount()}");

            Console.WriteLine(dataSpace.GetString());
        }
    }
}
