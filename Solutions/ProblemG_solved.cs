using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Solutions
{
    public class ProblemG_solved
    {
        public static void Main()
        {
            Main(Console.In);
        }

        public static void Main(TextReader input)
        {
            var nums = input.ReadLine().Split().Select(int.Parse).ToArray();
            var (n, m) = (nums[0], nums[1]);
            var c = input.ReadLine().Split().Select(int.Parse).Select(i => i - 1).ToArray();
            const long mod = 998244353;

            var linkCounts = new int[n];
            var allLinks = new List<(int a, int b)>();
            for (int i = 0; i < n - 1; i++)
            {
                nums = input.ReadLine().Split().Select(int.Parse).ToArray();
                var (a, b) = (nums[0], nums[1]);
                allLinks.Add((a - 1, b - 1));
                linkCounts[a - 1]++;
                linkCounts[b - 1]++;
            }

            var links = new int[n][];
            for (int i = 0; i < n; i++)
                links[i] = new int[linkCounts[i]];

            foreach (var link in allLinks)
            {
                links[link.a][--linkCounts[link.a]] = link.b;
                links[link.b][--linkCounts[link.b]] = link.a;
            }
            
            var counts = Enumerable.Range(0, n).Select(i => new int[1 << m]).ToArray();

            var stack = new Stack<(int node, bool final)>();
            stack.Push((0, false));

            var used = new HashSet<int> {0};
            var children = Enumerable.Range(0, n).Select(i => new List<int>()).ToArray();

            var localCounts = new int[1 << m];
            var newCounts = new int[1 << m];
            while (stack.Count > 0)
            {
                var (node, final) = stack.Pop();
                if (final)
                {
                    foreach (var child in children[node])
                    {
                        Array.Clear(localCounts, 0, localCounts.Length);
                        for (int leafMask = 0; leafMask < 1 << m; leafMask++)
                        {
                            var pm0 = leafMask | (1 << c[node]);
                            localCounts[pm0] = (int)(((long)localCounts[pm0] + counts[child][leafMask]) % mod);
                        }

                        Array.Clear(newCounts, 0, localCounts.Length);
                        for (int leafMask = 0; leafMask < 1 << m; leafMask++)
                        {
                            if (localCounts[leafMask] == 0)
                                continue;
                        
                            for (int parentMask = (1 << m) - 1; parentMask >= 0; parentMask--)
                            {
                                var pm = parentMask | leafMask;
                                if (counts[node][parentMask] == 0)
                                    continue;
                            
                                newCounts[pm] = (int)((newCounts[pm] + (long)localCounts[leafMask] * counts[node][parentMask]) % mod);
                            }

                            newCounts[leafMask] = (int)(((long)newCounts[leafMask] + localCounts[leafMask]) % mod);
                        }

                        for (int mask = 0; mask < 1 << m; mask++)
                            counts[node][mask] = (int)(((long)counts[node][mask] + newCounts[mask]) % mod);
                    }

                    counts[node][1 << c[node]] = (int)(((long)counts[node][1 << c[node]] + 1) % mod);
                }
                else
                {
                    stack.Push((node, true));
                    foreach (var child in links[node])
                    {
                        if (used.Add(child))
                        {
                            stack.Push((child, false));
                            children[node].Add(child);
                        }
                    }
                }
            }
            
            var result = counts.Aggregate(0L, (l, longs) => (l + longs[(1 << m) - 1]) % mod);
            Console.Out.WriteLine(result);
        }
    }
}