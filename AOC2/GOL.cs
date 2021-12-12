using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2
{
    static class GOL
    {

        public static List<List<string>> Enlarge(List<List<string>> oldCells, string defaultValue, int enlargeRow, int enlargeColumn)
        {
            List<List<string>> newCells = oldCells.DeepCopy();
            var extraRow = SL.GetNumbers(0, oldCells[0].Count).Select(x => defaultValue).ToList();
            for (int i = 0; i < enlargeRow; i++)
            {
                newCells.Insert(0,extraRow);
                newCells.Add(extraRow);
            }
            newCells = newCells.Select(x =>
            {
                for (int i = 0; i < enlargeColumn; i++)
                {
                    x.Insert(0, defaultValue);
                    x.Add(defaultValue);
                }
                return x;
            }).ToList();
            return newCells;
        }

        public static List<List<string>> Update(List<List<string>> oldCells, Dictionary<List<List<string>>, string> Rules, string defaultValue, int enlargeRow, int enlargeColumn, int centerI = 0, int centerJ = 0, bool Wraparround = false)
        {
            oldCells = Enlarge(oldCells,defaultValue, enlargeRow, enlargeColumn + 2);
            var newCells = oldCells.GridSelect(x => defaultValue);
            int height = oldCells.Count;
            int width = oldCells[0].Count;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    bool isUpdated = false;
                    foreach (var rule in Rules)
                    {

                        bool updated = UpdateRuleCell(oldCells, centerI, centerJ, Wraparround, newCells, height, width, i, j, rule);
                        isUpdated |= updated;
                        if (updated)
                        {
                            // break;
                            //newCells.Print();
                          //  Printer.Log(rule.Key.First().Flat(), rule.Value);
                            //Console.WriteLine("{0} {1}", i, j);
                            //Console.ReadLine();
                        }
                    }
                    if (!isUpdated)
                    {
                        newCells[i][j] = defaultValue;
                    }
                }
            }

            return newCells;
        }

        private static bool UpdateRuleCell(List<List<string>> oldCells, int centerI, int centerJ, bool Wraparround, List<List<string>> newCells, int height, int width, int i, int j, KeyValuePair<List<List<string>>, string> rule)
        {
            List<List<string>> patern = rule.Key;
            string newStatus = rule.Value;
            int paternHeight = patern.Count;
            int paternWidth = patern[0].Count;
            bool outOfRow = i - centerI < 0 || i - centerI + paternHeight > height;
            bool outOfColumn = j - centerJ < 0 || j - centerJ + paternWidth > width;
            // Console.WriteLine();
            bool match = (!outOfRow && !outOfColumn);
            //Printer.Log(i, j, centerI, centerJ , paternHeight,paternWidth, match);
            //Console.ReadLine();
            // if (!outOfRow && !outOfColumn)
            {

               
                //Console.WriteLine(match);
                for (int a = 0; a < paternHeight; a++)
                {
                    for (int b = 0; b < paternWidth; b++)
                    {
                        // Console.WriteLine("{0} {1} {2} {3} {4}", a, b, i, j, oldCells[SL.Mod(i + a - centerI, height)][SL.Mod(j + b - centerJ, width)] == patern[a][b]);
                        if (Wraparround || (!outOfRow && !outOfColumn))
                            match &= oldCells[SL.Mod(i + a - centerI, height)][SL.Mod(j + b - centerJ, width)] == patern[a][b];
                    }
                }
                if (match)
                {
                    newCells[i][j] = newStatus;
                    return true;
                }
            }
            return false;
        }

        public static List<List<string>> UpdateClassic(List<List<string>> oldCells, int paternHeight, int paternWidth, Func<string, int> map, Func<string, int, string> rule, bool skipcenter = true, bool wrap = true)
        {
            List<List<string>> newCells = oldCells.DeepCopy();
            int height = oldCells.Count;
            int width = oldCells[0].Count;
            int centerI = (paternHeight - 1) / 2;
            int centerJ = (paternWidth - 1) / 2;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    int count = 0;
                    for (int a = 0; a < paternHeight; a++)
                    {
                        for (int b = 0; b < paternWidth; b++)
                        {
                            if (!(a == centerI && b == centerJ))
                            {
                                bool outOfRow = i - centerI < 0 || i - centerI + paternHeight > height;
                                bool outOfColumn = j - centerJ < 0 || j - centerJ + paternWidth > width;
                                if (wrap || (!outOfRow && !outOfColumn))
                                {
                                    count += map(oldCells[SL.Mod(a - centerI + i, height)][SL.Mod(b - centerJ + j, width)]);
                                }

                            }
                        }
                    }

                    newCells[i][j] = rule(oldCells[i][j], count);
                    // newCells[i][j] = testCount.ToString();
                }
            }

            return newCells;
        }

    }
}
