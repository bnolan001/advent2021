// See https://aka.ms/new-console-template for more information
ProblemOne();
Console.WriteLine("-----------------------------");
ProblemTwo();
Console.WriteLine("-----------------------------");
Console.WriteLine("Completed Day 7");

static void ProblemOne()
{
    Console.WriteLine("Day 7 Problem 1");

    var data = File.ReadAllLines("submarines.txt");
    var crabSubmarines = new Dictionary<int, int>();
    var startingPositions = data[0].Split(',', StringSplitOptions.RemoveEmptyEntries).Select(d => int.Parse(d)).ToList();

    foreach(var startingPosition in startingPositions)
    {
        if (!crabSubmarines.ContainsKey(startingPosition))
            crabSubmarines.Add(startingPosition, 0);
        crabSubmarines[startingPosition]++;
    }
    var orderedPositions = crabSubmarines.OrderByDescending(d => d.Key).ToList();
    var standardPosition = -1;

    var fuelCost = int.MaxValue;
    foreach (var position in orderedPositions)
    {
        var currentCost = 0;
        var currentPosition = position.Key;
        foreach (var sub in crabSubmarines)
        {
            currentCost += Math.Abs(sub.Key - currentPosition) * sub.Value;
        }
        if (currentCost < fuelCost)
        {
            fuelCost = currentCost;
            standardPosition = currentPosition;
        }
    }

    Console.WriteLine($"Fuel cost to move to level {standardPosition} is {fuelCost}");
}

static void ProblemTwo()
{
    Console.WriteLine("Day 7 Problem 2");

    var data = File.ReadAllLines("submarines.txt");
    var crabSubmarines = new Dictionary<int, int>();
    var startingPositions = data[0].Split(',', StringSplitOptions.RemoveEmptyEntries).Select(d => int.Parse(d)).ToList();

    foreach (var startingPosition in startingPositions)
    {
        if (!crabSubmarines.ContainsKey(startingPosition))
            crabSubmarines.Add(startingPosition, 0);
        crabSubmarines[startingPosition]++;
    }
    var orderedPositions = crabSubmarines.OrderByDescending(d => d.Key).ToList();
    var standardPosition = -1;

    var fuelCost = int.MaxValue;
    for (var currentPosition = 0; currentPosition < orderedPositions[0].Key; currentPosition++)
    {
        var currentCost = 0;
        foreach (var sub in crabSubmarines)
        {
            var cost = Math.Abs(sub.Key - currentPosition) * (Math.Abs(sub.Key - currentPosition) + 1) / 2; 
            currentCost += cost * sub.Value;
            //Console.WriteLine($"For level {currentPosition} it costs {sub.Value} submarites at {sub.Key} a total of {cost} fuel, total so far {currentCost}");
        }
        if (currentCost < fuelCost)
        {
            fuelCost = currentCost;
            standardPosition = currentPosition;
        }
    }

    Console.WriteLine($"Fuel cost to move to level {standardPosition} is {fuelCost}");
}
