using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;


namespace AOC2
{
    class Day8 : Day
    {


        public Day8()
        {
            SL.printParse = false;

            string folder = @"C:\Users\Rogier\Desktop\AOC\";
            string name = "input.txt";
            string filename = folder + name;
            string filenameTest = folder + "test.txt";
            var testLines = File.ReadAllLines(filenameTest).ToList();
            var inputLines = File.ReadAllLines(filename).ToList();
            Console.WriteLine("test:");
            ParseLines(testLines);
            Console.WriteLine("input:");
            ParseLines(inputLines);
        }

        private void ParseLines(List<string> lines)
        {
            var parsed = lines;
            //var numbers = parsed.First().First().Split(',').Select(x => long.Parse(x)).ToList();
            var element = parsed.Select(line => new InputOutputLine(line)).ToList();
            Console.WriteLine(element.Sum(x => x.count));
        }

        class Number
        {
            public string Line = "";
            public List<string> Letters = new List<string>();
            public Number(string line)
            {
                Line = line;
                Letters = line.ToCharArray().Select(x => x.ToString()).ToList();
            }
        }


        class InputOutputLine
        {
            // string key = "";
            //long ID;
            public List<Number> Input = new List<Number>();
            public List<Number> Output = new List<Number>();
            public int count = 0;

            class Pattern
            {
                public List<string> From;
                public List<string> To;

                public int number;
                public Pattern(string to, int number)
                {
                    this.number = number;
                    From = new List<string>();
                    To = to.ToCharArray().Select(x => x.ToString()).ToList();
                }

                public bool Match(Number number, Dictionary<string, List<string>> Rules)
                {
                    if (number.Letters.Count != To.Count) return false;

                    HashSet<string> set = new HashSet<string>();
                    foreach (var letters in To.Select(letter => Rules[letter]))
                    {

                        foreach (var letter in letters)
                        {
                            set.Add(letter);
                        }
                    }
                    bool match = number.Letters.All(x => set.Contains(x));
                    return match;
                }
            }
            public InputOutputLine(string row)
            {

                //   Console.WriteLine(line);
                var inputOutput = row.Split('|');
                Input = Parser.Trim(inputOutput[0], 0, 1).Split(' ').Select(x => new Number(x)).ToList();
                Output = Parser.Trim(inputOutput[1], 1, 0).Split(' ').Select(x => new Number(x)).ToList();
                AllNumbers.AddRange(Input);
                AllNumbers.AddRange(Output);

                Dictionary<string, List<string>> RTN = new Dictionary<string, List<string>>
                {
                    ["a"] = listOfLetters,
                    ["b"] = listOfLetters,
                    ["c"] = listOfLetters,
                    ["d"] = listOfLetters,
                    ["e"] = listOfLetters,
                    ["f"] = listOfLetters,
                    ["g"] = listOfLetters
                };
                var numberOne = AllNumbers.First(x => x.Letters.Count == 2);
                Dictionary<string, int> string2Number = new Dictionary<string, int>() { ["abcefg"] = 0, ["cf"] = 1, ["acdeg"] = 2, ["acdfg"] = 3, ["bcdf"] = 4, ["abdfg"] = 5, ["abdefg"] = 6, ["acf"] = 7, ["abcdefg"] = 8, ["abcdfg"] = 9, };

                string2Number.ToList().ForEach(kvp => Console.WriteLine(kvp.Key.ToCharArray().Sum(x => x)));
                Console.ReadLine();

                //RTN.ToList().ForEach(x => Console.WriteLine("{0} => {1}", x.Key, string.Join(",", x.Value)));
                foreach (var x in AllNumbers.ToList())
                {
                    foreach (var patern in new List<string>() { "cf", "bcdf", "acf", "abcdefg" })
                    {
                        if (x.Letters.Count == patern.Length)
                        {
                            RTN = AddPatern(RTN, x, patern);
                            AllNumbers.Remove(x);
                        }

                    }
                }
                List<Pattern> paterns = string2Number.Select(kvp => new Pattern(kvp.Key, kvp.Value)).ToList();

                while (RTN.Sum(x => x.Value.Count) > 7)
                {
                    foreach (var number in AllNumbers)
                    {
                        var matches = paterns.Where(x => x.Match(number, OtherDicrection(RTN)));
                        if (number.Letters.Count == 5 && numberOne.Letters.All(x => number.Letters.Contains(x)))
                        {
                            matches = paterns.Where(x => paterns.IndexOf(x) == 3);
                        }
                        if (matches.Count() == 1)
                        {
                            var patern = matches.First();
                            RTN = AddPatern(RTN, number, string.Join("", patern.To));
                        }
                        if (RTN.Sum(x => x.Value.Count) <= 7)
                            break;
                    }
                }

                string line = string.Join("", Output.Select(number => string2Number[string.Join("", number.Letters.Select(x => RTN[x].First()).OrderBy(x => x).ToList())]).ToList());
                count = int.Parse(line);
            }
            List<Number> AllNumbers = new List<Number>();
            List<string> listOfLetters = "abcdefg".List();


            private Dictionary<string, List<string>> AddPatern(Dictionary<string, List<string>> dictLettersRandomToNormal, Number x, string word)
            {
                var wordlist = word.List();
                foreach (var letter in x.Letters)
                {
                    foreach (var kvp in dictLettersRandomToNormal.ToList())
                    {
                        if (letter == kvp.Key)
                            dictLettersRandomToNormal[kvp.Key] = kvp.Value.Intersect(wordlist).ToList();
                    }
                }
                foreach (var letter in listOfLetters.Where(y => !x.Letters.Contains(y)).ToList())
                {
                    foreach (var kvp in dictLettersRandomToNormal.ToList())
                    {
                        if (letter == kvp.Key)
                        {
                            dictLettersRandomToNormal[kvp.Key] = kvp.Value.Where(y => !wordlist.Contains(y)).ToList();
                        }
                    }
                }
                return dictLettersRandomToNormal;
            }

            private Dictionary<string, List<string>> OtherDicrection(Dictionary<string, List<string>> dict)
            {
                var other = new Dictionary<string, List<string>>();
                foreach (var letter in listOfLetters)
                {
                    other[letter] = dict.Where(kvp => kvp.Value.Contains(letter)).Select(x => x.Key).ToList();
                }
                return other;
            }
        }
    }
}
