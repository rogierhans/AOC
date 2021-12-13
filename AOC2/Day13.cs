using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AOC2
{
    class Day13 : Day
    {


        public Day13()
        {
            SL.printParse = false;
            GetInput(RootFolder + @"2021_13\");
        }
        public const string BLOCK = "\U00002588";
        public override void Main(List<string> Lines)
        {
            var cluster = Lines.ClusterLines();
            var coords = cluster.First().Select(x => x.Split(',').Select(y => int.Parse(y)).ToList()).ToList();
            var folds = ParseFolder(cluster[1]);
            var grid = SL.MakeList(coords.Max(x => x[1]) + 1, coords.Max(x => x[0]) + 1, 0);
            foreach (var cord in coords)
            {
                grid[cord[1]][cord[0]] = 1;
            }
            foreach (var (hFlip,index) in folds)
            {
                if (hFlip)
                {
                    var rgrid = grid.ReverseH();
                    grid = grid.ZipGrid(rgrid, (a, b) => a + b);
                    grid = grid.Select(x => x.Take(index).ToList()).ToList();
                }
                if (!hFlip)
                {
                    var rgrid = grid.ReverseV();
                    grid = grid.ZipGrid(rgrid, (a, b) => a + b);
                    grid = grid.Take(index).ToList();
                }
            }
            grid.GridSelect(x => x > 0 ? BLOCK : " ").Print();

            Console.WriteLine(grid.GridSelect(x => x > 0 ? 1 : 0).GridSum(x => x));
        }

        private List<(bool, int)> ParseFolder(List<string> lines)
        {
            var list = new List<(bool, int)>();
            foreach (var line in lines)
            {
                var input = line.Trim("fold along ".Length, 0).Split('=');
                list.Add((input[0] == "x", int.Parse(input[1])));
            }
            return list;
        }


    }

}

