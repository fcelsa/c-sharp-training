using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public bool morto;

        // static determina che questo parametro (campo) sarà parte della classe Persona e non esisterà nelle istanze create.
        static public int numeroPersone = 0, numeroMorti = 0;

        // questo è un costruttore
        public Persona(string nome, string cognome, string soprannome, bool morto)
        {
            this.nome = nome;
            this.cognome = cognome;
            this.soprannome = soprannome;
            this.morto = morto;
            Persona.numeroPersone++;
        }

        // eventualmente si può anche già specificare un valore di default da assegnare, ma quello sopra è il metodo 
        // che di solito si usa il new in chiamata passa i parametri al cstruttore per l'inizializzazione
        // per es.
        // Persona persona1 = new Persona("Beppe", "Rossi", "Bruglino", false)

        // questi che seguono sono constructor overload se cambia la firma (parametri passati, quantità o tipi) possiamo definire più metodi
        // che sono utilizzabili con comportamenti diversi.

        // senza parametri instanzio un oggetto vuoto
        public Persona()
        {
            Persona.numeroPersone++;
        }

        public Persona(bool morto)  // con questo overload vado ad instanziare un oggetto con valori predefiniti/calcolati
        {
            if (morto)
            {
                this.nome = "morto";
                this.cognome = "morto";
                this.soprannome = "morto";
                this.morto = morto;
                Persona.numeroPersone--;
                Persona.numeroMorti++;
            }
            else
            {
                this.nome = "N.d.";
                this.cognome = "N.d.";
                this.soprannome = "N.d.";
                this.morto = morto;
                Persona.numeroPersone++;
            }

        }

        public Persona(string soprannome)
        {
            this.soprannome = soprannome;
        }

        // questo metodo in quanto public sarà disponibile per tutte le istanze di questa classe e sarà anche ereditato
        public void Saluta()
        {
            Console.WriteLine($"ciao mi chiamo {this.nome} e vi saluto!");
        }

        public static void Bestemmia()
        {
            Console.WriteLine($"Questo metodo è statico quindi fa riferimento alla classe, sarà disponibile\n" +
                $"anche se nessun oggetto di questa classe è stato instanziato");
        }


    }

    class Paziente
    {
        // auto implemented proprieties (getter and setter)
        // a differenza di quanto visto sopra, per la classe Persona, qui i campi sono privati e si settano con il getter e setter
        // possono essere implementati come metodi classici, come proprietà o con "prop" in forma abbreviata.
        // in questo pezzo di codice ho usato tutti e tre i modi:
        //  - le funzioni (metodi) GetNome e SetNome che, dopo aver instanziato un oggetto...
        //    Paziente paziente1 = new Paziente("Tizio", "Tizi", "DotttorOne", 50);
        //    si potrà far cambiare proprietà per es. con paziente1.SetNome("Gosto");  ed accedere con persona1.GetNome();
        //  - la proprietà con get e set per il cognome, in questo caso si nota la convenzione di usare lo stesso nome di campo 
        //    ma con la maiuscola davanti e si vede l'uso di un arrow function o lambda per il getter.
        //  - il prop, scrivendo prop nell'editor viene espanso il getter e setter come si vede per la proprietà medicoCurante.
        //  - infine in questo caso specifico l'eta ha il suo setter con un controllo del valore.
        // ovviamente non è il caso di utilizzare metodi misti come qui, tuttavia è bene conoscere tutte le modalità utilizzabili.

        private string nome;
        private string cognome;
        private string medicoCurante;
        private int eta;

        public void SetNome(string nome)
        {
            this.nome = nome;
        }

        public string GetNome()
        {
            return this.nome;
        }

        public String Cognome 
        { 
            get => this.cognome;
            set => this.cognome = value; 
        }

        public String MedicoCurante { get; set; }
        public int Eta
        {
            get
            {
                return this.eta;
            }
            set
            {
                if ( value >= 0 ) this.eta = value;
            }
        }

    }

    // qui vediamo l'ereditarietà ... con il : si specifica che quella classe deriva dalla classe a destra, dopo il :
    // le faccio con public per semplificare... come la classe madre del resto ...


    class Studente: Persona
    {
        public string aula;
        public int voto;

        public Studente(string nome, string cognome, string soprannome, bool morto, string aula, int voto)   // : base(... ecc) 
        {
            this.aula = aula;
            this.voto = voto;
        }

    }

}
