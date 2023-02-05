using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;

namespace iso6346_check
{
    internal class Program
    {
        // Calculates the ISO Shipping Container Check Digit
        // for example MRKU245518-9
        static void Main(string[] args)
        {
            var cntrNumber = string.Empty;

            if (args.Length != 0)
            {
                cntrNumber = args[0];
            }
            else
            {
                Console.WriteLine("Inserire il codice di un container:\r");
                cntrNumber = Console.ReadLine().ToUpper();
            }


            if (CkIso6346(cntrNumber))
            {
                Console.WriteLine($"il container {cntrNumber} è valido\r");
            }
            else
            {
                Console.WriteLine($"numero di container errato! ({cntrNumber})");
            }

            Console.ReadKey();


            static bool CkIso6346(string cntr)
            {
                //cntr = " " + cntr.ToUpper();
                cntr = cntr.ToUpper();
                var s = 0;

                for (int i = 1; i <= 10; i++)
                {
                    int e;
                    if (i<5)
                    {
                        e = ((11 * (Asc(cntr[i-1]) - 56) / 10) + 1) * (int)Math.Pow(2, i - 1);
                    }
                    else
                    {
                        e = ((Asc(cntr[i-1])) - 48) * (int)Math.Pow(2, i - 1);
                    }
                    s += e;
                    Console.WriteLine(e);
                }

                var chk = s % 11;

                Console.WriteLine(chk);

                //// cast implici   to
                //short s1 = short.MaxValue;
                //int i1 = 123 + s1;

                //// cast esplicito
                //// con operatore
                //int i2 = int.MaxValue;
                //short s2 = (short)i2;

                //    // altro
                //    int i3 = int.MaxValue;
                //short s3 = Convert.ToInt16("123");



                ////' Dim i%, s&
                ////' For i = 1 To 10
                ////'  s = s + IIf(i < 5, Fix(11 * (Asc(Mid(k, i)) - 56) / 10) + 1, Asc(Mid(k, i)) - 48) * 2 ^ (i - 1)
                ////' Next i
                ////' ISO6346Check = (s - Fix(s / 11) * 11) Mod 10

                ////    FOR I = 1 TO 10
                ////        IFF I < 5
                ////            EL = (FIX(11 * (ASC(MID$(CKVALUE$, I,1)) -56) / 10 , 0) +1) *2 ^ (I - 1)
                ////         ELSEF
                ////            EL = (ASC(MID$(CKVALUE$, I, 1)) - 48) *(2 ^ (I - 1))
                ////        ENDF
                ////        S = S + EL
                ////    NEXT
                ////    CNTRSTATUS = (S - (FIX(S / 11 , 0) *11)) MOD 10

                return true;

            }
        }




        static int Asc(char String)
        {
            int int32 = Convert.ToInt32(String);
            if (int32 < 128)
                return int32;
            try
            {
                Encoding fileIoEncoding = Encoding.Default;
                char[] chars = new char[1] { String };
                if (fileIoEncoding.IsSingleByte)
                {
                    byte[] bytes = new byte[1];
                    fileIoEncoding.GetBytes(chars, 0, 1, bytes, 0);
                    return (int)bytes[0];
                }
                byte[] bytes1 = new byte[2];
                if (fileIoEncoding.GetBytes(chars, 0, 1, bytes1, 0) == 1)
                    return (int)bytes1[0];
                if (BitConverter.IsLittleEndian)
                {
                    byte num = bytes1[0];
                    bytes1[0] = bytes1[1];
                    bytes1[1] = num;
                }
                return (int)BitConverter.ToInt16(bytes1, 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }

}




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
//'*/
//    S=0:EL = 0
//    FOR I = 1 TO 10
//        IFF I < 5
//            EL = (FIX(11 * (ASC(MID$(CKVALUE$, I,1)) -56) / 10 , 0) +1) *2 ^ (I - 1)
//         ELSEF
//            EL = (ASC(MID$(CKVALUE$, I, 1)) - 48) *(2 ^ (I - 1))
//        ENDF
//        S = S + EL
//    NEXT
//    CNTRSTATUS = (S - (FIX(S / 11 , 0) *11)) MOD 10
//    RETURN
//END
