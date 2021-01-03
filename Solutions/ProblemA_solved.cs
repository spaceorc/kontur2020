using System;
using System.IO;
using System.Linq;

namespace Solutions
{
    public class ProblemA_solved
    {
        public static void Main()
        {
            Main(Console.In);
        }

        public static void Main(TextReader input)
        {
            var n = int.Parse(input.ReadLine());
            var min = int.MaxValue;
            for (int i = 0; i < n; i++)
            {
                var line = input.ReadLine().Split().Select(int.Parse).ToArray();
                var res = line[0] + line[1];
                if (res < min)
                    min = res;
            }
            Console.Out.WriteLine(min);
        }
    }
}