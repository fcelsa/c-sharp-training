namespace list_test
{
    internal class Program
    {
        static List<object>? sharedList = null;
        static List<object> ExtendList(object val, List<object>? list = null)
        {
            if (list == null)
            {
                // Simuliamo il comportamento di Python: la prima volta usiamo la lista condivisa
                if (sharedList == null)
                {
                    sharedList = new List<object>();
                }
                list = sharedList;
            }

            list.Add(val);
            return list;
        }
        static void Main(string[] args)
        {
            List<object> list1 = ExtendList(13);
            List<object> list2 = ExtendList(123, new List<object>());
            List<object> list3 = ExtendList('a');
            List<object> list4 = ExtendList("pippo", new List<object>());
            List<object> list5 = ExtendList("pippo");

            // Stampiamo i risultati
            Console.WriteLine(string.Join(", ", list1));
            Console.WriteLine(string.Join(", ", list2));
            Console.WriteLine(string.Join(", ", list3));
            Console.WriteLine(string.Join(", ", list4));
            Console.WriteLine(string.Join(", ", list5));

            if (ReferenceEquals(list1, list5))
            {
                Console.WriteLine("list1 e list5 sono lo stesso oggetto");
            }
            else
            {
                Console.WriteLine("list1 e list5 NON sono lo stesso oggetto");
            }
        }

    }

}
