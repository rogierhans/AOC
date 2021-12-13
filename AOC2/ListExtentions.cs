using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2
{
    public static class SL
    {
        public static T MaxItem<T>(this IEnumerable<T> list, Func<T, double> f)
        {
            var item = list.First();
            var bestNumber = double.MinValue;
            foreach (var element in list)
            {
                var number = f(element);
                if (bestNumber < number) {
                    bestNumber = number;
                    item = element;
                }
            }
            return item;
        }

        public static List<List<R>> ZipGrid<T, P, R>(this List<List<T>> list, List<List<P>> list2, Func<T, P, R> f)
        {
            return list.Zip(list2, (row1, row2) => row1.Zip(row2, (cell1, cell2) => f(cell1, cell2)).ToList()).ToList();
        }
        public static List<List<T>> ReverseV<T>(this List<List<T>> list)
        {
            var newList = new List<List<T>>();

            for (int i = list.Count - 1; i >= 0; i--)
            {
                newList.Add(list[i]);
            }

            return newList;
        }

        public static List<List<T>> ReverseH<T>(this List<List<T>> list)
        {
            var newList = new List<List<T>>();

            foreach (var line in list)
            {
                var newline = new List<T>(line);
                line.Reverse();
                newList.Add(newline);
            }

            return newList;
        }
        public static List<R> Product<T1, T2, R>(this IEnumerable<T1> list1, IEnumerable<T2> list2, Func<T1, T2, R> f)
        {
            List<R> newList = new List<R>();
            int length = Math.Min(list1.Count(), list2.Count());
            foreach (var element1 in list1) {
                foreach (var element2 in list2) {
                    newList.Add(f(element1, element2));
                }
            }
            return newList;
        }


        public static bool printParse = true;


        public static int Mod(int number, int max)
        {
            return (number % max + max) % max;
        }
        public static List<List<T>> DeepCopy<T>(this List<List<T>> oldCells)
        {
            return oldCells.Select(x => new List<T>(x)).ToList();

        }

        public static List<List<T>> MakeList<T>(int height, int width, T baseObject)
        {
            List<List<T>> list = new List<List<T>>();
            for (int i = 0; i < height; i++)
            {
                List<T> subList = new List<T>();
                for (int j = 0; j < width; j++)
                {
                    subList.Add(baseObject);
                }
                list.Add(subList);
            }
            return list;
        }

        public static void Line()
        {
            if (printParse) Console.WriteLine("#############################################");
        }

        public static void Log(string txt)
        {
            if (printParse) Console.WriteLine(txt);
        }
        public static void Log(object o)
        {
            if (printParse) Console.WriteLine(o);
        }
        public static List<List<T>> Transpose<T>(this List<List<T>> oldList)
        {
            List<List<T>> newlist = new List<List<T>>();

            for (int j = 0; j < oldList[0].Count; j++)
            {
                List<T> list = new List<T>();
                for (int i = 0; i < oldList.Count; i++)
                {
                    list.Add(oldList[i][j]);
                }
                newlist.Add(list);
            }

            return newlist;
        }

        public static List<List<I>> GridSelect<T, I>(this List<List<T>> oldList, Func<T, I> func)
        {
            return oldList.Select(x => x.Select(y => func(y)).ToList()).ToList();
        }

        public static int GridSum<T>(this List<List<T>> oldList, Func<T, int> func)
        {
            return oldList.Sum(x => x.Sum(y => func(y)));
        }

        public static List<long> GetNumbers(long from, long to)
        {
            List<long> list = new List<long>();
            for (long i = from; i < to; i++)
            {
                list.Add(i);
            }
            return list;
        }

        public static List<string> List(this string line)
        {
            return line.ToCharArray().Select(x => x.ToString()).ToList();
        }

        public static void Print<T>(this IEnumerable<T> oldList, string seperator = "")
        {
            string line = string.Join(seperator, oldList);
            Console.WriteLine(line);
        }

        public static void Print<T>(this IEnumerable<IEnumerable<T>> list, string seperator = "")
        {
            foreach (var oldLine in list)
            {
                string line = string.Join(seperator, oldLine);
                Console.WriteLine(line);
            }
        }

        public static List<List<T>> SubSelect<T>(this List<List<T>> list, List<(int, int)> neighbors, Func<T, T> f)
        {
            var newList = list.DeepCopy();
            foreach (var neighbor in neighbors)
            {
                newList[neighbor.Item1][neighbor.Item2] = f(list[neighbor.Item1][neighbor.Item2]);
            }
            return newList;
        }

        public static List<(int, int)> Neighbor4<T>(this List<List<T>> list, int i, int j)
        {
            List<(int, int)> neighborList = new List<(int, int)>();
            foreach (var offset in new List<(int, int)>() { (-1, 0), (1, 0), (0, -1), (0, 1) })
            {
                int newI = i + offset.Item1;
                int newJ = j + offset.Item2;

                bool outOfRow = newI < 0 || newI >= list.Count;
                bool outOfColumn = newJ < 0 || newJ >= list[0].Count;
                if (!outOfColumn && !outOfRow && !(i == newI && j == newJ))
                {
                    neighborList.Add((newI, newJ));
                }

            }
            return neighborList;
        }
        public static List<(int, int)> Neighbor8<T>(this List<List<T>> list, int i, int j, bool includeSelf = false)
        {
            return (NeighborList(i, j, -1, 1, -1, 1, list.Count, list[0].Count, includeSelf));
        }

        public static List<(int, int)> NeighborList<T>(this List<List<T>> list, int i, int j, int minI, int maxI, int minJ, int maxJ, bool includeSelf = true)
        {
            return (NeighborList(i, j, minI, maxI, minJ, maxJ, list.Count, list[0].Count, includeSelf));
        }

        public static List<(int, int)> NeighborList(int i, int j, int minI, int maxI, int minJ, int maxJ, int width, int height, bool includeSelf = true)
        {
            List<(int, int)> neighborList = new List<(int, int)>();
            for (int offsetI = minI; offsetI < maxI; offsetI++)
            {
                for (int offsetJ = minJ; offsetJ < maxJ; offsetJ++)
                {
                    int newI = i + offsetI;
                    int newJ = j + offsetJ;

                    bool outOfRow = newI < 0 || newI >= width;
                    bool outOfColumn = newJ < 0 || newJ >= height;
                    if (!outOfColumn && !outOfRow && !(!includeSelf && i == newI && j == newJ))
                    {
                        neighborList.Add((newI, newJ));
                    }
                }
            }


            return neighborList;
        }
    }
}
