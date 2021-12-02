// See https://aka.ms/new-console-template for more information

ProblemTwo();

static void ProblemOne() {
    Console.WriteLine("Day 1 Problem 1");

    var data = File.ReadAllLines("depths.txt");

    int increases = 0;
    int prevDepth = -1;
    bool firstTime = true;
    foreach (var line in data)
    {
        int depth = int.Parse(line.Trim());
        if (firstTime)
        {
            prevDepth = depth;
            firstTime = false;
            continue;
        }
        if (depth > prevDepth)
        {
            increases++;
        }

        prevDepth = depth;
    }

    Console.WriteLine($"Total Increases: {increases}");
}

static void ProblemTwo()
{
    Console.WriteLine("Day 1 Problem 2");

    var data = File.ReadAllLines("depths.txt");

    int increases = 0;
    var firstSet = new List<int>() {
        int.Parse(data[0]), int.Parse(data[1])};

    var secondSet = new List<int>() {
        int.Parse(data[1]), int.Parse(data[2])};
    for (var idx = 3; idx < data.Length; idx++)
    {

        firstSet.Add(int.Parse(data[idx - 1]));
        secondSet.Add(int.Parse(data[idx]));
        int depthOne = firstSet.Sum();
        int depthTwo = secondSet.Sum();

        Console.WriteLine($"Comparing A: {depthOne} to B: {depthTwo}");

        if (depthTwo > depthOne)
        {
            increases++;
        }

        firstSet.RemoveAt(0);
        secondSet.RemoveAt(0);
    }

    Console.WriteLine($"Total Increases: {increases}");
}