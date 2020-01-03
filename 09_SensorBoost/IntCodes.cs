using System;
using System.Collections.Generic;

namespace _09_SensorBoost
{
    class IntCode
    {
        private bool UsePhase = false;

        private int? Phase = null;

        private int sp = 0;
        private int bp = 0;

        public List<int> CodeList = new List<int>();
        public int[] Code;

        public IntCode(string codeString, bool usePhase = false)
        {
            foreach (var n in codeString.Split(','))
                CodeList.Add(int.Parse(n));

            Code = CodeList.ToArray();

            UsePhase = usePhase;
            if (!UsePhase)
                Phase = 0;
        }

        public bool Run(int pha, Queue queue)
        {
            int opCount;

            bool halted = false;
            bool completed = false;

            while (!completed)
            {
                //-- initially these are the offsets from the opcode
                //-- the try..catch changes them to actual locations

                int op = Code[sp] % 100;
                switch (op)
                {
                    case 1:     // add x,y => z
                        opCount = 3;

                        (int oper1, int oper2, int oper3) = operandCheck(sp, ref Code, opCount);

                        Code[oper3] = Code[oper1] + Code[oper2];

                        sp += incSP(opCount);

                        break;

                    case 2:     // mul x,y => z

                        opCount = 3;
                        (oper1, oper2, oper3) = operandCheck(sp, ref Code, opCount);

                        Code[oper3] = Code[oper1] * Code[oper2];

                        sp += incSP(opCount);

                        break;

                    case 3:     // rea => x

                        opCount = 1;
                        (oper1, oper2, oper3) = operandCheck(sp, ref Code, opCount);

                        {
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

                            Code[oper1] = val;

                            sp += incSP(opCount);
                        }
                        break;

                    case 4:     // wri x

                        opCount = 1;
                        (oper1, oper2, oper3) = operandCheck(sp, ref Code, opCount);

                        queue.Write(Code[oper1]);

                        sp += incSP(opCount);

                        break;

                    case 5:     // jnz x => y

                        opCount = 2;
                        (oper1, oper2, oper3) = operandCheck(sp, ref Code, opCount);

                        if (Code[oper1] != 0)
                            sp = incSP(Code[oper2]);
                        else
                            sp += incSP(opCount);

                        break;

                    case 6:     //jez x => y

                        opCount = 2;
                        (oper1, oper2, oper3) = operandCheck(sp, ref Code, opCount);

                        if (Code[oper1] == 0)
                            sp = incSP(Code[oper2]);
                        else
                            sp += incSP(opCount);

                        break;

                    case 7:     // lt x,y => z

                        opCount = 3;
                        (oper1, oper2, oper3) = operandCheck(sp, ref Code, opCount);

                        Code[oper3] = (Code[oper1] < Code[oper2]) ? 1 : 0;

                        sp += incSP(opCount);

                        break;

                    case 8:     // eq x,y => z

                        opCount = 3;
                        (oper1, oper2, oper3) = operandCheck(sp, ref Code, opCount);

                        Code[oper3] = (Code[oper1] == Code[oper2]) ? 1 : 0;

                        sp += incSP(opCount);

                        break;

                    case 9:

                        opCount = 1;
                        (oper1, oper2, oper3) = operandCheck(sp, ref Code, opCount);

                        bp += Code[oper1];

                        sp += incSP(opCount);

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

        private (int X, int Y, int Z) operandCheck(int sp, ref int[] Code, int opCount)
        {
            int oper1=1, oper2=2, oper3=3;

            if (opCount > 0)
            {
                int mode = (Code[sp] / 100) % 10;
                oper1 +=  mode == 1 ? sp : mode == 2 ? sp + bp : Code[sp];

                if (oper1 + 1 > Code.Length)
                    Array.Resize(ref Code, oper1 + 1);
            }
            if (opCount > 1)
            {
                int mode = (Code[sp] / 1000) % 10;
                oper2 += mode == 1 ? sp : mode == 2 ? sp + bp : Code[sp];

                if (oper2 + 1 > Code.Length)
                    Array.Resize(ref Code, oper2 + 1);
            }
            if (opCount > 2)
            {
                int mode = (Code[sp] / 10000) % 10;
                oper3 += mode == 1 ? sp : mode == 2 ? sp + bp : Code[sp];

                if (oper3 + 1 > Code.Length)
                    Array.Resize(ref Code, oper3 + 1);
            }

            return (oper1, oper2, oper3);
        }

        private int incSP(int addn)
        {
            //Console.Write($"{sp,4} - ");
            //Console.Write($"{Code[sp],4}");

            //for ( int i = 1; i < addn; i++)
            //    Console.Write($", {Code[sp + i]}");

            //Console.WriteLine();

            return addn + 1;
        }
    }
}
