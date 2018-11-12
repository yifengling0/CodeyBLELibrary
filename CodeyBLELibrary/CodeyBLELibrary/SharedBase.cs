using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CodeyBLELibrary
{
    public abstract class SharedBase : ICodeyShareable
    {
        public string Name { get; set; }

        /// <summary>
        /// 获取共享数据名字
        /// </summary>
        /// <returns>名字之后的数据索引</returns>
        public int ParseName(byte[] body)
        {
            int index = 1;
            List<byte> byteList = new List<byte>();

            for (index = 1; index < body.Length; index++)
            {
                if (body[index] != 0x00)
                {
                    byteList.Add(body[index]);
                }
                else
                {
                    break;
                }
            }

            Name = Encoding.UTF8.GetString(byteList.ToArray());

            //跳过00
            index++;

            return (index);
        }

        public abstract void Parse(CodeyPacket packet);
        public abstract  byte[] ToArray();
    }
}