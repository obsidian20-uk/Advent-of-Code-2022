using System;

string[] lines = System.IO.File.ReadAllLines(@"PuzzleInput.txt");

Console.WriteLine(Parse(lines[0]));


static int Parse(string data)
{
    for (int i = 0; i < data.Length; i++)
    {
        var found = true;
        var chunk = data.Skip(i).Take(14);
        foreach (var item in chunk)
        {
            if (chunk.Count(l => l == item) > 1)
            {
                found = false;
                break;
            }
        }
        if (found)
        {
            return i + 14;
        }
    }
    return 0;
}

Console.ReadLine();
