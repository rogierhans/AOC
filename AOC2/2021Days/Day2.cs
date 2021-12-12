using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AOC2
{
    class Day2 : Day
    {

        public Day2()
        {

            string folder = @"C:\Users\Rogier\Desktop\AOC\";
            string name = "input.txt";
            string filename = folder + name;
            string filenameTest = folder + "test.txt";
            var testLines = File.ReadAllLines(filenameTest).ToList();
            var inputLines = File.ReadAllLines(filename).ToList();
            Console.WriteLine("test:");
            ComputeLines(testLines);
            Console.WriteLine("input:");
            ComputeLines(inputLines);
        }






        private void ComputeLines(List<string> Lines)
        {
            long aim = 0;
            long dept = 0;
            long distance = 0;
            for (int i = 0; i < Lines.Count; i++)
            {
                var line = Lines[i];
                var input = line.Split(' ');
                var value = long.Parse(input[1]);
                if (input[0] == "forward")
                {
                    distance += value;
                    dept += aim * value;
                }
                if (input[0] == "down")
                {
                    aim += value;
                }
                if (input[0] == "up")
                {
                    aim -= value;
                }
            }
            Print(dept * distance);
        }
    }





























    class Row
    {
        //  public List<int> Numbers = new List<int>();
        public List<double> Numbers = new List<double>();
        public Row(string line)
        {
            Numbers = line.Split(' ').Select(x => double.Parse(x)).ToList();
        }
    }
}
