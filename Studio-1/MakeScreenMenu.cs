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

        public int SelectedIndex { get; private set; } = -1; // nothing selected

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

                var color = menu.SelectedIndex == i ? ConsoleColor.Yellow : ConsoleColor.Black;

                Console.ForegroundColor = color;
                Console.WriteLine(menu.Items[i]);
            }
        }
    }
}
