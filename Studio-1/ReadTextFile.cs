using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studio_1
{
    internal static class ReadTextFile
    {

        public static string ReadTextFileAsString(string txtFileName)
        {
            string theString;

            string fileCapitolo = Program.AssetFilePath + txtFileName;

            if (File.Exists(fileCapitolo))
            {
                // Read a text file line by line.
                string lines = File.ReadAllText(fileCapitolo, Encoding.Latin1);
                theString = lines;
            }
            else
            {
                theString = "File del capitolo non trovato " + fileCapitolo;
            }

            return theString;
        }

    }
}
