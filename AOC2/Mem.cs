using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2
{
    class Mem<Argument, Result>
    {

        private Dictionary<Argument, Dictionary<Argument, Result>> memory2 = new Dictionary<Argument, Dictionary<Argument, Result>>();
        private Dictionary<Argument, Dictionary<Argument, Dictionary<Argument, Result>>> memory3 = new Dictionary<Argument, Dictionary<Argument, Dictionary<Argument, Result>>>();


        private Dictionary<Argument, Result> memory1 = new Dictionary<Argument, Result>();
        public Result F(Func<Argument, Result> func, Argument first)
        {
            if (!memory1.ContainsKey(first))
            {
                memory1[first] = func(first);
            }
            return memory1[first];
        }
        public Result F(Func<Argument, Argument, Result> func, Argument first, Argument second)
        {
            if (!memory2.ContainsKey(first))
            {
                memory2[first] = new Dictionary<Argument, Result>();
            }
            if (!memory2[first].ContainsKey(second))
            {
                memory2[first][second] = func(first, second);
            }
            return memory2[first][second];
        }
        public Result F(Func<Argument, Argument, Argument, Result> func, Argument first, Argument second, Argument third)
        {
            if (!memory3.ContainsKey(first))
            {
                memory3[first] = new Dictionary<Argument, Dictionary<Argument, Result>>();
            }
            if (!memory3[first].ContainsKey(second))
            {
                memory3[first][second] = new Dictionary<Argument, Result>();
            }
            if (!memory3[first][second].ContainsKey(third))
            {
                memory3[first][second][third] = func(first, second, third);
            }
            return memory3[first][second][third];
        }
    }
}
