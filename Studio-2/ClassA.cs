
namespace Studio_2.Library
{
    public class ClassA
    {
        public int MaxRnd { get; set; } = 10;

        public static int MaxRndS { get; set; }

        public static long Pippo { get; set; } = 10;

        public ClassA()
        {

        }

        public static bool GetRandom()
        {
            return Random.Shared.Next(0, MaxRndS) == 1;
        }

        public static int GetRandomInt()
        {
            return Random.Shared.Next(0, MaxRndS);
        }
    }
}
