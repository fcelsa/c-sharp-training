using System;

namespace testbool
{
    internal class Program
    {
        static bool _ = true;
        static int __ = Convert.ToByte(_) + 64;
        static string ___ = ((char)__ ).ToString();

        static void Main(string[] args)
        {
            Console.WriteLine(___);
        }

    }

}
