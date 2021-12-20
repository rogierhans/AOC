using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AOC2
{
    class Day2018_12 : Day
    {


        public Day2018_12()
        {
            SL.printParse = false;
            GetInput(RootFolder + @"2018_12\");
        }
        public const string BLOCK = "\U00002588";
        public override void Main(List<string> Lines)
        {

            var state = Lines.FindPatterns("initial state: {0}", x => x.List().Select(y => y == "#" ? 1 : 0).ToList()).First();
            state.Insert(0, 0);
            state.Insert(0, 0);
            state.Insert(0, 0);
            state.Insert(0, 0);
            state.Add(0);
            state.Add(0);
            state.Add(0);
            state.Add(0);
            Console.WriteLine(state.Select(x => x.ToString()).ToList().Flat());
            var rules = Lines.FindPatterns("{0} => {1}", x => x.List(), x => x);
            Console.WriteLine(1 << 5);
            var rulesDict = new int[1 << 5];
            foreach (var (from, to) in rules)
            {
                var key = Convert.ToInt32(from.Select(y => y == "#" ? "1" : "0").ToList().Flat(), 2);
                //Console.WriteLine(key);
                rulesDict[key] = to == "#" ? 1 : 0;
            }

            while (true)
            {
                List<int> newstate = new List<int>();
                newstate.Insert(0, 0);
                newstate.Insert(0, 0);
                newstate.Insert(0, 0);
                newstate.Insert(0, 0);
                bool start = false;
                for (int i = 2; i < state.Count - 2; i++)
                {
                    int key = Converter(state, i);
                  //  Console.WriteLine(key);
                    var next = rulesDict[key];
                    if(next==1) start = true;
                    if(start)
                        newstate.Add(next);
                }
                newstate.Add(0);
                newstate.Add(0);
                newstate.Add(0);
                newstate.Add(0);
                while (newstate[newstate.Count - 1] == 0 && newstate[newstate.Count - 2] == 0 && newstate[newstate.Count - 3] == 0 && newstate[newstate.Count - 4] == 0 && newstate[newstate.Count - 5] == 0) {
                    newstate.RemoveAt(newstate.Count - 1);
                }
                if (state.Select(x=> x.ToString()).ToList().Flat() == newstate.Select(x => x.ToString()).ToList().Flat()) break;
                state = newstate;
               // newstate.Select(x => x == 1 ? "#" : ".").ToList().Print(" ");
                //Console.WriteLine();
                //Console.WriteLine();
                //Console.ReadLine();
            }
            Console.WriteLine(state.Where(x => x > 0).Count());
            Console.ReadLine();
        }

        public int Converter(List<int> state, int middle)
        {
            ///Console.WriteLine(middle + "<- index " + state.Count);
            int number = 0;
            for (int i = 0; i < 5; i++)
            {
                number |= state[middle + 2] << 0;
                number |= state[middle + 1] << 1;
                number |= state[middle] << 2;
                number |= state[middle - 1] << 3;
                number |= state[middle - 2] << 4;
            }

            return number;
        }






      
    }
}