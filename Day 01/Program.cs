Dictionary<int, int> Calories = new Dictionary<int, int>();
int ElfID = 1;

string[] lines = System.IO.File.ReadAllLines(@"PuzzleInput.txt");


foreach (string line in lines)
{
    if (line == "")
    {
        ElfID++;
        continue;
    }
    if (Calories.ContainsKey(ElfID))
    {
        Calories[ElfID] = Calories[ElfID] + int.Parse(line);
    }
    else
    {
        Calories.TryAdd(ElfID, int.Parse(line));
    }
}

var top3Total = Calories.OrderByDescending(c => c.Value).Take(3).ToList().Sum(s => s.Value);

Console.WriteLine(top3Total);

Console.ReadLine();
