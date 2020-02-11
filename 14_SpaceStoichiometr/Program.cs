using System;
using System.IO;

namespace _14_SpaceStoichiometr
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
            programText = $"10 ORE => 10 A\n1 ORE => 1 B\n7 A, 1 B => 1 C\n7 A, 1 C => 1 D\n7 A, 1 D => 1 E\n7 A, 1 E => 1 FUEL";
            
            string[] lines = programText.Split('\n');

            foreach ( var x in lines)
                Console.WriteLine(x);
         }
   }
}
