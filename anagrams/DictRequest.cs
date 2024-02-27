using System.Net;
using System.Text.Json;

namespace anagrams
{

    internal class DictRequest
    {
        public static string GetDefinitionFromDict(string parola)
        {
            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true,
                CommentHandling = JsonCommentHandling.Skip
            };
            string encodedParola = WebUtility.UrlEncode(parola);
            string finalDefinition = string.Empty;
            string definitionExtract = string.Empty;
            // Imposta l'URL del dizionario online
            string url = $"https://it.wiktionary.org/w/api.php?action=query&prop=extracts&titles={encodedParola}&format=json";
            var awaiter = CallURL(url);
            if (awaiter.Result != "")
            {
                string jsonContent = awaiter.Result;
                using JsonDocument document = JsonDocument.Parse(jsonContent, options);
                JsonElement root = document.RootElement;
                

                foreach (var property in document.RootElement.EnumerateObject())
                {
                    if (property.Name == "query")
                    {
                        string checkResult = property.Value.ToString();
                        if (checkResult.Contains("missing"))
                        {
                            finalDefinition = $"la parola {parola} non esiste";
                        }
                        else
                        {
                            int indiceExtract = checkResult.IndexOf("extract\":\"");
                            if (indiceExtract != -1)
                            {
                                string restoStringa = checkResult.Substring(indiceExtract + "extract\":\"".Length);
                                finalDefinition = HtmlToText.ConvertWithRegex(restoStringa);
                            }
                            else
                            {
                                Console.WriteLine("La sequenza 'extract\":\"' non è presente nella stringa.");
                            }
                        }
                    }
                }
            }
            return finalDefinition;
        }

        public static async Task<string> CallURL(string url)
        {
            HttpClient client = new HttpClient();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
            client.DefaultRequestHeaders.Accept.Clear();
            var response = client.GetStringAsync(url);
            return await response;
        }
    }
}
