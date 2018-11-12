using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CodeyBLELibrary
{
    public class BroadcastMessage : SharedBase
    {
        public BroadcastMessage()
        {

        }

        public BroadcastMessage(string name)
        {
            Name = name;
        }

        public override void Parse(CodeyPacket packet)
        {
            Debug.Assert(packet.Body[0] == (int)CodeyPacketType.Message);

            ParseName(packet.Body);
        }

        public override byte[] ToArray()
        {
            List<byte> byteList = new List<byte>();

            // 消息
            byteList.Add((byte)CodeyPacketType.Message);
            byteList.AddRange(Encoding.UTF8.GetBytes(Name));
            byteList.Add(0x00);

            return byteList.ToArray();
        }
    }
}