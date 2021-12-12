using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace AOC2
{
    class Day0 : Day
    {


        public Day0()
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
            // IndexForLoop(Lines);
            //ParseLines(Lines);
        }

        private void IndexForLoop(List<string> Lines)
        {
            for (int t = 0; t < Lines.Count; t++)
            {
                var line = Lines[t];
                var input = line.Split(' ');
                var key = input[0];
                //var value = long.Parse(input[1]);
                //var numbers = input.Skip(0).Select(x => long.Parse(x)).ToList();
            }

            Print(" ");
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
                for (int i = 0; i < lines.Count; i++)
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
