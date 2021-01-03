using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Solutions
{
    public class ProblemM_solved
    {
        public static void Main()
        {
            Main(Console.In);
        }

        public static void Main(TextReader input)
        {
            var n = int.Parse(input.ReadLine());
            var receipt = input.ReadLine().Split();
            var m = int.Parse(input.ReadLine());
            var price = new Dictionary<string, long>();
            for (int i = 0; i < m; i++)
            {
                var line = input.ReadLine().Split();
                price[line[0]] = long.Parse(line[1]);
            }

            var k = int.Parse(input.ReadLine());
            var cook = new Dictionary<string, string[]>();
            for (int i = 0; i < k; i++)
            {
                var line = input.ReadLine().Split();
                cook[line[1]] = line.Skip(2).ToArray();
            }

            var cache = new Dictionary<string, long>();

            cook["RECEIPT"] = receipt;
            Console.Out.WriteLine(Cost("RECEIPT"));

            long Cost(string ingredient)
            {
                if (cache.TryGetValue(ingredient, out var result))
                    return result;

                if (!price.TryGetValue(ingredient, out var selfPrice))
                    selfPrice = long.MaxValue;

                var cookCost = long.MaxValue;
                if (cook.TryGetValue(ingredient, out var components))
                {
                    cookCost = 0;
                    foreach (var component in components)
                    {
                        var componentCost = Cost(component);
                        if (componentCost < 0)
                        {
                            cookCost = long.MaxValue;
                            break;
                        }

                        cookCost += componentCost;
                    }
                }

                var cost = Math.Min(selfPrice, cookCost);
                if (cost == long.MaxValue)
                    cost = -1;

                return cache[ingredient] = cost;
            }
        }
    }
}