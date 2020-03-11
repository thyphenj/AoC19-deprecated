using System;
using System.Collections.Generic;
using System.Text;

namespace _14_SpaceStoichiometr
{
    class Chemical
    {
        public string Name;
        public int Qty;

        public Chemical(string[] arr)
        {
            Name = arr[1];
            Qty = Convert.ToInt32(arr[0]);
        }
        public Chemical(string name, int qty)
        {
            Name = name;
            Qty = qty;
        }
        public override string ToString()
        {
            return $"{Qty,3}:{Name.PadRight(5)}";
        }
    }
}
