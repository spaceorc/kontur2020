using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Solutions
{
    public class ProblemE_solved
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

            var items = new List<int>();
            for (int i = 0; i < n; i++)
            {
                items.Clear();
                var prev = 0;
                for (int j = 0; j < m; j++)
                {
                    if (lines[i][j] == '#')
                        prev++;
                    else
                    {
                        if (prev != 0)
                        {
                            items.Add(prev);
                            prev = 0;
                        }
                    }
                }

                if (prev != 0)
                    items.Add(prev);

                Console.Out.WriteLine($"{items.Count} {string.Join(" ", items)}");
            }

            Console.Out.WriteLine();

            for (int i = 0; i < m; i++)
            {
                items.Clear();
                var prev = 0;
                for (int j = 0; j < n; j++)
                {
                    if (lines[j][i] == '#')
                        prev++;
                    else
                    {
                        if (prev != 0)
                        {
                            items.Add(prev);
                            prev = 0;
                        }
                    }
                }

                if (prev != 0)
                    items.Add(prev);

                Console.Out.WriteLine($"{items.Count} {string.Join(" ", items)}");
            }
        }
    }
}