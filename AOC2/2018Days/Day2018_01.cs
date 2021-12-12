using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace AOC2
{
    class Day2018_1 : Day
    {


        public Day2018_1()
        {
            SL.printParse = false;
            GetInput(@"C:\Users\Rogier\Desktop\AOC\OtherDays\2018_01\");
        }


        public override void Main(List<string> Lines)
        {
            HashSet<int> set = new HashSet<int>();
            var numbers = Lines.Select(x => int.Parse(x)).ToList();
            int sum = 0;
            int i = 0; 
            while (true)
            {
                var number = numbers[i++ % numbers.Count];
                sum += number;
                if (set.Contains(sum))
                {
                    Console.WriteLine(sum);
                    break;
                }
                set.Add(sum);
            }
        }
    }
}
