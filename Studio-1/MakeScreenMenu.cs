using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studio_1
{
    public class MakeScreenMenu
    {
        public MakeScreenMenu(IEnumerable<string> items)
        {
            Items = items.ToArray();
        }

        public IReadOnlyList<string> Items { get; }

        public int SelectedIndex { get; private set; } = 0;  // -1 nothing selected  0 = 1st selected

        public string SelectedOption => SelectedIndex != -1 ? Items[SelectedIndex] : null;

        public void MoveUp() => SelectedIndex = Math.Max(SelectedIndex - 1, 0);

        public void MoveDown() => SelectedIndex = Math.Min(SelectedIndex + 1, Items.Count - 1);

    }

    // logic for drawing menu list
    public class ConsoleMenuPainter
    {
        readonly MakeScreenMenu menu;

        public ConsoleMenuPainter(MakeScreenMenu menu)
        {
            this.menu = menu;
        }

        public void Paint(int x, int y)
        {
            for (int i = 0; i < menu.Items.Count; i++)
            {
                Console.SetCursorPosition(x, y + i);

                var color = menu.SelectedIndex == i ? ConsoleColor.Yellow : ConsoleColor.Cyan;

                Console.ForegroundColor = color;
                Console.WriteLine(menu.Items[i]);
            }
        }

        public static string[] MenuItemList()
        {
            //var menu = new string[] {

            //    JsonItems.MenuFromJson(0,false),
            //    JsonItems.MenuFromJson(1,false),
            //    JsonItems.MenuFromJson(2,false),
            //    JsonItems.MenuFromJson(3,false),
            //    JsonItems.MenuFromJson(4,false),
            //    JsonItems.MenuFromJson(5,false),
            //    JsonItems.MenuFromJson(6,false),
            //    JsonItems.MenuFromJson(7,false),
            //    JsonItems.MenuFromJson(8,false),
            //    JsonItems.MenuFromJson(9,false),
            //    JsonItems.MenuFromJson(10,false),
            //    JsonItems.MenuFromJson(11,false),
            //    JsonItems.MenuFromJson(12,false),
            //    JsonItems.MenuFromJson(13,false),
            //    JsonItems.MenuFromJson(14,false),
            //    JsonItems.MenuFromJson(15,false),
            //    JsonItems.MenuFromJson(16,false),
            //    JsonItems.MenuFromJson(17,false),
            //    JsonItems.MenuFromJson(18,false),
            //    JsonItems.MenuFromJson(19,false),
            //    JsonItems.MenuFromJson(20,false),
            //    JsonItems.MenuFromJson(21,false),
            //    JsonItems.MenuFromJson(22,false),
            //    JsonItems.MenuFromJson(23,false),
            //    JsonItems.MenuFromJson(24,false)
            //};

            var menu = new string[25];

            for (int i = 0; i < menu.Length; i++)
            {
                menu[i] = JsonItems.MenuFromJson(i, false);
            }

            return menu;
        }

        public static string[] DescMenuItem(int descIdx)
        {
            var stringone = JsonItems.MenuFromJson(descIdx, true).Split("\n");
            return stringone;
        }

    }

}
