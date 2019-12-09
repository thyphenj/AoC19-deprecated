using System;
using System.IO;

namespace _05_SunnyWithAChanceOfAsteroids
{
    class Program
    {
        static bool glob = true;

        static void Main()
        {
            int[] theData =
                { 3,225,1,225,6,6,1100,1,238,225,104,0,1101,91,67,225,
                1102,67,36,225,1102,21,90,225,2,13,48,224,101,-819,224,224,4,224,
                1002,223,8,223,101,7,224,224,1,223,224,223,1101,62,9,225,1,139,22,224,
                101,-166,224,224,4,224,1002,223,8,223,101,3,224,224,1,223,224,223,
                102,41,195,224,101,-2870,224,224,4,224,1002,223,8,223,101,1,224,224,1,224,223,223,
                1101,46,60,224,101,-106,224,224,4,224,1002,223,8,223,1001,224,2,224,1,224,223,223,
                1001,191,32,224,101,-87,224,224,4,224,102,8,223,223,1001,224,1,224,1,223,224,223,
                1101,76,90,225,1101,15,58,225,1102,45,42,224,101,-1890,224,224,4,224,1002,223,8,223,
                1001,224,5,224,1,224,223,223,101,62,143,224,101,-77,224,224,4,224,1002,223,8,223,
                1001,224,4,224,1,224,223,223,1101,55,54,225,1102,70,58,225,1002,17,80,224,
                101,-5360,224,224,4,224,102,8,223,223,1001,224,3,224,1,223,224,223,4,223,99,0,0,0,677,0,0,0,0,0,0,0,0,0,0,0,
                1105,0,99999,1105,227,247,1105,1,99999,1005,227,99999,1005,0,256,1105,1,99999,
                1106,227,99999,1106,0,265,1105,1,99999,1006,0,99999,1006,227,274,1105,1,99999,
                1105,1,280,1105,1,99999,1,225,225,225,1101,294,0,0,105,1,0,1105,1,99999,
                1106,0,300,1105,1,99999,1,225,225,225,1101,314,0,0,106,0,0,1105,1,99999,
                1008,677,677,224,102,2,223,223,1005,224,329,1001,223,1,223,1108,677,226,224,
                1002,223,2,223,1006,224,344,101,1,223,223,107,677,226,224,1002,223,2,223,
                1006,224,359,101,1,223,223,108,677,677,224,1002,223,2,223,1006,224,374,
                1001,223,1,223,108,226,677,224,1002,223,2,223,1006,224,389,101,1,223,223,7,226,677,224,102,2,223,223,
                1006,224,404,1001,223,1,223,1108,677,677,224,1002,223,2,223,1005,224,419,101,1,223,223,
                1008,226,677,224,102,2,223,223,1006,224,434,101,1,223,223,107,226,226,224,102,2,223,223,
                1005,224,449,1001,223,1,223,1007,677,677,224,1002,223,2,223,1006,224,464,1001,223,1,223,
                1007,226,226,224,1002,223,2,223,1005,224,479,101,1,223,223,1008,226,226,224,102,2,223,223,
                1006,224,494,1001,223,1,223,8,226,226,224,102,2,223,223,1006,224,509,101,1,223,223,
                1107,677,677,224,102,2,223,223,1005,224,524,1001,223,1,223,1108,226,677,224,
                1002,223,2,223,1006,224,539,101,1,223,223,1107,677,226,224,1002,223,2,223,
                1006,224,554,101,1,223,223,1007,677,226,224,1002,223,2,223,1005,224,569,101,1,223,223,7,677,226,224,
                1002,223,2,223,1006,224,584,101,1,223,223,107,677,677,224,1002,223,2,223,1005,224,599,
                1001,223,1,223,8,226,677,224,1002,223,2,223,1005,224,614,101,1,223,223,7,677,677,224,
                1002,223,2,223,1006,224,629,1001,223,1,223,1107,226,677,224,1002,223,2,223,
                1006,224,644,101,1,223,223,108,226,226,224,102,2,223,223,1005,224,659,1001,223,1,223,8,677,226,224,
                1002,223,2,223,1005,224,674,101,1,223,223,4,223,99,226};

            //theData = new int[] { 3, 9, 8, 9, 10, 9, 4, 9, 99, -1, 8 };

            Part1(theData);

        }

        static void Part1(int[] theData)
        {
            int[] thisData = new int[theData.Length];
            Array.Copy(theData, thisData, theData.Length);

            long answer = RunProgram(thisData);
        }

