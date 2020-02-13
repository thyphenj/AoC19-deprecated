using System;
using System.Collections.Generic;
using System.Text;

namespace _14_SpaceStoichiometr
{
    class Item
    {
        public string Name;
        public int Qty;

        public Item(string[] arr)
        {
            Name = arr[1];
            Qty = Convert.ToInt32(arr[0]);
        }
        public Item(string name, int qty)
        {
            Name = name;
            Qty = qty;
        }
        public override string ToString()
        {
            return $"{Name,5}:{Qty,3}";
        }
    }
}
