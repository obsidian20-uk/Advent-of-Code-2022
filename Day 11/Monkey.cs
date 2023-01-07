using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day_11
{
    internal class Monkey
    {
        public Monkey(string monkeytext)
        {
            Regex regex = new Regex("monkey .*:starting items: (.*)operation: new = (.*)test: divisible by (.*)if true: throw to monkey (.*)if false: throw to monkey (.*)");
            var processedmonkeytext = regex.Match(monkeytext.ToLower());
            foreach (var item in processedmonkeytext.Groups[1].Value.Split(", "))
            {
                if (int.TryParse(item, out int val1))
                {
                    Items.Enqueue(val1);
                }
                else
                {
                    Console.WriteLine($"Invalid Starting item: {item}");
                }
            }
            var Operation = processedmonkeytext.Groups[2].Value.Split(" ");
            Operator = Operation[1];
            OperationValue = Operation[2];

            if (int.TryParse(processedmonkeytext.Groups[3].Value, out int val3))
            {
                TestCondition = val3;
            }
            else
            {
                Console.WriteLine($"Invalid Test Condition {processedmonkeytext.Groups[3].Value}");
            }

            if (int.TryParse(processedmonkeytext.Groups[4].Value, out int val4))
            {
                TrueMonkey = val4;
            }
            else
            {
                Console.WriteLine($"Invalid True Monkey Value {processedmonkeytext.Groups[4].Value}");
            }

            if (int.TryParse(processedmonkeytext.Groups[5].Value, out int val5))
            {
                FalseMonkey = val5;
            }
            else
            {
                Console.WriteLine($"Invalid False Monkey Value {processedmonkeytext.Groups[5].Value}");
            }

        }
        public Queue<long> Items { get; set; } = new Queue<long>();
        public string Operator { get; set; } = "";
        public string OperationValue { get; set; }
        public int TestCondition { get; set; }
        public int TrueMonkey { get; set; }
        public int FalseMonkey { get; set; }
        public long ItemsInspected { get; set; } = 0;

        public Tuple<int, long> InspectAndThrowItem(int roundNum, int moduloFactor)
        {
            int destination;
            var item = Items.Dequeue();
            long Operand;
            if (OperationValue == "old")
            {
                Operand = item;
            }
            else
            {
                Operand = int.Parse(OperationValue);
            }
            switch (Operator)
            {
                case "*":
                    item = item * Operand;
                    break;
                case "+":
                    item = item + Operand;
                    break;
            }

            //if (roundNum < 20)
            //{
            //    item = item / 3;
            //}
            
            item %= moduloFactor;

            if (item % TestCondition == 0)
            {
                destination = TrueMonkey;
            }
            else
            {
                destination = FalseMonkey;
            }
            ItemsInspected++;
            return new Tuple<int, long>(destination, item);
        }
    }
}
