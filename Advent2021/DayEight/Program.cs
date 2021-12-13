// See https://aka.ms/new-console-template for more information
ProblemOne();
Console.WriteLine("-----------------------------");
ProblemTwo();
Console.WriteLine("-----------------------------");
Console.WriteLine("Completed Day 8");

static void ProblemOne()
{
    Console.WriteLine("Day 8 Problem 1");

    var data = File.ReadAllLines("digits.txt");
    var knownDigits = 0;
    foreach(var line in data)
    {
        var inputOutput = line.Split('|');
        var numbers = inputOutput[1].Split(' ');
        var knownNumbersOnLine = numbers.Count(n => n.Length == 2 || n.Length == 4 || n.Length == 3 || n.Length == 7);
        knownDigits += knownNumbersOnLine;
    }
    
    Console.WriteLine($"Total number of known digits is {knownDigits}");
}

static void ProblemTwo()
{
    Console.WriteLine("Day 8 Problem 2");

    var data = File.ReadAllLines("digits.txt");
    var numberCodes = new Dictionary<string, int?>();
    int totalOutput = 0;
    foreach (var line in data)
    {
        var lineNumber = 0;
        var inputOutput = line.Split('|', StringSplitOptions.RemoveEmptyEntries);
        var input = inputOutput[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
        foreach(var code in input)
        {
            // Sort the codes so it is easier to look them up
            var sorted = code.ToCharArray();
            Array.Sort(sorted);
            var key = String.Join("", sorted);
            numberCodes[key] = GetNumericValue(key);
        }

        var iteration = 0;
        while (numberCodes.Values.Any(n => n == null))
        {
            foreach (var code in numberCodes.Keys)
            {
                var item = numberCodes[code];
                if (item == null)
                {
                    var value = GetNumericValueFromKnown(code, numberCodes, iteration);
                    if (value.HasValue)
                        numberCodes[code] = value.Value;
                }
            }
            iteration++;
        }
        var output = inputOutput[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
        foreach(var code in output)
        {
            var codeChars = code.ToCharArray();
            Array.Sort(codeChars);
            var digit = numberCodes[String.Join("", codeChars)];
            if (lineNumber > 0) lineNumber *= 10;
            lineNumber += digit.Value;
        }
        totalOutput += lineNumber;
        numberCodes.Clear();
    }

    Console.WriteLine($"Total {totalOutput}");
}

/// <summary>
/// Set the values that we know he value based on the number of characters in the code
/// </summary>
static int? GetNumericValue(string code)
{
    int? value = null;
    switch (code.Length)
    {
        case 2:
            value = 1;
            break;
        case 3:
            value = 7;
            break;
        case 4:
            value = 4;
            break;
        case 7:
            value = 8;
            break;
    }

    return value;
}

static int? GetNumericValueFromKnown(string code, Dictionary<string, int?> knownValues, int iteration)
{
    // Each iteration will open up a new set of digits that we can decode
    if (iteration == 0)
    {
        if (code.Length == 5)
        {
            var seven = knownValues.FirstOrDefault(k => k.Value == 7);
            if (seven.Value != null)
            {
                if (code.Contains(seven.Key)
                    || seven.Key.Where(c => code.Contains(c)).ToList().Count == seven.Key.Length)
                {
                    return 3;
                }
            }
        }
        else if (code.Count() == 6)
        {
            // Merging four and seven should help us find the 9
            var merged = String.Join("", knownValues.Where(k => k.Value == 7 || k.Value == 4).Select(s => s.Key)).Distinct();
            var preSort = String.Join("", merged).ToCharArray();
            Array.Sort(preSort);
            var fourSeven = String.Join("", preSort);
            if (fourSeven.Where(c => code.Contains(c)).ToList().Count == fourSeven.Length)
            {
                return 9;
            }
        }
    }
    else if (iteration == 2)
    {
        if (code.Count() == 6)
        {
            // Merging four and seven should help us find the 9
            var one = knownValues.Where(k => k.Value == 1).Select(s => s.Key).First();
            if (one.Where(c => code.Contains(c)).ToList().Count == one.Length)
            {
                return 0;
            }
        }
    }
    else if (iteration == 3)
    {
        if (code.Count() == 6)
            return 6;
    }
    else if (iteration == 4)
    {
        if (code.Count() == 5)
        {
            var six = knownValues.Where(k => k.Value == 6).Select(s => s.Key).First();
            if (code.Where(c => six.Contains(c)).ToList().Count == code.Length)
            {
                return 5;
            }
        }
    }
    else if (iteration == 5)
    {
        if (code.Count() == 5)
        {
            return 2;
        }
    }

    return null;
}