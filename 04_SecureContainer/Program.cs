using System;

namespace _04_SecureContainer
{
    class Program
    {
        static void Main()
        {
            string theData = "145852-616942";

            int loVal = Int32.Parse(theData.Substring(0, 6));
            int hiVal = Int32.Parse(theData.Substring(7, 6));

            Part1(loVal, hiVal);

            Part2(loVal, hiVal);

        }
        static void Part1(int loVal, int hiVal)
        {
            int result = 0;

            int i = loVal;
            while (i <= hiVal)
            {
                if (Part1Check(i.ToString()))
                {
                    result++;
                }
                i++;
            }
            Console.WriteLine($"{result}");
        }
        static void Part2(int loVal, int hiVal)
        {
            Console.WriteLine($"{loVal} - {hiVal}");
        }
        static bool Part1Check(string str)
        {
            bool asc = true;
            bool dbl = false;

            for ( int i = 1; i < str.Length && asc ; i++)
            {
                if (str[i - 1] > str[i])
                    asc = false;
                if (str[i - 1] == str[i])
                    dbl = true;
            }

            return asc && dbl;
        }
    }
}
