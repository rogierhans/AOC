using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace AOC2
{
    class Day2018_2 : Day
    {


        public Day2018_2()
        {
            SL.printParse = false;
            GetInput(RootFolder + @"OtherDays\2018_02\");
        }


        public override void Main(List<string> Lines)
        {
            int difference(string s1, string s2)
            {
               return  s1.List().Zip(s2.List(), (l1, l2) => l1 == l2 ? 1 : 0).Sum();
            }
            List<(int, string)> compared = Lines.Product(Lines, (a, b) => (difference(a, b), a + " " + b));
            Console.WriteLine(compared.First(a => a.Item1 == Lines.First().Length - 1).Item2);
        }

        private static void Part1(List<string> Lines)
        {
            long twos = 0;
            long threes = 0;
            foreach (var line in Lines)
            {
                var input = line.List();
                var letters = new HashSet<string>(input);
                var list = letters.ToList();
                list.Print();
                var reducedList = list.Select(x => input.Where(y => x == y).ToList().Count).ToList();
                if (reducedList.Contains(2)) twos++;
                if (reducedList.Contains(3)) threes++;
            }
            Console.WriteLine(twos * threes);
        }
    }
}
