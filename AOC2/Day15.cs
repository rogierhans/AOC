using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
namespace AOC2
{
    class Day15 : Day
    {


        public Day15()
        {
            SL.printParse = false;
            GetInput(RootFolder + @"2021_15\");
        }
        public const string BLOCK = "\U00002588";


        public long GetKey(int x, int y, int distance)
        {
            distance = distance << 4;
            distance += x;
            distance = distance << 4;
            distance += y;
            return distance;
        }
        public override void Main(List<string> Lines)
        {
            var grid = Lines.Select(g => g.List().Select(int.Parse).ToList()).ToList();
            var bigGrid = new List<List<int>>();
            for (int r = 0; r < 5; r++)
            {
                for (int i = 0; i < grid.Count; i++)
                {
                    List<int> list = new List<int>();
                    for (int c = 0; c < 5; c++)
                    {
                        for (int j = 0; j < grid.Count; j++)
                        {
                            list.Add((grid[i][j] + (r + c)) > 9 ? (grid[i][j] + (r + c)) % 10 + 1 : grid[i][j] + (r + c));
                        }
                    }
                    bigGrid.Add(list);
                }

            }
            // bigGrid.Print("");
            ///  Console.ReadLine();
            ///  
            grid = bigGrid;
            List<List<long>> dist = new List<List<long>>();

            List<List<(int, int)>> prev = new List<List<(int, int)>>();

            var coordsSL = new SortedList<long, (int, int)>();
            var coords = new List<(int, int)>();
            for (int i = 0; i < grid.Count; i++)
            {
                List<long> list = new List<long>();
                List<(int, int)> list2 = new List<(int, int)>();
                for (int j = 0; j < grid.Count; j++)
                {
                    list.Add(long.MaxValue);
                    list2.Add((-1, -1));
                    coords.Add((i, j));
                    if (!(i == 0 && j == 0))
                        coordsSL.Add(long.MaxValue, (i, j));
                }
                dist.Add(list);
                prev.Add(list2);
            }
            coordsSL.Add(0, (0, 0));
            while (coords.Count > 0)
            {
                var element2 = coordsSL.First();

                var element = coords.MaxItem(e => (double)-dist[e.Item1][e.Item2]);
                coords.Remove(element);
                var totalRisk = dist[element.Item1][element.Item2];
                foreach (var (nx, ny) in grid.Neighbor4(element.Item1, element.Item2))
                {
                    var riskLevel = grid[nx][ny];
                    var newTotal = riskLevel + totalRisk;
                    if (dist[nx][ny] > newTotal)
                    {
                        dist[nx][ny] = newTotal;
                        prev[nx][ny] = (element.Item1, element.Item2);
                    }
                }

            }
            dist.Print(" ");
            Console.WriteLine("done");
        }
    }
}