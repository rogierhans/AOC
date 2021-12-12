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
            for (int i = 0; i < 100000; i++)
            {

                new Day12DFS();

            }
            Console.WriteLine("ms: " + sw.Elapsed.TotalMilliseconds);
            Console.ReadLine();
        }
    }
}
