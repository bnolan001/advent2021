// See https://aka.ms/new-console-template for more information
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

            var val = histogram[row][col];
            if (row > 0
                && val >= histogram[row - 1][col])
            {
                continue;
            }
            if (row < numRows - 1
                && val >= histogram[row + 1][col])
            {
                continue;
            }
            if (col > 0
                && val >= histogram[row][col - 1])
            {
                continue;
            }
            if (col < numCols - 1
                && val >= histogram[row][col + 1])
            {
                continue;
            }
            minValues.Add(val);
        }
    }

    var sum = minValues.Sum() + minValues.Count();
    Console.WriteLine($"Total min values is {sum}");
}

static void ProblemTwo()
{
    Console.WriteLine("Day 9 Problem 2");
    var data = File.ReadAllLines("histogram.txt");


    Console.WriteLine($"");
}

static List<int> ParseLine(string line)
{
    return line.Select(l => int.Parse(l.ToString())).ToList();
}