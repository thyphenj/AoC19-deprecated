using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace _14_SpaceStoichiometr
{
    class Program
    {
        static void Main(string[] args)
        {
            Part1();
        }

        static void Part1()
        {
            string programText;
            //programText = File.ReadAllText(@"Text\data.txt");
            //programText = $"10 ORE => 10 A\n1 ORE => 1 B\n7 A, 1 B => 1 C\n7 A, 1 C => 1 D\n7 A, 1 D => 1 E\n7 A, 1 E => 1 FUEL";
            //programText = $"9 ORE => 2 A\n8 ORE => 3 B\n7 ORE => 5 C\n3 A, 4 B => 1 AB\n5 B, 7 C => 1 BC\n4 C, 1 A => 1 CA\n2 AB, 3 BC, 4 CA => 1 FUEL";
            programText = "157 ORE => 5 NZVS\n165 ORE => 6 DCFZ\n44 XJWVT, 5 KHKGT, 1 QDVJ, 29 NZVS, 9 GPVTF, 48 HKGWZ => 1 FUEL\n12 HKGWZ, 1 GPVTF, 8 PSHF => 9 QDVJ\n179 ORE => 7 PSHF\n177 ORE => 5 HKGWZ\n7 DCFZ, 7 PSHF => 2 XJWVT\n165 ORE => 2 GPVTF\n3 DCFZ, 7 NZVS, 5 HKGWZ, 10 PSHF => 8 KHKGT";
            string[] lines = programText.Replace("\r","").Split('\n');

            List<Recipe> recipes = new List<Recipe>();

            foreach (var x in lines)
            {
                Recipe recipe =  new Recipe(x.Split(" => ")[1].Split(" "));

                foreach (string s in x.Split(" => ")[0].Split(", "))
                {
                    recipe.Add(new Item(s.Split(" ")));
                }

                recipes.Add(recipe);
            }
            foreach (var s in recipes.Where(x => x.Target.Name == "FUEL"))
            {
                Console.WriteLine(s.ToString());
            }
            Console.WriteLine();
            foreach (var s in recipes.Where(x => x.Target.Name != "FUEL" && x.Ingredients.FirstOrDefault().Name != "ORE").OrderBy(x=>x.Target.Name))
            {
                Console.WriteLine(s.ToString());
            }
            Console.WriteLine();
            foreach (var s in recipes.Where(x => x.Ingredients.FirstOrDefault().Name == "ORE"))
            {
                Console.WriteLine(s.ToString());
            }
        }
    }
    class Item
    {
        public string Name;
        public int Qty;

        public Item ( string[] arr)
        {
            Name = arr[1];
            Qty = Convert.ToInt32(arr[0]);
        }
        public Item (string name, int qty)
        {
            Name = name;
            Qty = qty;
        }
        public override string ToString()
        {
            return $"{Name,5}:{Qty,3}";
        }
    }
    class Recipe
    {
        public Item Target;
        public List<Item> Ingredients;

        public Recipe (string[] strs)
        {
            Target = new Item(strs);
            Ingredients = new List<Item>();
        }

        public void Add ( Item item)
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
