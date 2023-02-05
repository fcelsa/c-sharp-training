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

    }
}
