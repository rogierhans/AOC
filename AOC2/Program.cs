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
            while (true)
            {
                var sw = new Stopwatch();
                sw.Start();
                new Day12DFS();
                Console.WriteLine("ms: "+sw.Elapsed.TotalMilliseconds);
                Console.ReadLine();
            }
        }
    }
}
