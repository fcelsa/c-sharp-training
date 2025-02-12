using System;
using static System.Console;
using System.Threading;

namespace threadtest1
{
    internal class Program
    {
        static List<string> lista = new List<string>(1000);

        static void Main(string[] args)
        {
            Thread thread1 = new Thread(new ThreadStart(Loop1));
            Thread thread2 = new Thread(new ThreadStart(Loop2));

            thread1.Start();
            thread2.Start();

            thread1.Join();
            thread2.Join();

            foreach (var line in lista)
            {
                WriteLine(line);
            }

            Console.ReadKey();

        }

        static void Loop1()
        {
            for (int i = 0; i < 100; i++)
            {
                lista.Add("Loop 1: " + i);
            }
        }

        static void Loop2()
        {
            for (int i = 0; i < 100; i++)
            {
                lista.Add("Loop 2: " + i);
            }
        }
        
    }
}
