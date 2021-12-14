using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace AOC2
{
    class Day3 : Day
    {


        public Day3()
        {
            SL.printParse = true;

            string folder = @"C:\Users\Rogier\Desktop\AOC\";
            string name = "input.txt";
            string filename = folder + name;
            string filenameTest = folder + "test.txt";
            var testLines = File.ReadAllLines(filenameTest).ToList();
            var inputLines = File.ReadAllLines(filename).ToList();
            Console.WriteLine("test:");
            ModeSelector(testLines);
            Console.WriteLine("input:");
            ModeSelector(inputLines);
        }


        private void ModeSelector(List<string> Lines)
        {
            CleanerSolution(Lines);
            // ParseLines(Lines);
        }
        private void CleanerSolution(List<string> lines)
        {
            var oxygenBits = ReduceToSingle(lines, x => x);
            var CO2Bits = ReduceToSingle(lines, x => 1 - x);
            var oxygen = Bits2Number(oxygenBits);
            var CO2 = Bits2Number(CO2Bits);
            Console.WriteLine(oxygen * CO2);
        }

        private List<int> ReduceToSingle(List<string> lines, Func<int, int> funcOnCommonBit)
        {
            var array = lines.Parse2D( x => int.Parse(x));
            for (int i = 0; i < array.First().Count; i++)
            {
                int commonBit = array.Transpose().Select(x => Round(x.Average())).ToList()[i];
                array = array.Where(bit => bit[i] == funcOnCommonBit(commonBit)).ToList();
                if (array.Count == 1) return array.First();
            }
            int Round(double number)
            {
                return (number == 0.5) ? 1 : (int)Math.Round(number);
            }
            throw new Exception("");
        }

        private long Bits2Number(List<int> list)
        {
            long number = 0;
            for (int i = 0; i < list.Count; i++)
            {
                number += list[list.Count - i - 1] << i;
            }
            return number;
        }
    }
}
