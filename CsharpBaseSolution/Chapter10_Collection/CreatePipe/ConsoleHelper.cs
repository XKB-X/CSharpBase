using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter10_Collection.CreatePipe
{
    public class ConsoleHelper
    {
        private static object syncObject = new object();
        public static void WriteLine(string message)
        {
            lock (syncObject)
            {
                Console.WriteLine(message);
            }
        }

        public static void WriteLine(string message, string color)
        {
            lock (syncObject)
            {
                Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), color);
                Console.WriteLine(message);
                Console.ResetColor();
            }
        }
    }
}
