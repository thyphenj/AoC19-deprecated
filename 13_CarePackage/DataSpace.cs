using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;


namespace _13_CarePackage
{
    class DataSpace
    {
        public Dictionary<Point, int> theData = new Dictionary<Point, int>();

        public int? X;
        public int? Y;
        public int Screen;

        private int maxX = 0;
        private int maxY = 0;



        public DataSpace()
        {
            X = null;
            Y = null;
        }

        public long Read()
        {
            return 0;
        }

        // *** We need to BUILD this from separate writes
        public void Write(long val)
        {
            int i = Convert.ToInt32(val);

            if (!X.HasValue)
                X = i;
            else if (!Y.HasValue)
                Y = i;
            else
            {
                if (X >= 0)
                {
                    if (X.Value > maxX) maxX = X.Value;
                    if (Y.Value > maxY) maxY = Y.Value;

                    theData.Add(new Point(X.Value, Y.Value), i);
                }
                else 
                {
                    Screen = i;
                }
                X = null;
                Y = null;
            }
        }

        public int GetCount()
        {
            int retval = theData.Where( x => x.Value == 2).Count();

            return retval;
        }
        public int[,] GetMatrix()
        {
            int[,] matrix = new int[maxX + 1, maxY + 1];

            foreach (var dat in theData)
                matrix[dat.Key.X, dat.Key.Y] = dat.Value;

            return matrix;
        }

        public string GetString()
        {
            string retval = "";
            string ch;

            int[,] matrix = GetMatrix();

            for (int y = 0; y <= maxY; y++)
            {
                for (int x = 0; x <= maxX; x++)
                {
                    ch = (matrix[x, y]) switch
                    {
                        0 => " ",
                        1 => "X",
                        2 => ".",
                        3 => "_",
                        4 => "o",
                        _ => "!",
                    };
                    retval += ch;
                }
                retval += $"\n";
            }

            return retval;
        }
    }
}
