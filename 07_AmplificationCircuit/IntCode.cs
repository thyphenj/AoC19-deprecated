using System;
using System.Collections.Generic;
using System.Text;

namespace _07_AmplificationCircuit
{
    class IntCode
    {
        private int? Phase = null;

        private int sp = 0;

        List<int> CodeList = new List<int>();
        int[] Code;

        public IntCode(string codeString)
        {
            foreach (var n in codeString.Split(','))
                CodeList.Add(int.Parse(n));
            Code = CodeList.ToArray();
        }

        public bool Run(int pha, Queue queue)
        {
            bool halted = false;
            bool completed = false;

            while (!completed)
            {
                int op = Code[sp] % 100;
                bool mod1, mod2, mod3;
                int x, y, z;

                mod1 = 1 == (Code[sp] / 100) % 10;
                mod2 = 1 == (Code[sp] / 1000) % 10;
                mod3 = 1 == (Code[sp] / 10000) % 10;

                switch (op)
                {
                    case 1:     // add x,y => z

                        x = mod1 ? sp + 1 : Code[sp + 1];
                        y = mod2 ? sp + 2 : Code[sp + 2];
                        z = mod3 ? sp + 3 : Code[sp + 3];

                        Code[z] = Code[x] + Code[y];

                        sp += add(4);

                        break;

                    case 2:     // mul x,y => z

                        x = mod1 ? sp + 1 : Code[sp + 1];
                        y = mod2 ? sp + 2 : Code[sp + 2];
                        z = mod3 ? sp + 3 : Code[sp + 3];

                        Code[z] = Code[x] * Code[y];

                        sp += add(4);
                        break;

                    case 3:     // rea => x

                        {
                            x = mod1 ? sp + 1 : Code[sp + 1];

                            int val;

                            if (Phase.HasValue)
                            {
                                val = queue.Read();
                            }
                            else
                            {
                                Phase = pha;
                                val = pha;
                            }

                            Code[x] = val;

                            sp += add(2);

                            break;
                        }
                    case 4:     // wri x

                        x = mod1 ? sp + 1 : Code[sp + 1];

                        queue.Write(Code[x]);
                        completed = true;

                        sp += add(2);

                        break;

                    case 5:     // jnz x => y

                        x = mod1 ? sp + 1 : Code[sp + 1];
                        y = mod2 ? sp + 2 : Code[sp + 2];

                        if (Code[x] != 0)
                            sp = add(Code[y]);
                        else
                            sp += add(3);

                        break;

                    case 6:     //jez x => y

                        x = mod1 ? sp + 1 : Code[sp + 1];
                        y = mod2 ? sp + 2 : Code[sp + 2];

                        if (Code[x] == 0)
                            sp = add(Code[y]);
                        else
                            sp += add(3);

                        break;

                    case 7:     // lt x,y => z

                        x = mod1 ? sp + 1 : Code[sp + 1];
                        y = mod2 ? sp + 2 : Code[sp + 2];
                        z = mod3 ? sp + 3 : Code[sp + 3];

                        Code[z] = (Code[x] < Code[y]) ? 1 : 0;

                        sp += add(4);

                        break;

                    case 8:     // eq x,y => z

                        x = mod1 ? sp + 1 : Code[sp + 1];
                        y = mod2 ? sp + 2 : Code[sp + 2];
                        z = mod3 ? sp + 3 : Code[sp + 3];

                        Code[z] = (Code[x] == Code[y]) ? 1 : 0;

                        sp += add(4);

                        break;

                    case 99:

                        completed = true;
                        halted = true;
                        break;

                    default:
                        Console.WriteLine($"*****************************BAD OPCODE {op} AT LOCATION {sp}");
                        break;
                }
            }
            return halted;

        }

        private int add ( int addn)
        {
            //Console.Write($"{sp,4} - ");
            //Console.Write($"{Code[sp],4}");

            //for ( int i = 1; i < addn; i++)
            //    Console.Write($", {Code[sp + i]}");

            //Console.WriteLine();

            return addn;
        }
    }
}
