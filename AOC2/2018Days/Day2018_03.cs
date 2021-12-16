using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace AOC2
{
    class Day2018_03 : Day
    {


        public Day2018_03()
        {
            SL.printParse = false;
            GetInput(RootFolder + @"2018_03\");
        }

        public override void Main(List<string> Lines)
        {
            var names  = Lines.Select(x => x.Split('@').First()).ToList();
            var coords = Lines.Select(x => x.Split('@').Last().Split(':').First().Trim(1, 0).Split(',').Select(y => int.Parse(y)).ToList()).ToList();
            var areas = Lines.Select(x => x.Split(':').Last().Trim(1, 0).Split('x').Select(y => int.Parse(y)).ToList()).ToList();
            var xx = coords.Select(x => x[0]).ToList();
            var yy = coords.Select(x => x[1]).ToList();
            var width = areas.Select(x => x[0]).ToList();
            var height = areas.Select(x => x[1]).ToList();
            var grid = Grid.Make(xx.Max() + width.Max(), yy.Max() + height.Max(), 0);
            //coords.Print(",");
            // areas.Print(",");
            //  grid.Print("");
            for (int claims = 0; claims < height.Count; claims++)
            {
                // Printer.Log(xx[claims], yy[claims], width[claims], height[claims]);
                var neighbors = grid.NeighborList(xx[claims], yy[claims], 0, width[claims], 0, height[claims]);
                //Printer.Log(neighbors.Count);
                //neighbors.Print(" ");
                //grid.SubSelect(neighbors, x => 8).Print(" ");
                grid = grid.SubSelect(neighbors, x => x + 1);
                // grid.Print(" ");
                //Console.ReadLine();
            }

            for (int claims = 0; claims < height.Count; claims++)
            {
                var neighbors = grid.NeighborList(xx[claims], yy[claims], 0, width[claims], 0, height[claims]);
                bool notClaimed = true;
                foreach (var cors in neighbors)
                {
                    notClaimed &= grid[cors.Item1][cors.Item2] == 1;
                }
                if (notClaimed)
                    Console.WriteLine(names[claims]);
            }
            var total = grid.GridSelect(x => x > 1 ? 1 : 0).GridSum(x => x);
            Console.WriteLine(total);
            Console.ReadLine();
        }
    }
}
