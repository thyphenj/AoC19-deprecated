using System;
using System.Collections.Generic;
using System.Text;

namespace _07_AmplificationCircuit
{
    class ProgramCode
    {
        private bool Glob = true;
        private int Phase;
        private string codeString;
        private string AmpName;

        List<int> Code = new List<int>();

        public ProgramCode(string ampName, string code)
        {
            AmpName = ampName;
            codeString = code;

            foreach (var n in codeString.Split(','))
                Code.Add(int.Parse(n));
        }

        public bool Run(int input, out int output, int? pha = null)
        {
            bool usePhase = false;

            if (pha.HasValue)
            {
                Phase = pha.Value;
                usePhase = true;
            }
            Console.WriteLine($"***Starting {AmpName}*** {Phase} {input}");

            if (Glob) Console.WriteLine($"----->input = {input}");

            output = 0;

            int sp = 0;

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

                        Logit(sp, op, "---", x, y, z);

                        Code[z] = Code[x] + Code[y];

                        sp += 4;
                        break;

                    case 2:     // mul x,y => z

                        x = mod1 ? sp + 1 : Code[sp + 1];
                        y = mod2 ? sp + 2 : Code[sp + 2];
                        z = mod3 ? sp + 3 : Code[sp + 3];

                        Logit(sp, op, "mul", x, y, z);

                        Code[z] = Code[x] * Code[y];

                        sp += 4;
                        break;

                    case 3:     // rea => x

                        x = mod1 ? sp + 1 : Code[sp + 1];

                        Logit(sp, op, "rd ");

                        if (usePhase)
                        {
                            Code[x] = Phase;
                            usePhase = false;
                        }
                        else
                        {
                            Code[x] = input;
                        }
                        sp += 2;
                        break;

                    case 4:     // wri x

                        x = mod1 ? sp + 1 : Code[sp + 1];

                        Logit(sp, op, "wri");

                        output = Code[x];
                        if (Glob) Console.WriteLine($"----->output= {output}");

                        completed = true;

                        sp += 2;
                        break;

                    case 5:     // jnz x => y
                        x = mod1 ? sp + 1 : Code[sp + 1];
                        y = mod2 ? sp + 2 : Code[sp + 2];

                        Logit(sp, op, "jnz", x, y);

                        if (Code[x] != 0)
                            sp = Code[y];
                        else
                            sp += 3;

                        break;

                    case 6:     //jez x => y
                        x = mod1 ? sp + 1 : Code[sp + 1];
                        y = mod2 ? sp + 2 : Code[sp + 2];

                        Logit(sp, op, "jez", x, y);

                        if (Code[x] == 0)
                            sp = Code[y];
                        else
                            sp += 3;

                        break;

                    case 7:     // lt x,y => z

                        x = mod1 ? sp + 1 : Code[sp + 1];
                        y = mod2 ? sp + 2 : Code[sp + 2];
                        z = mod3 ? sp + 3 : Code[sp + 3];

                        Logit(sp, op, "lt ", x, y, z);

                        Code[z] = (Code[x] < Code[y]) ? 1 : 0;

                        sp += 4;

                        break;

                    case 8:     // eq x,y => z

                        x = mod1 ? sp + 1 : Code[sp + 1];
                        y = mod2 ? sp + 2 : Code[sp + 2];
                        z = mod3 ? sp + 3 : Code[sp + 3];

                        Logit(sp, op, "eq ", x, y, z);

                        Code[z] = (Code[x] == Code[y]) ? 1 : 0;

                        sp += 4;

                        break;

                    case 99:
                        Logit(sp, op, "hlt");
                        return true;
                    default:
                        Console.WriteLine($"*****************************BREAK AT LOCATION {sp}");
                        completed = true;
                        break;
                }
            }
            return false;

        }
        private void Logit(int sp, int op, string str, int? x = null, int? y = null, int? z = null)
        {
            if (Glob)
            {
                Console.Write($"{sp,4} {Code[sp],5} - {op,2} (");
                if (x.HasValue)
                    Console.Write($"({x.Value} = {Code[x.Value]}");
                else
                    Console.Write("(    ");
                if (y.HasValue)
                    Console.Write($",{y.Value} = {Code[y.Value]}");
                else
                    Console.Write("     ");
                if (z.HasValue)
                    Console.Write($",{z.Value} = {Code[z.Value]})");
                else
                    Console.Write("     )");
                Console.WriteLine();
            }
        }
    }
}
