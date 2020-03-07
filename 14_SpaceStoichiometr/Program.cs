using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace _14_SpaceStoichiometr
{
    class Program
    {
        static void Main()
        {
            int which = 4;
            Part1(which);
        }

        static void Part1(int which)
        {
            Data data = new Data(which);
            Recipe recipe = new Recipe(data);

            recipe.Display();

            var xx = recipe.CreateTree();

            //recipe.ReplaceFuel();

            //recipe.Display();

            //recipe.Accumulate();
        }
    }
}
