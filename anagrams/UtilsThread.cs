namespace anagrams
{
    public class UtilsThread
    {
        public static void ScreenClock()
        {
            while(true)
            {
                try
                {
                    Thread.Sleep(1000);
                    var originalX = Console.CursorLeft;
                    var originalY = Console.CursorTop;
                    Console.SetCursorPosition(0, 1);
                    Console.Write(new string(' ', Console.WindowWidth));
                    Console.SetCursorPosition((Console.WindowWidth - 8) / 2, 0);
                    Console.Write("{0:HH:mm:ss}   time elapsed: todo ", DateTime.Now);
                    Console.SetCursorPosition(Console.WindowWidth - 8, 0);
                    //Console.Write(countTimer);
                    Console.SetCursorPosition(originalX, originalY);
                }

                catch (ThreadInterruptedException)

                {
                    break;
                }


            }


        }


    }


}



