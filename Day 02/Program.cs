string[] lines = System.IO.File.ReadAllLines(@"PuzzleInput.txt");

int Score = 0;

foreach (var line in lines)
{
    var lineSplit = line.Split(" ");
    switch (lineSplit[1])
    {
        case "Y":
            Score += 3;
            break;
        case "Z":
            Score += 6;
            break;
    }

    if (lineSplit[0] == "A") //Rock
    {
        if (lineSplit[1] == "X") //Lose - Scissors
        {
            Score += 3;
        }
        if (lineSplit[1] == "Y") //Draw - Rock
        {
            Score += 1;
        }
        if (lineSplit[1] == "Z") //Win - Paper
        {
            Score += 2;
        }
    }
    if (lineSplit[0] == "B") //Paper
    {
        if (lineSplit[1] == "X") //Lose - Rock
        {
            Score += 1;
        }
        if (lineSplit[1] == "Y") //Draw - Paper
        {
            Score += 2;
        }
        if (lineSplit[1] == "Z") //Win - Scissors
        {
            Score += 3;
        }
    }
    if (lineSplit[0] == "C") //Scissors
    {
        if (lineSplit[1] == "X") //Lose - Paper
        {
            Score += 2;
        }
        if (lineSplit[1] == "Y") //Draw - Scissors
        {
            Score += 3;
        }
        if (lineSplit[1] == "Z") //Win - Rock
        {
            Score += 1;
        }
    }
}

Console.WriteLine(Score);

Console.ReadLine();
