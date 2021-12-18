using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishLibrary
{
    public class Fish
    {
        public Fish Parent;
        public Fish left;
        public Fish right;
        public int depth;
        public long Number;
        public bool isLeaf = false;

        public Fish(long value)
        {
            Number = value;
            isLeaf = true;
        }
        public Fish(Fish left, Fish right)
        {
            this.left = left;
            this.right = right;
        }


       


        public override string ToString()
        {
            if (isLeaf)
                return Number.ToString();
            else
                return "(" + left + "," + right + ")";
        }


    }
}
