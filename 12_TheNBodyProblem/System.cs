using System;
using System.Collections.Generic;
using System.Text;

namespace _12_TheNBodyProblem
{
    class System
    {
        public Planet[] Planets = new Planet[4];

        public System ( Planet[] planets)
        {
            for (int i = 0; i < 4; i++)
                Planets[i] = planets[i];
        }
    }
}
