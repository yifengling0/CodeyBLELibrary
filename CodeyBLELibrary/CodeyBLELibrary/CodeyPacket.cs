namespace CodeyBLELibrary
{
    public class CodeyPacket
    {
        /// <summary>
        /// Header 程小奔固定0xf3 ？
        /// </summary>
        public const byte Header = 0xf3;

        /// <summary>
        /// 包长度的校验码 = f3+len
        /// </summary>
        public byte LenCheck { set; get; }

        public byte Len { set; get; }

        public byte[] Body { set; get; }

        /// <summary>
        /// Body的所有字节相加之和
        /// </summary>
        public byte CheckSum { set; get; }

        public const byte EndCode = 0xf4;
    }
}