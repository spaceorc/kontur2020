using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Solutions
{
    public class ProblemL_solved
    {
        public static void Main()
        {
            Main(Console.In);
        }

        public static void Main(TextReader input)
        {
            var dims = input.ReadLine().Split().Select(int.Parse).ToArray();
            var n = dims[0];
            var m = dims[1];
            var lines = new List<string>();
            for (int i = 0; i < n; i++)
            {
                var line = input.ReadLine();
                lines.Add(line);
            }

            var gcd = 0;
            for (int i = 0; i < n; i++)
            {
                var prev = 0;
                var prevC = 'X';
                for (int j = 0; j < m; j++)
                {
                    if (lines[i][j] == prevC)
                        prev++;
                    else
                    {
                        if (prev != 0)
                            gcd = Gcd(gcd, prev);

                        prev = 1;
                        prevC = lines[i][j];
                    }
                }

                if (prev != 0)
                    gcd = Gcd(gcd, prev);
            }

            var gcd2 = 0;
            for (int i = 0; i < m; i++)
            {
                var prev = 0;
                var prevC = 'X';
                for (int j = 0; j < n; j++)
                {
                    if (lines[j][i] == prevC)
                        prev++;
                    else
                    {
                        if (prev != 0)
                            gcd2 = Gcd(gcd2, prev);

                        prev = 1;
                        prevC = lines[j][i];
                    }
                }

                if (prev != 0)
                    gcd2 = Gcd(gcd2, prev);
            }

            Console.Out.WriteLine($"{n/gcd2} {m/gcd}");
            var result = new StringBuilder();
            for (int i = 0; i < n; i += gcd2)
            {
                for (int j = 0; j < m; j += gcd)
                    result.Append(lines[i][j]);

                result.AppendLine();
            }

            Console.Out.WriteLine(result);
        }

        static int Gcd(int a, int b)
        {
            while (a != 0 && b != 0)
            {
                if (a > b)
                    a %= b;
                else
                    b %= a;
            }

            return a + b;
        }
    }
}