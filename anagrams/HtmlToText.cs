using System;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace anagrams
{
	public class HtmlToText
	{
        public static string ConvertWithRegex(string html)
        {
            string plainText = Regex.Replace(html, "<.*?>", " ");
            plainText = plainText.Replace($$"""\n""", System.Environment.NewLine);
            return plainText;
        }
    }
}
