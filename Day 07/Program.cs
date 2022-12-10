string[] lines = System.IO.File.ReadAllLines(@"PuzzleInput.txt");

Dictionary<string, Directory> FileSystem = new Dictionary<string, Directory>();

FileSystem["/"] = new Directory();

Directory current = FileSystem["/"];
string currentPath = "/";

foreach (var line in lines)
{
    if (line.StartsWith("dir"))
    {
        var newDir = line.Replace("dir ", "");
        newDir = currentPath + newDir + "/";
        FileSystem[newDir] = new Directory();
        FileSystem[newDir].ParentPath = currentPath;
    }
    else if (line.StartsWith("$"))
    {
        if (line == "$ cd /")
        {
            current = FileSystem["/"];
            currentPath = "/";
        }
        else if (line == "$ cd ..")
        {
            currentPath = current.ParentPath;
            current = FileSystem[current.ParentPath];
        }
        else if (line.StartsWith("$ cd "))
        {
            var newCurrent = currentPath + line.Replace("$ cd ", "") + "/";
            current = FileSystem[newCurrent];
            currentPath = newCurrent;
        }
    }
    else
    {
        current.TotalFileSize += int.Parse(line.Split(" ")[0]);
    }
}

Dictionary<string, int> DirectorySizes = new Dictionary<string, int>();

foreach (var directory in FileSystem.Keys)
{
    var dirandSubdir = FileSystem.Where(k => k.Key.Contains(directory));
    var TotalFileSizes = dirandSubdir.Select(D => D.Value.TotalFileSize).Sum();
    DirectorySizes[directory] = TotalFileSizes;
}

var SpaceNeeded = 30000000 - (70000000 - DirectorySizes["/"]);

Console.WriteLine($"Part 1 : {DirectorySizes.Values.Where(d => d <= 100000).Sum()}");

var SuitableFolder = DirectorySizes.Where(d => d.Value >= SpaceNeeded).OrderBy(d => d.Value).FirstOrDefault();

Console.WriteLine($"Part 2 : {SuitableFolder.Value}");

Console.ReadLine();


class Directory
{
    public int TotalFileSize { get; set; }
    public string ParentPath { get; set; }
}
