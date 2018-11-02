namespace CodeyBLELibrary
{
    public class SharedVariable : ICodeyShareable
    {
        public string Name { get; set; }
        public string Value { set; get; }

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