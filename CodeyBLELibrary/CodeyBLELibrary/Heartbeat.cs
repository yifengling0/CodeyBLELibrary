using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace CodeyBLELibrary
{
    public class Heartbeat : ICodeyShareable
    {
        public string Name { get; set; } = ":Heartbeat";
        public byte[] Code { set; get; }

        public void Parse(CodeyPacket packet)
        {
            Debug.Assert(packet.Body[0] == (int)CodeyPacketType.Heartbeat);

            List<byte> byteList = new List<byte>();
            // 从后续的标识开始
            for (int index = 1; index < packet.Body.Length; index++)
            {
                byteList.Add(packet.Body[index]);
            }
            
            Code = byteList.ToArray();
        }

        public byte[] ToArray()
        {
            List<byte> byteList = new List<byte>();

            byteList.Add(0x05);
            byteList.Add(0x00);
            byteList.Add(0x00);

            return byteList.ToArray();
        }
    }
}