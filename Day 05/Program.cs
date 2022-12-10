using System;
using System.Text.RegularExpressions;

var Stacks = new Dictionary<int, Stack<string>>();

var output = "";

string[] lines = System.IO.File.ReadAllLines(@"PuzzleInput.txt");

var separator = lines.TakeWhile(t => t != "").Count();

var data = lines.Take(separator).ToList();

Parse(data);

var instructions = lines.Skip(separator + 1);

foreach (var item in instructions)
{
    Regex regex = new Regex("move (.*) from (.*) to (.*)");
    var matches = regex.Match(item);
    var number = int.Parse(matches.Groups[1].Value);
    var from = int.Parse(matches.Groups[2].Value);
    var to = int.Parse(matches.Groups[3].Value);
    var tempStack = new Stack<string>();
    for (int i = 0; i < number; i++)
    {
        tempStack.Push(Stacks[from].Pop());
    }
    while (tempStack.Count > 0)
    {
        Stacks[to].Push(tempStack.Pop());
    }
}

foreach (var stack in Stacks)
{
    output += stack.Value.Peek();
}


Console.WriteLine(output);


void Parse(List<string> data)
{
    data.Reverse();
    for (int i = 0; i < data.Count; i++)
    {
        if (i == 0)
        {
            Regex regex = new Regex("([0-9]+)");
            var row = regex.Match(data[i]);
            foreach (var item in regex.Matches(data[i]).ToList())
            {
                Stacks[int.Parse(item.Captures.FirstOrDefault().Value)] = new Stack<string>();
            }
        }
        else
        {
            Regex regex = new Regex(@"\[[A-Z]\]|    ");
            int StackID = 1;
            foreach (var item in regex.Matches(data[i]).ToList())
            {
                if (item.Captures.FirstOrDefault().Value.Replace("[", "").Replace("]", "").Replace(" ", "") != "")
                {
                    Stacks[StackID].Push(item.Captures.FirstOrDefault().Value.Replace("[", "").Replace("]", "").Replace(" ", ""));
                }
                StackID++;
            }
        }
    }
}

Console.ReadLine();
