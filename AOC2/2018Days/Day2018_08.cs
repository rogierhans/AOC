using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AOC2
{
    class Day2018_08 : Day
    {


        public Day2018_08()
        {
            SL.printParse = false;
            GetInput(RootFolder + @"2018_08\");
        }
        public const string BLOCK = "\U00002588";
        public override void Main(List<string> Lines)
        {
            var line = Lines.First();
            var numbers = line.Split(' ').Select(int.Parse).ToList();
            int index = 0;
            long x = DFS(numbers, ref index);
            Console.WriteLine(x);
        }

        private  long DFS(List<int> numbers, ref int currentIndex)
        {
            long total = 0;
            int currentChild = numbers[currentIndex++];
            int meta = numbers[currentIndex++];
            List<long> Sums = new List<long>();
            for (int i = 0; i < currentChild; i++)
            {
                var x = DFS(numbers, ref currentIndex);
                Sums.Add(x);
            }
            for (int i = 0; i < meta; i++)
            {
                if (Sums.Count == 0)
                {
                    total += numbers[currentIndex++];
                }
                else
                {
                    int index = numbers[currentIndex++];
                    if (index <= Sums.Count)
                    {
                        total += Sums[index - 1];
                    }
                }
            }
            return  total;
        }
    }
}