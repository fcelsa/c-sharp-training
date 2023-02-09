namespace Studio_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // tipi di dati principali

            bool bugiardo = true;
            byte piccolissimo = byte.MaxValue;         // 255
            short piccoloIntero = short.MaxValue;      // da -32768 a 32767
            int normoIntero = int.MaxValue;            // 2147483647
            long grandeIntero = long.MaxValue;         // 9223372036854775807
            float fpNormale = float.MaxValue;          // 3,4028235E+38
            double fpGrande = double.MaxValue;         // 1,7976931348623157E+308
            decimal fpSuperdotato = decimal.MaxValue;  // 79228162514264337593543950335
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

            // casting dei tipi di dati - può essere...
            // implicito   il compilatore fa da solo, un tipo piccolo in uno più grande non è un problema
            normoIntero = 1024 * 256;
            grandeIntero = normoIntero;
            Console.WriteLine("valore di normoIntero è ora " + normoIntero + " e l'ho assegnato a grandeIntero che è un long \n " + 
                "sia l'editor che il runtime non si lamentano perché ci sta dentro");

            // esplicito   normalmente nelle variabili di tipo numerico quando si assegna un valore grande ad un tipo più piccolo
            //             per esempio assegnare un int a short segnala errore e serve un cast esplicito come sotto (le parentesi davanti all'assegnazione)
            //             attenzione il casting è circolare, es. 
            piccoloIntero =(short)grandeIntero;
            Console.WriteLine("piccoloIntero ora vale: " + piccoloIntero + "  e qui si vede cosa significa circolare...");

            float fpz = 65.39f;
            int pz = (int)fpz;
            Console.WriteLine("Il caso più frequente di casting, troncare i decimali, fpz vale " + fpz + " e con 'int pz = (int)fpz;' pz vale " + pz);
             
            // conversioni specifiche   un metodo di conversione specifico serve quando i tipi sono totalmente diversi fra se, per esempio stringhe
            // anche perché metodi di conversione sbagliati potrebbero sollevare un eccezione come qui sotto
            //int i2 = int.MaxValue;
            //short s2 = Convert.ToInt16(i2);

            int i3 = 122;
            double d3 = 112.52;
            string stringa = Convert.ToString(i3) + " " + Convert.ToString(d3);
            Console.WriteLine(stringa);


            // stringhe
            // si possono racchiudere fra ' oppure ""  il che può essere utile per usarle nella stringa stessa,
            // ma è sempre preferibile il metodo escape dei caratteri, per esempio \n è il ritorno a capo e \" serve per fare i doppi apici 

            // escape caratteri speciali
            Console.WriteLine("segue una citazione: \n\"ricordati che devi morire\"");

            // concatenazione e formattazione di stringhe e variabili
            // concat    è con il + come ho usato sopra, fino a qui...
            // format    come qui sotto, il metodo che odio...
            string stringa2 = string.Format("Io sono un intero {0}  ed io sono un double {1}  ed io un float {2}", i3, d3, fpz);
            Console.WriteLine(stringa2);   // ovviamente senza string.format quanto sopra poteva essere passato diretto a Console.WriteLine...

            // interpolazione
            // si fa con $ prima di " e si racchiudono le variabili fra {} ...ed è il metodo che preferisco, come qui sotto:
            sbyte fbMin = sbyte.MinValue;
            sbyte fbMax = sbyte.MaxValue;
            string ilPipponeR1 = $"il tipo byte, che al massimo vale {piccolissimo}, ha un fratello che si chiama sbyte";
            string ilPipponeR2 = $"che ha il segno, e va da {fbMin} a {fbMax}...";
            string ilPipponeR3 = $"Mentre gli altri tipi hanno sorelle e cugini a volontà ! LoL :-)";
            Console.WriteLine($"qui si vede interpolazione dell'interpolazione \n{ilPipponeR1}\n{ilPipponeR2}\n{ilPipponeR3}\n");


            #region multiline verbatim string literal

            var stringone = $"""
Questo è uno stringone che supporta ritorno a capo come qui
e non interpreta gli escape /n \n \\\\ \\\\&&& ci si mette che ci pare...
caratteri speciali senza necessità di escape fra le tre virgolette in cima ed in fondo (su linea separata)
si può scrivere ed andare a capo come e dove ci pare, supporta comunque l'interpolazione, ed è il caso
di ricordare che fra le graffe ci può essere anche un espressione od una funzione, non solo nomi di variabili, per es.
segue l'espressione per generare una stringa di apici ripetuta 10 volte :{new string('"', 10)}   
ora di seguito metto il risultato di fbMax: {fbMax} e proseguo con fpz ed pz: {fpz} - {pz} 

""";

            Console.WriteLine(stringone);

            #endregion

            // Verbatim literal non interpreta gli escape.
            var path = $@"C:\\asdc\asdf";
            // serve soprattutto per questo:
            string query = @"SELECT foo, bar FROM table WHERE id = 42";
            Console.WriteLine(query);

            string name = "Mark";
            var date = DateTime.Now;

            Console.WriteLine("Hello, {0}! Today is {1}, it's {2:HH:mm} now.", name, date.DayOfWeek, date);

            Console.WriteLine($"Hello, {name}! Today is {date.DayOfWeek}, it's {date:HH:mm} now.");

            Console.WriteLine($"|{"Left",-7}|{"Right",7}|");

            const int FieldWidthRightAligned = 20;
            Console.WriteLine($"{Math.PI,20} - default formatting of the pi number");
            Console.WriteLine($"{Math.PI,FieldWidthRightAligned:F3} - display only three decimal digits of the pi number");

            // esempi per dichiarare array di stringhe o altri oggetti non numerici
            string[] arrayDiStr = { "John", "James", "Joan", "Jamie" };
            var arrayDiStr2 = new string[] { "testo1", "testo2", "testo3", "testo4" };


            // string ha diversi metodi, toUpper toLower Length indexing IndexOf Substring e molti altri...
            // la string è sostanzialmente un array di char e quindi può essere indicizzata com []
            // esempi...

            Console.WriteLine(ilPipponeR1.ToUpper());
            Console.WriteLine(ilPipponeR1.Length);
            Console.WriteLine(ilPipponeR1[10]);            //indexing
            int ultimoCarIDX = ilPipponeR1.Length - 1;
            Console.WriteLine(ilPipponeR1[ultimoCarIDX]);
            Console.WriteLine($"Metodo veloce per avere l'ultimo carattere stringa {ilPipponeR1[^1]} ^n significa l'ennesimo carattere a partire dalla fine o da destra");
            Console.WriteLine(ilPipponeR1.IndexOf(','));   //indice della prima occorrenza del carattere cercato in argomento del metodo.
            Console.WriteLine(ilPipponeR1.Substring(12));  //intellicode tende a far semplificare con stringa[12..]

            // numeri    le solite cose normali con gli operatori e le parentesi, ricordare solo che il resto della divisione (modulo) si fa con %
            // vedi poi i metodi della classe Math.
            // inoltre dobbiamo imparare a memoria:
            // incrementi, decrementi ed assegnazioni.
            int a = 1;
            ++a; Console.WriteLine(a);
            int b = a++;
            Console.WriteLine(b);  // operatori ++ e -- per incremento e decremento, se davanti esegue l'incremento prima della riassegnazione IMPORTANTE
            a += 5;  // questa è un assegnazione, è come fare a = a +5

            // Parsing dei numeri in stringhe:
            string s1 = "16";
            string s2 = "8";

            int np = Int32.Parse(s1) + Int32.Parse(s2);
            Console.WriteLine(np);

            // edit from mac 8 Feb 12:29
            Console.WriteLine($"il valore di a qui ora è 8 --> {a} mentre il valore di b è 2 --> {b}");
            Console.WriteLine($"questo perché b ha ricevuto il valore di a prima che fosse incrementato ulteriormente (a++) e mentre a veniva assegnato su b " +
                $"quando ancora valeva 2 a++ incrementa di 1 solo dopo assumendo il valore di 3 a cui poi è stato sommato 5... totale 8");

        KEYINPUTLOOP:

            Console.WriteLine("Ora divertiamoci con il cursore e altri tasti...");

            while (true)
            {
                var ch = Console.ReadKey(false).Key;
                switch (ch)
                {
                    case ConsoleKey.Escape:
                        Console.WriteLine(ch);
                        break;
                    case ConsoleKey.Enter:
                        Console.Clear();
                        Console.Write(ch);
                        goto KEYINPUTLOOP;
                    case ConsoleKey.UpArrow:
                        Console.Write(ch);
                        goto KEYINPUTLOOP;
                    case ConsoleKey.DownArrow:
                        Console.Write(ch);
                        goto KEYINPUTLOOP;
                }

                Console.WriteLine("Programma terminato... bye... ");
                break;
            }
            

        }
    }
}