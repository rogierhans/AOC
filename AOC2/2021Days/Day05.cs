using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace AOC2
{
    class Day5 : Day
    {


        public Day5()
        {
            SL.printParse = false;

            string folder = @"C:\Users\Rogier\Desktop\AOC\";
            string name = "input.txt";
            string filename = folder + name;
            string filenameTest = folder + "test.txt";
            var testLines = File.ReadAllLines(filenameTest).ToList();
            var inputLines = File.ReadAllLines(filename).ToList();
            Console.WriteLine("test:");
            ModeSelector(testLines, 10);
            Console.WriteLine("input:");
            ModeSelector(inputLines, 1000);
        }


        private void ModeSelector(List<string> Lines, int gridsize)
        {
            IndexForLoop(Lines, gridsize);
            //ParseLines(Lines);
        }


        private void IndexForLoop(List<string> Lines, int gridsize)
        {
            int[,] array = new int[gridsize, gridsize];
            for (int t = 0; t < Lines.Count; t++)
            {

                var line = Lines[t];
                var input = line.Replace(" -> ", ",").Split(',');
                int x1 = int.Parse(input[0]);
                int y1 = int.Parse(input[1]);
                int x2 = int.Parse(input[2]);
                int y2 = int.Parse(input[3]);


                int minx = Math.Min(x1, x2);
                int miny = Math.Min(y1, y2);

                int maxX = Math.Max(x1, x2);
                int maxY = Math.Max(y1, y2);
                if (x1 == x2 || y1 == y2)
                {
                    for (int x = minx; x <= maxX; x++)
                    {
                        for (int y = miny; y <= maxY; y++)
                        {
                            array[x, y]++;
                        }
                    }
                }
                else
                {
                    if (x1 < x2 && y1 < y2)
                    {
                        for (int i = 0; i <= x2 - x1; i++)
                        {
                            array[x1 + i, y1 + i]++;
                        }
                    }
                    else if (x1 < x2 && y1 > y2)
                    {
                        for (int i = 0; i <= x2 - x1; i++)
                        {
                            array[x1 + i, y1 - i]++;
                        }
                    }
                    else if (x1 > x2 && y1 < y2)
                    {
                        for (int i = 0; i <= x1 - x2; i++)
                        {
                            array[x1 - i, y1 + i]++;
                        }
                    }
                    else if (x1 > x2 && y1 > y2)
                    {
                        for (int i = 0; i <= x1 - x2; i++)
                        {
                            array[x1 - i, y1 - i]++;
                        }
                    }
                    else {
                        throw new Exception();
                    }

                }

                for (int x = 0; x < 10; x++)
                {
                    string strLine = "";
                    for (int y = 0; y < 10; y++)
                    {
                        strLine += array[x, y];
                    }
                    //Console.WriteLine(strLine);
                }
              //  Console.WriteLine("{0} {1} {2} {3}", x1, x2, y1, y2);
                //Console.ReadLine();
                //  
                //var key = input[0];
                //var value = input[1];
                // 

            }

            for (int x = 0; x < 10; x++)
            {
                string strLine = "";
                for (int y = 0; y < 10; y++)
                {
                    strLine += array[x, y];
                }
                Console.WriteLine(strLine);
            }
            int count = 0;
            for (int x = 0; x < gridsize; x++)
            {
                for (int y = 0; y < gridsize; y++)
                {
                    if (array[x, y] >= 2) count++;
                }

            }
            Print(count);
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
