// exercise-1
// un programma che accetta una parola da linea di comando e ne fa un'anagramma

using System;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {

        if (args.Length < 1)
        {
            Console.WriteLine("argomento mancante");
            return;
        }

        string word = args[0];

        Console.WriteLine($"Gli anagrammi di {word} sono: ");

        foreach (string anagrammi in GetAnagrams(word).Distinct().Where((w)=>w!=word))
        {
            Console.WriteLine(anagrammi);
        }

    }

    //TODO: da finire con esperimento algoritmo con numeri primi
    //      il prodotto di numeri primi e' uguale per parole anagrammabili
    //string primeMap = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    static IEnumerable<string> GetAnagrams(string word)
    {
        return GetAnagrams(word.ToCharArray(), 0);
    }

    static IEnumerable<string> GetAnagrams(char[] word, int currentIndex)
    {
        if (currentIndex == word.Length - 1)
        {
            yield return new string(word);
        }
        else
        {
            for (int i = currentIndex; i < word.Length; i++)
            {
                Swap(word, currentIndex, i);
                foreach (string anagram in GetAnagrams(word, currentIndex + 1))
                {
                    yield return anagram;
                }
                Swap(word, currentIndex, i);
            }
        }
    }

    static void Swap(char[] word, int i, int j)
    {
        char temp = word[i];
        word[i] = word[j];
        word[j] = temp;
    }

}

