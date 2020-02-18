﻿using System;
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
        public List<Node> Kids;

        public Node ( string name)
        {
            Name = name;
            Kids = new List<Node>();
        }
    }
}
