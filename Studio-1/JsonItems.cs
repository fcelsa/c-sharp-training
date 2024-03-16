using System.Text.Json;

namespace Studio_1
{
    public class JsonItems 
    {
        public int Id { get; set; }
        public string MenuName { get; set; }
        public string DescFile { get; set; }

        private static JsonItems[] jsonitems;
        public static void JsonInit()
        {
            var menuItemsJson = new JsonItems[24];
            string json = string.Empty;

            // attenzione il file viene inizializzato così come segue solo se non esiste
            if (!File.Exists(Program.FullJsonDataFilePath))
            {
                for (int i = 0; i < 24; i++)
                {
                    menuItemsJson[i] = new JsonItems();
                    menuItemsJson[i].Id = i;
                    menuItemsJson[i].MenuName = $"voce menu # {i}";
                    menuItemsJson[i].DescFile = $"default.txt";
                }

                json = JsonSerializer.Serialize(menuItemsJson, new JsonSerializerOptions { WriteIndented = true });

                File.WriteAllText(Program.FullJsonDataFilePath, json);
                jsonitems = menuItemsJson;

            }
            else
            {
                json = File.ReadAllText(Program.FullJsonDataFilePath);
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
                        menuResult = ReadTextFile.ReadTextFileAsString(item.DescFile);
                    }
                }
            }

            return menuResult;

        }
    }
}
