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
    }
}

