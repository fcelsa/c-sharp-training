using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studio_1
{
    public class MakeScreenMenu
    {
        public MakeScreenMenu(IEnumerable<string> items)
        {
            Items = items.ToArray();
        }

        public IReadOnlyList<string> Items { get; }

        public int SelectedIndex { get; private set; } = 0;  // -1 nothing selected  0 = 1st selected

        public string SelectedOption => SelectedIndex != -1 ? Items[SelectedIndex] : null;


        public void MoveUp() => SelectedIndex = Math.Max(SelectedIndex - 1, 0);

        public void MoveDown() => SelectedIndex = Math.Min(SelectedIndex + 1, Items.Count - 1);


    }

    // logic for drawing menu list
    public class ConsoleMenuPainter
    {
        readonly MakeScreenMenu menu;

        public ConsoleMenuPainter(MakeScreenMenu menu)
        {
            this.menu = menu;
        }

        public void Paint(int x, int y)
        {
            for (int i = 0; i < menu.Items.Count; i++)
            {
                Console.SetCursorPosition(x, y + i);

                var color = menu.SelectedIndex == i ? ConsoleColor.Yellow : ConsoleColor.Cyan;

                Console.ForegroundColor = color;
                Console.WriteLine(menu.Items[i]);
            }
        }

        public static string[] MenuItemList()
        {
            var menu = new string[] {
                "Introduzione                   ", // 0
                "Tipi di dati e casting         ", // 1
                "Stringhe e Char                ", // 2
                "Blocchi condizionali if        ", // 3
                "Operatori ternari              ", // 4
                "Cicli for, foreach, do, while  ", // 5
                "Switch case statements         ", // 6
                "...e uscirne: break e continue ", // 7
                "Le eccezioni con try catch     ", // 8
                "Gestire input utente           ", // 9
                "Argomenti in riga di comando   ", //10
                "Metodi e method overloading    ", //11
                "Array e collection             ", //12
                "Interagire con il S.O.         ", //13
                "Leggere e scrivere i file      ", //14
                "Accesso a dati remoti          ", //15
                "Database ed SQL                ", //16
                "Item 17                        ", //17
                "Item 18                        ", //18
                "Item 19                        ", //19
                "Item 20                        ", //20
                "Item 21                        ", //21
                "Item 22                        ", //22
                "Item 23                        ", //23
                "Saluti ed uscita dal programma "  //24
            };

            return menu;
        }

        public static string[] DescMenuItem(int descIdx)
        {
            var stringone = "";

            switch (descIdx)
            {
                case 0:
                    stringone =
$"""
Questo è il risultato degli appunti e del codice scritto per imparare 
le basi di C#; i concetti appresi sono direttamente nel codice, 
con relativi commenti, sulla base di questo menu ho cercato di 
racchiudere i blocchi di codice relativo all'argomento in blocchi 
#region ... #endregion, una direttiva del preprocessor che permette
agli editor di collassare quella parte di codice racchiuso li dentro
per migliorare la leggibilità.
Alcuni concetti, costrutti ed algoritmi utili sono disseminati nel
codice per generare questo stesso menu.
Program.cs è l'entry point di ogni programma c# per console.
I primi concetti base son quasi tutti in questo file e per gli argomenti
più avanzati ho creato una classe apposita in file separato.


Con [CTRL]+[E] viene eseguito la parte di codice di competenza e mostrato l'output.
""";
                    break;

                case 1:
                    stringone =
$"""
I tipi di dato standard sono moltissimi, alcuni concetti base da ricordare:
Le stringhe sono array di char, tanto è vero che String è una classe, esiste
la parola chiave string solo per pura comodità nella scrittura del codice.
I tipi interi sono vari in base alla dimensione e per tutti esiste la unsigned
per es. Int32 --> UInt32, unica eccezione il tipo byte, che va da 0 a 255
ma c'é sbyte che va da -127 a 128.
""";
                    break;

                case 2:
                    stringone =
    $"""
stringone della minchia... 
attenzione, qui si va a capo senza escapare i \n 
""";

                    break;

                case 3:
                    stringone =
$"""

""";
                    break;

                case 4:
                    stringone =
$"""

""";
                    break;

                case 5:
                    stringone =
$"""

""";
                    break;

                case 6:
                    stringone =
$"""

""";
                    break;


                case 7:
                    stringone =
$"""

""";
                    break;

                case 8:
                    stringone =
$"""

""";
                    break;

                case 9:
                    stringone =
$"""

""";
                    break;

                case 10:
                    stringone =
$"""

""";
                    break;

                case 11:
                    stringone =
$"""

""";
                    break;

                case 12:
                    var impreca = PlayArray.PlayA(1);

                    stringone =
$"""
La definizione di array di esempio è nel codice commentato della classe PlayArray

Ricorda alcuni concetti: 
 - non si può sforare la dimensione dell'array ed una volta definite le dimensioni
   queste non possono essere cambiate, al limite possiamo combinarli ...
 - il ciclo tipico per leggere gli array è il foreach
 - ma per ciclare correttamente array a più dimensioni, servono cicli for nidificati.

 Gli array sono strutture semplici le cui dimensioni possono essere controllate,
 ma nei linguaggi OOP come C# è più frequente parlare di collection; le collezioni
 sono mutabili e possono contenere dati di tipo diverso, nel caso delle ArrayList
 anche di tipo diverso, ma vediamole tutte:
  - ArrayList  : possono contenere dati di tipo diverso, anche altre liste o array
  - List       : contengono tipi di dati definiti e quindi da dichiarare
  - Hashtable  : collection con coppie chiave-valore, si manipolano solo per chiave.
  - Dictionary : sono simili alle HashTable, ma il tipo di dato di chiave e valore 
                 devono essere dichiarati.
  - Stack      : struttura LIFO
  - Queue      : struttura FIFO

""";
                    break;

                case 13:
                    stringone =
$"""

""";
                    break;

                case 14:
                    stringone =
$"""

""";
                    break;

                case 15:
                    stringone =
$"""

""";
                    break;

                case 16:
                    stringone =
$"""

""";
                    break;

                case 17:
                    stringone =
$"""

""";
                    break;

                case 18:
                    stringone =
$"""

""";
                    break;

                case 19:
                    stringone =
$"""

""";
                    break;

                case 20:
                    stringone =
$"""

""";
                    break;

                case 21:
                    stringone =
$"""

""";
                    break;

                case 22:
                    stringone =
$"""

""";
                    break;

                case 23:
                    stringone =
$"""

""";
                    break;

                case 24:
                    stringone =
$"""

""";
                    break;

                default:
                    stringone = @"sono lo stringone di default... ";
                    break;
            }

            return stringone.Split("\n");
            
        }

    }

}
