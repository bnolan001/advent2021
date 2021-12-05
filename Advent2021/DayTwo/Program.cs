// See https://aka.ms/new-console-template for more information
ProblemOne();
Console.WriteLine("-----------------------------");
ProblemTwo();
Console.WriteLine("-----------------------------");
Console.WriteLine("Completed Day 2");

static void ProblemOne()
{
    Console.WriteLine("Day 2 Problem 1");

    var data = File.ReadAllLines("movement.txt");

    var horizontal = 0;
    int vertical = 0;
    
    foreach (var line in data)
    {
        var split = line.Trim().Split(' ');
        var amount = int.Parse(split[1]);
        switch (split[0])
        {
            case "forward":
                horizontal += amount;
                break;

            case "down":
                vertical += amount;
                break;

            case "up":
                vertical -= amount;
                break;

            default:
                break;
        }
    }

    Console.WriteLine($"Final Position: Horizontal {horizontal}, Vertical {vertical}");
    Console.WriteLine($"Total change {horizontal * vertical}");
}

static void ProblemTwo()
{
    Console.WriteLine("Day 2 Problem 2");
    var data = File.ReadAllLines("movement.txt");

    var horizontal = 0;
    int vertical = 0;
    int aim = 0;

    foreach (var line in data)
    {
        var split = line.Trim().Split(' ');
        var amount = int.Parse(split[1]);
        switch (split[0])
        {
            case "forward":
                horizontal += amount;
                vertical += amount * aim;
                break;

            case "down":
                aim += amount;
                break;

            case "up":
                aim -= amount;
                break;

            default:
                break;
        }
    }

    Console.WriteLine($"Final Position: Horizontal {horizontal}, Vertical {vertical}");
    Console.WriteLine($"Total change {horizontal * vertical}");
}