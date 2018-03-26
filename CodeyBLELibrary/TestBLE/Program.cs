using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BluetoothLE;

namespace TestBLE
{
    class Program
    {
        static void Main(string[] args)
        {
            GattSampleContext context  = GattSampleContext.Context;

            context.StartEnumeration();

            while (Console.ReadKey().KeyChar !='Q')
            {
                Thread.Sleep(1000);
            }
        }
    }
}
