// See https://aka.ms/new-console-template for more information
ProblemOne();
Console.WriteLine("-----------------------------");
ProblemTwo();
Console.WriteLine("-----------------------------");
Console.WriteLine("Completed Day 11");

static void ProblemOne()
{
    Console.WriteLine("Day 11 Problem 1");
    var data = File.ReadAllLines("octopuses.txt");
    var octopuses = new List<List<int>>();
    var totalFlashes = 0;

    foreach (var line in data)
    {
        octopuses.Add(ParseLine(line));
    }

    Console.WriteLine($"Iteration 1");
    PrintOctopuses(octopuses);

    for (int i = 0; i < 100; i++)
    {
        totalFlashes += IterateLifecycle(octopuses);
        ResetFlashedOctopuses(octopuses);        
    }

    Console.WriteLine($"Total flashes {totalFlashes}");
}

static void ProblemTwo()
{
    Console.WriteLine("Day 11 Problem 2");
    var data = File.ReadAllLines("octopuses.txt");
    var octopuses = new List<List<int>>();
    var iteration = 0;

    foreach (var line in data)
    {
        octopuses.Add(ParseLine(line));
    }

    while(!AreAllFlashes(octopuses))
    {
        IterateLifecycle(octopuses);
        ResetFlashedOctopuses(octopuses);
        PrintOctopuses(octopuses);
        iteration++;
    }

    Console.WriteLine($"First time all flash is iteration {iteration}");
}

static List<int> ParseLine(string line)
{
    return line.Select(l => int.Parse(l.ToString())).ToList();
}

static void ResetFlashedOctopuses(List<List<int>> octopuses)
{
    for (int row = 0; row < octopuses.Count; row++)
    {
        for (int col = 0; col < octopuses[row].Count; col++)
        {
            if (octopuses[row][col] > 9)
            {
                octopuses[row][col] = 0;
            }
        }
    }
}

static int IterateLifecycle(List<List<int>> octopuses)
{
    var flashes = 0;
    for(int row = 0; row < octopuses.Count; row++)
    {
        for (int col = 0; col < octopuses[row].Count; col++)
        {
            octopuses[row][col]++;
            if (octopuses[row][col] == 10)
            {
                flashes++;
                flashes += NeighborImpact(row, col, octopuses);
            }
        }
    }

    return flashes;
}

static int NeighborImpact(int flashRow, int flashCol, List<List<int>> octopuses)
{
    var startCol = flashCol > 0? flashCol - 1: 0;
    var startRow = flashRow > 0? flashRow - 1: 0;
    var endCol = flashCol < octopuses[0].Count - 1 ? flashCol + 1 : flashCol;
    var endRow = flashRow < octopuses[0].Count - 1 ? flashRow + 1 : flashRow;
    var flashes = 0;

    for (var row = startRow; row <= endRow; row++)
    {
        for (var col = startCol; col <= endCol; col++)
        {
            octopuses[row][col]++;
            if (octopuses[row][col] == 10)
            {
                flashes++;
                flashes += NeighborImpact(row, col, octopuses);
            }
        }
    }

    return flashes;
}

static bool AreAllFlashes(List<List<int>> octopuses)
{
    foreach(var line in octopuses)
    {
        if (line.Any(l => l != 0))
        {
            return false;
        }
    }
    return true;
}

static void PrintOctopuses(List<List<int>> octopuses)
{
    foreach(var octopus in octopuses)
    {
        Console.WriteLine(String.Join(' ', octopus));
    }
    Console.WriteLine("---------------------");
}