using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

namespace AOC2
{
    class Day12BFS : Day
    {


        public Day12BFS()
        {
            SL.printParse = false;
            GetInput(RootFolder + @"");
        }

        public override void Main(List<string> Lines)
        {

            (var Caves, var SmallCaves, var BigCaves, var ValidTrans) = GetCaves(Lines);
            List<List<int>> NumberOfPaths = new List<List<int>>();
            for (int i = 0; i < SmallCaves.Count; i++)
            {
                var list = new List<int>();
                for (int j = 0; j < SmallCaves.Count; j++)
                {
                    list.Add(CountPaths(SmallCaves[i], SmallCaves[j], ValidTrans, Caves.ToList(), BigCaves.ToList()));
                }
                NumberOfPaths.Add(list);
            }
            Console.WriteLine(SmallCaves.Count);
            var sw = new Stopwatch();
            sw.Start();
            BFS(NumberOfPaths, SmallCaves);
            //  DFS(smallTrans, SmallCaves);
            Console.WriteLine(sw.Elapsed.TotalMilliseconds);
            // Console.ReadLine();
        }

        private void BFS(List<List<int>> NumberOfPaths, List<string> Caves)
        {
            Queue<(List<int>, int, bool)> queue = new Queue<(List<int>, int, bool)>();
            int startIndex = Caves.IndexOf("start");
            int endIndex = Caves.IndexOf("end");

            queue.Enqueue((new List<int>() { startIndex }, 1, false));
            int count = 0;
            int iterationCounter = 0; // voor debug
            while (queue.Count > 0)
            {
                iterationCounter++;
                (List<int> visted, int currentPaths, bool twice) = queue.Dequeue();
                int caveFrom = visted.Last();
                for (int caveTo = 0; caveTo < NumberOfPaths.Count; caveTo++)
                {
                    if (NumberOfPaths[caveFrom][caveTo] == 0 || caveTo == startIndex) continue;
                    if (caveTo == endIndex && NumberOfPaths[caveFrom][caveTo] > 0)
                    {
                        count += currentPaths;
                    }
                    else if (!visted.Contains(caveTo))
                    {
                       // new List<int>(visted) { caveTo }.Print(" "); // voor debug
                        queue.Enqueue((new List<int>(visted) { caveTo }, currentPaths * NumberOfPaths[caveFrom][caveTo], twice));
                    }
                    else
                    if (!twice && visted.Contains(caveTo))
                    {
                        queue.Enqueue((new List<int>(visted) { caveTo }, currentPaths * NumberOfPaths[caveFrom][caveTo], true));
                    }
                }

            }
            Console.WriteLine(iterationCounter);
            Console.WriteLine(count);
        }



        private static (List<string>, List<string>, List<string>, Dictionary<string, List<string>>) GetCaves(List<string> Lines)
        {
            Dictionary<string, List<string>> ValidTrans = new Dictionary<string, List<string>>();
            HashSet<string> Caves = new HashSet<string>();
            HashSet<string> SmallCaves = new HashSet<string>();
            HashSet<string> BigCaves = new HashSet<string>();
            foreach (var trans in Lines.Select(x => x.Split('-')))
            {
                void Add(string from, string to)
                {
                    if (!ValidTrans.ContainsKey(from)) ValidTrans[from] = new List<string>();
                    ValidTrans[from].Add(to);
                }
                Caves.Add(trans[0]);
                Caves.Add(trans[1]);
                Add(trans[0], trans[1]);
                Add(trans[1], trans[0]);
            }
            foreach (var cave in Caves)
            {
                if (char.IsUpper(cave.ToCharArray()[0]))
                {
                    BigCaves.Add(cave);
                }
                if (char.IsLower(cave.ToCharArray()[0]))
                {
                    SmallCaves.Add(cave);
                }
            }
            return (Caves.ToList(), SmallCaves.ToList(), BigCaves.ToList(), ValidTrans);
        }
        private int CountPaths(string start, string end, Dictionary<string, List<string>> ValidTrans, List<string> caves, List<string> BigCaves)
        {

            Queue<string> q = new Queue<string>();
            HashSet<string> visted = new HashSet<string>();
            q.Enqueue(start);
            int count = 0;
            while (q.Count > 0)
            {
                var current = q.Dequeue();
                foreach (var next in caves.Where(next => ValidTrans.ContainsKey(current) && ValidTrans[current].Contains(next) && !visted.Contains(next)))
                {
                    if (next == end)
                    {
                        count++;
                    }
                    else if (BigCaves.Contains(next))
                    {
                        visted.Add(next);
                        q.Enqueue(next);
                    }
                }
            }
            return count;
        }





    }
}
