using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace AOC2
{
    class Day10 : Day
    {


        public Day10()
        {
            SL.printParse = false;
            string folder = @"C:\Users\Rogier\Desktop\AOC\";
            string name = "input.txt";
            string filename = folder + name;
            string filenameTest = folder + "test.txt";
            var testLines = File.ReadAllLines(filenameTest).ToList();
            var inputLines = File.ReadAllLines(filename).ToList();
            Console.WriteLine("test:");
            IndexForLoop(testLines);
            Console.WriteLine("input:");
            IndexForLoop(inputLines);
        }


        private void IndexForLoop(List<string> Lines)
        {
            List<long> scores = new List<long>();
            var score = new Dictionary<string, long> { ["("] = 1, ["["] = 2, ["{"] = 3, ["<"] = 4 };
            //foreach (var line in Lines)
            //{
            //    var reducedLine = line;
            //    for (int i = 0; i < line.Length; i++)
            //    {
            //        reducedLine = reducedLine.Replace("()", "").Replace("{}", "").Replace("[]", "").Replace("<>", "");
            //    }
            //    bool isValid = !reducedLine.List().Any(letter => new List<string>() { ")", "}", "]", ">" }.Contains(letter));
            //    if (isValid)
            //    {
            //        scores.Add(reducedLine.List().AsEnumerable().Reverse().Aggregate((long)0, (a, b) => a * 5 + score[b]));
            //    }
            //}
            //scores = scores.OrderBy(x => x).ToList();
            //Console.WriteLine(scores[scores.Count / 2]);
            //return;
            List<long> numbers = new List<long>();
            var validCombinations = new List<(string, string)> { ("(", ")"), ("{", "}"), ("[", "]"), ("<", ">") };

            for (int t = 0; t < Lines.Count; t++)
            {
                var input = Lines[t].List();
                Stack<string> stack = new Stack<string>();
                bool skip = false;
                for (int i = 0; i < input.Count; i++)
                {
                    var letter = input[i];
                    if (new List<string>() { "(", "{", "[", "<" }.Contains(letter))
                    {
                        stack.Push(letter);
                    }
                    if (new List<string>() { ")", "}", "]", ">" }.Contains(letter))
                    {
                        var otherLetter = stack.Pop();
                        var pair = (otherLetter, letter);
                        if (!validCombinations.Contains(pair)) skip = true;
                    }
                }
                long count = stack.Aggregate((long)0, (a, b) => a * 5 + score[b]);
                if (!skip)
                    scores.Add(count);
            }

        }
    }
}
