using System.Collections.Generic;
using System.Linq;

namespace CodeyBLELibrary
{
    public static class CodeyProtocolSerializer
    {
        public static byte[]  Serialize(CodeyPacket packet)
        {
            List<byte> byteList = new List<byte>();

            packet.Len = (byte)packet.Body.Length;
            packet.LenCheck = (byte) (packet.Len + CodeyPacket.Header);
            packet.CheckSum = (byte)packet.Body.Sum(s => s);

            byteList.Add(CodeyPacket.Header);
            byteList.Add(packet.LenCheck);
            byteList.Add(packet.Len);
            byteList.Add(0x00); //长度占位
            byteList.AddRange(packet.Body);
            byteList.Add(packet.CheckSum);
            byteList.Add(CodeyPacket.EndCode);

            return (byteList.ToArray());
        }
    }
}