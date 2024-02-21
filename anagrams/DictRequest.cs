
namespace anagrams
{
    internal class DictRequest
    {

        public static string GetDefinitionFromDict(string parola)
        {
            // Crea un client HTTP
            using (var client = new HttpClient())

            // Imposta l'URL del dizionario online
            var url = $"https://it.wiktionary.org/w/api.php?action=query&prop=extracts&titles={parola}&format=json";

            // Invia una richiesta GET al dizionario
            var response = await client.GetAsync(url);

            // Se la richiesta ha esito positivo
            if (response.IsSuccessStatusCode)
            {
                // Leggi la risposta come stringa JSON
                var json = await response.Content.ReadAsStringAsync();

                // Deserializza la stringa JSON in un oggetto
                var dizionario = await System.Text.Json.JsonSerializer.DeserializeAsync<Dictionary<string, object>>(json);

                // Estrai la definizione dalla risposta
                var definizione = dizionario["extract"][0]["extract"].ToString();

                return definizione;
            }
            else
            {
                // Stampa un messaggio di errore
                return "Errore: la richiesta al dizionario online è fallita.";
            }
        }
    }
}
