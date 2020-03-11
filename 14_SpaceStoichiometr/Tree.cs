using System;
using System.Collections.Generic;
using System.Text;

namespace _14_SpaceStoichiometr
{
    class Tree
    {
        public Node Root;

        public Tree()
        {
            Root = null;
        }

        public static void PrintTree(Node from)
        {
            Console.Write($"[{from.Required,3} {from.Name,5}] ");
            if (from.Kids.Count > 0)
            {
                PrintTree(from.Kids[0]);
                for (int i = 1; i < from.Kids.Count; i++)
                {
                    Console.WriteLine();
                    for (int j = 0; j <= from.Depth; j++)
                        Console.Write("----------> ");
                    PrintTree(from.Kids[i]);
                }
            }
        }
        public static void PopulateRequired(Node from)
        {
            foreach (var kid in from.Kids)
            {
                //                reagent = 
            }
        }
    }
    class Node
    {
        public string Name;
        public int Depth;
        public List<Node> Kids;
        public int Required;

        public Node(string name, int depth, int required)
        {
            Name = name;
            Depth = depth;
            Kids = new List<Node>();
            Required = required;
        }
        public override string ToString()
        {
            return $"{Name} has {Kids.Count} Kids";
        }
    }
}
