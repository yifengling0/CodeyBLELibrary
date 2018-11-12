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
            Connect();

            while (true)
            {
                var data = Console.ReadKey().KeyChar;

                if (data != 'Q')
                {
                    Thread.Sleep(1000);

                    if (data == 'u')
                    {
                        Codey.Instance.SetValue("pc", 1000);
                    }

                    if (data == 'y')
                    {
                        Codey.Instance.SendMessage("pc_msg");
                    }
                }
                else
                {
                    break;
                }
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
            Codey.Instance.VariableValueChanged += Instance_VariableValueChanged;
            Codey.Instance.MessageReceived += Instance_MessageReceived;
        }

        private static void Instance_MessageReceived(Codey sender, MessageReceivedArgs args)
        {
            Console.WriteLine("msg:" + args.Message.Name);
        }

        private static void Instance_VariableValueChanged(Codey sender, VariableValueChangedArgs args)
        {
            Console.WriteLine(args.Variable.Name + ":" + args.Variable.Value);
        }

        private static void Instance_CodeyConnected(Codey sender, EventArgs args)
        {
            
        }
    }
}
