﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AOC2
{
    class Day16 : Day
    {


        public Day16()
        {
            SL.printParse = false;
            GetInput(RootFolder + @"2021_16\");
        }
        public const string BLOCK = "\U00002588";
        public override void Main(List<string> Lines)
        {
            Dictionary<string, string> hex2String = new Dictionary<string, string>()
            {
                ["0"] = "0000",
                ["1"] = "0001",
                ["2"] = "0010",
                ["3"] = "0011",
                ["4"] = "0100",
                ["5"] = "0101",
                ["6"] = "0110",
                ["7"] = "0111",
                ["8"] = "1000",
                ["9"] = "1001",
                ["A"] = "1010",
                ["B"] = "1011",
                ["C"] = "1100",
                ["D"] = "1101",
                ["E"] = "1110",
                ["F"] = "1111",
            };
            string startString = Lines.First().List().Select(x => hex2String[x]).ToList().Flat();
            var number = ReadPacket(ref startString);
            Console.WriteLine(number == 180616437720);
            Console.ReadLine();
        }

        private long ReadPacket(ref string bits)
        {
            int ReadInt(ref string subBists, int length) { return Convert.ToInt32(ReadString(ref subBists, length), 2); };
            List<long> numbers = new List<long>();
            int version = ReadInt(ref bits, 3);
            int packet = ReadInt(ref bits, 3);
            string bitsStringLiteral = "";
            if (packet == 4)
            {
                while (true)
                {
                    bool breakNext = ReadString(ref bits, 1) != "1";
                    string block = ReadString(ref bits, 4);
                    if (block.Length < 4)
                    {
                        while (block.Length < 4) { block += "0"; }
                    }
                    bitsStringLiteral += block;
                    if (breakNext) break;
                }
                return Convert.ToInt64(bitsStringLiteral, 2);
            }
            else
            {
                string mode = ReadString(ref bits, 1);
                if (mode == "0")
                {
                    int length = ReadInt(ref bits, 15);
                    string subBits = ReadString(ref bits, length);
                    while (subBits.Length > 0)
                    {
                        numbers.Add(ReadPacket(ref subBits));
                    }
                    return ToNumber(numbers, packet.ToString());
                }
                if (mode == "1")
                {
                    var length = ReadInt(ref bits, 11);
                    for (int i = 0; i < length; i++)
                    {
                        numbers.Add(ReadPacket(ref bits));
                    }
                    return ToNumber(numbers, packet.ToString());
                }
            }
            throw new Exception("");
        }

        public string ReadString(ref string line, int take)
        {
            var first = line.Substring(0, take);
            line = line.Substring(take, line.Length - take);

            return first;
        }

        private long ToNumber(List<long> numbers, string mode)
        {
            if (mode == "0")
            {
                return numbers.Sum();
            }
            if (mode == "1")
            {
                long product = 1;
                foreach (var number in numbers)
                {
                    product *= number;
                }
                return product;
            }
            if (mode == "2")
            {
                return numbers.Min();
            }
            if (mode == "3")
            {
                return numbers.Max();
            }
            if (mode == "5")
            {
                return numbers[0] > numbers[1] ? 1 : 0;
            }
            if (mode == "6")
            {
                return numbers[0] < numbers[1] ? 1 : 0;
            }
            if (mode == "7")
            {
                return numbers[0] == numbers[1] ? 1 : 0;
            }
            throw new Exception("");
        }
    }

}

