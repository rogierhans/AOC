using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace AOC2
{
    class Day9 : Day
    {


        public Day9()
        {
            SL.printParse = false;

            string folder = @"C:\Users\Rogier\Desktop\AOC\";
            string name = "input.txt";
            string filename = folder + name;
            string filenameTest = folder + "test.txt";
            var testLines = File.ReadAllLines(filenameTest).ToList();
            var inputLines = File.ReadAllLines(filename).ToList();
            Console.WriteLine("test:");
            Main(testLines);
            Console.WriteLine("input:");
            Main(inputLines);
        }


        public override void Main(List<string> Lines)
        {
            List<int> sizeBasins = new List<int>();
            var grid = Lines.Select(x => x.List().Select(y => int.Parse(y)).ToList()).ToList();
            for (int i = 0; i < grid.Count; i++)
            {
                for (int j = 0; j < grid[0].Count; j++)
                {
                    bool lower = true;
                    foreach (var offset in new List<(int, int)>() { (-1, 0), (1, 0), (0, -1), (0, 1) })
                    {
                        int x = i + offset.Item1;
                        int y = j + offset.Item2;
                        bool outOfRow = x < 0 || x >= grid.Count;
                        bool outOfColumn = y < 0 || y >= grid[0].Count;
                        if (!outOfColumn && !outOfRow)
                        {
                            lower &= grid[i][j] < grid[x][y];
                        }
                    }
                    if (lower)
                    {
                        sizeBasins.Add(BasinSize(i, j, grid));
                    }
                }
            }
            var order = sizeBasins.OrderByDescending(x => x).ToList();
            order.Take(3).ToList().ForEach(x => Console.WriteLine(x));
            Console.WriteLine(order[0] * order[1] * order[2]);
            Console.ReadLine();
        }

        
        private int BasinSize(int startx, int stary, List<List<int>> grid)
        {
            int count = 0;

            Queue<(int, int)> q = new Queue<(int, int)>();
            var visited = grid.GridSelect(x => false);
            q.Enqueue((startx, stary));
            while (q.Count > 0)
            {
                var position = q.Dequeue();
                foreach (var offset in new List<(int, int)>() { (-1, 0), (1, 0), (0, -1), (0, 1) })
                {
                    int x = position.Item1 + offset.Item1;
                    int y = position.Item2 + offset.Item2;
                    bool outOfRow = x < 0 || x >= grid.Count;
                    bool outOfColumn = y < 0 || y >= grid[0].Count;
                    if (!outOfColumn && !outOfRow && !visited[x][y] && grid[x][y] != 9)
                    {
                        q.Enqueue((x, y));
                        visited[x][y] = true;
                        count++;
                    }
                }
            }
            return count;
        }
    }
}
