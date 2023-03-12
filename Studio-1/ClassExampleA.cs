using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studio_1
{
    class Persona
    {
        public string nome;             // questi sono campi
        public string cognome;
        public string soprannome;
        public int natoAA;
        public int natoMM;
        public int natoGG;
        public bool morto;

        // questo è un costruttore
        public Persona(string nome, string cognome, string soprannome, int natoAA, int natoMM, int natoGG, bool morto)
        {
            this.nome = nome;
            this.cognome = cognome;
            this.soprannome = soprannome;
            this.natoAA = natoAA;
            this.natoMM = natoMM;
            this.natoGG = natoGG;
            this.morto = morto;
        }
        // eventualmente si può anche già specificare un valore di default da assegnare, ma quello sopra è il metodo 
        // che di solito si usa più spesso, il new in chaimata passa i parametri al cstruttore per l'inizializzazione
        // per es.
        // Persona persona1 = new Persona("Beppe", "Rossi", "Bruglino", 1928, 1, 27, true)


        // questi che seguono sono constructor overload se cambia la firma (parametri passati, quantità o tipi) possiamo definire più metodi
        // che sono utilizzabili con comportamenti diversi.
        public Persona() { }  // senza parametri instanzio un oggetto vuoto

        public Persona(bool morto)  // con questo overload vado ad instanziare un oggetto con valori predefiniti/calcolati
        {
            if (morto)
            {
                this.nome = "morto";
                this.cognome = "morto";
                this.soprannome = "morto";
                this.natoAA = 0;
                this.natoMM = 0;
                this.natoGG = 0;
                this.morto = morto;
            }
            else
            {
                this.nome = "N.d.";
                this.cognome = "N.d.";
                this.soprannome = "N.d.";
                this.natoAA = 2023;
                this.natoMM = 1;
                this.natoGG = 1;
                this.morto = morto;
            }

        }

        public Persona(string soprannome)
        {
            this.soprannome = soprannome;
        }

        public void Saluta()
        {
            Console.WriteLine($"ciao mi chiamo {this.nome} e vi saluto!");
        }
    }

    class Paziente
    {
        private string nome;
        private string cognome;
        private string soprannome;
        private int natoAA;
        private int natoMM;
        private int natoGG;
        private bool morto;

    }
}
