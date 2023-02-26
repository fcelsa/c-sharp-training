using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studio_1
{
    internal class PlayArray
    {
        // due modi di definire gli array, PlayA e PlayB + modifica + iterazione
        public string PlayA(int scelta)
        {
            string[] imprecazioni = { "maremma m.", "maremma p.", "maremma c.", "maremma g." };

            // sarebbe da controllare se il parametro passato supera la dimensione perché non si può sforare!
            // imprecazioni[4] darebbe errore 
            return imprecazioni[scelta];

        }

        public string[] PlayB()
        {
            string[] parolacce = new string[4];
            parolacce[0] = "merda!";
            parolacce[1] = "shit!";
            parolacce[2] = "fuck!";
            parolacce[3] = "cazzo!";

            return parolacce;

        }

        public bool PlayC(string parolaccia, int position)
        {
            var parolacce = this.PlayB();
            parolacce[position] = parolaccia;

            foreach (string item in parolacce)
            {
                Console.WriteLine(item);
            }

            return true;

        }

        public void PlayMatrici()
        {
            // array a due dimensioni o matrice
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

            for (int dim1 = 0; riga < dim1.GetLength(0); dim1++)
            {
                for (int dim2 = 0; dim2 < codici.GetLength(1); dim2++)
                {
                    for (int dim3 = 0; dim3 < codici.GetLength(1); dim3++)
                    {
                        Console.WriteLine("sono in dim1 " + dim1 + " dim2 " + dim2 + " dim3 " + dim3);
                        Console.WriteLine("Valore:" + codici3d[dim1, dim2, dim3]);
                    }
                }
            }

            // con il foreach (ovviamente valido anche per gli array 2d) si può ciclare tutta la tabella senza gli indici

            foreach (var codicesingolo in codici3d) 
            {
                Console.WriteLine(codicesingolo);
            }

        }


    }
}

