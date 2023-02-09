using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;

namespace iso6346_check

//CKISO6346:
//'// originally take from this Visual Basic code:
//'/*
//'Function ISO6346Check(k As String) ' Calculates the ISO Shipping Container Check Digit
//' Dim i%, s&
//' For i = 1 To 10
//'  s = s + IIf(i < 5, Fix(11 * (Asc(Mid(k, i)) - 56) / 10) + 1, Asc(Mid(k, i)) - 48) * 2 ^ (i - 1)
//' Next i
//' ISO6346Check = (s - Fix(s / 11) * 11) Mod 10
//'End Function

// to get info about: https://www.bic-code.org/

{
    internal class Program
    {
        // Calculates the ISO Shipping Container Check Digit
        // for example MRKU245518-9
        static void Main(string[] args)
        {
            Console.BackgroundColor= ConsoleColor.Blue;
            Console.ForegroundColor= ConsoleColor.White;
            Console.Clear();
            
            string cntrNumber;

            while (true)
            {

                if (args.Length != 0)
                {
                    cntrNumber = args[0];
                }
                else
                {
                    Console.WriteLine("Inserire il codice di un container:\r");
                    cntrNumber = Console.ReadLine()!;
                }

                if (string.IsNullOrEmpty(cntrNumber) || cntrNumber.Length < 11 || cntrNumber.Length > 12 || !char.IsAsciiDigit(cntrNumber[cntrNumber.Length - 1]))
                {
                    Console.WriteLine($"Codice container non valido");
                }
                else
                {
                    break;
                }
            }

            var serialPart = cntrNumber[..10].ToUpper();
            var checkDigit = Int16.Parse(cntrNumber.Substring(cntrNumber.Length -1, 1));

            Console.WriteLine(serialPart+ "     --> " + checkDigit);


            if (checkDigit == CkIso6346(serialPart))
            {
                Console.WriteLine($"il container {cntrNumber} è valido\r");
            }
            else
            {
                Console.WriteLine($"numero di container errato! ({cntrNumber})");
            }

            Console.ReadKey();
            Console.ResetColor();
            Console.Clear();
            return;


            static int CkIso6346(string cntr)
            {
                cntr = cntr.ToUpper();
                var s = 0;

                for (int i = 1; i <= 10; i++)
                {
                    int e;
                    if (i<5)
                    {
                        e = ((11 * (Utils.Asc(cntr[i-1]) - 56) / 10) + 1) * (int)Math.Pow(2, i - 1);
                    }
                    else
                    {
                        e = ((Utils.Asc(cntr[i-1])) - 48) * (int)Math.Pow(2, i - 1);
                    }
                    s += e;
                    Console.WriteLine(e);
                }

                var chk = (s % 11) % 10;

                Console.WriteLine(chk);

                return chk;

            }
        }
    }
}
