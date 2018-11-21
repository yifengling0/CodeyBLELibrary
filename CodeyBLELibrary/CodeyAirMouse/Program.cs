using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CodeyBLELibrary;
using Windows.Foundation;

namespace CodeyAirMouse
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
                }
                else
                {
                    break;
                }
            }
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

            //因为小奔自己发消息不能自己接受，所以将消息返回去，而且只能用小写。
            if (args.Message.Name.Equals("Start"))
            {
                Codey.Instance.SendMessage("start");
            }

            if (args.Message.Name.Equals("left"))
            {
                mouse_event((uint)MouseEventFlags.LeftDown, 0, 0, 0, 0);
                mouse_event((uint)MouseEventFlags.LeftUp, 0, 0, 0, 0);
            }
            else if (args.Message.Name.Equals("right"))
            {
                mouse_event((uint)MouseEventFlags.RightDown, 0, 0, 0, 0);
                mouse_event((uint)MouseEventFlags.RightUp, 0, 0, 0, 0);
            }
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);
        //Mouse actions
        public enum MouseEventFlags
        {
            LeftDown = 0x00000002,
            LeftUp = 0x00000004,
            MiddleDown = 0x00000020,
            MiddleUp = 0x00000040,
            Move = 0x00000001,
            Absolute = 0x00008000,
            RightDown = 0x00000008,
            RightUp = 0x00000010
        }


        private static void Instance_VariableValueChanged(Codey sender, VariableValueChangedArgs args)
        {
            //Console.WriteLine(args.Variable.Name + ":" + args.Variable.Value);

            if (args.Variable.Name.Equals("dx"))
            {
                mouse_event((uint)MouseEventFlags.Move, (uint)(args.Variable.IntValue()), 0, 0, 0);
            }
            else if (args.Variable.Name.Equals("dy"))
            {
                mouse_event((uint)MouseEventFlags.Move, 0, (uint)(-args.Variable.IntValue()), 0, 0);
            }

        }

        private static void Instance_CodeyConnected(Codey sender, EventArgs args)
        {
            Console.WriteLine("Codey is connected!");
            Codey.Instance.SetValue("k1", 10);
            Codey.Instance.SetValue("k2", 10);
        }
    }
}
