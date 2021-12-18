using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishLibrary
{

    public class Packet
    {
        public int Version;
        public int PacketId;
        public long Number;
        public List<Packet> SubPackets = new List<Packet>();

        public bool Literal()
        {
            return PacketId == 4;
        }
        public Packet(int version, int packetId, long number)
        {
            Version = version;
            PacketId = packetId;
            Number = number;
        }

        public Packet(int version, int packetId, List<Packet> subPackets)
        {
            Version = version;
            PacketId = packetId;
            SubPackets = subPackets;
        }

        public override string ToString()
        {
            if (Literal()) return "(" + Number + ")";
            else
            {
                return String.Join("U+002CU+0020", SubPackets.Select(x => x.ToString()));
            }
        }
    }
}
