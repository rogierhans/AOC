using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace AOC2
{
    class Day2018_12 : Day
    {


        public Day2018_12()
        {
            SL.printParse = true;

            string folder = @"C:\Users\Rogier\Desktop\AOC\";
            string name = "input.txt";
            string filename = folder + name;
            string filenameTest = folder + "test.txt";
            var testLines = File.ReadAllLines(filenameTest).ToList();
            var inputLines = File.ReadAllLines(filename).ToList();
            Console.WriteLine("test:");
            ModeSelector(testLines);
            Console.WriteLine("input:");
            ModeSelector(inputLines);
        }


        private void ModeSelector(List<string> Lines)
        {
            IndexForLoop(Lines);
            //ParseLines(Lines);
        }

        private void IndexForLoop(List<string> Lines)
        {
            var clustered = Lines.ClusterLines();
            var grid = new List<List<string>>() { Lines.ClusterLines().First().First().Replace("initial state: ", "").ToCharArray().Select(x => x.ToString()).ToList() };
            grid.Print("");
            var rest = clustered[1];
            var ruleText = rest.Select(x => Parser.Split(x, " => ")).ToList();
            ruleText.Print("=>");
            Dictionary<List<List<string>>, string> ruleDict = new Dictionary<List<List<string>>, string>();
            foreach (var rule in ruleText)
            {
                var pattern = rule.First().ToCharArray().Select(x => x.ToString()).ToList();
               // pattern.Print();
                //Console.WriteLine(rule[1]);
                ruleDict[new List<List<string>> { pattern }] = rule[1];
            }
            int offset = 0;
            for (int j = 0; j < 20; j++)
            {
                grid = GOL.Update(grid, ruleDict, ".",0,2, 0, 2);
                offset += 4;
                var number = grid.First().Select((s, i) => s == "#" ? i - offset :0).Sum(); 
                grid.Select(x => x.Skip(offset).ToList()).ToList().Print("");
                Console.WriteLine(number);
            }
           // Console.ReadLine();
        }

        private void GOLHistory()
        {
            Console.WriteLine("hoi");

            var grid = SL.MakeList(50, 50, ".");
            grid[2][2] = "O";
            grid[2][3] = "O";
            grid[2][4] = "O";
            grid[1][4] = "O";
            grid[0][3] = "O";
            grid[12][2] = "O";
            grid[12][3] = "O";
            grid[12][4] = "O";
            grid[11][4] = "O";
            grid[10][3] = "O";
            grid[10][4] = "O";

            grid.Print();
            while (true)
            {

                string Rule(string status, int count)
                {
                    if (count == 2 && status == "O") return "O";
                    else if (count == 3) return "O";
                    else return ".";
                }
                int neighborhoodLength = 3;
                int neighborhoodWidth = 3;
                grid = GOL.UpdateClassic(grid, neighborhoodLength, neighborhoodWidth, x => x == "O" ? 1 : 0, Rule);

            }
        }



        private void ParseLines(List<string> lines)
        {
            var clusterLine = lines.ClusterLines();
            var parsed = clusterLine;
            //var numbers = parsed.First().First().Split(',').Select(x => long.Parse(x)).ToList();
            var element = parsed.Select(line => new Element(line)).ToList();
            for (int i = 0; i < element.Count; i++)
            {


            }
        }

        class Element
        {
            // string key = "";
            //long ID;


            public Element(List<string> lines)
            {
                ///ParseSingle(lines.First());
                ParseMulti(lines);
            }
            private void ParseSingle(string line)
            {
                var sperator = ' ';
                var input = line.Split(sperator);
            }


            private void ParseMulti(List<string> lines)
            {
                SL.Line();
                for (int i = 1; i < lines.Count; i++)
                {
                    var line = lines[i];
                    SL.Log(line);

                    var sperator = ' ';

                    var input = line.Split(sperator).Select(x => long.Parse(x)).ToList();

                }
            }


        }

    }
}
