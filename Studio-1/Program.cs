namespace Studio_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // tipi di dati e relativo casting

            bool bugiardo = true;
            byte piccolissimo = byte.MaxValue;
            short piccoloIntero = short.MaxValue;
            int normoIntero = int.MaxValue;
            long grandeIntero = long.MaxValue;
            float decimaleNormale = float.MaxValue;
            double decimaleGrande = double.MaxValue;
            decimal decimaleSuperdotato = decimal.MaxValue;
            string parolaccia = "stocazzo!";

            Console.WriteLine(bugiardo);
            Console.WriteLine(piccolissimo);
            Console.WriteLine(piccoloIntero);
            Console.WriteLine(normoIntero);
            Console.WriteLine(grandeIntero);
            Console.WriteLine(decimaleNormale);
            Console.WriteLine(decimaleGrande);
            Console.WriteLine(decimaleSuperdotato);
            Console.WriteLine(parolaccia);

            // casting dei tipi di dati, che può essere...
            // implicito
            int ix = 1000 + piccoloIntero;
            Console.WriteLine($"valore di ix è ora {ix} a cui avevo sommato {piccoloIntero} che è uno short \n e ne editor ne runtime si lamentano perché ci sta dentro");


            
            
            
            // esplicito




            //// cast esplicito
            //// con operatore
            //int i2 = int.MaxValue;
            //short s2 = (short)i2;

            //    // altro
            //    int i3 = int.MaxValue;
            //short s3 = Convert.ToInt16("123");

            // 

        }
    }
}