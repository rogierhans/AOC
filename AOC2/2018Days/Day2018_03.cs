using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace AOC2
{
    class Day2018_3 : Day
    {


        public Day2018_3()
        {
            SL.printParse = false;
            GetInput(RootFolder + @"");
        }

        public override void Main(List<string> Lines)
        {
            var coords = Lines.Select(x => x.Split('@').Last().Split(':').First().Trim(1, 0).Split(',').Select(y => int.Parse(y)).ToList()).ToList();
            var areas = Lines.Select(x => x.Split(':').Last().Trim(1, 0).Split('x').Select(y => int.Parse(y)).ToList()).ToList();
            var xx = coords.Select(x => x[0]).ToList();
            var yy = coords.Select(x => x[1]).ToList();
            var width = areas.Select(x => x[0]).ToList();
            var height = areas.Select(x => x[1]).ToList();
            var grid = SL.MakeList(width.Max(),height.Max(), 0);
            //coords.Print(",");
            areas.Print(",");
        }
    }
}