        static long RunProgram(int[] theData)
        {
            bool completed = false;

            int sp = 0;

            while (!completed)
            {
                int op = theData[sp] % 100;
                bool mod1 = 1 == (theData[sp] / 100) % 10;
                bool mod2 = 1 == (theData[sp] / 1000) % 10;
                bool mod3 = 1 == (theData[sp] / 10000) % 10;
                int x, y, z;

                switch (op)
                {
                    case 1:     // add x,y => z

                        x = mod1 ? sp + 1 : theData[sp + 1];
                        y = mod2 ? sp + 2 : theData[sp + 2];
                        z = theData[sp + 3];

                        if (glob) Console.WriteLine($"{theData[sp],5} - {op,2} (add) ({x,4},{y,4}) => ({z,4}) [{theData[x]},{theData[y]}=>{theData[z]}]");

                        theData[z] = theData[x] + theData[y];

                        sp += 4;
                        break;

                    case 2:     // mul x,y => z

                        x = mod1 ? sp + 1 : theData[sp + 1];
                        y = mod2 ? sp + 2 : theData[sp + 2];
                        z = theData[sp + 3];

        
                        if (glob) Console.WriteLine($"{theData[sp],5} - {op,2} (mul) ({x,4},{y,4}) => ({z,4}) [{theData[x]},{theData[y]}=>{theData[z]}]");

                        theData[z] = theData[x] * theData[y];

                        sp += 4;
                        break;

                    case 3:     // rea => x

                        x = mod1 ? sp + 1 : theData[sp + 1];

                        Console.Write("===> ");
                        theData[x] = Int32.Parse(Console.ReadLine());

                        sp += 2;
                        break;

                    case 4:     // wri x

                        x = mod1 ? sp + 1 : theData[sp + 1];

                        if (glob) Console.WriteLine($"{theData[sp],5} - {op,2} (wri) ({x,4}     ) => (    ) [{theData[x]}]");


                        Console.WriteLine($"===< {theData[x]}");

                        sp += 2;
                        break;

                    case 5:     // jnz x => y
                        x = mod1 ? sp + 1 : theData[sp + 1];
                        y = mod2 ? sp + 2 : theData[sp + 2];

                        if (glob) Console.WriteLine($"{theData[sp],5} - {op,2} (jnz) ({x,4},-sp-) => ({y,4}) [{theData[x]},-sp-=>{theData[y]}]");

                        if (theData[x] != 0)
                            sp = theData[y];
                        else
                            sp += 3;

                       break;

                    case 6:     //jez x => y
                        x = mod1 ? sp + 1 : theData[sp + 1];
                        y = mod2 ? sp + 2 : theData[sp + 2];

                        if (glob) Console.WriteLine($"{theData[sp],5} - {op,2} (jez) ({x,4},-sp-) => ({y,4}) [{theData[x]},-sp-=>{theData[y]}]");

                        if (theData[x] == 0)
                            sp = theData[y];
                        else
                            sp += 3;

                        break;

                    case 7:     // lt x,y => z

                        x = mod1 ? sp + 1 : theData[sp + 1];
                        y = mod2 ? sp + 2 : theData[sp + 2];
                        z = theData[sp + 3];

                        if (glob) Console.WriteLine($"{theData[sp],5} - {op,2} (lt ) ({x,4},{y,4}) => ({z,4}) [{theData[x]},{theData[y]}=>{theData[z]}]");

                        theData[z] = (theData[x] < theData[y]) ? 1 : 0;

                        sp += 4;
      
                        break;

                    case 8:     // eq x,y => z

                        x = mod1 ? sp + 1 : theData[sp + 1];
                        y = mod2 ? sp + 2 : theData[sp + 2];
                        z = theData[sp + 3];

                        if (glob) Console.WriteLine($"{theData[sp],5} - {op,2} (eq ) ({x,4},{y,4}) => ({z,4}) [{theData[x]},{theData[y]}=>{theData[z]}]");

                        theData[z] = (theData[x] == theData[y]) ? 1 : 0;
                        
                        sp += 4;
                        
                        break;

                    case 99:
                        Console.WriteLine("\n\nDone");
                        completed = true;
                        break;
                    default:
                        Console.WriteLine($"BREAK AT LOCATION {sp}");
                        completed = true;
                        break;
                }
            }
            return theData[0];
        }
    }
}

