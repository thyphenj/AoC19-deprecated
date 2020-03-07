using System;
using System.Collections.Generic;
using System.Text;

namespace _15_OxygenSystem
{
    class IntCodeComputer
    {
        private int sp = 0;
        private int bp = 0;
        private int op = 0;

        public long[] Code;

        public IntCodeComputer(string codeString)
        {
            List<long> tokens = new List<long>();

            foreach (var n in codeString.Split(','))
                tokens.Add(long.Parse(n));

            Code = tokens.ToArray();
        }

        public void Run(DataSpace theData)
        {
            int opCount;
            int oper1, oper2, oper3;

            bool halted = false;

            while (!halted)
            {
                op = (int)Code[sp];

                switch (op % 100)
                {
                    case 1:     // add x,y => z

                        opCount = 3;
                        (oper1, oper2, oper3) = GetOperands(sp, opCount);

                        Code[oper3] = Code[oper1] + Code[oper2];

                        sp += IncSP(opCount);

                        break;

                    case 2:     // mul x,y => z

                        opCount = 3;
                        (oper1, oper2, oper3) = GetOperands(sp, opCount);

                        Code[oper3] = Code[oper1] * Code[oper2];

                        sp += IncSP(opCount);

                        break;

                    case 3:     // rea => x

                        opCount = 1;
                        (oper1, _, _) = GetOperands(sp, opCount);

                        Code[oper1] = theData.Read();

                        sp += IncSP(opCount);

                        break;

                    case 4:     // wri x

                        opCount = 1;
                        (oper1, _, _) = GetOperands(sp, opCount);

                        theData.Write(Code[oper1]);

                        sp += IncSP(opCount);

                        break;

                    case 5:     // jnz x => y

                        opCount = 2;
                        (oper1, oper2, _) = GetOperands(sp, opCount);

                        if (Code[oper1] != 0)
                            sp = (int)Code[oper2];
                        else
                            sp += IncSP(opCount);

                        break;

                    case 6:     //jez x => y

                        opCount = 2;
                        (oper1, oper2, _) = GetOperands(sp, opCount);

                        if (Code[oper1] == 0)
                            sp = (int)Code[oper2];
                        else
                            sp += IncSP(opCount);

                        break;

                    case 7:     // lt x,y => z

                        opCount = 3;
                        (oper1, oper2, oper3) = GetOperands(sp, opCount);

                        Code[oper3] = (Code[oper1] < Code[oper2]) ? 1 : 0;

                        sp += IncSP(opCount);

                        break;

                    case 8:     // eq x,y => z

                        opCount = 3;
                        (oper1, oper2, oper3) = GetOperands(sp, opCount);

                        Code[oper3] = (Code[oper1] == Code[oper2]) ? 1 : 0;

                        sp += IncSP(opCount);

                        break;

                    case 9:

                        opCount = 1;
                        (oper1, _, _) = GetOperands(sp, opCount);

                        bp += (int)Code[oper1];

                        sp += IncSP(opCount);

                        break;

                    case 99:

                        halted = true;
                        break;

                    default:
                        Console.WriteLine($"*****************************BAD OPCODE {op} AT LOCATION {sp}");
                        halted = true;
                        break;
                }
            }
            return;

        }

        // *** Private methods -----------------------------------------------------------------------------------------

        private (int X, int Y, int Z) GetOperands(int sp, int opCount)
        {
            int oper1, oper2, oper3;

            oper1 = opCount > 0 ? GetOperand(1, sp) : 0;
            oper2 = opCount > 1 ? GetOperand(2, sp) : 0;
            oper3 = opCount > 2 ? GetOperand(3, sp) : 0;

            return (oper1, oper2, oper3);
        }

        private int GetOperand(int opNum, int sp)
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

        private int IncSP(int addn)
        {
            return addn + 1;
        }
    }
}
