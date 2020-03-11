using System;
using System.Collections.Generic;
using System.Text;

namespace _14_SpaceStoichiometr
{
    class Reaction
    {
        public Chemical Product;
        public List<Chemical> Reagents;

        public Reaction(string[] strs)
        {
            Product = new Chemical(strs);
            Reagents = new List<Chemical>();
        }

        public void Add(Chemical checmical)
        {
            Reagents.Add(checmical);
        }

        public override string ToString()
        {
            string ing = "";
            foreach (var i in Reagents)
                ing += $"{i.ToString()}  ";
            return $"{Product.ToString()}  <===  {ing}";
        }
    }
}
