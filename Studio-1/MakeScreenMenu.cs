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

                JsonItems.MenuFromJson(0,false), 
                JsonItems.MenuFromJson(1,false), 
                JsonItems.MenuFromJson(2,false), 
                JsonItems.MenuFromJson(3,false), 
                JsonItems.MenuFromJson(4,false), 
                JsonItems.MenuFromJson(5,false), 
                JsonItems.MenuFromJson(6,false), 
                JsonItems.MenuFromJson(7,false), 
                JsonItems.MenuFromJson(8,false), 
                JsonItems.MenuFromJson(9,false), 
                JsonItems.MenuFromJson(10,false),
                JsonItems.MenuFromJson(11,false),
                JsonItems.MenuFromJson(12,false),
                JsonItems.MenuFromJson(13,false),
                JsonItems.MenuFromJson(14,false),
                JsonItems.MenuFromJson(15,false),
                JsonItems.MenuFromJson(16,false),
                JsonItems.MenuFromJson(17,false),
                JsonItems.MenuFromJson(18,false),
                JsonItems.MenuFromJson(19,false),
                JsonItems.MenuFromJson(20,false),
                JsonItems.MenuFromJson(21,false),
                JsonItems.MenuFromJson(22,false),
                JsonItems.MenuFromJson(23,false),
                JsonItems.MenuFromJson(24,false)
            };

            return menu;
        }

        public static string[] DescMenuItem(int descIdx)
        {
            var stringone = "";

            switch (descIdx)
            {
                case 0:
                    stringone = JsonItems.MenuFromJson(0, true);
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
{JsonItems.MenuFromJson(3,true)}
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
Oggetti, classi, metodi costruttori vanno visti nel codice; tenere a mente che
la definizione di una classe principalmente si fa per oggetti istanziabili.
Se un oggetto non è istanziabile, la classe si riduce semplicemente a
un insieme di metodi (funzioni) per eseguire determinate operazioni, 
passando eventualmente parametri e ricevendone eventuali valori di ritorno.
In Program.cs c'è un esempio semplice di definizione di classe e nel
codice di esempio eseguibile da qui alcune operazioni di creazione oggetto 
e nuove istanze ed a seguire un esempio di costruttore.
In tutto il codice ci sono altre classi definite come quella per fare il
menu stesso.

""";
                    break;

                case 14:
                    stringone =
$"""
I metodi getter e setter di una proprietà di una classe, 
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
{JsonItems.MenuFromJson(22,true)}
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
