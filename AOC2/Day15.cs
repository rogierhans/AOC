using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Priority_Queue;
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

        public override void Main(List<string> Lines)
        {
            var grid = Lines.Select(g => g.List().Select(int.Parse).ToList()).ToList();
            List<List<int>> bigGrid = GreateBigGrid(grid);
            grid = bigGrid;
            var dist = new List<List<long>>();
            var q = new SimplePriorityQueue<(int, int), long>();
            Init(grid, dist, q);
            dist[0][0] = 0;
            q.UpdatePriority((0, 0), 0);

            while (q.Count > 0)
            {
                var (x, y) = q.Dequeue();
                if (dist[x][y] > dist.Last().Last()) break;
                foreach (var (nx, ny) in grid.Neighbor4(x, y))
                {
                    var riskLevel = grid[nx][ny];
                    var newTotal = riskLevel + dist[x][y];
                    if (dist[nx][ny] > newTotal)
                    {
                        dist[nx][ny] = newTotal;
                        q.UpdatePriority((nx, ny), newTotal);
                    }
                }

            }
            Console.WriteLine(dist.Last().Last());
            // Console.ReadLine();
        }

        private static void Init(List<List<int>> grid, List<List<long>> dist, SimplePriorityQueue<(int, int), long> coords2)
        {
            for (int i = 0; i < grid.Count; i++)
            {
                List<long> list = new List<long>();
                for (int j = 0; j < grid.Count; j++)
                {
                    list.Add(long.MaxValue);
                    coords2.Enqueue((i, j), long.MaxValue);
                }
                dist.Add(list);
            }
        }

        private static List<List<int>> GreateBigGrid(List<List<int>> grid)
        {
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

            return bigGrid;
        }
    }
}