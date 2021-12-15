using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AOC2
{
    class Day2018_05 : Day
    {


        public Day2018_05()
        {
            SL.printParse = false;
            GetInput(RootFolder + @"2018_05\");
        }
        public const string BLOCK = "\U00002588";
        public override void Main(List<string> Lines)
        {
            var OGLine = Lines.First();
            var lower = "abcdefghijklmnopqrstuvwxyz".List();
            var upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".List();
            var Combination2 = lower.Zip(upper, (x, y) => x + y);
            var Combination1 = upper.Zip(lower, (x, y) => x + y);
            var Combination3 = upper.Zip(lower, (x, y) => (x , y));
            Combination1.ToList().Print(" ");
            Combination2.ToList().Print(" ");
            Combination3.ToList().Print(" ");

            int newCount = -1;
            foreach (var co in Combination3) {
                var line = OGLine.Replace(co.Item1, "");
                line = line.Replace(co.Item2, "");
                int count = line.Length;
                while (count > newCount) {

                    count = line.Length;
                    foreach (var combi in Combination1) {
                        line = line.Replace(combi, "");
                    }
                    foreach (var combi in Combination2) {
                        line = line.Replace(combi, "");
                    }
                    newCount = line.Length;
                }
                Console.WriteLine(co + " "+ count);
            }
          //  Console.WriteLine(line);
            Console.ReadLine();
        }
    }
}