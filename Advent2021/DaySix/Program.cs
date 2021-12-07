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
    PrintSchool(fish);
    for (var num = 0; num < numDays; num++)
    {
        for (int idx = 0; idx < fish.Count; idx++)
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
    Console.WriteLine($"");
}

static void PrintSchool(List<int> fish)
{
    var output = String.Join(' ', fish);
    Console.WriteLine(output);
}