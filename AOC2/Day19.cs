using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
//using ParsecSharp;
//using FishLibrary;
namespace AOC2
{

    class Day19 : Day
    {


        public Day19()
        {
            SL.printParse = false;
            GetInput(RootFolder + @"2021_19\");
        }
        public const string BLOCK = "\U00002588";

        public override void Main(List<string> Lines)
        {
            //Lines.Print();
            var scaners = Lines.ClusterLines().Select(clines => new Scanner(clines)).ToList();
            List<(long, long, long, Position, Position)> AllDistances = new List<(long, long, long, Position, Position)>();
            //scaners.ForEach(s => AllDistances.AddRange(s.DistanceList));
            //// scaners.Print("\n");
            //// 
            //// var mem = new Mem();
            //// var mem2 = new Mem();
            //// Dictionary<(long, long, long, Position, Position), int> UniekeDistances = new Dictionary<(long, long, long, Position, Position), int>();
            //foreach (var d in AllDistances)
            //{
            //    //mem.Add(d.Item1, d.Item2, d.Item3, 1);
            //    //   if (!UniekeDistances.ContainsKey(d)) UniekeDistances[d] = 0;
            //    // UniekeDistances[d]++;
            //}
            // var total = scaners.Sum(x => x.OGRelativePOS.Count);
            //// Console.WriteLine(UniekeDistances.Where(kvp => kvp.Value== 1).Count());
            //foreach (var kvp in mem.Dict)
            //{
            //    foreach (var kvp2 in kvp.Value)
            //    {
            //        foreach (var kvp3 in kvp2.Value)
            //        {
            //            if (kvp3.Value == 2)
            //            {
            //                total--;
            //            }
            //            Console.WriteLine("({0},{1},{2}) = {3}", kvp.Key, kvp2.Key, kvp3.Key, kvp3.Value);
            //        }
            //    }

            //}

            scaners[0].PositionScanner = new Position(0, 0, 0);
            scaners[0].HasPosition = true;
            // Console.WriteLine(total);
            //Console.ReadLine();
            while (scaners.Where(x => x.HasPosition).Count() < scaners.Count())
            {
                for (int i = 0; i < scaners.Count; i++)
                {
                    for (int j = 0; j < scaners.Count; j++)
                    {
                        if (i != j)
                            scaners[i].Machtes(scaners[j]);
                        // Console.WriteLine(total);
                        //Console.ReadLine();
                    }
                }
            }
            long max = 0;
            for (int i = 0; i < scaners.Count; i++)
            {
                for (int j = 0; j < scaners.Count; j++)
                {
                    if (i != j)
                    {
                        var distance = (Math.Abs(scaners[i].PositionScanner.X - scaners[j].PositionScanner.X)) +
                           Math.Abs((scaners[i].PositionScanner.Y - scaners[j].PositionScanner.Y)) +
                            Math.Abs((scaners[i].PositionScanner.Z - scaners[j].PositionScanner.Z));
                        max = Math.Max(max,distance);
                    }
                        
                }
            }
            Console.WriteLine(max);
            Console.ReadLine();
            //Console.WriteLine(total + scaners.Sum(x => x.count));
            //scaners.Print("\n");
            
        }
        public struct Position
        {
            public int X, Y, Z;

            public Position(int x, int y, int z)
            {
                X = x;
                Y = y;
                Z = z;
            }
            public override string ToString()
            {
                return "(" + X + "," + Y + "," + Z + ")";
            }
            public (long, long, long, Position, Position) Distance(Position other)
            {
                return (Squired(X, other.X), Squired(Y, other.Y), Squired(Z, other.Z), this, other);
            }

            public long DistanceNumber(Position other)
            {
                return Squired(X, other.X) + Squired(Y, other.Y) + Squired(Z, other.Z);
            }
            public long Squired(int one, int two)
            {

                return (one - two) * (one - two);
            }


            public bool Equal(Position other)
            {
                return (X == other.X) && (Y == other.Y) && (Z == other.Z);
            }

        }

        public class Scanner
        {
            public string Name = "";
            public string Coordssystem;
            public Position PositionScanner;
            // private List<Position> OGRelativePOS = new List<Position>();
            public List<List<Position>> PermuationPose = new List<List<Position>>();
            //public List<(long, long, long, Position, Position)> DistanceList = new List<(long, long, long, Position, Position)>();
            // public HashSet<Position> twiceCounted = new HashSet<Position>();
            // public int count = 0;
            int SavePerm = 0;

            List<string> oldLines;
            public Scanner(List<string> inputLines)
            {
                oldLines = inputLines;
                Name = inputLines[0];
                Coordssystem = Name;
                var OGRelativePOS = inputLines.FindPatterns("{0},{1},{2}", int.Parse, int.Parse, int.Parse).Select(x => new Position(x.Item1, x.Item2, x.Item3)).ToList();
                PermuationPose = FindPerms(OGRelativePOS);
                Console.WriteLine(PermuationPose.Count);
                // SetDistance();
            }

            public Dictionary<long, Position> GetPostionsThatMatch(Scanner otherScanner, int perm, int otherPerm)
            {
                var distanceOther = otherScanner.GetDistance(otherPerm);
                var mydisance = GetDistance(perm);
                var matcheDistance = distanceOther.Where(d => mydisance.Any(l => Match(l, d)));
                var posFromDistThatMatch = matcheDistance.Select(x => x.Item5).ToList();

                HashSet<Position> beamersThatMatch = new HashSet<Position>();
                foreach (var pos in posFromDistThatMatch)
                {
                    beamersThatMatch.Add(pos);
                }
                return numberToPos(beamersThatMatch.ToList());
            }
            public void Machtes(Scanner otherScanner)
            {
                bool mathedCanidite = CheckIfMatched(otherScanner);
                if (HasPosition && mathedCanidite && !otherScanner.HasPosition)
                {
                    for (int i = 0; i < 48; i++)
                    {
                        //  Console.WriteLine(Name + "and " + otherScanner.Name);
                        var dict1 = GetPostionsThatMatch(otherScanner, 0, i);
                        var dict2 = otherScanner.GetPostionsThatMatch(this, i, 0);


                        Dictionary<Position, Position> tuples = new Dictionary<Position, Position>();



                        foreach (var kvp in dict1)
                        {
                            tuples[kvp.Value] = dict2[kvp.Key];
                        }
                        var minues = tuples.Select(kvp => Minus(kvp.Key, kvp.Value)).ToList();
                        bool p = true;
                        for (int j = 1; j < minues.Count; j++)
                        {
                            p &= minues[j - 1].Equal(minues[j]);
                        }
                        if (p)
                        {
                            Console.WriteLine(Name + " " + otherScanner.Name);
                            Console.WriteLine(PositionScanner + " " + minues.First());
                            otherScanner.SetPositon(minues.First(), i);
                            return;
                        }


                    }
                    // 
                }
            }

            private bool CheckIfMatched(Scanner otherScanner)
            {
                var matcheDistance = otherScanner.GetDistance(0).Where(d => GetDistance(0).Any(l => Match(l, d)));
                var posFromDistThatMatch = matcheDistance.Select(x => x.Item4).ToList();

                HashSet<Position> beamersThatMatch = new HashSet<Position>();
                foreach (var pos in posFromDistThatMatch)
                {
                    beamersThatMatch.Add(pos);
                }
                var mathedCanidite = beamersThatMatch.Count() == 12;
                return mathedCanidite;
            }

            public bool HasPosition = false;
            private void SetPositon(Position newPos, int perm)
            {
                SavePerm = perm;
                HasPosition = true;
                PositionScanner = newPos;
                var OGRelativePOS = PermuationPose[perm];// //  oldLines.FindPatterns("{0},{1},{2}", int.Parse, int.Parse, int.Parse).Select(x => new Position(x.Item1, x.Item2, x.Item3)).ToList();
                var newpostions = OGRelativePOS.Select(x => Minus(newPos, x)).ToList();
               // newpostions.ForEach(x => CheckValid(x));
                //newpostions.Print("\n");
                PermuationPose = FindPerms(newpostions);
                Console.WriteLine(Name + "set to" + newPos);
                
            }

            public List<List<Position>> FindPerms(List<Position> machtedPos)
            {
                List<List<Position>> perms = new List<List<Position>>();
                foreach (bool negativeX in new List<bool> { true, false })
                {
                    foreach (bool negativeY in new List<bool> { true, false })
                    {
                        foreach (bool negativeZ in new List<bool> { true, false })
                        {
                            for (int i = 0; i < 6; i++)
                            {
                                var newPoses = new List<Position>(machtedPos);
                                if (negativeX)
                                {
                                    newPoses = newPoses.Select(pos => new Position(-pos.X, pos.Y, pos.Z)).ToList();
                                }
                                if (negativeY)
                                {
                                    newPoses = newPoses.Select(pos => new Position(pos.X, -pos.Y, pos.Z)).ToList();
                                }
                                if (negativeZ)
                                {
                                    newPoses = newPoses.Select(pos => new Position(pos.X, pos.Y, -pos.Z)).ToList();
                                }
                                newPoses = newPoses.Select(pos => PermPos(pos)[i]).ToList();
                                perms.Add(newPoses);
                            }
                        }
                    }
                }
                return perms;
            }


            public Position Flip(Position pos)
            {
                return new Position(-pos.X, -pos.Y, -pos.Z);
            }
            public List<Position> PermPos(Position pos)
            {
                List<Position> list = new List<Position>() {
                new Position(pos.X,pos.Y,pos.Z),
                new Position(pos.X,pos.Z,pos.Y),
                new Position(pos.Y,pos.X,pos.Z),
                new Position(pos.Y,pos.Z,pos.X),
                new Position(pos.Z,pos.X,pos.Y),
                new Position(pos.Z,pos.Y,pos.X),
                };
                return list;
            }

            public Position Minus(Position pos1, Position pos2)
            {
                return new Position(pos1.X - pos2.X, pos1.Y - pos2.Y, pos1.Z - pos2.Z);
            }

            public Position Plus(Position pos1, Position pos2)
            {
                return new Position(pos1.X + pos2.X, pos1.Y + pos2.Y, pos1.Z + pos2.Z);
            }


            public bool Match((long, long, long, Position, Position) l, (long, long, long, Position, Position) k)
            {
                var a = l.Item1;
                var b = l.Item2;
                var c = l.Item3;
                var x = k.Item1;
                var y = k.Item2;
                var z = k.Item3;
                bool match = (a == x && b == y && c == z) ||
                       (a == x && b == z && c == y) ||
                       (a == y && b == x && c == z) ||
                       (a == y && b == z && c == x) ||
                       (a == z && b == x && c == y) ||
                       (a == z && b == y && c == x);
                //if (match)
                //{
                //    if (!twiceCounted.Contains(l.Item4))
                //    {
                //        twiceCounted.Add(l.Item4);
                //        count--;
                //    }
                //    if (!twiceCounted.Contains(l.Item5))
                //    {
                //        twiceCounted.Add(l.Item5);
                //        count--;
                //    }
                //}
                return match;
            }


            private List<(long, long, long, Position, Position)> GetDistance(int perm)
            {
                var DistanceList = new List<(long, long, long, Position, Position)>();
                for (int i = 0; i < PermuationPose[perm].Count; i++)
                {
                    var pos1 = PermuationPose[perm][i];
                    for (int j = 0; j < PermuationPose[perm].Count; j++)
                    {
                        if (i != j)
                        {
                            var pos2 = PermuationPose[perm][j];
                            DistanceList.Add(pos1.Distance(pos2));
                        }
                    }

                }
                return DistanceList;
            }
            private Dictionary<long, Position> numberToPos(List<Position> postThatMatch)
            {
                var posToNumber = new Dictionary<long, Position>();
                for (int i = 0; i < postThatMatch.Count; i++)
                {
                    var pos1 = postThatMatch[i];
                    long distanceNumber = 0;
                    for (int j = 0; j < postThatMatch.Count; j++)
                    {
                        if (i != j)
                        {

                            var pos2 = postThatMatch[j];
                            var lel = pos1.Distance(pos2);
                            distanceNumber += pos1.DistanceNumber(pos2);

                        }
                    }
                    posToNumber[distanceNumber] = pos1;
                }
                return posToNumber;
            }


            public int numberOfMatches(int matches)
            {
                for (int i = 3; i <= 12; i++)
                {
                    if ((i * (i - 1)) / 2 == matches) return i;
                }
                return 0;
            }

            public override string ToString()
            {
                return "Scanner:" + Name + "\n";// + String.Join("\n", OGRelativePOS) + "\n";// + String.Join("\n", posToNumber.Select(kvp => kvp.Key + "->" + kvp.Value));
            }
            List<(long, long, long)> checkList = File.ReadAllLines(@"C:\Users\Rogier\Dropbox\AOC2\AOC2\InputFiles\2021_19\posValidShit.txt").ToList().FindPatterns("{0},{1},{2}", long.Parse, long.Parse, long.Parse);

            public void CheckValid(Position pos)
            {
                bool eq = checkList.Any(element =>
                {
                   // Console.WriteLine("{0} {1} {2}", element.Item1 - pos.X, element.Item2 - pos.Y, element.Item3 - pos.Z);
                    return element.Item1 == pos.X && element.Item2 == pos.Y && element.Item3 == pos.Z;
                });
                Console.WriteLine(eq);
            }

        }

    }
}

