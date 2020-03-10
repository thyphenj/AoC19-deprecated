using System;
using System.Collections.Generic;
using System.Text;

namespace _14_SpaceStoichiometr
{
    class Reaction
    {
        public Item Product;
        public List<Item> Reagents;

        public Reaction(string[] strs)
        {
            Product = new Item(strs);
            Reagents = new List<Item>();
        }

        public void Add(Item item)
        {
            Reagents.Add(item);
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
