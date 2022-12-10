using System;

string[] lines = System.IO.File.ReadAllLines(@"PuzzleInput.txt");

var count = 0;

foreach (var pairs in lines.Select(l => l.Split(",")))
{
    var found = false;
    var pair1 = pairs[0].Split("-").Select(p => int.Parse(p)).ToList();
    var pair2 = pairs[1].Split("-").Select(p => int.Parse(p)).ToList();
    var pair1detailed = new List<int>();
    var pair2detailed = new List<int>();

    for (int i = pair1.Min(); i <= pair1.Max(); i++)
    {
        pair1detailed.Add(i);
    }

    for (int i = pair2.Min(); i <= pair2.Max(); i++)
    {
        pair2detailed.Add(i);
    }

    foreach (var section in pair1detailed)
    {
        if (pair2detailed.Contains(section) && !found)
        {
            count++;
            found = true;
        }
    }
}


Console.WriteLine(count);

Console.ReadLine();
