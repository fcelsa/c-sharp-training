using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using static System.Net.Mime.MediaTypeNames;

namespace anagrams
{ 
    public class Program
    {
        static List<Word> ListOfParole { get; set; } = new();

        const string QUIT_KEYWORD = "q";
        const string CHG_DICT_KEYWORD = "c";
        const string HELP_KEYWORD = "h";
        const string STAT_KEYWORD = "s";

        static void Main(string[] args)
        {
            var filePath = string.Empty;

            if (args.Length > 0)
            {
                filePath = args[0];
            }
            else
            {
                filePath = GetFileFromUser();
            }

        START_FILE_REQUEST:

            while (!LoadDictionary(filePath))
            {
                filePath = GetFileFromUser();
                if (filePath.ToLower() == QUIT_KEYWORD)
                {
                    return;
                }
            }

            if (args.Length > 1 && !string.IsNullOrEmpty(args[1]) && !string.IsNullOrWhiteSpace(args[1]))
            {
                FindAnagrams(args[1]);
            }
            else
            {
                while (true)
                {
                    Console.WriteLine("Inserisci una parola da anagrammare od un comando (h per help)");
                    var line = Console.ReadLine();
                    if (!string.IsNullOrEmpty(line) && !string.IsNullOrWhiteSpace(line))
                    {
                        line = line.ToLower();
                        var performance = Stopwatch.GetTimestamp();


                        switch (line)
                        {
                            case QUIT_KEYWORD:
                                return;

                            case CHG_DICT_KEYWORD:
                                filePath = GetFileFromUser();
                                goto START_FILE_REQUEST;

                            case HELP_KEYWORD:
                                PrintHelp();
                                break;

                            case STAT_KEYWORD:
                                PrintStat();
                                break;

                            default:
                                FindAnagrams(line);
                                break;
                        }

                        Console.WriteLine($"operazione eseguita in {Stopwatch.GetElapsedTime(performance).TotalMilliseconds} ms");

                    }
                }
            }


        }

        private static void PrintStat()
        {
            Console.WriteLine($"Il dizionario selezionato contiene {ListOfParole.Count} parole uniche\n");
            var mostLength = ListOfParole.Aggregate((max, cur) => max.Name.Length > cur.Name.Length ? max : cur);
            var mostShortest = ListOfParole.Aggregate((min, cur) => min.Name.Length < cur.Name.Length ? min : cur);
            Console.WriteLine($"Le parole più lunghe del dizionario sono {mostLength.Name} \n composta da {mostLength.Name.Length} caratteri.");
            Console.WriteLine($"la parola più corta del dizionario è {mostShortest.Name} \n composta da {mostShortest.Name.Length} caratteri.");
        }

        private static void PrintHelp()
        {
            Console.WriteLine("Comandi: ");
            Console.WriteLine("c = cambia dizionario");
            Console.WriteLine("q = quit");
            Console.WriteLine("s = statistiche dizionario");
        }

        private static void FindAnagrams(string parola)
        {
            var parolaP = Utils.GetProduct(parola);
            var parolaE = ListOfParole.Where(p => p.Name.ToLower() == parola).ToList();
            var anagrams = ListOfParole.Where(p => p.PrimeProduct == parolaP && p.Name.ToLower() != parola).ToList();

            if (anagrams.Count != 0)
            {
                if(parolaE.Count != 0) Console.WriteLine($"trovate {anagrams.Count} parole nel dizionario:");
                if(parolaE.Count == 0) Console.WriteLine($"non esiste {parola} in questo dizionario, però è anagrammabile con queste parole:");

                foreach (var anagram in anagrams) Console.WriteLine(anagram.Name);
            }
            else
            {
                if (parolaE.Count != 0) Console.WriteLine($"la parola {parola} esiste nel dizionario ma non ha anagrammi");
                if (parolaE.Count == 0) Console.WriteLine($"la parola {parola} non esiste e non ha angrammi nel dizionario");
            }


            //versione da 15 ms
            //var numAnagrams = 0;
            //foreach (var p in listOfParole)
            //{
            //    if (p.PrimeProduct == parolaP && p.Name.ToLower() != parola.ToLower())
            //    {
            //        Console.WriteLine(p.Name);
            //        numAnagrams++;
            //    }
            //}
            //Console.WriteLine($"trovate {numAnagrams} parole");

        }

        static bool LoadDictionary(string filePath)
        {
            if (File.Exists(filePath))
            {
                var performance = Stopwatch.GetTimestamp();
                ListOfParole.Clear();

                foreach (var line in File.ReadLines(filePath))
                {
                    var lline = line.ToLower();
                    ListOfParole.Add(new Word(line, Utils.GetProduct(lline)));
                }

                Console.WriteLine($"Dizionario: {filePath}");
                Console.WriteLine($"tempo elaborazione {Stopwatch.GetElapsedTime(performance).TotalMilliseconds} ms");
                return true;
            } 
            else
            {
                Console.WriteLine($"Il file {filePath} non esiste!");
                return false;
            }
                
        }

        static string GetFileFromUser()
        {
            Console.WriteLine("Inserisci il percorso del file parole [dizionario.txt]");
            var filePath = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(filePath) || string.IsNullOrEmpty(filePath) )
            {
                filePath = Path.Combine(Environment.CurrentDirectory, "dizionario.txt");
                return filePath;
            }
            else
            {
                return filePath;
            }

        }

    }
}