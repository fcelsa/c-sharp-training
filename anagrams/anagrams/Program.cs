using System.Diagnostics;

namespace anagrams
{ 
    public class Program
    {
        static List<Word> ListOfParole { get; set; } = new();

        const string QUIT_KEYWORD = "q";
        const string CHG_DICT_KEYWORD = "c";
        const string HELP_KEYWORD = "h";

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
                            default:
                                FindAnagrams(line);
                                break;
                        }

                        Console.WriteLine($"operazione eseguita in {Stopwatch.GetElapsedTime(performance).TotalMilliseconds} ms");

                    }
                }
            }


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
            var anagrams = ListOfParole.Where(p => p.PrimeProduct == parolaP && p.Name.ToLower() != parola).ToList();

            if (anagrams.Count != 0)
            {
                Console.WriteLine($"trovate {anagrams.Count} parole");

                foreach (var anagram in anagrams) Console.WriteLine(anagram.Name);
            }
            else
            {
                Console.WriteLine($"la parola {parola} non ha anagrammi in questo dizionario");
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