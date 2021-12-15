using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AOC2
{
    class Day2018_07 : Day
    {


        public Day2018_07()
        {
            SL.printParse = false;
            GetInput(RootFolder + @"2018_07\");
        }
        public const string BLOCK = "\U00002588";
        public override void Main(List<string> Lines)
        {
            var steps = Lines.FindPatterns("Step {0} must be finished before step {1} can begin.", x => x, x => x);
            steps.Print();
            Dictionary<string, List<string>> dependency = new Dictionary<string, List<string>>();
            Dictionary<string, List<string>> reversedDep = new Dictionary<string, List<string>>();
            HashSet<string> letters = new HashSet<string>();
            foreach (var (a, b) in steps)
            {
                letters.Add(a);
                letters.Add(b);
                dependency[a] = new List<string>();
                reversedDep[a] = new List<string>();
                dependency[b] = new List<string>();
                reversedDep[b] = new List<string>();
            }
            foreach (var (a, b) in steps)
            {
                dependency[a].Add(b);
                reversedDep[b].Add(a);
            }
            List<string> Orders = new List<string>(letters);
            List<string> Finised = new List<string>();
            int t = 0;
            var elves = new List<Elf> { new Elf(), new Elf(), new Elf(), new Elf(), new Elf() };
            while (Finised.Count < letters.Count)
            {
                while (elves.Any(elf => !elf.hasJob) && Orders.Where(x => reversedDep[x].All(g => Finised.Contains(g))).Count() > 0)
                {
                    var elf = elves.First(x => x.Done() && !x.hasJob);
                    var order = Orders.First(x => reversedDep[x].All(g => Finised.Contains(g)));
                    elf.GiveJob(order);
                    Orders.Remove(order);

                }
                foreach (var elf in elves.Where(x => x.hasJob))
                {
                    if (elf.Done())
                    {
                        Finised.Add(elf.job);
                        elf.hasJob = false;
                    }
                }
                elves.ForEach(x => x.Reduce());
                t++;
            }
            Console.WriteLine(t);
        }

        class Elf
        {
            public int timeLeft = 0;
            public bool hasJob = false;
            public string job;
            public void Reduce()
            {

                timeLeft--;
            }
            public bool Done()
            {
                return timeLeft <= 0;
            }
            public void GiveJob(string letter)
            {
                hasJob = true;
                job = letter;
                timeLeft = (int)letter.First() - 'A' + 60;
            }
        }
    }
}