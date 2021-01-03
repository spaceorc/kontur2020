using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Solutions
{
    public class ProblemJ_solved
    {
        public static void Main()
        {
            Main(Console.In);
        }

        public static void Main(TextReader input)
        {
            var x = input.ReadLine().Split().Select(int.Parse).ToArray();
            var t = int.Parse(input.ReadLine());

            var orientations = new Dictionary<int, int[]>();
            foreach (var o in AllOrientations())
                orientations.Add(Hash(o), o.ToArray());

            var translations = new Dictionary<(int orientation, int dir), int>();
            foreach (var o in orientations)
            {
                var orientation = o.Value.ToArray();
                N(orientation);
                translations.Add((o.Key, 0), Hash(orientation));
                S(orientation);

                E(orientation);
                translations.Add((o.Key, 1), Hash(orientation));
                W(orientation);

                S(orientation);
                translations.Add((o.Key, 2), Hash(orientation));
                N(orientation);

                W(orientation);
                translations.Add((o.Key, 3), Hash(orientation));
            }

            var position = (dir: 0, orientation: Hash(new[] {0, 1, 2, 4, 3, 5}));
            var used = new Dictionary<int, long>();
            used.Add(Hash(position), 0);
            long s = 0;
            while (true)
            {
                s += x[orientations[position.orientation][5]];
                position = Next(position);
                if (used.ContainsKey(Hash(position)))
                    break;

                used.Add(Hash(position), s);
            }

            var valid = new HashSet<long>(used.Values);
            for (int i = 0; i < t; i++)
            {
                var q = long.Parse(input.ReadLine());
                Console.Out.WriteLine(valid.Contains(q % s) ? "Yes" : "No");
            }

            (int dir, int orientation) Next((int dir, int orientation) p)
            {
                var (dir, orientation) = p;
                var top = orientations[orientation][5];
                dir = (dir + x[top]) % 4;
                orientation = translations[(orientation, dir)];
                return (dir, orientation);
            }
        }

        static int Hash(int[] orientation)
        {
            return orientation.Aggregate(0, (current, t) => current * 6 + t);
        }
        
        static int Hash((int dir, int orientation) position)
        {
            var (dir, orientation) = position;
            return orientation * 4 + dir;
        }

        static IEnumerable<int[]> AllOrientations()
        {
            var orientation = new[] {0, 1, 2, 4, 3, 5};
            for (int i = 0; i < 4; i++)
            {
                yield return orientation;
                CW(orientation);
            }

            N(orientation);
            for (int i = 0; i < 4; i++)
            {
                yield return orientation;
                CW(orientation);
            }

            S(orientation);
            S(orientation);

            for (int i = 0; i < 4; i++)
            {
                yield return orientation;
                CW(orientation);
            }

            N(orientation);
            W(orientation);

            for (int i = 0; i < 4; i++)
            {
                yield return orientation;
                CW(orientation);
            }

            E(orientation);
            E(orientation);

            for (int i = 0; i < 4; i++)
            {
                yield return orientation;
                CW(orientation);
            }

            E(orientation);

            for (int i = 0; i < 4; i++)
            {
                yield return orientation;
                CW(orientation);
            }
        }

        static void CW(int[] orientation)
        {
            var t = orientation[1];
            orientation[1] = orientation[2];
            orientation[2] = orientation[3];
            orientation[3] = orientation[4];
            orientation[4] = t;
        }

        static void CCW(int[] orientation)
        {
            CW(orientation);
            CW(orientation);
            CW(orientation);
        }

        static void N(int[] orientation)
        {
            var t = orientation[0];
            orientation[0] = orientation[1];
            orientation[1] = orientation[5];
            orientation[5] = orientation[3];
            orientation[3] = t;
        }

        static void S(int[] orientation)
        {
            N(orientation);
            N(orientation);
            N(orientation);
        }

        static void W(int[] orientation)
        {
            CW(orientation);
            N(orientation);
            CCW(orientation);
        }

        static void E(int[] orientation)
        {
            W(orientation);
            W(orientation);
            W(orientation);
        }
    }
}