// See https://aka.ms/new-console-template for more information
using DayFive;

ProblemOne();
Console.WriteLine("-----------------------------");
ProblemTwo();
Console.WriteLine("-----------------------------");
Console.WriteLine("Completed Day 5");

static void ProblemOne()
{
    Console.WriteLine("Day 5 Problem 1");

    var data = File.ReadAllLines("lines.txt");
    var lines = new List<Line>();
    var maxX = 0;
    var maxY = 0;
    foreach(var entry in data)
    {
        var line = new Line();
        line.SetDefinition(entry);
        lines.Add(line);
        maxX = maxX < line.GetMaxHorizontal() ? line.GetMaxHorizontal(): maxX;
        maxY = maxY < line.GetMaxVertical() ? line.GetMaxVertical(): maxY;
    }
    
    var graph = new int[maxX + 1, maxY+ 1];
    int overlap = 0;
    foreach(var line in lines)
    {
        if (line.Direction != Direction.Diagonal)
        {
            GraphTheLine(graph, line, ref overlap);
        }
    }

    Console.WriteLine($"Total overlap is {overlap} ");    
}

static void ProblemTwo()
{
    Console.WriteLine("Day 5 Problem 2");

    var data = File.ReadAllLines("lines.txt");
        var lines = new List<Line>();
    var maxX = 0;
    var maxY = 0;
    foreach (var entry in data)
    {
        var line = new Line();
        line.SetDefinition(entry);
        lines.Add(line);
        maxX = maxX < line.GetMaxHorizontal() ? line.GetMaxHorizontal() : maxX;
        maxY = maxY < line.GetMaxVertical() ? line.GetMaxVertical() : maxY;
    }

    var graph = new int[maxX + 1, maxY + 1];
    int overlap = 0;
    foreach (var line in lines)
    {
        if (line.Direction != Direction.Diagonal)
        {
            GraphTheLine(graph, line, ref overlap);
        } else
        {
            GraphTheDiagonalLine(graph, line, ref overlap);
        }
    }

    Console.WriteLine($"Total overlap is {overlap} ");
}

static void GraphTheLine(int[,] graph, Line line, ref int overlap)
{
    int endX = line.StartX > line.EndX ? line.StartX : line.EndX;
    int startX = line.StartX < line.EndX ? line.StartX : line.EndX;
    int endY = line.StartY > line.EndY ? line.StartY : line.EndY;
    int startY = line.StartY < line.EndY ? line.StartY : line.EndY;
    for (; startX <= endX; startX++)
    {
        for(int idxY = startY; idxY <= endY; idxY++)
        {
            graph[startX, idxY]++;
            if (graph[startX, idxY] == 2) overlap++;
        }
    }
}

static void GraphTheDiagonalLine(int[,] graph, Line line, ref int overlap)
{
    var xDirection = line.StartX > line.EndX ? -1 : 1;
    var yDirection = line.StartY > line.EndY ? -1 : 1;
    var drawLine = true;
    var step = 0;
    while(drawLine)
    {
        var xPosition = (step * xDirection) + line.StartX;
        var yPosition = (step * yDirection) + line.StartY;
        graph[xPosition, yPosition]++;
        if (graph[xPosition, yPosition] == 2) overlap++;

        if (xPosition == line.EndX
            && yPosition == line.EndY)
        {
            drawLine = false;
        }
    step++;
    }
}

static void PrintGraph(int[,] graph, int maxX, int maxY)
{
    for(var y = 0; y <= maxY;y++)
    {
        for(var x = 0; x <= maxX; x++)
        {
            Console.Write($"{graph[x, y]}\t");
        }
        Console.WriteLine();
    }
}