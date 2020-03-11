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
                    reaction.Add(new Chemical(s.Split(" ")));
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

            TheTree.Root = new Node(FUEL.Product.Name, depth,FUEL.Product.Qty);

            CreateSubTree(TheTree.Root, FUEL.Reagents, 0);

            return TheTree;
        }

        private void CreateSubTree(Node root, List<Chemical> ingredients, int depth)
        {
            foreach ( var x in ingredients)
            {
                var y = new Node(x.Name, depth+1,x.Qty);
                var z = Reactions.Where(f => f.Product.Name == y.Name).FirstOrDefault();
                if ( z != null)
                {
                    CreateSubTree(y, z.Reagents, depth+1);
                }
                root.Kids.Add(y);
            }
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
