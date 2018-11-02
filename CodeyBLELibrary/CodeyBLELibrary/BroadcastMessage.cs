namespace CodeyBLELibrary
{
    public class BroadcastMessage : ICodeyShareable
    {
        public string Name { get; set; }
        public void Parse(CodeyPacket packet)
        {
            throw new System.NotImplementedException();
        }

        public byte[] ToPacket()
        {
            throw new System.NotImplementedException();
        }
    }
}