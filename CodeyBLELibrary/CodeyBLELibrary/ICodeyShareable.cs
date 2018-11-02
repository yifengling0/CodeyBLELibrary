namespace CodeyBLELibrary
{
    public interface ICodeyShareable
    {
        string Name { set; get; }
        void Parse(CodeyPacket packet);
        byte[] ToPacket();
    }
}