using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace AOC2
{
    class Day6 : Day
    {


        public Day6()
        {
            SL.printParse = false;

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
            int max = 9;
            long[] fishCount = new long[max];
            var numbers = Lines.First().Split(',').Select(x => long.Parse(x));
            foreach (var number in numbers)
            {
                fishCount[number]++;
            }
            for (int k = 0; k < 256; k++)
            {
                var numberOfSpawnFish = fishCount[0];
                for (int i = 1; i < max; i++)
                {
                    fishCount[i - 1] = fishCount[i];
                }
                fishCount[8] = numberOfSpawnFish;
                fishCount[6] += numberOfSpawnFish;
            }
            

        }

        private void SolveNice(List<string> Lines)
        {
            var numbers = Lines.First().Split(',').Select(x => long.Parse(x));
            long[] fishCount = new long[9];
            foreach (var number in numbers)
            {
                fishCount[number]++;
            }
            for (int k = 0; k < 256;)
            {
                fishCount[(7 + k) % 9] += fishCount[k % 9];
                k++;
            }
            Print(fishCount.Sum());
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
