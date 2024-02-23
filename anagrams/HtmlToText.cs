using System;
using System.Text.RegularExpressions;

namespace anagrams
{
	public class HtmlToText
	{
        public static string ConvertWithRegex(string html)
        {
            string plainText = Regex.Replace(html, "<.*?>", "");
            return plainText;
        }
    }
}
