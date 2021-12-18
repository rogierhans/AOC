using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AOC2
{
    class Day2018_09 : Day
    {


        public Day2018_09()
        {
            SL.printParse = false;
            GetInput(RootFolder + @"2018_09\");
        }
        public const string BLOCK = "\U00002588";
        public override void Main(List<string> Lines)
        {
           (long players, long points) = Lines.First().Pattern("{0} players; last marble is worth {1} points",long.Parse, long.Parse);

            points = points * 100;

            LinkedList<long> list = new LinkedList<long>();
            var current = list.AddFirst(0);
            long[] playerPoints =new long[players];
            long i = 1;
            long currentPlayerIndex = 0;
            while (i <  points)
            {

                if (i % 23 == 0) {
                    long total = i;
                    for (long j = 0; j < 7; j++)
                    {
                        current = current.CyclePrev();
                    }
                    current = current.CycleNext(); 
                    total += current.CyclePrev().Value;
                    list.Remove(current.CyclePrev());

                    playerPoints[currentPlayerIndex] += total;
                }
                else
                {
                    current = current.CycleNext();
                    current = list.AddAfter(current,i);
                }
                // list.ToList().Print(" ");
               // playerPoints.ToList().Print(" ");

                i++;
                currentPlayerIndex = (currentPlayerIndex + 1) % players;
            }
            Console.WriteLine(playerPoints.Max());

            Console.ReadLine();
        }

    }
}