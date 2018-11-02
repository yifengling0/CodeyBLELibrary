namespace CodeyBLELibrary
{
    public class CodeyShareableFactory
    {
        public static ICodeyShareable Create(CodeyPacket packet)
        {
            return new BroadcastMessage();
        }
    }
}