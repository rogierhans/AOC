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
            for (int k = 0; k < 50; k++)
            {
                var defaultValue = k % 2 == 0 ? map.First() : map.Last();
                var newoutputGrid = new DictList2D<int>(defaultValue);
                //var elements = outputGrid.GetElements();
                var minX = outputGrid.minX-2;
                var maxX = outputGrid.maxX+2;
                var minY = outputGrid.minY - 2;
                var maxY = outputGrid.maxY+2;
                var array =  outputGrid.CreateArray(minX-2, maxX+2 , minY - 2, maxY +2);

                var xOffset = 0 - minX+2;
                var yOffset = 0 - minY+2;
                for (int i = minX; i < maxX; i++)
                {
                    for (int j = minY; j < maxY; j++)
                    {
                        int next = GetNext(map, outputGrid, i, j,  array,xOffset, yOffset);
                        if (next != defaultValue)
                            newoutputGrid.Add(i, j, next);
                    }
                }

                outputGrid = newoutputGrid;
            }
            Console.WriteLine(outputGrid.GetElements().Where(x => x.Item1 == 1).Count());
        }

        private int GetNext(List<int> map, DictList2D<int> outputGrid, int i, int j, int[,] array, int xOffset, int yOffset)
        {
            List<int> bitString = new List<int>();
            int number = 0;
            byte k = 8;
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    number |= array[i + x + xOffset, y + j + yOffset] << k;
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

            public int minX = 0;
            public int maxX = 0;
            public int minY = 0;
            public int maxY = 0;
            public void Add(int i, int j, T element)
            {

                minX = Math.Min(i, minX);
                minY=  Math.Min(j, minY);

                maxX=  Math.Max(i, maxX);
                maxY =Math.Max(j, maxY);
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

            public T[,] CreateArray(int minX, int maxX, int minY, int maxY)
            {
                int height = maxX - minX;
                int width = maxY - minY;
                T[,] array = new T[height, width];

                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        array[i, j] = DefaultValue;
                    }
                }
                foreach (var (element, i, j) in GetElements()) {
                    array[i - minX, j - minY] = element;
                }


                return array;
            }
        }
    }
}

