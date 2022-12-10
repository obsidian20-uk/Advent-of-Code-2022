using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_10
{
	public class Solution1
    {
        public static void Run()
        {
            var registerHistory = new List<int>();
            registerHistory.Add(1);
            string[] instructions = System.IO.File.ReadAllLines(@"PuzzleInput.txt");

            foreach (var instruction in instructions)
            {
                var instructionsplit = instruction.Split(" ");
                switch (instructionsplit[0])
                {
                    case "addx":
                        registerHistory.Add(registerHistory.LastOrDefault());
                        registerHistory.Add(registerHistory.LastOrDefault() + int.Parse(instructionsplit[1]));
                        break;
                    case "noop":
                        registerHistory.Add(registerHistory.LastOrDefault());
                        break;
                }
            }

            int ScreenPos = 0;
            var row = new char[40];

            for (int i = 0; i < 240; i++)
            {
                if (ScreenPos - 1 <= registerHistory[i] && ScreenPos + 1 >= registerHistory[i])
                {
                    row[ScreenPos] = '#';
                }
                else
                {
                    row[ScreenPos] = ' ';
                }

                ScreenPos++;

                if (ScreenPos > 39)
                {
                    ScreenPos = 0;
                    var output = new string(row);
                    Console.WriteLine(output);
                    row = new char[40];
                }

            }

            var Task1Output = 0;

            for (int i = 19; i < registerHistory.Count; i = i + 40)
            {
                Task1Output += (i + 1) * registerHistory[i];
                Console.WriteLine($"{i + 1} * {registerHistory[i]} = {(i + 1) * registerHistory[i]}");
            }

            Console.WriteLine("Task 1 = " + Task1Output);

        }
    }
}