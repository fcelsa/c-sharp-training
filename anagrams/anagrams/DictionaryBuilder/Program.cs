using ClassDictionary;

namespace DictionaryBuilder
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var listOfParole = new List<Word>();

            Console.WriteLine("Inserisci il percorso del file parole ");
            var filePath = Console.ReadLine();
            if (File.Exists(filePath))
            {
                foreach (var line in File.ReadLines(filePath)) {
                    var lline = line.ToLower();
                    listOfParole.Add(new Word(line, AnagramUtils.GetProduct(lline)));
                }
            }
            else
            {
                Console.WriteLine("Il file non esiste!");
            }

            var annap = AnagramUtils.GetProduct("anna");
            var nanap = AnagramUtils.GetProduct("nana");
            var anagrams = listOfParole.Where(p => p.PrimeProduct == annap).ToList();

            Console.WriteLine("Premi un tasto per concludere");
            Console.ReadKey();

        }
    }
}

// 
//