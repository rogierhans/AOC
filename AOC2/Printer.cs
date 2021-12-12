using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2
{
    static class  Printer
    {
        public static string Flat(this List<string> s, string seperator = "") {
            return string.Join(seperator, s);
        }
        public static void Log(params object[] values) {
            Console.WriteLine(string.Join(" ", values));

        }
    }
}
