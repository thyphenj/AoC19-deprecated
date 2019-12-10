using System;
using System.IO;
using System.Collections.Generic;

namespace _06_UniversalOrbitMap
{
    class Program
    {
        static List<Orig> theData = new List<Orig>();
        static TreeNode<Orig> SAN, YOU;
        static int distance = 0;

        static void Main()
        {
            string[] tester = { "COM)B", "B)C", "C)D", "D)E", "E)F", "B)G", "G)H", "D)I", "E)J", "J)K", "K)L", "K)YOU", "I)SAN" };
            //--- temp data structure
            foreach (string line in File.ReadLines(@"data.txt"))
            //  foreach (string line in tester)
            {
                theData.Add(new Orig(line));
            }

            //--- create the root
            TreeNode<Orig> COM = new TreeNode<Orig>(new Orig("root)COM"));

            //--- now add the tree recursively
            addSubtree(COM);

            Console.WriteLine(distance);

            var san = SAN;
            var you = YOU;
            while (san.Level > you.Level)
                san = san.Parent;
            while (you.Level > san.Level)
                you = you.Parent;
            while (you.Data.name != san.Data.name)
            {
                you = you.Parent;
                san = san.Parent;
            }
            distance = (YOU.Level - you.Level) + (SAN.Level - san.Level) - 2;
            Console.WriteLine(distance);
        }
        static void addSubtree(TreeNode<Orig> currentNode)
        {
            var name = currentNode.Data.moon;
            foreach (var xx in theData.FindAll(x => x.root == name))
            {
                TreeNode<Orig> node = currentNode.AddChild(xx);
                distance += node.Level;
                addSubtree(node);
                if (node.Data.name == "SAN")
                    SAN = node;
                if (node.Data.name == "YOU")
                    YOU = node;
            }
        }
    }
}
