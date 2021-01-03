using System;
using System.IO;
using System.Linq;
using static System.Math;

namespace Solutions
{
    public class ProblemH_solved
    {
        public static void Main()
        {
            Main(Console.In);
        }

        public static void Main(TextReader input)
        {
            var n = int.Parse(input.ReadLine());
            var v = new V[n];
            for (int i = 0; i < n; i++)
                v[i] = new V(input.ReadLine());

            var permutation = Enumerable.Range(0, v.Length).ToArray();

            long maxArea = 0;
            int[] maxPermutation = null;
            while (true)
            {
                var area = Area(v, permutation);
                if (area > maxArea)
                {
                    maxArea = area;
                    maxPermutation = permutation.ToArray();
                }

                if (!NextPermutation(permutation))
                    break;
            }

            if (maxPermutation == null)
                Console.Out.WriteLine("No");
            else
            {
                Console.Out.WriteLine($"Yes = {maxArea}");
                Console.Out.WriteLine(string.Join(" ", maxPermutation.Select(i => i + 1)));
            }
        }

        // doubled area in fact
        private static long Area(V[] v, int[] permutation)
        {
            var area = 0L;
            for (int i = 0; i < v.Length; i++)
            {
                var intersects = false;
                for (int k = i == v.Length - 1 ? 1 : 0; k <= i - 2; k++)
                {
                    if (Intersect(v[permutation[i]], v[permutation[(i + 1) % v.Length]], v[permutation[k]], v[permutation[k + 1]]))
                    {
                        intersects = true;
                        break;
                    }
                }

                if (intersects)
                    return -1;

                area += Area(V.Zero, v[permutation[i]], v[permutation[(i + 1) % v.Length]]);
            }

            return Abs(area);
        }

        // doubled area in fact
        private static long Area(V a, V b, V c)
        {
            return (b.x - a.x) * (c.y - a.y) - (b.y - a.y) * (c.x - a.x);
        }

        private static bool ProjectionIntersect(long a, long b, long c, long d)
        {
            if (a > b)
            {
                var t = a;
                a = b;
                b = t;
            }

            if (c > d)
            {
                var t = c;
                c = d;
                d = t;
            }

            return Max(a, c) <= Min(b, d);
        }

        private static void Swap(int[] a, int i, int j)
        {
            int t = a[i];
            a[i] = a[j];
            a[j] = t;
        }

        private static bool NextPermutation(int[] a)
        {
            int j = a.Length - 2;
            while (j != -1 && a[j] >= a[j + 1])
                j--;
            if (j == -1)
                return false;
            int k = a.Length - 1;
            while (a[j] >= a[k])
                k--;
            Swap(a, j, k);
            int l = j + 1, r = a.Length - 1;
            while (l < r)
                Swap(a, l++, r--);
            return true;
        }

        private static bool Intersect(V a, V b, V c, V d)
        {
            return ProjectionIntersect(a.x, b.x, c.x, d.x)
                   && ProjectionIntersect(a.y, b.y, c.y, d.y)
                   && Sign(Area(a, b, c)) * Sign(Area(a, b, d)) <= 0
                   && Sign(Area(c, d, a)) * Sign(Area(c, d, b)) <= 0;
        }

        private class V
        {
            public static readonly V Zero = new V(0, 0);
            public readonly long x;
            public readonly long y;

            public V(string s)
            {
                var arr = s.Split().Select(long.Parse).ToArray();
                x = arr[0];
                y = arr[1];
            }

            public V(long x, long y)
            {
                this.x = x;
                this.y = y;
            }

            public override string ToString() => $"{x} {y}";
        }
    }
}