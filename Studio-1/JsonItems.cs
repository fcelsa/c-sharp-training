using System.Text.Json;

namespace Studio_1
{
    public class JsonItems 
    {
        public int Id { get; set; }
        public string MenuName { get; set; }
        public string MenuDescription { get; set; }

        private static JsonItems[] jsonitems;

        public static void JsonInit()
        {
            var menuItemsJson = new JsonItems[24];
            string json = string.Empty;

            // attenzione il file viene inizializzato così se non esiste
            if (!File.Exists(Program.JSONDATAFILE))
            {
                for (int i = 0; i < 25; i++)
                {
                    menuItemsJson[i] = new JsonItems();
                    menuItemsJson[i].Id = i;
                    menuItemsJson[i].MenuName = $"voce menu # {i}";
                    menuItemsJson[i].MenuDescription = $"Descrizione della voce di menu # {i}  presente!  ora lo puoi editare!!";
                }

                json = JsonSerializer.Serialize(menuItemsJson, new JsonSerializerOptions { WriteIndented = true });

                File.WriteAllText(Program.JSONDATAFILE, json);
                jsonitems = menuItemsJson;

            }
            else
            {
                json = File.ReadAllText(Program.JSONDATAFILE);
                jsonitems = JsonSerializer.Deserialize<JsonItems[]>(json);
            }
        }
        
        public static string MenuFromJson(int id,  bool isDesc)
        {
            string menuResult = string.Empty;
            
            foreach (var item in jsonitems)
            {
                if (item.Id == id)
                {
                    if (!isDesc)
                    {
                        menuResult = item.MenuName;
                    }
                    else
                    {
                        menuResult = item.MenuDescription;
                    }
                }
            }

            return menuResult;

        }
    }
}
