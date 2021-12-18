using System;
using System.Collections.Generic;
using System.Linq;
using ParsecSharp;
using FishLibrary;
namespace AOC2
{

    class Day18 : Day
    {


        public Day18()
        {
            SL.printParse = false;
            GetInput(RootFolder + @"2021_18\");
        }
        public const string BLOCK = "\U00002588";
        public override void Main(List<string> Lines)
        {
            var schoolOfFish = Lines.Select(FSharpParser.strToFish).ToList();
            long max = 0;
            for (int i = 0; i < schoolOfFish.Count; i++)
            {
                for (int j = 0; j < schoolOfFish.Count; j++)
                {
                    if (i == j) continue;
                    var megaFish = new Fish(CopyFish(schoolOfFish[i]), CopyFish(schoolOfFish[j]));
                    PutFishInTheOven(megaFish);
                    max = Math.Max(max, Magnitude(megaFish));
                }
            }
            Console.WriteLine(max);
            Console.ReadLine();
        }
        public void PutFishInTheOven(Fish fish)
        {
            bool p = true;
            while (p)
            {
                SetDepth(fish, 0);
                p = false;
                p |= Explode(fish);
                if (!p)
                {
                    p |= LeftMostSplit(fish);
                }
            }

        }
        public long Magnitude(Fish fish)
        {
            if (fish.isLeaf) return fish.Number;
            else
            {
                return Magnitude(fish.left) * 3 + Magnitude(fish.right) * 2;
            }
        }
        public bool LeftMostSplit(Fish fish)
        {
            if (fish.isLeaf)
            {
                if (fish.Number > 9)
                {
                    double halved = (double)fish.Number / 2.0;
                    int leftNumber = (int)Math.Floor(halved);
                    int rightNumber = (int)Math.Ceiling(halved);
                    fish.isLeaf = false;
                    fish.left = new Fish(leftNumber);
                    fish.right = new Fish(rightNumber);
                    return true;
                }
                else return false;
            }
            else
            {
                bool p = LeftMostSplit(fish.left);
                if (p) return p;
                else return LeftMostSplit(fish.right);
            }
        }

        public bool Explode(Fish fish)
        {
            List<Fish> fishOrder = new List<Fish>();
            GetLiterals(fish, fishOrder);
            return ExplodeLeftMost(fish, fishOrder);
        }


        public void GetLiterals(Fish fish, List<Fish> fishes)
        {
            if (fish.isLeaf) fishes.Add(fish);
            else
            {
                GetLiterals(fish.left, fishes);
                GetLiterals(fish.right, fishes);
            }
        }
        public bool ExplodeLeftMost(Fish fish, List<Fish> fishOrder)
        {
            if (fish.isLeaf) return false;
            else if (fish.depth == 4)
            {
                int indexLeft = fishOrder.IndexOf(fish.left);
                if (indexLeft != 0)
                {
                    fishOrder[indexLeft - 1].Number += fish.left.Number;
                }
                int indexRight = fishOrder.IndexOf(fish.right);
                if (indexRight != fishOrder.Count() - 1)
                {
                    fishOrder[indexRight + 1].Number += fish.right.Number;
                }
                BecomeLeaf(fish);
                return true;
            }
            else
            {
                bool p = ExplodeLeftMost(fish.left, fishOrder);
                if (p) return p;
                else return ExplodeLeftMost(fish.right, fishOrder);
            }
        }

        private void BecomeLeaf(Fish fish)
        {
            fish.left = null; fish.right = null; fish.isLeaf = true; fish.Number = 0;
        }

        public Fish CopyFish(Fish fish)
        {
            if (fish.isLeaf) return new Fish(fish.Number);
            else
            {
                return new Fish(CopyFish(fish.left), CopyFish(fish.right));
            }

        }

        public void SetDepth(Fish fish, int d)
        {
            fish.depth = d;
            if (!fish.isLeaf)
            {
                SetDepth(fish.left, d + 1);
                SetDepth(fish.right, d + 1);
            }
        }
    }
}

