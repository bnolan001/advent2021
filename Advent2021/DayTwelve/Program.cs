// See https://aka.ms/new-console-template for more information
// See https://aka.ms/new-console-template for more information
using DayTwelve;

ProblemOne();
Console.WriteLine("-----------------------------");
ProblemTwo();
Console.WriteLine("-----------------------------");
Console.WriteLine("Completed Day 11");

static void ProblemOne()
{
    Console.WriteLine("Day 11 Problem 1");
    var data = File.ReadAllLines("caves.txt");
    var caveGraph = new Cave("start");
    var allCaves = new Dictionary<string, Cave>();
    var distinctPaths = new List<string>();
    var visitedSmallcaves = new HashSet<string>();
    allCaves.Add(caveGraph.Name, caveGraph);
    BuildGraph(data, caveGraph, allCaves);

    FindDistinctPaths(caveGraph, visitedSmallcaves, "", distinctPaths);
    Console.WriteLine($"");
}

static void ProblemTwo()
{
    Console.WriteLine("Day 12 Problem 2");
    var data = File.ReadAllLines("caves.txt");
    var caveGraph = new Cave("start");
    var allCaves = new Dictionary<string, Cave>();

    BuildGraph(data, caveGraph, allCaves);

    Console.WriteLine($"");
}

static void BuildGraph(string[] data, Cave cave, Dictionary<string, Cave> allCaves)
{
    foreach (var line in data)
    {
        var coords = ParseLine(line);
        if (coords.Count() < 2)
        {
            continue;
        }
        if (!allCaves.TryGetValue(coords[0], out var startCave))
        {
            startCave = new Cave(coords[0]);
            allCaves.Add(coords[0], startCave);
            cave.LinkCave(startCave);
        }

        if (!allCaves.TryGetValue(coords[1], out var endCave))
        {
            endCave = new Cave(coords[1]);
            allCaves.Add(coords[1], endCave);
        }
        startCave.LinkCave(endCave);
        endCave.LinkCave(startCave);
    }
}

static string[] ParseLine(string line)
{
    var coords = line.Split('-', StringSplitOptions.RemoveEmptyEntries);

    return coords;
}

static void FindDistinctPaths(Cave caveGraph, HashSet<string> visitedSmallCaves, string path, List<string> paths)
{
    foreach(var cave in caveGraph.Caves.Values)
    {
        if (cave.Name.Equals("end"))
        {
            var completePath = $"{path}->{cave.Name}";
            paths.Add(completePath);
            Console.WriteLine(completePath);
        }
        else if (cave.IsBig
            || !visitedSmallCaves.Contains(cave.Name))
        {
            if (!cave.IsBig)
            {
                visitedSmallCaves.Add(cave.Name);
            }
            FindDistinctPaths(cave, visitedSmallCaves, $"{path}->{caveGraph.Name}", paths);
        }
    }
    if (!caveGraph.IsBig)
        visitedSmallCaves.Remove(caveGraph.Name);
}