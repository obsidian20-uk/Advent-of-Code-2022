using System.Data.SqlTypes;

namespace Day_11
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var Monkeys = new List<Monkey>();
            string[] lines = System.IO.File.ReadAllLines(@"PuzzleInput.txt");
            foreach (var monkeytext in InputProcessor(lines.Where(l => l != "").ToList()))
            {
                Monkeys.Add(new Monkey(monkeytext));
            }

            var factor = Monkeys.Aggregate(1, (c, m) => c * m.TestCondition);

            var test = 17 % 9699690;

            for (int i = 0; i < 10000; i++)
            {
                foreach (var monkey in Monkeys)
                {
                    while (monkey.Items.Count() != 0)
                    {
                        var item = monkey.InspectAndThrowItem(i, factor);
                        Monkeys[item.Item1].Items.Enqueue(item.Item2);
                    }
                }
            }

            var top2Monkeys = Monkeys.OrderByDescending(m => m.ItemsInspected).Take(2).ToArray();

            foreach (var item in top2Monkeys)
            {
                Console.WriteLine(item.ItemsInspected);
            }

            var MonkeyBusiness = top2Monkeys[0].ItemsInspected * top2Monkeys[1].ItemsInspected;

            Console.WriteLine($"Monkey Business: {MonkeyBusiness}");

            Console.ReadLine();
        }

        public static List<string> InputProcessor(List<string> input)
        {
            List<string> output = new List<string>();
            var outputstring = "";

            for (int i = 0; i < input.Count; i++)
            {
                if (input[i] != "")
                {
                    outputstring += input[i].Trim();
                }
                if ((i + 1) % 6 == 0)
                {
                    output.Add(outputstring);
                    outputstring = "";
                }
            }
            return output;
        }
    }
}