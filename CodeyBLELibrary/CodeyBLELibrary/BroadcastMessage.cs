using System.Diagnostics;

namespace CodeyBLELibrary
{
    public class BroadcastMessage : SharedBase
    {
        public override void Parse(CodeyPacket packet)
        {
            Debug.Assert(packet.Body[0] == (int)CodeyPacketType.Message);

            ParseName(packet.Body);
        }

        public override byte[] ToPacket()
        {
            throw new System.NotImplementedException();
        }
    }
}