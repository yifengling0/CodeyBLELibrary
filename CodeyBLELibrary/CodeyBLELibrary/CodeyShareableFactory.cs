using System.Diagnostics;

namespace CodeyBLELibrary
{
    public class CodeyShareableFactory
    {
        public static ICodeyShareable Generate(CodeyPacket packet)
        {
            ICodeyShareable ret = null;

            switch ((CodeyPacketType) packet.Body[0])
            {
                case CodeyPacketType.Variable:
                    ret = new SharedVariable();
                    break;
                case CodeyPacketType.Message:
                    ret = new BroadcastMessage();
                    break;
                case CodeyPacketType.Heartbeat:
                    ret = new Heartbeat();
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }

            ret.Parse(packet);

            return (ret);
        }
    }
}