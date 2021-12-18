using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using FishLibrary;
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
            Packet packet = HexaParser.strToPacket(startString);
            var result = ToNumber(packet);
            Console.WriteLine("Resutls -> " + result);
        }

        public long ToNumber(Packet packet)
        {
            if (packet.Literal()) return packet.Number;
            else
            {
                return ToNumber(packet.SubPackets.Select(ToNumber).ToList(), packet.PacketId);
            }
        }

        private long ToNumber(List<long> numbers, int mode)
        {
            if (mode == 0) return numbers.Sum();
            if (mode == 1) return numbers.Aggregate((a, b) => a * b);
            if (mode == 2) return numbers.Min();
            if (mode == 3) return numbers.Max();
            if (mode == 5) return numbers[0] > numbers[1] ? 1 : 0;
            if (mode == 6) return numbers[0] < numbers[1] ? 1 : 0;
            if (mode == 7) return numbers[0] == numbers[1] ? 1 : 0;
            throw new Exception("");
        }
    }

}

