using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AOC2
{
    class Day2018_04 : Day
    {


        public Day2018_04()
        {
            SL.printParse = false;
            GetInput(RootFolder + @"2018_04\");
        }
        public const string BLOCK = "\U00002588";
        public override void Main(List<string> Lines)
        {
            var shifts = Lines.FindPatterns("[{0}] Guard #{1} begins shift", DateTime.Parse, int.Parse);
            var asleep = Lines.FindPatterns("[{0}] falls asleep", DateTime.Parse);
            var wakes = Lines.FindPatterns("[{0}] wakes up", DateTime.Parse);
            HashSet<int> Guards = new HashSet<int>();
            List<(DateTime, int)> Events = new List<(DateTime, int)>();
            foreach (var (date, guardID) in shifts)
            {
                Guards.Add(guardID);
                Events.Add((date, guardID));
            }
            foreach (var date in asleep)
            {
                Events.Add((date, -1));
            }
            foreach (var date in wakes)
            {
                Events.Add((date, -2));
            }
            Events = Events.OrderBy(x => x.Item1).ToList();
            (int id, (int bestMinute, int totalMinutes)) = Guards.Select(x => (x, FindMinute(x, Events))).MaxItem(x => x.Item2.Item1);
            Console.WriteLine("{0} {1} {2}", totalMinutes, id, totalMinutes * id);

        }
        private (int, int) FindMinute(int currentId, List<(DateTime, int)> Events)
        {
            int numberOfMinutes = -1;
            int minute = -1;
            int[] countMin = new int[60];
            int currentID = -3;
            DateTime currentSleep = new DateTime();
            foreach (var (date, id) in Events)
            {
                if (id >= 0) currentID = id;
                if (id == -1) { currentSleep = date; }
                if (id == -2 && currentID == currentId)
                {
                    for (int i = currentSleep.Minute; i < date.Minute; i++)
                    {
                        countMin[i]++;
                    }
                }
            }
            for (int i = 0; i < 60; i++)
            {
                if (numberOfMinutes < countMin[i])
                {
                    minute = i;
                    numberOfMinutes = countMin[i];
                }
            }
            var best = countMin.Select((result, index) => (result, index)).MaxItem(x => (double)x.Item1);
            return best;
        }
    }

}

