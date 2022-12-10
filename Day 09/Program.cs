using System.Linq;

string[] lines = System.IO.File.ReadAllLines(@"PuzzleInput.txt");

var KnotLocations = new List<Location>();
var TailLocations = new List<string>();
var NumberOfKnots = 10;
for (int i = 0; i < NumberOfKnots; i++)
{
    KnotLocations.Add(new Location(0, 0));
}

TailLocations.Add($"{KnotLocations.LastOrDefault().X},{KnotLocations.LastOrDefault().Y}");


foreach (var line in lines)
{
    var command = line.Split(' ');
    for (int i = 0; i < int.Parse(command[1]); i++)
    {
        switch (command[0])
        {
            case "R":
                KnotLocations[0].X += 1;
                break;
            case "L":
                KnotLocations[0].X -= 1;
                break;
            case "U":
                KnotLocations[0].Y += 1;
                break;
            case "D":
                KnotLocations[0].Y -= 1;
                break;
        }

        for (int j = 1; j < KnotLocations.Count; j++)
        {
            if (!KnotLocations[j].IsAdjacent(KnotLocations[j - 1]))
            {
                if (KnotLocations[j - 1].X - KnotLocations[j].X > 0)
                {
                    KnotLocations[j].X += 1;
                }
                if (KnotLocations[j - 1].X - KnotLocations[j].X < 0)
                {
                    KnotLocations[j].X -= 1;
                }
                if (KnotLocations[j - 1].Y - KnotLocations[j].Y > 0)
                {
                    KnotLocations[j].Y += 1;
                }
                if (KnotLocations[j - 1].Y - KnotLocations[j].Y < 0)
                {
                    KnotLocations[j].Y -= 1;
                }
            }
        }

        TailLocations.Add($"{KnotLocations.LastOrDefault().X},{KnotLocations.LastOrDefault().Y}");
    }
}
Console.WriteLine($"{TailLocations.Distinct().Count()}");

Console.ReadLine();

public class Location
{
    public Location(int x, int y)
    {
        X = x;
        Y = y;
    }
    public int X { get; set; }
    public int Y { get; set; }

    public bool IsAdjacent(Location otherLocation)
    {
        if (Math.Abs(X - otherLocation.X) <= 1 && Math.Abs(Y - otherLocation.Y) <= 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
