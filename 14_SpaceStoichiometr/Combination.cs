using System;
using System.Collections.Generic;
using System.Text;

namespace _14_SpaceStoichiometr
{
    class Combination
    {
        public Item Target;
        public List<Item> Ingredients;

        public Combination(string[] strs)
        {
            Target = new Item(strs);
            Ingredients = new List<Item>();
        }

        public void Add(Item item)
        {
            Ingredients.Add(item);
        }

        public override string ToString()
        {
            string ing = "";
            foreach (var i in Ingredients)
                ing += $"{i.ToString()}  ";
            return $"{Target.ToString()}  <===  {ing}";
        }
    }
}
