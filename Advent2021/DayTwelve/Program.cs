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
    var allCaves = new Dictionary<string, Cave>();
    var distinctPaths = new HashSet<string>();
    var visitedSmallcaves = new Dictionary<string, int>();
    BuildGraph(data, allCaves);
    var startCave = allCaves.Where(c => c.Key.Equals("start")).First().Value;
    FindDistinctPaths(startCave, visitedSmallcaves, "", distinctPaths, 1);
    Console.WriteLine($"Total of {distinctPaths.Count} were found");
}

static void ProblemTwo()
{
    Console.WriteLine("Day 12 Problem 2");
    var data = File.ReadAllLines("caves.txt");
    var allCaves = new Dictionary<string, Cave>();
    var distinctPaths = new HashSet<string>();
    var visitedSmallcaves = new Dictionary<string, int>();
    BuildGraph(data, allCaves);
    var startCave = allCaves.Where(c => c.Key.Equals("start")).First().Value;
    FindDistinctPaths(startCave, visitedSmallcaves, "", distinctPaths, 2);
    Console.WriteLine($"Total of {distinctPaths.Count} were found");
}

static void BuildGraph(string[] data, Dictionary<string, Cave> allCaves)
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

static void FindDistinctPaths(Cave caveGraph, Dictionary<string, int> visitedSmallCaves, string path, HashSet<string> distinctPaths, int smallVisitPermits)
{
    path = $"{path}->{caveGraph.Name}";
    if (!caveGraph.IsBig)
    {
        if (visitedSmallCaves.ContainsKey(caveGraph.Name))
        {
            visitedSmallCaves[caveGraph.Name]++;
            if (visitedSmallCaves[caveGraph.Name] > smallVisitPermits)
            {
                return;
            }
        }
        else
        {
            if (caveGraph.Name.Equals("start"))
            {
                visitedSmallCaves.Add(caveGraph.Name, smallVisitPermits);
            }
            else
            {
                visitedSmallCaves.Add(caveGraph.Name, 1);
            }
        }
    }
    foreach (var cave in caveGraph.Caves.Values)
    {
        if (cave.Name.Equals("end"))
        {
            var completePath = $"{path}->{cave.Name}";
            if (!distinctPaths.Contains(completePath)) 
            { 
                distinctPaths.Add(completePath);
                Console.WriteLine(completePath);
            }
        }
        else if (cave.IsBig
            || (!visitedSmallCaves.ContainsKey(cave.Name) || visitedSmallCaves[cave.Name] < smallVisitPermits))
        {
            FindDistinctPaths(cave, visitedSmallCaves, path, distinctPaths, smallVisitPermits);
        }
    }
    if (!caveGraph.IsBig && !caveGraph.Name.Equals("start"))
        visitedSmallCaves[caveGraph.Name]--;
}