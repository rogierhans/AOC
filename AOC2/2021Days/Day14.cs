using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AOC2
{
    class Day14 : Day
    {


        public Day14()
        {
            SL.printParse = false;
            GetInput(RootFolder + @"2021_14\");
        }
        public const string BLOCK = "\U00002588";
        public override void Main(List<string> Lines)
        {
            var begin = Lines.First().List();
            var rules = Lines.FindPatterns("{0} -> {1}", x => x, x => x);
            Dictionary<string, (string, string)> dict = new Dictionary<string, (string, string)>();
            Dictionary<string, int> stringToIndex = new Dictionary<string, int>();
            Dictionary<int, string> indexToString = new Dictionary<int, string>();
            for (int i = 0; i < rules.Count; i++)
            {
                stringToIndex[rules[i].Item1] = i;
                indexToString[i] = rules[i].Item1;
            }

            long[] bucket = new long[rules.Count];
            for (int i = 0; i < begin.Count() - 1; i++)
            {
                string key = begin[i] + begin[i + 1];
                bucket[stringToIndex[key]]++;
            }

            foreach (var (a, b) in rules)
            {

                dict[a] = (a.List()[0] + b, b + a.List()[1]);

            }

            for (int k = 0; k < 40; k++)
            {
                long[] newBucket = new long[rules.Count];
                for (int i = 0; i < newBucket.Length; i++)
                {
                    var (key1, key2) = dict[indexToString[i]];
                    newBucket[stringToIndex[key1]] += bucket[i];
                    newBucket[stringToIndex[key2]] += bucket[i];
                }
                bucket = newBucket;
            }


            Dictionary<string, long> count = new Dictionary<string, long>();
            for (int i = 0; i < bucket.Length; i++)
            {

                var letter1 = rules[i].Item1.List()[0];
                var letter2 = rules[i].Item1.List()[1];
                if (!count.ContainsKey(letter1)) count[letter1] = 0;
                count[letter1] += bucket[i];
                if (!count.ContainsKey(letter2)) count[letter2] = 0;
                count[letter2] += bucket[i];
            }
            var numbers = count.Select(kvp => kvp.Value).Select(x => x % 2 == 0 ? x / 2 : (x + 1) / 2);
            Console.WriteLine("{0} {1} {2}", numbers.Max(), numbers.Min(), (numbers.Max() - numbers.Min()));
            // Console.ReadLine();
        }
    }

}

