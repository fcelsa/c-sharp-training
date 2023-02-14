using System.Security.Cryptography;
using System.Text;

namespace TestingRandom
{
    internal class Program
    {
        const int MAX = 100;
        const int SAMPLES = 20000;

        static void Main(string[] args)
        {
            var interiRandom = new int[MAX];

            for (int i = 0; i <= SAMPLES; i++)
            {
                //interiRandom[Random.Shared.Next(MAX)]++;
                interiRandom[RandomNumberGenerator.GetInt32(MAX)]++;
            }

            var sb = new StringBuilder();

            for (int i = 0; i < MAX; i++)
            {
                //sb.AppendLine($"{i}, {interiRandom[i]}");
                sb.Append(new string('#', interiRandom[i]));
                sb.AppendLine($" ({interiRandom[i].ToString()})");
            }

            

            File.WriteAllText("result.txt", sb.ToString());
        }
    }
}