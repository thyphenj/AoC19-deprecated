using System;
using System.Collections.Generic;
using System.Text;

namespace _14_SpaceStoichiometr
{
    class Tree
    {
        public Node Root;

        public Tree ()
        {
            Root = null;
        }
    }
    class Node
    {
        public string Name;
        public int Depth;
        public List<Node> Kids;

        public Node ( string name, int depth)
        {
            Name = name;
            Depth = depth;
            Kids = new List<Node>();
        }
        public override string ToString()
        {
            return $"{Name} has {Kids.Count} Kids";
        }
    }
}
