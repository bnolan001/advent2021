// See https://aka.ms/new-console-template for more information
ProblemOne();
Console.WriteLine("-----------------------------");
ProblemTwo();
Console.WriteLine("-----------------------------");
Console.WriteLine("Completed Day 3");

static void ProblemOne()
{
    Console.WriteLine("Day 3 Problem 1");

    var data = File.ReadAllLines("diagnostic.txt");
    var arrayLength = data[0].Trim().Length;
    var mostCommon = new int[arrayLength];

    foreach (var line in data)
    {
        for(int idx = 0; idx < arrayLength; idx++)
        {
            switch (line[idx])
            {
                case '0':
                    mostCommon[idx]--;
                    break;
                case '1':
                    mostCommon[idx]++;
                    break;

                default:
                    break;
            }
        }
    }
    double gamma = 0;
    double epsilon = 0;
    int pow = arrayLength - 1;
    for(var idx = 0; idx <= pow; idx++)
    {
        if (mostCommon[idx] > 0)
        {
            gamma += Math.Pow(2, (pow - idx));
        }
        else
        {
            epsilon += Math.Pow(2, (pow - idx));
        }
    }

    var powerConsumption = gamma * epsilon;

    Console.WriteLine($"Gamma rate {gamma}, Epsilon {epsilon}, Power Consumption {powerConsumption}");
}

static void ProblemTwo()
{
    Console.WriteLine("Day 3 Problem 2");

    var data = File.ReadAllLines("diagnostic.txt");
    var oxygenData = data;
    var arrayLength = data[0].Trim().Length;
    var c02Data = data;
    var mostCommon = new int[arrayLength];

    for (int idx = 0; idx < arrayLength; idx++)
    {
        var mostCommonValue = GetMostCommon(idx, oxygenData);
        if (oxygenData.Length > 1) {
            if (oxygenData.Count(s => s[idx] == mostCommonValue) > 0)
            {
                oxygenData = oxygenData.Where(s => s[idx] == mostCommonValue).ToArray();
            }
        }

        if (c02Data.Length > 1)
        {
            var leastCommonValue = GetMostCommon(idx, c02Data) == '1' ? '0' : '1';
            if (c02Data.Count(s => s[idx] == leastCommonValue) > 0)
            {
                c02Data = c02Data.Where(s => s[idx] == leastCommonValue).ToArray();
            }
        }
    }
    var oxygenValue = ConvertBinaryToDouble(oxygenData[0]);
    var c02Value = ConvertBinaryToDouble(c02Data[0]);
    var lifeSupportRating = oxygenValue * c02Value;
    Console.WriteLine($"Oxygen {oxygenValue}, C02 {c02Value}, Life Support Rating {lifeSupportRating}");
}

static char GetMostCommon(int idx, string[] data)
{
    int total = 0;

    foreach(var line in data)
    {
        if (line[idx] == '1')
        {
            total++;
        }
        else { total--; }
    }

    return total >= 0 ? '1' : '0';
}

static double ConvertBinaryToDouble(string binary)
{
    double value = 0;
    int pow = binary.Length - 1;
    for(int idx =0; idx <= pow; idx++)
    {
        if (binary[idx] == '1')
        {
            value += Math.Pow(2, pow - idx);
        }
    }

    return value;
}