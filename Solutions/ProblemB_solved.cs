using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Solutions
{
    public class ProblemB_solved
    {
        public static void Main()
        {
            Main(Console.In);
        }

        public static void Main(TextReader input)
        {
            var n = input.ReadLine().Split().Select(int.Parse).ToArray();
            var a = input.ReadLine().Split().Select(int.Parse).ToArray();
            var b = input.ReadLine().Split().Select(int.Parse).ToArray();

            var queue = new Queue<int[]>();
            var used = new Dictionary<long, int> {{Hash(a), 0}};
            queue.Enqueue(a);

            while (queue.Count > 0)
            {
                var cur = queue.Dequeue();
                var curHash = Hash(cur);
                if (IsReady(cur, b))
                {
                    Console.Out.WriteLine(used[curHash]);
                    return;
                }

                for (var from = 0; from < 3; from++)
                for (var to = 0; to < 3; to++)
                {
                    if (from == to)
                        continue;

                    var next = new int[3];
                    Array.Copy(cur, next, 3);
                    Move(n, next, from, to);
                    var nextHash = Hash(next);
                    if (used.ContainsKey(nextHash))
                        continue;
                    
                    used.Add(nextHash, used[curHash] + 1);
                    queue.Enqueue(next);
                }
            }
            
            Console.Out.WriteLine(-1);
        }

        static long Hash(int[] a)
        {
            return a[0] * 1000001L * 1000001L + a[1] * 1000001L + a[2];
        }

        static bool IsReady(int[] a, int[] b)
        {
            return a[0] == b[0]
                   && (a[1] == b[1] && a[2] == b[2]
                       ||
                       a[1] == b[2] && a[2] == b[1])
                   ||
                   a[0] == b[1]
                   && (a[1] == b[0] && a[2] == b[2]
                       ||
                       a[1] == b[2] && a[2] == b[0])
                   ||
                   a[0] == b[2]
                   && (a[1] == b[0] && a[2] == b[1]
                       ||
                       a[1] == b[1] && a[2] == b[0]);
        }

        static void Move(int[] n, int[] a, int from, int to)
        {
            var aFrom = a[from];
            a[from] -= Math.Min(a[from], n[to] - a[to]);
            a[to] = Math.Min(a[to] + aFrom, n[to]);
        }
    }
}