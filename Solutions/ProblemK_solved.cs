using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Solutions
{
    public class ProblemK_solved
    {
        public static void Main()
        {
            Main(Console.In);
        }

        public static void Main(TextReader input)
        {
            var nums = input.ReadLine().Split().Select(long.Parse).ToArray();
            var (n, m, a, b) = (nums[0], nums[1], nums[2], nums[3]);
            var result = new Dictionary<long, long>();

            result[0] = 0;
            for (int i = 1; i <= n; i++)
            {
                result[i] = Math.Min(
                    result[i - 1] + b + 1,
                    result[Math.Max(0, i - m)] + a + Math.Min(i, m));
            }

            Console.Out.WriteLine(result[n]);
        }
    }
}