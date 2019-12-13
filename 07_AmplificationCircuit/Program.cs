using System;
using System.IO;
using System.Collections.Generic;

namespace _07_AmplificationCircuit
{
    class Program
    {
        static bool glob = false;

        static void Main()
        {
            //string tester = "3,15,3,16,1002,16,10,16,1,16,15,15,4,15,99,0,0";
            //string tester = "3,23,3,24,1002,24,10,24,1002,23,-1,23,101,5,23,23,1,24,23,23,4,23,99,0,0";
            //string tester = "3,31,3,32,1002,32,10,32,1001,31,-2,31,1007,31,0,33,1002,33,7,33,1,33,31,31,1,32,31,31,4,31,99,0,0,0";
            string tester = "3,26,1001,26,-4,26,3,27,1002,27,2,27,1,27,26,27,4,27,1001,28,-1,28,1005,28,6,99,0,0,5";
            List<int> theProgram = new List<int>();

            //foreach (var n in File.ReadAllText("data.txt").Split(','))
            foreach (var n in tester.Split(','))
                theProgram.Add(Int32.Parse(n));

            Part2(theProgram);

        }
        static void Part1(List<int> theProgram)
        {
            int max = 0;

            for (int a = 0; a < 5; a++)
                for (int b = 0; b < 5; b++)
                    for (int c = 0; c < 5; c++)
                        for (int d = 0; d < 5; d++)
                            for (int e = 0; e < 5; e++)
                            {
                                if (!(a == b || a == c || a == d || a == e || b == c || b == d || b == e || c == d || c == e || d == e))
                                {
                                    int transfer = 0;

                                    foreach (int phase in new int[] { a, b, c, d, e })
                                        transfer = RunProgram(theProgram, phase, transfer);

                                    if (transfer > max)
                                    {
                                        max = transfer;
                                        Console.WriteLine($"{a} {b} {c} {d} {e} - {transfer}");
                                    }
                                }
                            }
        }

        static void Part2(List<int> theProgram)
        {
            int max = 0;

            for (int a = 0; a < 5; a++)
                for (int b = 0; b < 5; b++)
                    for (int c = 0; c < 5; c++)
                        for (int d = 0; d < 5; d++)
                            for (int e = 0; e < 5; e++)
                            {
                                if (!(a == b || a == c || a == d || a == e || b == c || b == d || b == e || c == d || c == e || d == e))
                                {
                                    int transfer = 0;

                                    foreach (int phase in new int[] { a + 5, b + 5, c + 5, d + 5, e + 5 })
                                    {
                                        transfer = RunProgram(theProgram, phase, transfer);
                                    }
                                    if (transfer > max)
                                    {
                                        max = transfer;
                                        Console.WriteLine($"{a+5} {b+5} {c+5} {d+5} {e+5} - {transfer}");
                                    }
                                }

                            }
        }
        static int RunProgram(List<int> theData, int phase, int input)
        {
            bool completed = false;
            int output = 0;

            int sp = 0;
            bool useInput = false;
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

                        //Console.Write("===> ");
                        //theData[x] = Int32.Parse(Console.ReadLine());

                        if (!useInput)
                        {
                            theData[x] = phase;
                            useInput = true;
                        }
                        else
                        {
                            theData[x] = input;
                        }
                        sp += 2;
                        break;

                    case 4:     // wri x

                        x = mod1 ? sp + 1 : theData[sp + 1];

                        if (glob) Console.WriteLine($"{theData[sp],5} - {op,2} (wri) ({x,4}     ) => (    ) [{theData[x]}]");

                        //Console.WriteLine($"===< {theData[x]}");
                        output = theData[x];
                        
                        completed = true;

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
                        completed = true;
                        break;
                    default:
                        Console.WriteLine($"BREAK AT LOCATION {sp}");
                        completed = true;
                        break;
                }
            }
            return output;
        }
    }
}


