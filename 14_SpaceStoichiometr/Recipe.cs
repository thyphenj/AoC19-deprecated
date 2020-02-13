using System;
using System.Collections.Generic;
using System.Linq;

namespace _14_SpaceStoichiometr
{
    class Recipe
    {
        public List<Combination> Combinations = new List<Combination>();
        public List<Combination> OREs = new List<Combination>();
        public Combination FUEL;

        public Recipe(Data data)
        {
            foreach (string x in data.Lines)
            {
                Combination recipe = new Combination(x.Split(" => ")[1].Split(" "));

                foreach (string s in x.Split(" => ")[0].Split(", "))
                {
                    recipe.Add(new Item(s.Split(" ")));
                }
                if (recipe.Target.Name == "FUEL")
                    FUEL = recipe;
                else if (recipe.Ingredients.Where(x => x.Name == "ORE").Count() == 1)
                    OREs.Add(recipe);
                else
                    Combinations.Add(recipe);
            }
        }

        public void ReplaceFuel()
        {
            List<int> removeList = new List<int>();

            int i = 0;
            while (i < FUEL.Ingredients.Count)
            {
                Item item = FUEL.Ingredients[i];

                var r = Combinations.Where(x => x.Target.Name == item.Name).FirstOrDefault();
                if (r != null)
                {
                    removeList.Add(i);
                    foreach (var w in r.Ingredients)
                    {
                        w.Qty *= item.Qty;
                        FUEL.Add(w);
                    }
                }
                i++;
            }

            foreach (int j in removeList.OrderByDescending(x => x))
                FUEL.Ingredients.RemoveAt(j);
        }

        public void Accumulate()
        {
            Dictionary<string, int> Totals = new Dictionary<string, int>();

            foreach (var i in FUEL.Ingredients)
            {
                if (Totals.ContainsKey(i.Name))
                    Totals[i.Name] += i.Qty;
                else
                    Totals.Add(i.Name, i.Qty);
            }

            int oreSum = 0;
            foreach (var i in Totals)
            {
                var theORE = OREs.Where(x => x.Target.Name == i.Key).FirstOrDefault();

                int needToProduce = theORE.Target.Qty;
                needToProduce = ((i.Value + needToProduce - 1) / needToProduce)*needToProduce;

                int oresRequired = theORE.Ingredients[0].Qty * needToProduce / theORE.Target.Qty;

                Console.WriteLine($"{i.Key,5} {i.Value}  {needToProduce} {oresRequired}");

                oreSum += oresRequired;
            }
            Console.WriteLine($"COUNT == {oreSum}");
        }

        public IEnumerable<Combination> GetCombination()
        {
            foreach (Combination r in Combinations)
                yield return r;
        }

        public IEnumerable<Combination> GetORE()
        {
            foreach (Combination r in OREs)
                yield return r;
        }

        public Combination GetFUEL()
        {
            return FUEL;
        }
        public override string ToString()
        {
            string retval = "";

            retval += FUEL.ToString() + "\n\n";

            foreach (Combination s in Combinations)
            {
                retval += s.ToString() + "\n";
            }
            retval += "\n";

            foreach (Combination s in OREs)
            {
                retval += s.ToString() + "\n";
            }
            return retval;
        }

        public void Display()
        {
            Console.WriteLine("-----------------------------------------------------------------------");
            Console.WriteLine(ToString());
        }
    }
}
