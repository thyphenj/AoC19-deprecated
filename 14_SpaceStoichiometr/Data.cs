using System.IO;
using System.Collections.Generic;
using System.Text;

namespace _14_SpaceStoichiometr
{
    class Data
    {
        public string[] Lines;

        public Data(int which = 0)
        {
            if (which == 0)
                Lines = File.ReadAllLines(@"Text\data.txt");
            else
            {
                string[] testData =
                    { $"10 ORE => 10 A\n1 ORE => 1 B\n7 A, 1 B => 1 C\n7 A, 1 C => 1 D\n7 A, 1 D => 1 E\n7 A, 1 E => 1 FUEL"
                    , $"9 ORE => 2 A\n8 ORE => 3 B\n7 ORE => 5 C\n3 A, 4 B => 1 AB\n5 B, 7 C => 1 BC\n4 C, 1 A => 1 CA\n2 AB, 3 BC, 4 CA => 1 FUEL"
                    , $"157 ORE => 5 NZVS\n165 ORE => 6 DCFZ\n44 XJWVT, 5 KHKGT, 1 QDVJ, 29 NZVS, 9 GPVTF, 48 HKGWZ => 1 FUEL\n12 HKGWZ, 1 GPVTF, 8 PSHF => 9 QDVJ\n179 ORE => 7 PSHF\n177 ORE => 5 HKGWZ\n7 DCFZ, 7 PSHF => 2 XJWVT\n165 ORE => 2 GPVTF\n3 DCFZ, 7 NZVS, 5 HKGWZ, 10 PSHF => 8 KHKGT"
                    };
                if (which > 0 && which <= testData.Length)
                    Lines = testData[which - 1].Replace("\r", "").Split('\n');
                else
                    Lines = new string[] { "1 ORE => 1 FUEL" };
            }
        }
    }
}
