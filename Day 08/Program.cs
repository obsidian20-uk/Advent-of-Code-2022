using System.Linq;

string[] lines = System.IO.File.ReadAllLines(@"PuzzleInput.txt");

var numLines = lines.Length;
var longestLine = lines.Select(l => l.Length).Max();

var forest = new Tree[numLines, longestLine];

for (int i = 0; i < numLines; i++)
{
    var line = lines[i].ToArray().Select(c => (int)char.GetNumericValue(c)).ToArray();

    for (int j = 0; j < line.Length; j++)
    {
        forest[i, j] = new Tree(line[j], false);
    }
}

for (int i = 0; i < forest.GetLength(0); i++)
{
    for (int j = 0; j < forest.GetLength(1); j++)
    {
        var toTheTop = SliceRow(forest, j, 0, i).Reverse().ToArray();
        var toTheBottom = SliceRow(forest, j, i + 1, forest.GetLength(0)).ToArray();
        var toTheLeft = SliceColumn(forest, i, 0, j).Reverse().ToArray();
        var toTheRight = SliceColumn(forest, i, j + 1, forest.GetLength(1)).ToArray();

        forest[i, j].ViewLeft = toTheLeft.Count();
        forest[i, j].ViewRight = toTheRight.Count();
        forest[i, j].ViewUp = toTheTop.Count();
        forest[i, j].ViewDown = toTheBottom.Count();

        if (!toTheLeft.Any() || !toTheRight.Any() || !toTheBottom.Any() || !toTheTop.Any())
        {
            forest[i, j].Visible = true;
        }
        else
        {
            if (forest[i, j].Height > toTheLeft.Max(t => t.Height))
            {
                forest[i, j].Visible = true;
            }
            if (forest[i, j].Height > toTheRight.Max(t => t.Height))
            {
                forest[i, j].Visible = true;
            }
            if (forest[i, j].Height > toTheTop.Max(t => t.Height))
            {
                forest[i, j].Visible = true;
            }
            if (forest[i, j].Height > toTheBottom.Max(t => t.Height))
            {
                forest[i, j].Visible = true;
            }
        }


        if (toTheLeft.Count() > 0)
        {
            for (int l = 0; l < toTheLeft.Count(); l++)
            {
                if (toTheLeft[l].Height >= forest[i, j].Height)
                {
                    forest[i, j].ViewLeft = l + 1;
                    break;
                }
            }
        }

        if (toTheRight.Count() > 0)
        {
            for (int r = 0; r < toTheRight.Count(); r++)
            {
                if (toTheRight[r].Height >= forest[i, j].Height)
                {
                    forest[i, j].ViewRight = r + 1;
                    break;

                }
            }
        }

        if (toTheTop.Count() > 0)
        {
            for (int t = 0; t < toTheTop.Count(); t++)
            {
                if (toTheTop[t].Height >= forest[i, j].Height)
                {
                    forest[i, j].ViewUp = t + 1;
                    break;

                }
            }
        }
        if (toTheBottom.Count() > 0)
        {
            for (int b = 0; b < toTheBottom.Count(); b++)
            {
                if (toTheBottom[b].Height >= forest[i, j].Height)
                {
                    forest[i, j].ViewDown = b + 1;
                    break;

                }
            }
        }

        Console.WriteLine($"{i},{j}: {forest[i, j].Height} : {forest[i, j].ViewLeft} * {forest[i, j].ViewRight} * {forest[i, j].ViewUp} * {forest[i, j].ViewDown} = {forest[i, j].ScenicScore}");

    }

}

var count = 0;
var HighestScenicScore = 0;

for (int i = 0; i < forest.GetLength(0); i++)
{
    for (int j = 0; j < forest.GetLength(1); j++)
    {
        if (forest[i, j].Visible == true)
        {
            count++;
        }
        if (forest[i, j].ScenicScore > HighestScenicScore)
        {
            HighestScenicScore = forest[i, j].ScenicScore;
        }
    }
}

Console.WriteLine("Visible Trees: " + count);

Console.WriteLine("Highest Scenic Score Trees: " + HighestScenicScore);

Console.ReadLine();

IEnumerable<T> SliceRow<T>(T[,] array, int row, int start, int length)
{
    for (var i = start; i < length; i++)
    {
        yield return array[i, row];
    }
}

IEnumerable<T> SliceColumn<T>(T[,] array, int column, int start, int length)
{
    for (var i = start; i < length; i++)
    {
        yield return array[column, i];
    }
}

class Tree
{
    public Tree(int height, bool visible)
    {
        Height = height;
        Visible = visible;
    }
    public int Height;
    public bool Visible = false;
    public int ViewLeft;
    public int ViewRight;
    public int ViewUp;
    public int ViewDown;

    public int ScenicScore
    {
        get
        {
            return (ViewLeft * ViewRight * ViewUp * ViewDown);
        }
    }
}

