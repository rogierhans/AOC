using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2
{
    public static class Parser
    {
     
        public static List<List<T>> Parse2D<T>(this List<string> lines, Func<string, T> func)
        {
            return lines.Select(line => line.ToCharArray().Select(x => func(x.ToString())).ToList()).ToList();
        }

        public static List<List<string>> ClusterLines(this List<string> lines)
        {
            List<List<string>> List = new List<List<string>>();

            var currentList = new List<string>();
            foreach (var line in lines)
            {

                if (line == "")
                {
                    Console.WriteLine("adsds");
                    List.Add(currentList);
                    currentList = new List<string>();
                }
                else
                {
                    currentList.Add(line);
                }
            }
            List.Add(currentList);
            return List;
        }

        public static string Trim(this string line, int front, int back)
        {
            return line.Substring(front, line.Count() - front - back);
        }

        public static (string, string) Split_Inc(string line, int index)
        {
            return (line.Substring(0, index), line.Substring(index, line.Count() - index));
        }
        public static (string, string) Split_Ex(string line, int index)
        {
            return (line.Substring(0, index), line.Substring(index + 1, line.Count() - (index + 1)));
        }

        public static string PatternMatch(string line, string front, string back)
        {

            for (int i = 0; i < line.Count(); i++)
            {
                var subline = line.Substring(i, front.Length);
                if (front == subline)
                {
                    for (int j = i + front.Length; j < line.Count(); j++)
                    {
                        var subline2 = line.Substring(j, back.Length);
                        if (subline2 == back)
                        {
                            return line.Substring(i + front.Length, j - (i + front.Length));
                        }
                    }
                }
            }
            return "";

        }

        public static List<string> Split(string line, string seperator)
        {
            return line.Replace(seperator, ";").Split(';').ToList();
        }

        public static (string, string, string) ParrenthesisParser(string line, string front, string middle, string end)
        {
            for (int i = 0; i < line.Count(); i++)
            {
                if (LineMatch(line, front, i))
                {
                    Console.WriteLine(line.Substring(i));
                    int counter = 0;
                    for (int j = i+1; j < line.Count(); j++)
                    {
                        if (LineMatch(line, front, j)) { counter++;

                        }
                        if (LineMatch(line, end, j))
                        {
                            if (counter == 0)
                            {
                                return (line.Substring(i + front.Length, (j + end.Length) - (i + front.Length)), "", "");
                            }
                            counter--;
                        }
                        Console.WriteLine(counter);
                    }
                    throw new Exception("");
                }
            }

            throw new Exception("");
        }

        public static bool LineMatch(string line, string pattern, int offset)
        {
            if (pattern.Length + offset > line.Length) return false;
            var chars1 = line.ToCharArray();
            var chars2 = pattern.ToCharArray();
            bool p = true;
            for (int i = 0; i < chars2.Length; i++)
            {
                p &= chars1[i + offset] == chars2[i];
            }
            return p;
        }
    }
}
