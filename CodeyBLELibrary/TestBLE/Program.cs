using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CodeyBLELibrary;

namespace TestBLE
{
    class Program
    {
        static void Main(string[] args)
        {
            //Connect();
            string data = "f3 39 46 00 02 76 61 72 00 0c 31 32 33 34 35 36 37 38 39 30 61 62 63 64 65 66 67 68 69 6a 6b 6c 6d 6e 6f 70 71 72 73 74 75 76 77 78 79 7a 31 32 33 34 35 36 37 38 39 30 61 62 63 64 65 66 67 68 69 6a 6b 6c 6d 6e 6f 70 71 72 fb f4 f3";

            byte[] array = strToToHexByte(data);

            CodeyProtocolParser parser = new CodeyProtocolParser();

            foreach (var b in array)
            {
                if (parser.PushData(b))
                {
                    var packet = parser.GetPacket();
                }
            }
            

            while (Console.ReadKey().KeyChar !='Q')
            {
                Thread.Sleep(1000);
            }
        }


        private static byte[] strToToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

        static void Connect()
        {
            Codey.Instance.Connect();
            Codey.Instance.CodeyConnected += Instance_CodeyConnected;
        }

        private static void Instance_CodeyConnected(Codey sender, EventArgs args)
        {
            //var data = new byte[3] {1, 2, 3};
            //Codey.Instance.WriteDataAsync(data);
        }
    }
}
