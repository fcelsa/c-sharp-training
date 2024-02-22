using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace anagrams
{
    internal class DictRequest
    {

        public static string GetDefinitionFromDict(string parola)
        {
            string encodedParola = WebUtility.UrlEncode(parola);
            string finalDefinition = string.Empty;
            // Imposta l'URL del dizionario online
            string url = $"https://it.wiktionary.org/w/api.php?action=query&prop=extracts&titles={encodedParola}&format=json";
            var awaiter = CallURL(url);
            if (awaiter.Result != "")
            {
                string jsonContent = awaiter.Result;

                // Deserializzazione del JSON in un oggetto
                //dynamic jsonData = JsonConvert.DeserializeObject(jsonContent) ?? "result not found";
                dynamic jsonData = JObject.Parse(jsonContent);

                // Accesso alle proprietà dell'oggetto
                string definitionTitle = jsonData.title;
                string definitionExtract = jsonData.extract;

                // Stampa dei dati
                Console.WriteLine(jsonData);
                Console.WriteLine($"Title: {definitionTitle}");
                Console.WriteLine($"extract: {definitionExtract}");
                finalDefinition = definitionExtract;
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
