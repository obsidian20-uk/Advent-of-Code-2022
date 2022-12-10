using System;

Dictionary<int, int> Calories = new Dictionary<int, int>();
int ElfID = 1;

string[] lines = System.IO.File.ReadAllLines(@"PuzzleInput.txt");

var priorities = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

int Score = 0;

Dictionary<int, List<string>> Groups = new Dictionary<int, List<string>>();

for (int i = 0; i < lines.Length / 3; i++)
{
    var found = false;

    var groupLines = lines.Skip(i * 3).Take(3).ToList();
    foreach (var item in groupLines[0].ToCharArray())
    {
        if (groupLines[1].Contains(item) && groupLines[2].Contains(item) && !found)
        {
            Score += Array.IndexOf(priorities, item) + 1;
            found = true;
        }
    }
}

//foreach (var line in lines)
//{
//    var compartment1 = line.Take(line.Length / 2 );
//    var compartment2 = line.Skip(line.Length / 2);
//    var found = false;
//    foreach (var item in compartment1)
//    {
//        if (compartment2.Contains(item) && !found)
//        {
//            Score += Array.IndexOf(priorities, item) + 1;
//            found = true;
//        }
//    }
//}



Console.WriteLine(Score);

Console.ReadLine();
