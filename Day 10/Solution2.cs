using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_10
{
    public class Solution2
    {
        private static int registerValue = 1;
        private static int cycle = 0;
        private static int sum = 0;
        private static int screenPos = 0;
        public static void Run()
        {
            string[] instructions = System.IO.File.ReadAllLines(@"PuzzleInput.txt");

            foreach (var instruction in instructions)
            {
                var instructionsplit = instruction.Split(" ");
                switch (instructionsplit[0])
                {
                    case "addx":
                        runCycle(2);
                        registerValue += int.Parse(instructionsplit[1]);
                        break;
                    case "noop":
                        runCycle(1);
                        break;
                }
            }

            Console.WriteLine(sum);
        }

        private static void runCycle(int iterations)
        {
            for (int i = 0; i < iterations; i++)
            {
                if (screenPos - 1 <= registerValue && screenPos + 1 >= registerValue)
                {
                    Console.Write("#");
                }
                else
                {
                    Console.Write(" ");
                }
                cycle++;
                screenPos++;
                if (cycle % 40 == 20)
                {
                    sum += cycle * registerValue;
                }
                if (cycle % 40 == 0)
                {
                    screenPos = 0;
                    Console.WriteLine("");
                }

            }
        }
    }
}
