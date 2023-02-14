using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace anagrams
{
    public static class CliDesign
    {
        static readonly Thread thread1 = new(UtilsThread.ScreenClock);
        public static void PrepareScreen()
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Clear();

            // disegno della cornice
            //

            //orologio (e timer?) ...ma controllare che il thread non sia già in esecuzione
            thread1.Start();
        }

        public static void ShowScreen()
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Clear();
        }
        public static void ResetScreen()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Clear();

            // terminazione dei thread attivati.
            thread1.Interrupt();
            
        }

    }

}
