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
        private int op = 0;

        public long[] Code;

        public IntCode(string codeString, bool usePhase = false)
        {
            List<long> CodeList = new List<long>();

            int i = 0;
            foreach (var n in codeString.Split(','))
                CodeList.Add(long.Parse(n));

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
                int oper1, oper2, oper3;

                op = (int)Code[sp];
                switch (op % 100)
                {
                    case 1:     // add x,y => z

                        opCount = 3;
                        (oper1, oper2, oper3) = getOperands(sp, opCount);

                        Code[oper3] = Code[oper1] + Code[oper2];

                        sp += incSP(opCount);

                        break;

                    case 2:     // mul x,y => z

                        opCount = 3;
                        (oper1, oper2, oper3) = getOperands(sp, opCount);

                        Code[oper3] = Code[oper1] * Code[oper2];

                        sp += incSP(opCount);

                        break;

                    case 3:     // rea => x

                        opCount = 1;
                        (oper1, oper2, oper3) = getOperands(sp, opCount);

                        {
                            long val;

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
                        (oper1, oper2, oper3) = getOperands(sp, opCount);

                        queue.Write(Code[oper1]);

                        sp += incSP(opCount);

                        break;

                    case 5:     // jnz x => y

                        opCount = 2;
                        (oper1, oper2, oper3) = getOperands(sp, opCount);

                        if (Code[oper1] != 0)
                            sp = (int)Code[oper2];
                        else
                            sp += incSP(opCount);

                        break;

                    case 6:     //jez x => y

                        opCount = 2;
                        (oper1, oper2, oper3) = getOperands(sp, opCount);

                        if (Code[oper1] == 0)
                            sp = (int)Code[oper2];
                        else
                            sp += incSP(opCount);

                        break;

                    case 7:     // lt x,y => z

                        opCount = 3;
                        (oper1, oper2, oper3) = getOperands(sp, opCount);

                        Code[oper3] = (Code[oper1] < Code[oper2]) ? 1 : 0;

                        sp += incSP(opCount);

                        break;

                    case 8:     // eq x,y => z

                        opCount = 3;
                        (oper1, oper2, oper3) = getOperands(sp, opCount);

                        Code[oper3] = (Code[oper1] == Code[oper2]) ? 1 : 0;

                        sp += incSP(opCount);

                        break;

                    case 9:

                        opCount = 1;
                        (oper1, oper2, oper3) = getOperands(sp, opCount);

                        bp += (int)Code[oper1];

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

        private int getOperand(int opNum, int sp)
        {
            int oper1 = 0;

            int mode = (int)Code[sp] / Convert.ToInt32(10 * Math.Pow(10, opNum)) % 10;

            if (mode == 0)
                oper1 = (int)Code[sp + opNum];
            if (mode == 1)
                oper1 = sp + opNum;
            if (mode == 2)
                oper1 = (int)Code[sp + opNum] + bp;

            if (oper1 + 1 > Code.Length)
                Array.Resize(ref Code, oper1 + 1);

            return oper1;
        }
        private (int X, int Y, int Z) getOperands(int sp, int opCount)
        {
            int oper1, oper2, oper3;

            oper1 = opCount > 0 ? getOperand(1, sp) : 0;
            oper2 = opCount > 1 ? getOperand(2, sp) : 0;
            oper3 = opCount > 2 ? getOperand(3, sp) : 0;

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
