using System.Security.Cryptography;

namespace anagrams
{
    internal class Utils
    {
        public static UInt128 GetProduct(string word)
        {
            UInt128 product = 1;
            foreach (var c in word.ToLower())
            {
                product *= GetPrime(c);
            }
            return product;
        }

        public static UInt128 GetPrime(char c) => c switch
        {
            'a' or 'à' or 'á' or 'â' or 'ã' or 'ä' or 'å' or 'æ' => 2,
            'b' => 3,
            'c' or 'ç' => 5,
            'd' => 7,
            'e' or 'è' or 'é' or 'ê' or 'ë' => 11,
            'f' => 13,
            'g' => 17,
            'h' => 19,
            'i' or 'ì' or 'í' or 'î' or 'ï' => 23,
            'j' => 29,
            'k' => 31,
            'l' => 37,
            'm' => 41,
            'n' => 43,
            'o' or 'ò' or 'ó' or 'ô' or 'õ' or 'ö' or 'ø' => 47,
            'p' => 53,
            'q' => 59,
            'r' => 61,
            's' => 67,
            't' => 71,
            'u' or 'ù' or 'ú' or 'û' or 'ü' => 73,
            'v' => 79,
            'w' => 83,
            'x' => 89,
            'y' or 'ý' or 'ÿ' => 97,
            'z' => 101,

            _ => 1,
        };


        public static UInt128 GetSecureRandomPWord(int lenWord)
        {
            UInt128 rndPWord = 1;
            int rnd = 0;
            for (int i = 1; i <= lenWord; i++)
            {
                do
                {
                  rnd = RandomNumberGenerator.GetInt32(2, 101);
                } 
                while (!IsPrime(rnd));


                rndPWord *= (UInt128)rnd;
            }
            
            return rndPWord;
        }

        public static bool IsPrime(int number)
        {
            if (number < 2) return false;
            if (number % 2 == 0) return (number == 2);
            int root = (int)Math.Sqrt((double)number);
            for (int i = 3; i <= root; i += 2)
            {
                if (number % i == 0) return false;
            }
            return true;
        }

        public static string GetDefinitionFromDict(string parola)
        {
            string definizione = parola + " :  stocazzo";
            return definizione;

        }

    }

}
