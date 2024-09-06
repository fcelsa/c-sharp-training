using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studio_1
{
    internal class PlayArray
    {
        // due modi di definire gli array, modifica + iterazione
        public static string PlayA(int scelta)
        {
            string[] imprecazioni = { "maremma m.", "maremma p.", "maremma c.", "maremma g." };

            // sarebbe da controllare se il parametro passato supera la dimensione perché non si può sforare!
            // imprecazioni[4] darebbe errore 
            return imprecazioni[scelta];

        }

        public static string[] PlayB()
        {
            string[] parolacce = new string[4];
            parolacce[0] = "merda!";
            parolacce[1] = "shit!";
            parolacce[2] = "fuck!";
            parolacce[3] = "cazzo!";

            return parolacce;

        }

        public static bool PlayC(string parolaccia, int position)
        {
            var parolacce = PlayArray.PlayB();
            parolacce[position] = parolaccia;

            foreach (string item in parolacce)
            {
                Console.WriteLine(item);
            }

            return true;

        }

        public static bool PlayMatrici()
        {
            // array multidimensionali o matrice esempi array 2D e 3D
            string[,] codici = new string[3, 2]
            {
                {"ABCD", "0010" },
                {"DDEF", "0020" },
                {"HHKK", "0030" }
            };

            Console.WriteLine("Array 2D prendo riga 2 colonna 1 " + codici[2,1]);
            Console.WriteLine("ricorda: anche per la matrici si parte da 0");
            Console.WriteLine("Esempio di iterazione righe colonne con for:");

            for (int riga = 0; riga < codici.GetLength(0); riga++)
            {
                for (int colonna = 0; colonna < codici.GetLength(1); colonna++)
                {
                    Console.WriteLine("sono in riga " + riga + " e colonna " + colonna );
                    Console.WriteLine("Valore:" + codici[riga, colonna]);
                }
            }

            string[,,] codici3d = new string[3, 2, 2]
            {
                {
                    {"ABCD", "0010" },
                    {"DDEF", "0020" },
                },
                {
                    {"ABCD", "0010" },
                    {"DDEF", "0020" },
                },
                {
                    {"ABCD", "0010" },
                    {"DDEF", "0020" },
                }
            };

            Console.WriteLine("Array 3D prendo riga 1 colonna 1 dim 1 :" + codici3d[1, 1, 1]);
            Console.WriteLine("ricorda: anche per la matrici si parte da 0");
            Console.WriteLine("Esempio di iterazione righe colonne con for:");

            for (int dim1 = 0; dim1 < codici3d.GetLength(0); dim1++)
            {
                for (int dim2 = 0; dim2 < codici3d.GetLength(1); dim2++)
                {
                    for (int dim3 = 0; dim3 < codici3d.GetLength(1); dim3++)
                    {
                        Console.WriteLine("sono in dim1 " + dim1 + " dim2 " + dim2 + " dim3 " + dim3);
                        Console.WriteLine("Valore:" + codici3d[dim1, dim2, dim3]);
                    }
                }
            }

            Console.WriteLine("con il foreach (ovviamente valido anche per gli array 2d) si può ciclare tutta la tabella senza gli indici");
            foreach (var codicesingolo in codici3d) 
            {
                Console.WriteLine(codicesingolo);
            }

            return true;
        }

        public static bool PlayArrayOfArray()
        {
            // array irregolari o array di array o Jagged - la dimensione non è predefinita può avere elementi diversi nelle dimensioni

            int[][] arrayAdMinchiam =
            {
                new int[] { 10, 11, 12, 13,14, 15, 16,17, 18, 19, 20, 21, 22,23, 24, 25,26, 27, 28, 29, 30 },
                new int[] { 1, 2, 3, 4,5, 6, 7,8, 9 },
                new int[] { 100, 1000, 10000, 100000 },
                new int[] { 5, 7, 9 }
            };

            // esempio elemento
            Console.WriteLine(arrayAdMinchiam[2][2]);

            // iterazione come per array dimensionale ma ci basta prendere nomevariabile.Length 
            for (int riga = 0; riga < arrayAdMinchiam.Length; riga++)
            {
                for (int colonna = 0; colonna < arrayAdMinchiam[riga].Length; colonna++)
                {
                    Console.WriteLine("sono in riga " + riga + " e colonna " + colonna);
                    Console.WriteLine("Valore:" + arrayAdMinchiam[riga][colonna]);
                }
            }

            return true;
        }

        public static bool PlayCollection()
        {
            // due modi di difinire una collection (o arraylist)
            // gli array list possono avere tipi differenti ed oggetti differenti anche altri array ecc.

            ArrayList MiaCollezione = new ArrayList();
            MiaCollezione.Add("elemento 1");
            MiaCollezione.Add("elemento 2");

            Console.WriteLine(MiaCollezione.Count);
            Console.WriteLine(MiaCollezione);

            ArrayList MiaCollezione2 = new ArrayList()
            {
                10,20,30,40,50,60,70,80,90,"elemento9", true, null, "elemento12", null
            };

            // ci sono poi vari metodi per manipolare la lista i cui elementi sono oggetti a tutti gli effetti,
            // infatti la possibile iterazione è questa:
            foreach (object item in MiaCollezione2)
            {
                Console.WriteLine(item);
            }

            return true;
        }

        public static bool PlayTheList()
        {
            // le liste possono contenere solo tipi di dati definiti
            // ma la lunghezza può essere indefinita e si possono aggiungere e rimuovere elementi.

            List<string> myList = new List<string>();
            
            string[] arrrayDiStoCazzo = { "sto cazzo", "sta minchia", "sta fava" };
            
            myList.AddRange(arrrayDiStoCazzo);

            myList.Add("sta fregna");

            Console.WriteLine(myList);

            foreach (string item in myList)
            {
                Console.WriteLine(item);
            }

            return true;
        }

        public static bool PlayHashtable() 
        {
            // Le hashtable sono collection con dati chiave - valore
            // si leggono e manipolano solo per chiave, non per indice.

            Hashtable myHashtable = new Hashtable();

            Hashtable myHashtable2 = new Hashtable()
            {
                {"AR", "Arezzo" },
                {"FI", "Firenze" },
                {"SI", "Siena" },
                {"GR", "Grosseto" },
                {"PI", "Pisa"},
                {"LI", "Livorno" },
                {"PO", "Prato" },
                {"PT", "Pistoia" },
                {"LU", "Lucca" },
                {"MS", "Massa" }
            };

            Console.WriteLine(myHashtable2["LU"]);

            foreach (DictionaryEntry citta in myHashtable2)
            {
                Console.WriteLine($"Chiave: {citta.Key}  valore: {citta.Value}");
                Console.WriteLine(myHashtable2.ContainsValue("Grosseto"));
            }

            return myHashtable2.ContainsKey("GR");
        }

        public static bool PlayDictionary()
        {
            // Le dict o dictionary sono collection con dati chiave - valore come le hashtable
            // si leggono e manipolano solo per chiave, non per indice, ma il tipo di dati sia
            // della chiave che del valore devono essere definiti.

            Dictionary<int, string> myDict1 = new Dictionary<int, string>()
            {
                {1, "Arezzo" },
                {2, "Firenze" },
                {3, "Siena" },
                {4, "Grosseto" },
                {5, "Pisa"},
                {6, "Livorno" },
                {7, "Prato" },
                {8, "Pistoia" },
                {9, "Lucca" },
                {10, "Massa" }
            };

            Console.WriteLine(myDict1[9]);

            foreach (KeyValuePair<int,string> id in myDict1)
            {
                Console.WriteLine($"Chiave: {id.Key}  valore: {id.Value}");
            }

            return myDict1.ContainsKey(10);
        }

        public static bool PlayStack()
        {
            // stack: strutture LIFO, per capire il concetto di stack (pila) basta pensare al mazzo di carte da gioco.
            // vari metodi, ma per capire il concetto prova Push Pop Clear ...
            Stack<string> mazzo = new Stack<string>();
            mazzo.Push("2");
            mazzo.Push("3");
            mazzo.Push("4");
            mazzo.Push("5");
            mazzo.Push("6");
            mazzo.Push("7");
            mazzo.Push("Fante");
            mazzo.Push("Regina");
            mazzo.Push("Re");
            mazzo.Push("Asso");

            mazzo.Pop();

            Console.WriteLine(mazzo.Peek());

            return true;

        }

        public static bool PlayQueue() 
        {
            // Queue: strutture FIFO, pensa alla coda dal dottore, alla posta ecc.
            // metodi enqueue, peek, dequeue, clear
            Queue<string> laCoda = new Queue<string>();
            laCoda.Enqueue("Beppe");
            laCoda.Enqueue("Giova");
            laCoda.Enqueue("Gosto");
            laCoda.Enqueue("Cicccio");
            laCoda.Enqueue("Leo");
            laCoda.Enqueue("Fra");

            Console.WriteLine(laCoda.Peek());
            laCoda.Dequeue();
            foreach (var persona in laCoda) Console.WriteLine(persona);

            while (laCoda.Count > 0)
            {
                Console.WriteLine(laCoda.Dequeue());
            }

            return true;
        }


    }
}

