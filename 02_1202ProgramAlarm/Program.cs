using System;
using System.IO;

namespace _02_1202ProgramAlarm
{
    class Program
    {
        static void Main()
        {
            long[] theData =
                { 1,0,0,3,1,1,2,3,1,3,4,3,1,5,0,3,2,1,10,19,2,9,19,23,2,23,
                10,27,1,6,27,31,1,31,6,35,2,35,10,39,1,39,5,43,2,6,43,47,2,47,
                10,51,1,51,6,55,1,55,6,59,1,9,59,63,1,63,9,67,1,67,6,71,2,71,
                13,75,1,75,5,79,1,79,9,83,2,6,83,87,1,87,5,91,2,6,91,95,1,95,
                9,99,2,6,99,103,1,5,103,107,1,6,107,111,1,111,10,115,2,115,13,
                119,1,119,6,123,1,123,2,127,1,127,5,0,99,2,14,0,0};

            Part1(theData);

            Part2(theData);
        }

        static void Part1(long[] theData)
        {
            long[] thisData = new long[theData.Length];
            Array.Copy(theData, thisData, theData.Length);
            
            thisData[1] = 12;
            thisData[2] = 2;

            long answer = RunProgram(thisData);

            Console.WriteLine($"{answer}");
        }
        static void Part2 ( long[] theData)
        {
            long target = 19690720;

            for ( int i = 0; i < 100; i++)
            {
                for ( int j = 0; j < 100; j++)
                {
                    long[] thisData = new long[theData.Length];
                    Array.Copy(theData, thisData, theData.Length);

                    thisData[1] = i;
                    thisData[2] = j;
                    long ans = RunProgram(thisData);
                    if (ans == target)
                        Console.WriteLine($"{i},{j} => {ans} ... {100*i+j}");
                }
            }
        }

        static long RunProgram ( long[] theData)
        {
            int i = 0;
            while (i < theData.Length && theData[i] != 99)
            {
                switch (theData[i])
                {
                    case 1:
                        theData[theData[i + 3]] = theData[theData[i + 1]] + theData[theData[i + 2]];
                        i += 4;
                        break;
                    case 2:
                        theData[theData[i + 3]] = theData[theData[i + 1]] * theData[theData[i + 2]];
                        i += 4;
                        break;
                    default:
                        break;
                }
            }
            return theData[0];
        }
    }
}

