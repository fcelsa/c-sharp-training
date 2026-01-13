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
        static Thread? thread1 = null;
        public static void PrepareScreen()
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Clear();

            // disegno della cornice
            //

            // orologio (e timer?) ... avvia il thread solo se non è già in esecuzione
            if (thread1 == null || !thread1.IsAlive)
            {
                thread1 = new Thread(UtilsThread.ScreenClock)
                {
                    IsBackground = true
                };
                thread1.Start();
            }
        }

        public static void ShowScreen()
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Clear();
        }
        public static void ResetScreen()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Clear();

            // terminazione dei thread attivati.
            if (thread1 != null && thread1.IsAlive)
            {
                try
                {
                    thread1.Interrupt();
                }
                catch { }
            }
            thread1 = null;
            
        }

    }

}
