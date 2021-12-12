using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AOC2
{
    class Day1
    {
        public Day1()
        {
            var lines = File.ReadAllLines(@"C:\Users\Rogier\Desktop\AOC\OtherDays\day1.txt");
            var input = lines.Select(x => double.Parse(x)).ToList();
            int count = 0;
            for (int i = 0; i < input.Count - 3; i++)
            {
                if (input[i] < input[i + 3]) count++;
            }
            Console.WriteLine(count);
        }
    }
}
