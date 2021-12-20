using System;
using System.Collections.Generic;
using System.Linq;
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
            var scaners = Lines.ClusterLines().Select(clines => new Scanner(clines)).ToList();
            List<(long, long, long, Position, Position)> AllDistances = new List<(long, long, long, Position, Position)>();
            scaners[0].PositionScanner = new Position(0, 0, 0, false);
            scaners[0].HasPosition = true;
            while (scaners.Where(x => x.HasPosition).Count() < scaners.Count())
            {
                for (int i = 0; i < scaners.Count; i++)
                {
                    for (int j = 0; j < scaners.Count; j++)
                    {
                        if (i != j)
                            scaners[i].Machtes(scaners[j]);
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
                        max = Math.Max(max, distance);
                    }

                }
            }
            Console.WriteLine(max);
        }
        public class Position
        {
            public int X, Y, Z;

            public Position(int x, int y, int z, bool findPerms)
            {
                X = x;
                Y = y;
                Z = z;
                if (findPerms)
                    FindPerms();
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
            public List<Position> PermOfPos = new List<Position>();
            public void FindPerms()
            {

                foreach (bool negativeX in new List<bool> { true, false })
                {
                    foreach (bool negativeY in new List<bool> { true, false })
                    {
                        foreach (bool negativeZ in new List<bool> { true, false })
                        {
                            for (int i = 0; i < 6; i++)
                            {
                                var pos = new Position(X, Y, Z, false);
                                if (negativeX)
                                {
                                    pos = new Position(-pos.X, pos.Y, pos.Z, false);
                                }
                                if (negativeY)
                                {
                                    pos = new Position(pos.X, -pos.Y, pos.Z, false);

                                }
                                if (negativeZ)
                                {
                                    pos = new Position(pos.X, pos.Y, -pos.Z, false);
                                }
                                PermOfPos.Add(PermPos(pos)[i]);
                            }
                        }
                    }
                }
            }
            public List<Position> PermPos(Position pos)
            {
                List<Position> list = new List<Position>() {
                new Position(pos.X,pos.Y,pos.Z,false),
                new Position(pos.X,pos.Z,pos.Y,false),
                new Position(pos.Y,pos.X,pos.Z,false),
                new Position(pos.Y,pos.Z,pos.X,false),
                new Position(pos.Z,pos.X,pos.Y,false),
                new Position(pos.Z,pos.Y,pos.X,false),
                };
                return list;
            }
        }

        public class Scanner
        {
            public int id = 0;
            public string Name = "";
            public string Coordssystem;
            public Position PositionScanner;
            List<Position> Positions = new List<Position>();

            public Scanner(List<string> inputLines)
            {
  
                Name = inputLines[0];
                id = Name.Pattern("--- scanner {0} ---", int.Parse);
                Coordssystem = Name;
                Positions = inputLines.FindPatterns("{0},{1},{2}", int.Parse, int.Parse, int.Parse).Select(x => new Position(x.Item1, x.Item2, x.Item3, true)).ToList();
                SetDistance();
            }
            private void SetPositon(Position newPos, int perm)
            {
                HasPosition = true;
                PositionScanner = newPos;
                var OGRelativePOS = Positions.Select(x => x.PermOfPos[perm]);
                Positions = OGRelativePOS.Select(x => Minus(newPos, x)).ToList();
                SetDistance();
                Console.WriteLine(Name + "set to" + newPos);

            }


            public void Machtes(Scanner otherScanner)
            {
                if (HasPosition &&  !otherScanner.HasPosition && TwelveBeaconsTheSame(otherScanner))
                {
                    var list1 = otherScanner.Distances.Where(d => Distances.Any(l => Match(l, d))).Select(x => x.Item5).ToList();
                    var list2 = Distances.Where(d => otherScanner.Distances.Any(l => Match(l, d))).Select(x => x.Item5.PermOfPos[0]).ToList();
                    var dict1 = CreatePostionID(list1);
                    var dict2 = CreatePostionID(list2);
                    var tuple = dict1.Select(kvp => (kvp.Value, dict2[kvp.Key])).ToList();
                    for (int i = 0; i < 48; i++)
                    {
                        var first = Minus(tuple[0].Item1.PermOfPos[i], tuple[0].Item2);
                        bool p = true;
                        int j = 1;
                        while (p && j < 12)
                        {
                            var other = Minus(tuple[j].Item1.PermOfPos[i], tuple[j].Item2);
                            p &= first.Equal(other);
                            first = Minus(tuple[j].Item1.PermOfPos[i], tuple[j].Item2);
                            j++;
                        }
                        if (p)
                        {
                            otherScanner.SetPositon(first, i);
                            return;
                        }
                    }
                }
            }

            int[] Machted = new int[64];
            private bool TwelveBeaconsTheSame(Scanner otherScanner)
            {
                if (Machted[otherScanner.id] == 1) { return true; }
                if (Machted[otherScanner.id] == -1) {
                    return false; }
                var matcheDistance = otherScanner.Distances.Where(d => Distances.Any(l => Match(l, d)));
                var posFromDistThatMatch = matcheDistance.Select(x => x.Item4);

                var mathedCanidite = posFromDistThatMatch.Count() == 132;

                Machted[otherScanner.id] = mathedCanidite ? 1 : -1;
                return mathedCanidite;
            }

            public bool HasPosition = false;



            public Position Minus(Position pos1, Position pos2)
            {
                return new Position(pos1.X - pos2.X, pos1.Y - pos2.Y, pos1.Z - pos2.Z, true);
            }

            public Position Plus(Position pos1, Position pos2)
            {
                return new Position(pos1.X + pos2.X, pos1.Y + pos2.Y, pos1.Z + pos2.Z, true);
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
                return match;
            }

            List<(long, long, long, Position, Position)> Distances = new List<(long, long, long, Position, Position)>();
            private void SetDistance()
            {
                Distances = new List<(long, long, long, Position, Position)>();
                for (int i = 0; i < Positions.Count; i++)
                {
                    var pos1 = Positions[i];
                    for (int j = 0; j < Positions.Count; j++)
                    {
                        if (i != j)
                        {
                            var pos2 = Positions[j];
                            Distances.Add(pos1.Distance(pos2));
                        }
                    }
                }

            }
            private static Dictionary<long, Position> CreatePostionID(List<Position> postThatMatch)
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

            public override string ToString()
            {
                return "Scanner:" + Name + "\n";
            }
        }

    }
}

