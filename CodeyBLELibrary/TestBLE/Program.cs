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

            while (Console.ReadKey().KeyChar !='Q')
            {
                Thread.Sleep(1000);
            }
        }

        static void Connect()
        {
            CodeyConnecter.Instance.Connect();
            CodeyConnecter.Instance.CodeyConnected += Instance_CodeyConnected;
        }

        private static void Instance_CodeyConnected(CodeyConnecter sender, EventArgs args)
        {
            var data = new byte[3] {1, 2, 3};
            CodeyConnecter.Instance.WriteDataAsync(data);
        }
    }
}
