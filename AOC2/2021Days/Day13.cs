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
            var coords = Lines.FindPatterns("{0},{1}", int.Parse, int.Parse);
            var folds = Lines.FindPatterns("fold along {0}={1}", x => x =="x", int.Parse);
            foreach (var (hFlip, index) in folds)
            {
                var newCoords = new List<(int, int)>();
                foreach (var (x, y) in coords)
                {
                    int newX = hFlip && x > index ? index - (x - index) : x;
                    int newY = !hFlip && y > index ? index - (y - index) : y;
                    newCoords.Add((newX, newY));
                }
                coords = newCoords;
            }
            Print(coords);
            Console.ReadLine();
        }

        private static void Print(List<(int, int)> coords)
        {
            var newGrid = Grid.Make(coords.Max(x => x.Item2) + 1, coords.Max(x => x.Item1) + 1, 0);
            foreach (var (x, y) in coords)
            {
                newGrid[y][x] = 1;
            }
            newGrid.GridSelect(x => x > 0 ? BLOCK : " ").Print();
        }
    }

}

