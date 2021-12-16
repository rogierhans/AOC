using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;


namespace AOC2
{
    static class  Printer
    {
        public static string Flat(this List<string> s, string seperator = "") {
            return string.Join(seperator, s);
        }
        public static void Log(params object[] values) {
            Console.WriteLine(string.Join(" ", values));

        }
        public static void Print<T>(this List<T> oldList, string seperator = "")
        {
            string line = string.Join(seperator, oldList);
            Console.WriteLine(line);
        }

        public static void Print<T>(this List<List<T>> list, string seperator = "")
        {
            foreach (var oldLine in list)
            {
                string line = string.Join(seperator, oldLine);
                Console.WriteLine(line);
            }
        }

        public static void PrintPlusPlus<T>(this List<List<T>> list, string seperator = "")
        {
            List<string> lines = new List<string>();
            foreach (var oldLine in list)
            {
                string line = string.Join(seperator, oldLine);
                lines.Add(line);
            }
            File.WriteAllLines(@"C:\Users\Rogier\Desktop\Print.txt", lines);
            Process myProcess = new Process();
            Process.Start("notepad++.exe", @"C:\Users\Rogier\Desktop\Print.txt");
        }
    }
}
