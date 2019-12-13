using System;
using System.Collections.Generic;
using System.Text;

namespace _07_AmplificationCircuit
{
    class ProgramCode
    {
        private bool glob = false;

        List<int> Code = new List<int>();

        public ProgramCode ( string code)
        {
            foreach (var n in code.Split(','))
                Code.Add(int.Parse(n));
        }

        public bool Run (int input, int phase, out int output)
        {
            bool completed = false;
            output = 0;

            int sp = 0;
            bool useInput = false;
            while (!completed)
            {
                int op = Code[sp] % 100;
                bool mod1 = 1 == (Code[sp] / 100) % 10;
                bool mod2 = 1 == (Code[sp] / 1000) % 10;
                bool mod3 = 1 == (Code[sp] / 10000) % 10;
                int x, y, z;

                switch (op)
                {
                    case 1:     // add x,y => z

                        x = mod1 ? sp + 1 : Code[sp + 1];
                        y = mod2 ? sp + 2 : Code[sp + 2];
                        z = Code[sp + 3];

                        if (glob) Console.WriteLine($"{Code[sp],5} - {op,2} (add) ({x,4},{y,4}) => ({z,4}) [{Code[x]},{Code[y]}=>{Code[z]}]");

                        Code[z] = Code[x] + Code[y];

                        sp += 4;
                        break;

                    case 2:     // mul x,y => z

                        x = mod1 ? sp + 1 : Code[sp + 1];
                        y = mod2 ? sp + 2 : Code[sp + 2];
                        z = Code[sp + 3];


                        if (glob) Console.WriteLine($"{Code[sp],5} - {op,2} (mul) ({x,4},{y,4}) => ({z,4}) [{Code[x]},{Code[y]}=>{Code[z]}]");

                        Code[z] = Code[x] * Code[y];

                        sp += 4;
                        break;

                    case 3:     // rea => x

                        x = mod1 ? sp + 1 : Code[sp + 1];

                        if (!useInput)
                        {
                            Code[x] = phase;
                            useInput = true;
                        }
                        else
                        {
                            Code[x] = input;
                        }
                        sp += 2;
                        break;

                    case 4:     // wri x

                        x = mod1 ? sp + 1 : Code[sp + 1];

                        if (glob) Console.WriteLine($"{Code[sp],5} - {op,2} (wri) ({x,4}     ) => (    ) [{Code[x]}]");

                        //Console.WriteLine($"===< {Code[x]}");
                        output = Code[x];

                        completed = true;

                        sp += 2;
                        break;

                    case 5:     // jnz x => y
                        x = mod1 ? sp + 1 : Code[sp + 1];
                        y = mod2 ? sp + 2 : Code[sp + 2];

                        if (glob) Console.WriteLine($"{Code[sp],5} - {op,2} (jnz) ({x,4},-sp-) => ({y,4}) [{Code[x]},-sp-=>{Code[y]}]");

                        if (Code[x] != 0)
                            sp = Code[y];
                        else
                            sp += 3;

                        break;

                    case 6:     //jez x => y
                        x = mod1 ? sp + 1 : Code[sp + 1];
                        y = mod2 ? sp + 2 : Code[sp + 2];

                        if (glob) Console.WriteLine($"{Code[sp],5} - {op,2} (jez) ({x,4},-sp-) => ({y,4}) [{Code[x]},-sp-=>{Code[y]}]");

                        if (Code[x] == 0)
                            sp = Code[y];
                        else
                            sp += 3;

                        break;

                    case 7:     // lt x,y => z

                        x = mod1 ? sp + 1 : Code[sp + 1];
                        y = mod2 ? sp + 2 : Code[sp + 2];
                        z = Code[sp + 3];

                        if (glob) Console.WriteLine($"{Code[sp],5} - {op,2} (lt ) ({x,4},{y,4}) => ({z,4}) [{Code[x]},{Code[y]}=>{Code[z]}]");

                        Code[z] = (Code[x] < Code[y]) ? 1 : 0;

                        sp += 4;

                        break;

                    case 8:     // eq x,y => z

                        x = mod1 ? sp + 1 : Code[sp + 1];
                        y = mod2 ? sp + 2 : Code[sp + 2];
                        z = Code[sp + 3];

                        if (glob) Console.WriteLine($"{Code[sp],5} - {op,2} (eq ) ({x,4},{y,4}) => ({z,4}) [{Code[x]},{Code[y]}=>{Code[z]}]");

                        Code[z] = (Code[x] == Code[y]) ? 1 : 0;

                        sp += 4;

                        break;

                    case 99:
                        return true;
                    default:
                        Console.WriteLine($"BREAK AT LOCATION {sp}");
                        completed = true;
                        break;
                }
            }
            return false;

        }
    }
}
