using System;
using System.IO;
using Solutions;

namespace Runner
{
    class Program
    {
        static void Main()
        {
            //File.WriteAllText("inputB.txt", string.Join("+", Enumerable.Repeat("9", 100000)));
            //ProblemB.Main(new StreamReader("inputB.txt"));
            //ProblemB.Main();

//             var input = @"
// 4 1
// 1 1 1 1
// 1 2
// 2 3
// 2 4
// ".Trim();

            var input = @"
8
-37014733 -74508659
-28309386 -47777745
50871037 71259303
58942854 -54374479
93276296 -5249376
19854770 73070603
-15695264 62665346
-69329048 -72962594
".Trim();
//             var input = @"
// 4 1
// 1 1 1 1
// 1 2
// 2 3
// 2 
// ".Trim();

            ProblemH_solved.Main(new StringReader(input));
        }
    }
}