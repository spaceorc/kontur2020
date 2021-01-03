using System;
using System.IO;
using System.Linq;
using static System.Math;

namespace Solutions
{
    public class ProblemI_solved
    {
        public static void Main()
        {
            Main(Console.In);
        }

        public static void Main(TextReader input)
        {
            var line = input.ReadLine().Split().Select(long.Parse).ToArray();
            var n = line[0];
            var b = line[1];
            
            var a = input.ReadLine().Split().Select(long.Parse).ToArray();

            var cur = 0L;
            var sum = 0L;
            for (int i = 0; i < n; i++)
            {
                cur = Max(cur - b, 0) + a[i];
                sum += cur;
            }
            cur = Max(cur - b, 0);
            sum += cur;

            Console.Out.WriteLine(sum);
        }
    }
}