using System.Collections.Generic;
using System.Linq;

namespace CodeyBLELibrary
{
    public class CodeyProtocolParser
    {
        public enum State
        {
            StartFirst,
            LenCheck,
            Len,
            Body,
            CheckSum,
            End,
            None
        }

        private CodeyPacket _packet = null;
        private int _dateCnt;
        private State _state = State.None;

        /// <summary>
        ///     不断的放入数据，如果数据可以凑成一个句子，就返回true
        /// </summary>
        /// <returns></returns>
        public bool PushData(byte data)
        {
            var ret = false;
            switch (_state)
            {
                case State.None:
                    if (data == CodeyPacket.Header)
                    {
                        _state = State.StartFirst;
                        _packet = new CodeyPacket();
                    }
                    break;
                case State.StartFirst:
                    _state = State.LenCheck;
                    _packet.LenCheck = data;
                    break;
                case State.LenCheck:
                    _state = State.Len;
                    _packet.Len = data;
                    if ((byte)(_packet.Len + CodeyPacket.Header) == _packet.LenCheck)
                    {
                        _packet.Body = new byte[_packet.Len];
                        _state = State.Len;
                        _dateCnt = 0;
                    }
                    else
                    {
                        _state = State.None;
                    }
                    break;
                case State.Len:
                    // 这部分应该是长度的一部分
                    _state = State.Body;
                    break;
                case State.Body:
                    _packet.Body[_dateCnt] = data;
                    if (_dateCnt >= _packet.Len-1)
                    {
                        _state = State.CheckSum;
                    }
                    _dateCnt++;
                    break;
                case State.CheckSum:
                    if (data == CheckSum(_packet.Body))
                    {
                        _packet.CheckSum = data;
                        _state = State.End;
                    }
                    else
                    {
                        _state = State.None;
                    }
                    break;
                case State.End:
                    if (data == CodeyPacket.EndCode)
                    {
                        ret = true;
                    }
                    _state = State.None;
                    break;
            }

            return ret;
        }

        private byte CheckSum(byte[] packetBody)
        {
            var result = (byte) packetBody.Sum(s=>s);

            return (result);
        }

        public CodeyPacket GetPacket()
        {
            return (_packet);
        }
    }
}