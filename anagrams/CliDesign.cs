using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace anagrams
{
    internal class CliDesign
    {
        public static void PrepareScreen()
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Clear();

            // disegno della cornice
            //

            //orologio (e timer?) ...ma controllare che il thread non sia già in esecuzione
            //var w1 = new UtilsThread();
            //ThreadStart s1 = w1.ScreenClock;
            //var thread1 = new Thread(s1);
            //thread1.Start();

        }

        public static void ShowScreen()
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
        }
        public static void ResetScreen()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Clear();

            // terminazione dei thread attivati.
            // thread1.Interrupt();
            
        }

    }

}
