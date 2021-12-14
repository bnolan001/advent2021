// See https://aka.ms/new-console-template for more information
using DayNine;

ProblemOne();
Console.WriteLine("-----------------------------");
ProblemTwo();
Console.WriteLine("-----------------------------");
Console.WriteLine("Completed Day 9");

static void ProblemOne()
{
    Console.WriteLine("Day 9 Problem 1");
    var data = File.ReadAllLines("histogram.txt");
    var histogram = new List<List<int>>();
    histogram.Add(ParseLine(data[0]));
    histogram.Add(ParseLine(data[1]));
    var minValues = new List<int>();
    var numRows = data.Count();
    var numCols = data[0].Count();
    for (var row = 0; row < numRows; row++)
    {
        if (row + 2 < numRows)
        {
            histogram.Add(ParseLine(data[row + 2]));
        }
        for (var col = 0; col < numCols; col++)
        {
            if (IsBasin(row, col, numRows, numCols, histogram))
            {
                minValues.Add(histogram[row][col]);
            }
        }
    }

    var sum = minValues.Sum() + minValues.Count();
    Console.WriteLine($"Total min values is {sum}");
}

static void ProblemTwo()
{
    Console.WriteLine("Day 9 Problem 2");
    var data = File.ReadAllLines("histogram.txt");

    var histogram = new List<List<int>>();
    histogram.Add(ParseLine(data[0]));
    histogram.Add(ParseLine(data[1]));
    var downSlopeCoords = new HashSet<string>();
    var basinCoords = new List<Basin>();
    var numRows = data.Count();
    var numCols = data[0].Count();
    for (var row = 0; row < numRows; row++)
    {
        if (row + 2 < numRows)
        {
            histogram.Add(ParseLine(data[row + 2]));
        }
        for (var col = 0; col < numCols; col++)
        {
            if (IsBasin(row, col, numRows, numCols, histogram))
            {
                basinCoords.Add(new Basin
                {
                    Row = row,
                    Column = col,
                });
            }
        }        
    }
    foreach (var basin in basinCoords)
    {
        GetBasinArea(basin, histogram, basin.Row, basin.Column, downSlopeCoords);
        downSlopeCoords.Clear();
    }
    var orderedBasins = basinCoords.OrderByDescending(b => b.Walls.Count()).ToList();

    long basin1 = orderedBasins[0].Walls.Count() + 1;
    long basin2 = orderedBasins[1].Walls.Count() + 1;
    long basin3 = orderedBasins[2].Walls.Count() + 1;
    var total = basin1 * basin2 * basin3;
    Console.WriteLine($"Total basins is {total}");
}

static List<int> ParseLine(string line)
{
    return line.Select(l => int.Parse(l.ToString())).ToList();
}

static bool IsBasin(int row, int col, int numRows, int numCols, List<List<int>> histogram)
{
    var val = histogram[row][col];
    if (row > 0
        && val >= histogram[row - 1][col])
    {
        return false;
    }
    if (row < numRows - 1
        && val >= histogram[row + 1][col])
    {
        return false;
    }
    if (col > 0
        && val >= histogram[row][col - 1])
    {
        return false;
    }
    if (col < numCols - 1
        && val >= histogram[row][col + 1])
    {
        return false;
    }

    return true;
}

static void GetBasinArea(Basin basin, List<List<int>> histogram, int row, int col, HashSet<string> downSlopeCoords)
{
    if (histogram[row][col] == 9)
    {
        basin.Walls.RemoveAt(basin.Walls.Count - 1);
        return;
    }
    if (row > 0 
        && !downSlopeCoords.Contains($"{row - 1}|{col}") 
        &&  histogram[row][col] < histogram[row - 1][col])
    {
        basin.Walls.Add(histogram[row - 1][col]);
        downSlopeCoords.Add($"{row - 1}|{col}");
        GetBasinArea(basin, histogram, row - 1, col, downSlopeCoords);
    }
    if (row < histogram.Count- 1
        && !downSlopeCoords.Contains($"{row + 1}|{col}") 
        &&  histogram[row][col] < histogram[row + 1][col])
    {
        basin.Walls.Add(histogram[row + 1][col]);
        downSlopeCoords.Add($"{row + 1}|{col}");
        GetBasinArea(basin, histogram, row + 1, col, downSlopeCoords);
    }

    if (col > 0
        && !downSlopeCoords.Contains($"{row}|{col - 1}") 
        && histogram[row][col] < histogram[row][col - 1])
    {
        basin.Walls.Add(histogram[row][col - 1]);
        downSlopeCoords.Add($"{row}|{col - 1}");
        GetBasinArea(basin, histogram, row, col - 1, downSlopeCoords);
    }
    if (col < histogram[row].Count - 1
        && !downSlopeCoords.Contains($"{row}|{col + 1}") 
          && histogram[row][col] < histogram[row][col + 1])
    {
        basin.Walls.Add(histogram[row][col + 1]);
        downSlopeCoords.Add($"{row}|{col + 1}");
        GetBasinArea(basin, histogram, row, col + 1, downSlopeCoords);
    }

    return;
}

static void PrintWalls(Basin basin)
{
    Console.WriteLine($"Basin [{basin.Row},{basin.Column}] has walls {String.Join(", ", basin.Walls)}");
}