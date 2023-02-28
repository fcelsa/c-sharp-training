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
            // array moltidimensionali o matrice esempi array 2D e 3D
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
    }
}

