using System;
using System.Collections.Generic;
using System.Linq;
//using ParsecSharp;
//using FishLibrary;
namespace AOC2
{

    class Day20 : Day
    {


        public Day20()
        {
            SL.printParse = false;
            GetInput(RootFolder + @"2021_20\");
        }
        public const string BLOCK = "\U00002588";

        public override void Main(List<string> Lines)
        {
            //Lines.Print("\n");
            var split = Lines.ClusterLines();
            var map = split[0].First().List().Select(x => x == "#" ? 1 : 0).ToList();
            //map.Print();
            var second = split[1].Parse2D(x => x);
            second.Print();
            DictList2D<int> outputGrid = new DictList2D<int>(0);
            for (int i = 0; i < second.Count; i++)
            {
                for (int j = 0; j < second[0].Count; j++)
                {
                    if (second[i][j] == "#")
                        outputGrid.Add(i, j, 1);
                }
            }
            PrintGrid(outputGrid);
          //  Console.ReadLine();
            for (int k = 0; k < 50; k++)
            {
                PrintGrid(outputGrid);
                var defaultValue = k % 2 == 0 ? map.First() : map.Last();
                var newoutputGrid = new DictList2D<int>(defaultValue);
                //            var elements = grid.GetElements();
                //var minX = elements.Min(x => x.Item2) - 10;
                //var maxX = elements.Max(x => x.Item2) + 10;
                //var minY = elements.Min(x => x.Item3) - 10;
                //var maxY = elements.Max(x => x.Item3) + 10;
                foreach (var (element, potI, potJ) in outputGrid.GetElements())
                {

                    for (int a = -1; a <= 1; a++)
                    {
                        for (int b = -1; b <= 1; b++)
                        {
                            int i = potI + a;
                            int j = potJ + b;

                            int next = GetNext(map, outputGrid, i, j);
                            if (next != defaultValue)
                                newoutputGrid.Add(i, j, next);
                        }
                    }
                }


                outputGrid = newoutputGrid;
                //PrintGrid(outputGrid);
            }
            Console.WriteLine(outputGrid.GetElements().Where(x => x.Item1 == 1).Count());
            Console.ReadLine();
        }

        private int GetNext(List<int> map, DictList2D<int> outputGrid, int i, int j)
        {
            List<int> bitString = new List<int>();
            int number = 0;
            int k = 8;
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    number += outputGrid.Get(i + x, y + j) << k;
                    k--;
                }
            }
            var next = map[number];
            return next;
        }

        private void PrintGrid(DictList2D<int> grid)
        {
            var elements = grid.GetElements();
            var minX = elements.Min(x => x.Item2) - 10;
            var maxX = elements.Max(x => x.Item2) + 10;
            var minY = elements.Min(x => x.Item3) - 10;
            var maxY = elements.Max(x => x.Item3) + 10;

            Console.WriteLine("{0} {1} {2} {3}", minX, minY, maxX, maxX);
            for (int i = minX; i < maxX; i++)

            {
                string line = "";

                for (int j = minY; j < maxY; j++)
                {
                    line += grid.Get(i, j);
                }
                Console.WriteLine(line);
            }
        }


        class DictList2D<T>
        {
            Dictionary<int, Dictionary<int, T>> dict = new Dictionary<int, Dictionary<int, T>>();
            private readonly T DefaultValue;

            public DictList2D(T defaultValue)
            {
                DefaultValue = defaultValue;
            }

            public T Get(int i, int j)
            {
                if (dict.ContainsKey(i) && dict[i].ContainsKey(j))
                {
                    return dict[i][j];
                }
                else return DefaultValue;
            }
            public void Add(int i, int j, T element)
            {
                // Console.WriteLine(i + " " +j);
                if (!dict.ContainsKey(i)) dict[i] = new Dictionary<int, T>();
                dict[i][j] = element;
            }

            public List<(T, int, int)> GetElements()
            {
                var list = new List<(T, int, int)>();
                foreach (var kvp in dict)
                {
                    int index1 = kvp.Key;
                    foreach (var kvp2 in kvp.Value)
                    {
                        int index2 = kvp2.Key;
                        list.Add((kvp2.Value, index1, index2));
                    }
                }
                return list;
            }
        }
    }
}

