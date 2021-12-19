using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2
{
    class Mem
    {


        public Dictionary<long, Dictionary<long, Dictionary<long, int>>> Dict = new Dictionary<long, Dictionary<long, Dictionary<long, int>>>();
        public void Add(long first, long second, long third, int value)
        {
            if (!Dict.ContainsKey(first))
            {
                Dict[first] = new Dictionary<long, Dictionary<long, int>>();
            }
            if (!Dict[first].ContainsKey(second))
            {
                Dict[first][second] = new Dictionary<long, int>();
            }
            if (!Dict[first][second].ContainsKey(third))
            {
                Dict[first][second][third] = 0;
            }
            Dict[first][second][third] += value;
        }

        public int GetValue(long first, long second, long third)
        {
            return Dict[first][second][third];
        }
    }
}
