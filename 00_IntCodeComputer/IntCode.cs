using System;
using System.Collections.Generic;

namespace IntCodeComputer
{
    class IntCode
    {
        private bool UsePhase;

        private int? Phase;

        private int sp;
        private int bp;
        private int op;

        public long[] Code;
        /// <summary>
        /// Constructor for IntCodeComputer
        /// </summary>
        /// <param name="codeString"></param>
        /// <param name="usePhase"></param>
        public IntCode(string codeString, bool usePhase = false)
        {
            List<long> CodeList = new List<long>();

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
                        (oper1, oper2, oper3) = getOperands(opCount);

                        Code[oper3] = Code[oper1] + Code[oper2];

                        sp += moveSP(opCount);

                        break;

                    case 2:     // mul x,y => z

                        opCount = 3;
                        (oper1, oper2, oper3) = getOperands(opCount);

                        Code[oper3] = Code[oper1] * Code[oper2];

                        sp += moveSP(opCount);

                        break;

                    case 3:     // rea => x

                        opCount = 1;
                        (oper1, _, _) = getOperands(opCount);

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

                            sp += moveSP(opCount);
                        }
                        break;

                    case 4:     // wri x

                        opCount = 1;
                        (oper1, _, _) = getOperands(opCount);

                        queue.Write(Code[oper1]);

                        sp += moveSP(opCount);

                        break;

                    case 5:     // jnz x => y

                        opCount = 2;
                        (oper1, oper2, _) = getOperands(opCount);

                        if (Code[oper1] != 0)
                            sp = (int)Code[oper2];
                        else
                            sp += moveSP(opCount);

                        break;

                    case 6:     //jez x => y

                        opCount = 2;
                        (oper1, oper2, _) = getOperands(opCount);

                        if (Code[oper1] == 0)
                            sp = (int)Code[oper2];
                        else
                            sp += moveSP(opCount);

                        break;

                    case 7:     // lt x,y => z

                        opCount = 3;
                        (oper1, oper2, oper3) = getOperands(opCount);

                        Code[oper3] = (Code[oper1] < Code[oper2]) ? 1 : 0;

                        sp += moveSP(opCount);

                        break;

                    case 8:     // eq x,y => z

                        opCount = 3;
                        (oper1, oper2, oper3) = getOperands(opCount);

                        Code[oper3] = (Code[oper1] == Code[oper2]) ? 1 : 0;

                        sp += moveSP(opCount);

                        break;

                    case 9:

                        opCount = 1;
                        (oper1, _, _) = getOperands(opCount);

                        bp += (int)Code[oper1];

                        sp += moveSP(opCount);

                        break;

                    case 99:

                        completed = true;
                        halted = true;
                        break;

                    default:
                        Console.WriteLine($"*****************************BAD OPCODE {op} AT LOCATION {sp}");
                        completed = true;
                        halted = true;
                        break;
                }
            }
            return halted;
        }

        /// <summary>
        /// Increment Stack Pointer
        /// </summary>
        /// <param name="offset">number of operands, ie one less than how far to advance sp</param>
        /// <returns>new stack pointer</returns>
        private int moveSP(int offset)
        {
            return offset + 1;
        }

        /// <summary>
        /// Determine operand addresses
        /// </summary>
        /// <param name="opCount">number of operands to decode</param>
        /// <returns>up to three operand addresses</returns>
        private (int X, int Y, int Z) getOperands(int opCount)
        {
            return (opCount > 0 ? getOperand(1) : 0
                  , opCount > 1 ? getOperand(2) : 0
                  , opCount > 2 ? getOperand(3) : 0);
        }

        /// <summary>
        /// Gets an individual operand
        /// </summary>
        /// <param name="opNum"></param>
        /// <returns></returns>
        private int getOperand(int opNum)
        {
            int oper = 0;

            int mode = (int)Code[sp] / Convert.ToInt32(10 * Math.Pow(10, opNum)) % 10;

            switch (mode)
            {
                case 0:
                    oper = (int)Code[sp + opNum];
                    break;
                case 1:
                    oper = sp + opNum;
                    break;
                case 2:
                    oper = (int)Code[sp + opNum] + bp;
                    break;
            }

            if (oper + 1 > Code.Length)
                Array.Resize(ref Code, oper + 1);

            return oper;
        }
    }
}
