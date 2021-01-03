using System;
using System.IO;
using System.Text;

namespace Solutions
{
    public class ProblemF_solved
    {
        public static void Main()
        {
            Main(Console.In);
        }

        public static void Main(TextReader input)
        {
            var a = input.ReadLine();
            var b = input.ReadLine();

            var letters = new int['z' - 'a' + 1]; 
            foreach (var c in a)
                letters[c - 'a']++;
            foreach (var c in b)
                letters[c - 'a']++;

            var left = a.Length;
            var result = new StringBuilder();
            for (var c = 'a'; c <= 'z' && left > 0; c++)
            {
                var v = letters[c - 'a'];
                var use = Math.Min(left, v);
                result.Append(c, use);
                left -= use;
            }

            Console.Out.WriteLine(result);
        }
    }
}