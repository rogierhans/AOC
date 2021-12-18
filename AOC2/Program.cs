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
            //var line = File.ReadAllText(@"C:\Users\Rogier\Desktop\JSON.txt");
            //var xD = line.Pattern("{\"event\":\"2021\",\"owner_id\":\"189709\",\"members\":{{0}}" + "}{\"event\":\"2021\"{1}", x =>x ,x=>x);
            ////var first = xD.Item1.Pattern("{0}:{{1}}{2}",int.Parse,x=>x,x=>x);
            //var first = xD.Item1.Pattern("\"{0}\":{{1}},{2}", int.Parse, x => x,x=> x);
            //Console.WriteLine(first.Item1);
            //Console.WriteLine(first.Item2);
            //Console.WriteLine(first.Item3);
            //return;

            while (true)
            {
                var sw = new Stopwatch();
                sw.Start();
                new Day18();
                Console.WriteLine(sw.Elapsed.TotalMilliseconds);
            }

        }
    }
}
