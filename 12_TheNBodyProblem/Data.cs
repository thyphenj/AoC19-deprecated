using System;
using System.Collections.Generic;
using System.Text;

namespace _12_TheNBodyProblem
{
    class Data
    {
        string theData = $"<x=-15, y=1, z=4>\n<x=1, y=-10, z=-8>\n<x=-5, y=4, z=9>\n<x=4, y=6, z=-2>";

      //  string testData = $"<x=-1, y=0, z=2>\n<x=2, y=-10, z=-7>\n<x=4, y=-8, z=8>\n<x=3, y=5, z=-1>";
        string testData = $"<x=-8, y=-10, z=0>\n<x=5, y=5, z=10>\n<x=2, y=-7, z=3>\n<x=9, y=-8, z=-3>";
        public Planet[] Planets = new Planet[4];

        public Data(int dataset = 0)
        {
            if (dataset == 0)
            {
                //    Planets[0] = new Planet(-15, 1, 4);
                //    Planets[1] = new Planet(1, -10, -8);
                //    Planets[2] = new Planet(-5, 4, 9);
                //    Planets[3] = new Planet(4, 6, -2);
                testData = theData;
            }


            foreach (var ch in $"\r<>xyz= ")
                testData = testData.Replace(ch.ToString(), "");
            var xxx = testData.Split($"\n");
            int i = 0;
            foreach (var str in xxx)
            {
                int[] arr = new int[3];
                int j = 0;
                foreach (var a in str.Split(','))
                    arr[j++] = Convert.ToInt32(a);
                Planets[i++] = new Planet(arr[0], arr[1], arr[2]);
            }
        }
    }
}
