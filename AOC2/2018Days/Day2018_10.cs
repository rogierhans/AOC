using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AOC2
{
    class Day2018_10 : Day
    {


        public Day2018_10()
        {
            SL.printParse = false;
            GetInput(RootFolder + @"2018_10\");
        }
        public const string BLOCK = "\U00002588";
        public override void Main(List<string> Lines)
        {
            var birds = Lines.FindPatterns("position=<{0},{1}> velocity=<{2},{3}>", int.Parse, int.Parse, int.Parse, int.Parse);
            var velocity = new List<(int, int)>();
            var coords = new List<(int, int)>();
            foreach (var (x, y, dx, dy) in birds)
            {
                velocity.Add((dx, dy));
                coords.Add((x, y));
            }
            for (int i = 0; ; i++)
            {
                for (int c = 0; c < coords.Count; c++)
                {
                    coords[c] = (coords[c].Item1 + velocity[c].Item1, (coords[c].Item2 + velocity[c].Item2));
                }
                Console.WriteLine(i);
                Print(coords);

               // Console.ReadLine();
            }
            
        }

        private static void Print(List<(int, int)> coords)
        {
            int maxX = coords.Max(x => x.Item1);
            int maxY = coords.Max(x => x.Item2);
            int minX = coords.Min(x => x.Item1);
            int minY = coords.Min(x => x.Item2);
            int height = maxY - minY +1;
            int width = maxX - minX +1;
            Printer.Log(height, width);
             if (height < 200 && width < 200)
            {
                {
                    var newGrid = Grid.Make(height, width, 0);
                    foreach (var (x, y) in coords)
                    {
                        newGrid[y - minY][x - minX] = 1;
                    }
                    newGrid.GridSelect(x => x > 0 ? BLOCK : " ").Print();
                }
                Console.ReadLine();
            }

        }
    }
}