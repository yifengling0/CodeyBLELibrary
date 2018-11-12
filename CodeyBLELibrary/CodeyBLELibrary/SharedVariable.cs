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

        public SharedVariable()
        {
        }

        public SharedVariable(string name, Int32 value)
        {
            Name = name;
            SetValue(value);
        }

        public SharedVariable(string name, string value)
        {
            Name = name;
            SetValue(value);
        }

        //设置变量的值
        public void SetValue(Int32 value)
        {
            ValueType = VariableType.Int32;
            Value = value.ToString();
        }

        /// <summary>
        /// 设置变量的值
        /// </summary>
        /// <param name="value"></param>
        public void SetValue(string value)
        {
            ValueType = VariableType.String;
            Value = value;
        }

        public Int32 IntValue()
        {
            if (ValueType == VariableType.Int32)
            {
                return Int32.Parse(Value);
            }
            else
            {
                return Int32.MinValue;
            }
        }

        public override void Parse(CodeyPacket packet)
        {
            Debug.Assert(packet.Body[0] == (int) CodeyPacketType.Variable);

            int index = ParseName(packet.Body);

            List<byte> byteList = new List<byte>();

            //获取变量标识，区分是数值还是字符串
            ValueType = (VariableType)packet.Body[index++];

            // 从后续的标识开始
            for (; index < packet.Body.Length; index++)
            {
                byteList.Add(packet.Body[index]);
            }

            switch (ValueType)
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

        public override byte[] ToArray()
        {
            List<byte> byteList = new List<byte>();

            // 变量
            byteList.Add((byte) CodeyPacketType.Variable);
            byteList.AddRange(Encoding.UTF8.GetBytes(Name));
            byteList.Add(0x00);

            switch (ValueType)
            {
                case VariableType.Int32: // littleendian 整形数
                    var result = Int32.Parse(Value);
                    byteList.Add((byte)VariableType.Int32);
                    byteList.AddRange(BitConverter.GetBytes(result));
                    break;
                case VariableType.String: // 字符串
                    byteList.Add((byte)VariableType.String);
                    byteList.AddRange(Encoding.UTF8.GetBytes(Value));
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }

            return byteList.ToArray();
        }
    }
}