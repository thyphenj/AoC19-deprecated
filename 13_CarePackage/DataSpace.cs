using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;


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
            int theBall = theData.Where(x => x.Value == 4).FirstOrDefault().Key.X;
            int theBat = theData.Where(x => x.Value == 3).FirstOrDefault().Key.X;
            if (theBat < theBall)
                return 1;
            else if (theBat > theBall)
                return -1;
            else 
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

                    Point pt = new Point(X.Value, Y.Value);
                    if (theData.ContainsKey(pt))
                    {
                        theData[pt] = i;
                        //Console.Clear();
                        //Console.WriteLine($"{Screen,10}");
                        //Console.WriteLine(GetString());
                        //Thread.Sleep(6);
                    }
                    else
                        theData.Add(pt, i);
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
            int retval = theData.Where(x => x.Value == 2).Count();

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

            return retval ;
        }
    }
}
