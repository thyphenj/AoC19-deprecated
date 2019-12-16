using System;
using System.Collections.Generic;
using System.Text;

namespace _07_AmplificationCircuit
{
    class ProgramCode
    {
        private readonly bool Glob = true;
        private int Phase;
        private string CodeString;
        private string AmpName;

        List<int> Code = new List<int>();

        public ProgramCode(string ampName, string codeString)
        {
            AmpName = ampName;
            CodeString = codeString;

            foreach (var n in CodeString.Split(','))
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

                        //Logit(sp, op, "add", x, y, z);
                        Console.WriteLine($"sp={sp,3} ADD({Code[sp],4}) loc{z}={Code[x]}+{Code[y]}");
                        Code[z] = Code[x] + Code[y];

                        sp += 4;
                        break;

                    case 2:     // mul x,y => z

                        //Logit(sp, op, "mul", x, y, z);
                        Console.WriteLine($"sp={sp,3} MUL({Code[sp],4}) loc{z}={Code[x]}*{Code[y]}");

                        Code[z] = Code[x] * Code[y];

                        sp += 4;
                        break;

                    case 3:     // rea => x

                        if (usePhase)
                        {
                            Code[x] = Phase;
                            usePhase = false;
                        }
                        else
                        {
                            Code[x] = input;
                        }

                        //Logit(sp, op, "rd ", x);

                        Console.WriteLine($"sp={sp,3} REA({Code[sp],4}) loc{x}={Code[x]}");

                        sp += 2;
                        break; 

                    case 4:     // wri x

                        Console.WriteLine($"sp={sp,3} WRI({Code[sp],4}) loc{x} ({Code[x]})");

                        output = Code[x];
                        if (Glob) Console.WriteLine($"----->output= {output}");

                        completed = true;

                        sp += 2;
                        break;

                    case 5:     // jnz x => y

                        //Logit(sp, op, "jnz", x, y); 
                        Console.WriteLine($"sp={sp,3} JNZ({Code[sp],4}) loc{x} if {Code[x]} <> 0 goto loc{y}({Code[y]})");

                        if (Code[x] != 0)
                            sp = Code[y];
                        else
                            sp += 3;

                        break;

                    case 6:     //jez x => y

                        Logit(sp, op, "jez", x, y);

                        if (Code[x] == 0)
                            sp = Code[y];
                        else
                            sp += 3;

                        break;

                    case 7:     // lt x,y => z

                        Logit(sp, op, "lt ", x, y, z);

                        Code[z] = (Code[x] < Code[y]) ? 1 : 0;

                        sp += 4;

                        break;

                    case 8:     // eq x,y => z

                        Logit(sp, op, "eq ", x, y, z);

                        Code[z] = (Code[x] == Code[y]) ? 1 : 0;

                        sp += 4;

                        break;

                    case 99:
                        Logit(sp, op, "hlt");
                        return true;
                    default:
                        Console.WriteLine($"*****************************BAD OPCODE {op} AT LOCATION {sp}");
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
                string locs = "(", vals = "(";
                if (x.HasValue)
                {
                    locs += x.Value;
                    vals += Code[x.Value];
                }
                if (y.HasValue)
                {
                    locs += "," + y.Value;
                    vals += "," + Code[y.Value];
                }
                if (z.HasValue)
                {
                    locs += "," + z.Value;
                    vals += "," + Code[z.Value];
                }

                Console.WriteLine($"{sp,4} {Code[sp],5} - {str} " + locs + ") " + vals + ")");
            }
        }
    }
}
