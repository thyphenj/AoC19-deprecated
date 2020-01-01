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

        public void Run(int pha, Queue queue)
        {
            bool completed = false;
            while (!completed)
            {
                int op = Code[sp] % 100;

                bool mod1 = 1 == (Code[sp] / 100) % 10;
                bool mod2 = 1 == (Code[sp] / 1000) % 10;
                bool mod3 = 1 == (Code[sp] / 10000) % 10;

                int x = mod1 ? sp + 1 : Code[sp + 1];
                int y = mod2 ? sp + 2 : Code[sp + 2];
                int z = mod3 ? sp + 3 : Code[sp + 3];

                switch (op)
                {
                    case 1:     // add x,y => z

                        Code[z] = Code[x] + Code[y];

                        sp += 4;
                        break;

                    case 2:     // mul x,y => z

                        Code[z] = Code[x] * Code[y];

                        sp += 4;
                        break;

                    case 3:     // rea => x

                        {
                            int val;

                            if (Phase.HasValue)
                            {
                                Phase = pha;
                                val = pha;
                            }
                            else
                            {
                                val = queue.Read();
                            }

                            Code[x] = val;

                            sp += 2;
                            break;
                        }
                    case 4:     // wri x

                        queue.Write(Code[x]);

                        sp += 2;
                        completed = true;

                        break;

                    case 5:     // jnz x => y

                        if (Code[x] != 0)
                            sp = Code[y];
                        else
                            sp += 3;

                        break;

                    case 6:     //jez x => y

                        if (Code[x] == 0)
                            sp = Code[y];
                        else
                            sp += 3;

                        break;

                    case 7:     // lt x,y => z

                        Code[z] = (Code[x] < Code[y]) ? 1 : 0;

                        sp += 4;

                        break;

                    case 8:     // eq x,y => z

                        Code[z] = (Code[x] == Code[y]) ? 1 : 0;

                        sp += 4;

                        break;

                    case 99:

                        completed = true;

                        return;

                    default:
                        Console.WriteLine($"*****************************BAD OPCODE {op} AT LOCATION {sp}");
                        break;
                }
                Logit();
            }
            return;

        }
        private void Logit()
        {
            foreach (var x in Code)
            {
                Console.Write($"{x}, ");
            }
            Console.WriteLine();
        }
    }
}
