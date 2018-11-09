using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CodeyBLELibrary
{
    public enum VariableType
    {
        String = 0x0c,
        Int32 = 0x05
    }

    public class SharedVariable : SharedBase
    {
        public VariableType ValueType { private set; get; }
        public string Value { set; get; }

        public override void Parse(CodeyPacket packet)
        {
            Debug.Assert(packet.Body[0] == (int) CodeyPacketType.Variable);

            int index = ParseName(packet.Body);

            List<byte> byteList = new List<byte>();

            //获取变量标识，区分是数值还是字符串
            VariableType valueType = (VariableType)packet.Body[index++];

            // 从后续的标识开始
            for (; index < packet.Body.Length; index++)
            {
                byteList.Add(packet.Body[index]);
            }

            switch (valueType)
            {
                case VariableType.Int32: // littleendian 整形数
                    Value = BitConverter.ToInt32(byteList.ToArray(), 0).ToString();
                    break;
                case VariableType.String: // 字符串
                    Value = Encoding.UTF8.GetString(byteList.ToArray());
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }
        }

        public override byte[] ToPacket()
        {
            throw new System.NotImplementedException();
        }
    }
}