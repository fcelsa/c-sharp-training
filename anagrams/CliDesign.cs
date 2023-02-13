using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anagrams
{
    internal class CliDesign
    {
        public static void PrepareScreen()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Clear();

            //var w1 = new UtilsThread();
            //ThreadStart s1 = w1.ScreenClock;
            //var thread1 = new Thread(s1);
            //thread1.Start();

        }

        public static void ShowScreen()
        {
            Console.BackgroundColor = ConsoleColor.Black;
        }
        public static void ResetScreen()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Clear();
            thread1.Stop();
        }


    }

}
