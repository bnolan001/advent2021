// See https://aka.ms/new-console-template for more information
ProblemOne();
Console.WriteLine("-----------------------------");
ProblemTwo();
Console.WriteLine("-----------------------------");
Console.WriteLine("Completed Day 6");

static void ProblemOne()
{
    Console.WriteLine("Day 6 Problem 1");

    var data = File.ReadAllLines("fish.txt");
    var numDays = 80;
    var fish = data[0].Split(',', StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToList<int>();
    PrintSchool(fish);
    for(var num = 0; num < numDays; num++)
    {
        for(int idx = 0; idx < fish.Count; idx++)
        {
            if (fish[idx] == 0)
            {
                fish.Add(9);
                fish[idx] = 7;
            }
            fish[idx]--;
        }
    }

    Console.WriteLine($"Total fish is {fish.Count}");
}

static void ProblemTwo()
{
    Console.WriteLine("Day 6 Problem 2");

    var data = File.ReadAllLines("fish.txt");
    var numDays = 256;
    var fish = data[0].Split(',', StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToList<int>();
    var fishBuckets = new List<long>
    {
        {fish.Count(f => f == 0) },
        {fish.Count(f => f == 1) },
        {fish.Count(f => f == 2) },
        {fish.Count(f => f == 3) },
        {fish.Count(f => f == 4) },
        {fish.Count(f => f == 5) },
        {fish.Count(f => f == 6) },
        {fish.Count(f => f == 7) },
        {fish.Count(f => f == 8) },
    };
    PrintSchool(fish);
    long totalFish = 0;
    for (var num = 0; num < numDays; num++)
    {
        var spawn = fishBuckets[0];
        fishBuckets.RemoveAt(0);
        fishBuckets.Add(spawn);
        fishBuckets[6] += spawn;
    }
    totalFish = fishBuckets.Sum(); 
    Console.WriteLine($"Total fish is {totalFish}");
}

static void PrintSchool(List<int> fish)
{
    var output = String.Join(' ', fish);
    Console.WriteLine(output);
}