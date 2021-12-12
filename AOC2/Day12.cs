using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace AOC2
{
    class Day12 : Day
    {
        // First attempt Day12,  it takes a looooooooooong time

        public Day12()
        {
            SL.printParse = false;
            GetInput(RootFolder + @"");
        }

        public override void Main(List<string> Lines)
        {
            // Lines.Print("\n");
            HashSet<string> ValidTrans = new HashSet<string>();
            var Transtions = Lines.Select(x => x.Split('-')).ToList();
            foreach (var trans in Transtions)
            {
                var from = trans[0];
                var to = trans[1];
                ValidTrans.Add(from + "_" + to);
                ValidTrans.Add(to + "_" + from);
            }
            var Froms = Transtions.Select(x => x[0]).ToList();
            var Tos = Transtions.Select(x => x[1]).ToList();

            HashSet<string> SmallCaves = new HashSet<string>();
            HashSet<string> BigCaves = new HashSet<string>();

            GetBigAndSmall(Froms, Tos, SmallCaves, BigCaves);
            //  BigCaves.ToList().Print(" ");
            //  SmallCaves.ToList().Print(" ");
            List<string> Caves = new List<string>();
            Caves.AddRange(BigCaves);
            Caves.AddRange(SmallCaves);
            Caves.Add("end");
            //    GetPermutations(SmallCaves.ToList()).Print(" ");
            // Console.ReadLine();
            // .Print(" ");

            long total = CountTransitions("start", "end", ValidTrans, Caves, BigCaves.ToList());
            var perms = GetPermutations(SmallCaves.ToList());
            for (int p = 0; p < perms.Count; p++)
            {
                if (p % 1000 == 0) {
                    Console.WriteLine("{0} of {1}", p, perms.Count);
                }

                var perm = perms[p];
                long count = 1;
                //  perm.Print();
                count *= CountTransitions("start", perm.First(), ValidTrans, Caves, BigCaves.ToList());
                for (int i = 0; i < perm.Count - 1; i++)
                {
                    count *= CountTransitions(perm[i], perm[i + 1], ValidTrans, Caves, BigCaves.ToList());
                }
                count *= CountTransitions(perm.Last(), "end", ValidTrans, Caves, BigCaves.ToList());
                //  Console.WriteLine(perm.Flat() + " " + count);
                total += count;
                //  
            }
            Console.WriteLine(total);
            Console.ReadLine();
        }

        private void GetBigAndSmall(List<string> Froms, List<string> Tos, HashSet<string> SmallCaves, HashSet<string> BigCaves)
        {
            foreach (var cave in Froms)
            {
                if ((cave != "start" && cave != "end") && char.IsUpper(cave.ToCharArray()[0]))
                {
                    BigCaves.Add(cave);
                }
                if ((cave != "start" && cave != "end") && char.IsLower(cave.ToCharArray()[0]))
                {
                    SmallCaves.Add(cave);
                }
            }
            foreach (var cave in Tos)
            {
                if ((cave != "start" && cave != "end") && char.IsUpper(cave.ToCharArray()[0]))
                {
                    BigCaves.Add(cave);
                }
                if ((cave != "start" && cave != "end") && char.IsLower(cave.ToCharArray()[0]))
                {
                    SmallCaves.Add(cave);
                }
            }

        }

        private long CountTransitions(string start, string end, HashSet<string> ValidTrans, List<string> caves, List<string> BigCaves)
        {

            Queue<string> q = new Queue<string>();
            HashSet<string> visted = new HashSet<string>();
            q.Enqueue(start);
            int count = 0;
            while (q.Count > 0)
            {
                var current = q.Dequeue();
                //  Console.WriteLine("current {0}", current);

                foreach (var next in caves.Where(next => ValidTrans.Contains(current + "_" + next) && !visted.Contains(next)))
                {
                    if (next == end)
                    {
                        count++;
                    }
                    if (BigCaves.Contains(next))
                    {
                        visted.Add(next);
                        q.Enqueue(next);
                    }
                }

            }
            return count;
        }


        List<List<string>> GetPermutations(List<string> list)
        {
            HashSet<string> set = new HashSet<string>();
            var allPerms = new List<List<string>>();
            for (int i = 1; i <= list.Count; i++)
            {
                foreach (var perm in GetPermutations(list, i))
                {
                    string flat = perm.Flat();
                    if (!set.Contains(flat))
                    {
                        set.Add(flat);
                        allPerms.Add(perm);
                    }
                }
            }
            for (int i = 0; i < list.Count; i++)
            {
                List<string> newList = new List<string>(list);
                newList.Add(list[i]);
                // Console.WriteLine(newList.Flat());
                for (int j = 1; j <= newList.Count; j++)
                {
                    foreach (var perm in GetPermutations(newList, j))
                    {
                        string flat = perm.Flat();
                        if (!set.Contains(flat))
                        {
                            set.Add(flat);
                            allPerms.Add(perm);
                        }
                    }
                }
            }
            return allPerms;
        }

        List<List<string>> GetPermutations(List<string> list, int length)
        {
            if (length == 1) return list.Select(t => new List<string> { t }).ToList();
            else
            {
                var allPerms = new List<List<string>>();
                for (int i = 0; i < list.Count; i++)
                {
                    var filter = new List<string>(list);
                    filter.RemoveAt(i);
                    var perms = GetPermutations(filter, length - 1).DeepCopy();
                    perms.ForEach(x => x.Insert(0, list[i]));
                    allPerms.AddRange(perms);
                }
                return allPerms;
            }
        }




        class Element
        {
            // string key = "";
            //long ID;


            public Element(string line)
            {
                ParseSingle(line);
                //ParseMulti(lines);
            }


            private void ParseSingle(string line)
            {
                var sperator = '-';
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
