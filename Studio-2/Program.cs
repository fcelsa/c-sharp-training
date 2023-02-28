using Studio_2.Library;

namespace Studio_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            var a = new ClassA();
            var b = new ClassB();

            ClassA.GetRandom();

            a.MaxRnd = 50;

            ClassA.GetRandomInt();
            ClassA.Pippo = 10;


            b.GetRandom();

            // modificatori di accesso
            var c = new Library.AccessClass();
            //c.MPrivate();
            c.MPublic();
            //c.MProtected();
            //c.MInternal();
            //c.MProtectedPrivate();
            //c.MProtectedInternal();
        }
    }
}