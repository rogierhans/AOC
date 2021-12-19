//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.IO;
//using System.Windows.Forms;
//using System.Diagnostics;
//using Gurobi;
//namespace AOC2
//{
//    class Day7 : Day
//    {


//        public Day7()
//        {
//            SL.printParse = false;

//            string folder = @"C:\Users\Rogier\Desktop\AOC\";
//            string name = "input.txt";
//            string filename = folder + name;
//            string filenameTest = folder + "test.txt";
//            var testLines = File.ReadAllLines(filenameTest).ToList();
//            var inputLines = File.ReadAllLines(filename).ToList();
//            Console.WriteLine("test:");
//            ModeSelector(testLines);
//            Console.WriteLine("input:");
//            ModeSelector(inputLines);
//        }


//        private void ModeSelector(List<string> Lines)
//        {
//            // GurobiBloat(Lines);
//            Stopwatch sw = new Stopwatch();
//            sw.Start();
//            IndexForLoop(Lines);
//            sw.Stop();
//            Console.WriteLine(sw.ElapsedMilliseconds);
//            //ParseLines(Lines);
//        }


//        private void IndexForLoop(List<string> Lines)
//        {
//            var numbers = Lines.First().Split(',').Select(x => long.Parse(x)).ToList();
//            var mem = new Mem<long, long>();
//            var bestFuel = SL.GetNumbers(numbers.Min(), numbers.Max()).Min(midpoint => numbers.Sum(x => mem.F(distance => SL.GetNumbers(0, distance).Sum() + distance, Math.Abs(midpoint - x))));
//            Console.WriteLine(bestFuel);
//            // Console.ReadLine();
//        }

//        private void GurobiBloat(List<string> Lines)
//        {
//            var env = new GRBEnv();
//            var model = new GRBModel(env);
//            var objective = new GRBQuadExpr();

//            var numbers = Lines.First().Split(',').Select(x => long.Parse(x)).ToList();
//            objective -= numbers.Count();
//            var bestPosition = model.AddVar(numbers.Min(), numbers.Max(), 0.0, GRB.INTEGER, "");
//            var absCost = numbers.Select(x => model.AddVar(0, numbers.Max(), 0.0, GRB.INTEGER, "")).ToList();

//            for (int i = 0; i < numbers.Count; i++)
//            {
//                model.AddConstr(absCost[i] >= numbers[i] - bestPosition, "");
//                model.AddConstr(absCost[i] >= bestPosition - numbers[i], "");
//                objective.Add(absCost[i] / 2);
//            }
//            numbers.ForEach(number => objective.Add(((number - bestPosition) * (number - bestPosition)) * 0.5));
//            model.SetObjective(objective);
//            model.Optimize();
//            Console.WriteLine(bestPosition.X);
//        }


//        private void ParseLines(List<string> lines)
//        {
//            var clusterLine = lines.ClusterLines();
//            var parsed = clusterLine;
//            //var numbers = parsed.First().First().Split(',').Select(x => long.Parse(x)).ToList();
//            var element = parsed.Select(line => new Element(line)).ToList();
//            for (int i = 0; i < element.Count; i++)
//            {




//            }
//        }

//        class Element
//        {
//            // string key = "";
//            //long ID;


//            public Element(List<string> lines)
//            {
//                ///ParseSingle(lines.First());
//                ParseMulti(lines);
//            }
//            private void ParseSingle(string line)
//            {
//                var sperator = ' ';
//                var input = line.Split(sperator);
//            }
//            private void ParseMulti(List<string> lines)
//            {
//                SL.Line();

//                for (int i = 0; i < lines.Count; i++)
//                {
//                    var line = lines[i];
//                    SL.Log(line);

//                    var sperator = ' ';

//                    var input = line.Split(sperator).Select(x => long.Parse(x)).ToList();

//                }
//            }


//        }

//    }
//}
