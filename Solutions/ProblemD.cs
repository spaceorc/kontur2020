using System;
using System.IO;

namespace Solutions
{
    public class ProblemD
    {
        public static void Main()
        {
            Main(Console.In);
        }

        public static void Main(TextReader input)
        {
            var n = int.Parse(input.ReadLine());
            for (int i = 0; i < n; i++)
            {
                var line = input.ReadLine();
                
                Console.Out.WriteLine(line);
            }
        }
    }
}