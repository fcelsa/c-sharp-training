namespace Studio_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // tipi di dati e relativo casting

            bool bugiardo = true;
            byte piccolissimo = byte.MaxValue;             // 255
            short piccoloIntero = short.MaxValue;          // da -32768 a 32767
            int normoIntero = int.MaxValue;                // 2147483647
            long grandeIntero = long.MaxValue;             // 9223372036854775807
            float fpNormale = float.MaxValue;              // 3,4028235E+38
            double fpGrande = double.MaxValue;             // 1,7976931348623157E+308
            decimal fpSuperdotato = decimal.MaxValue;      // 79228162514264337593543950335
            string parolaccia = "stocazzo!";

            Console.WriteLine(bugiardo);
            Console.WriteLine(piccolissimo);
            Console.WriteLine(piccoloIntero);
            Console.WriteLine(normoIntero);
            Console.WriteLine(grandeIntero);
            Console.WriteLine(fpNormale);
            Console.WriteLine(fpGrande);
            Console.WriteLine(fpSuperdotato);
            Console.WriteLine(parolaccia);

            // casting dei tipi di dati, che può essere...
            // implicito   il compilatore fa da solo, un tipo piccolo in uno più grande non è un problema
            int ix = 1000 + piccoloIntero;
            Console.WriteLine($"valore di ix è ora {ix} a cui avevo sommato {piccoloIntero} che è uno short \n e ne editor ne runtime si lamentano perché ci sta dentro");

            // esplicito   normalmente nelle variabili di tipo numerico quando si assegna un valore grande ad un tipo più piccolo
            //             per esempio questo short sy = iy; da errore e serve un cast esplicito come sotto (le parentesi davanti all'assegnazione)
            //             attenzione il casting è circolare, nel senso che int è signed quindi assegnando come sotto 65535 ad una short, questa diventa -1  
            int iy = 65535;
            short sy = (short)iy;
            Console.WriteLine(sy);
            // il casting esplicito più usuale: tagliare via i decimali
            float fpz = 65.39f;
            int pz = (int)fpz;
            Console.WriteLine(pz);
            
            // metodo di conversione specifico   questo serve quando i tipi sono totalmente diversi fra se, per esempio stringhe
            // anche perché metodi di conversione sbagliati potrebbero sollevare un eccezione come qui sotto
            //int i2 = int.MaxValue;
            //short s2 = Convert.ToInt16(i2);

            int i3 = 122;
            double d3 = 112.52;
            string stringa = Convert.ToString(i3) + " " + Convert.ToString(d3);
            Console.WriteLine(stringa);


            // stringhe
            // si possono racchiudere fra ' oppure ""  il che può essere utile per...

            // escape caratteri

            // concat come si vede poco sopra con i +
            // format 
            string stringa2 = string.Format("Io sono un intero {0}  ed io sono un double {1}  ed io un float {3}", i3, d3, fpz);
            Console.WriteLine(stringa2);   // ovviamente senza string.format quanto sopra poteva essere passato diretto a Console.WriteLine...


            // interpolazione
            // visibile più sopra con $ prima di " ...ed è il metodo che mi piace di più.





        }
    }
}