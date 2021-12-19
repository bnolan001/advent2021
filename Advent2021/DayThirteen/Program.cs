// See https://aka.ms/new-console-template for more information
using DayThirteen;

ProblemOne();
Console.WriteLine("-----------------------------");
ProblemTwo();
Console.WriteLine("-----------------------------");
Console.WriteLine("Completed Day 13");

static void ProblemOne()
{
    Console.WriteLine("Day 13 Problem 1");
    var data = File.ReadAllLines("numbers.txt");
    var origami = ParseData(data);
    origami.CreatePaper();
    origami.FoldPaper(origami.Folds[0]);
    var totalDots = GetTotalDots(origami);
    Console.WriteLine($"Total dots are {totalDots}");
}

static void ProblemTwo()
{
    Console.WriteLine("Day 13 Problem 2");
    var data = File.ReadAllLines("numbers.txt");
    var origami = ParseData(data);
    Console.WriteLine($"");
}

static Origami ParseData(string[] data)
{
    var origami = new Origami();
    bool parsingPoints = true;
    var coords = new List<Coordinate>();
    for(int idx = 0; idx < data.Length; idx++)
    {
        if (parsingPoints)
        {
            if (String.IsNullOrEmpty(data[idx]))
            {
                parsingPoints = false;
                continue;
            }
            var strCoords = data[idx].Split(',');
            origami.Coordinates.Add(new Coordinate { X = int.Parse(strCoords[0]), Y = int.Parse(strCoords[1])}); 
        }
        else
        {
            int splitOn = data[idx].IndexOf('=');
            if (splitOn > 0)
            {
                var fold = int.Parse(data[idx].Substring(splitOn + 1));
                var coord = data[idx].Contains('x')? new Coordinate { X=fold, Y=0 }: new Coordinate { X = 0, Y = fold };
                origami.Folds.Add(coord);
            }
        }
    }
    
    return origami;
}

static void PrintPaper(Origami origami)
{
    //for (var row = origami.Paper.Count - 1; row >= 0; row--)
    foreach(var row in origami.Paper)
    {
        Console.WriteLine(String.Join(" ", row));
        //Console.WriteLine(String.Join(" ", origami.Paper[row]));
    }
    Console.WriteLine("----------------");
}

static int GetTotalDots(Origami origami)
{
    var totalDots = 0;
    foreach(var row in origami.Paper)
    {
        totalDots += row.Count(d => d > 0);
    }

    return totalDots;
}