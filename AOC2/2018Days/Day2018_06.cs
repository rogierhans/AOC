using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AOC2
{
    class Day2018_06 : Day
    {


        public Day2018_06()
        {
            SL.printParse = false;
            GetInput(RootFolder + @"2018_06\");
        }
        public const string BLOCK = "\U00002588";
        public override void Main(List<string> Lines)
        {
            var coords = Lines.FindPatterns("{0}, {1}", int.Parse, int.Parse);
            coords.Print();
            var grid = Grid.Make(coords.Max(x => x.Item1) + 1, coords.Max(x => x.Item2) + 1, "A");
            int count = 0;
            for (int i = 0; i < grid.Count; i++)
            {
                for (int j = 0; j < grid[0].Count; j++)
                {
                    long totalDistance = 0;
                    for (int c = 0; c < coords.Count; c++)
                    {
                        var (x, y) = coords[c];
                        int distance = Math.Abs(x - i) + Math.Abs(y - j);
                        totalDistance += distance;
                    }
                    if (totalDistance < 10000) count++;
                }
            }
            Console.WriteLine(count);
            Console.ReadLine();
        }
    }
}