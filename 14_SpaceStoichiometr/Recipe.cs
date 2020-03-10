using System;
using System.Collections.Generic;
using System.Linq;

namespace _14_SpaceStoichiometr
{
    class Recipe
    {
        public Reaction FUEL;
        public List<Reaction> Reactions = new List<Reaction>();

        Tree TheTree = new Tree();

        public Recipe(Data data)
        {
            foreach (string x in data.Lines)
            {
                var xxxx = x.Split(" => ");
                
                Reaction reaction = new Reaction(xxxx[1].Split(" "));

                foreach (string s in xxxx[0].Split(", "))
                {
                    reaction.Add(new Item(s.Split(" ")));
                }
                reaction.Reagents = reaction.Reagents.OrderBy(x => x.Name).ToList();

                if (reaction.Product.Name == "FUEL")
                    FUEL = reaction;
                else
                    Reactions.Add(reaction);
            }

            Reactions = Reactions.OrderBy(x => x.Product.Name).ToList();
        }

        public Tree CreateTree()
        {
            int depth = 0;

            TheTree.Root = new Node(FUEL.Product.Name, depth);

            Recurse(TheTree.Root, FUEL.Reagents, 0);

            return TheTree;
        }

        private void Recurse(Node root, List<Item> ingredients, int depth)
        {
            foreach ( var x in ingredients)
            {
                var y = new Node(x.Name, depth+1);
                var z = Reactions.Where(f => f.Product.Name == y.Name).FirstOrDefault();
                if ( z != null)
                {
                    Recurse(y, z.Reagents, depth+1);
                }
                root.Kids.Add(y);
            }
        }

        public Recipe ReplaceFuel()
        {
            List<int> removeList = new List<int>();
            List<Item> Spare = new List<Item>();

            int i = 0;
            while (i < FUEL.Reagents.Count)
            {
                Item item = FUEL.Reagents[i];

                var r = Reactions.Where(x => x.Product.Name == item.Name).FirstOrDefault();
                if (r != null)
                {
                    removeList.Add(i);
                    foreach (var w in r.Reagents)
                    {
                        Item a = new Item(w.Name, w.Qty)
                        {
                            Qty = w.Qty * ((item.Qty + r.Product.Qty - 1) / r.Product.Qty)
                        };
                        FUEL.Add(a);

                    }
                }
                i++;
                Display();
            }

            foreach (int j in removeList.OrderByDescending(x => x))
                FUEL.Reagents.RemoveAt(j);

            return this;
        }

        public void Accumulate()
        {
            Dictionary<string, int> Totals = new Dictionary<string, int>();

            foreach (var i in FUEL.Reagents)
            {
                if (Totals.ContainsKey(i.Name))
                    Totals[i.Name] += i.Qty;
                else
                    Totals.Add(i.Name, i.Qty);
            }

            int oreSum = 0;
            foreach (var i in Totals)
            {
                //TODO var theORE = OREs.Where(x => x.Target.Name == i.Key).FirstOrDefault();

                //TODO int needToProduce = theORE.Target.Qty;
                //TODO needToProduce = ((i.Value + needToProduce - 1) / needToProduce) * needToProduce;

                //TODO int oresRequired = theORE.Ingredients[0].Qty * needToProduce / theORE.Target.Qty;

                //TODO Console.WriteLine($"{i.Key,5} {i.Value}  {needToProduce} {oresRequired}");

                //TODO oreSum += oresRequired;
            }
            Console.WriteLine($"COUNT == {oreSum}");
        }

        public IEnumerable<Reaction> GetReaction()
        {
            foreach (var r in Reactions)
                yield return r;
        }

        //TODO public IEnumerable<Combination> GetORE()
        //TODO {
        //TODO foreach (Combination r in OREs)
        //TODO yield return r;
        //TODO }

        public Reaction GetFUEL()
        {
            return FUEL;
        }
        public override string ToString()
        {
            string retval = "";

            retval += FUEL.ToString() + "\n\n";

            foreach (var s in Reactions)
            {
                retval += s.ToString() + "\n";
            }
            retval += "\n";

            return retval;
        }

        public void Display()
        {
            Console.WriteLine("-----------------------------------------------------------------------");
            Console.WriteLine(ToString());
        }
    }
}
