using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace AOC2
{
    class Program
    {
        [STAThreadAttribute]
        static void Main(string[] args)
        {
            var sw = new Stopwatch();
            sw.Start();
            new Day2018_05();
            //new Day13();
            Console.WriteLine(sw.Elapsed.TotalMilliseconds);

        }
    }
}
