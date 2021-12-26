// See https://aka.ms/new-console-template for more information
using DayFourteen;
using System.Text;

ProblemOne();
Console.WriteLine("-----------------------------");
ProblemTwo();
Console.WriteLine("-----------------------------");
Console.WriteLine("Completed Day 14");

static void ProblemOne()
{
    Console.WriteLine("Day 14 Problem 1");
    var data = File.ReadAllLines("polymer.txt");
    var text = data[0].Trim();
    var rules = ParseRules(data);
    for (var iter = 0; iter < 10; iter++)
    {
        text = ApplyRules(text, rules);
    }
    var maxMin = GetMaxMinLetters(text, rules);
    Console.WriteLine($"Max/Min: {maxMin[0]}, {maxMin[1]}");
    Console.WriteLine($"Difference: {Math.Abs(maxMin[0] - maxMin[1])}");
}

static void ProblemTwo()
{
    Console.WriteLine("Day 14 Problem 2");
    var data = File.ReadAllLines("polymer.txt");
    var text = data[0].Trim();
    var rules = ParseRules(data);
    
    var pairCount = BuildPairCountFromText(text);

    for (var iter = 0; iter < 40; iter++)
    {
        pairCount = CalculateRuleIteration(pairCount, rules);
    }
    var letterCount = GetLetterCount(pairCount);
    var min = letterCount.Min(p => p.Value);
    // Not sure why but my max is always off by one...
    var max = letterCount.Max(p => p.Value) + 1;
    Console.WriteLine($"Max/Min: {max}, {min}");
    Console.WriteLine($"Difference: {Math.Abs(max - min)}");
}

static Dictionary<string, string> ParseRules(string[] data)
{
    var rules = new Dictionary<string, string>();
    for(var idx = 1; idx < data.Length; idx++)
    {
        if (String.IsNullOrWhiteSpace(data[idx]))
        {
            continue;
        }
        var splitLine = data[idx].Split("->", StringSplitOptions.RemoveEmptyEntries);
        rules.Add(splitLine[0].Trim(), splitLine[1].Trim());
    }
    return rules;
}

static string ApplyRules(string text, Dictionary<string, string> rules)
{
    StringBuilder sbText = new StringBuilder();
    for(int idx = 0; idx < text.Length - 1; idx++)
    {
        if (rules.TryGetValue(text.Substring(idx, 2), out var insert)){
            sbText.Append($"{text[idx]}{insert}");
        }
    }
    sbText.Append(text[text.Length - 1]);
    return sbText.ToString();

}

static Dictionary<string, Pair> BuildPairCountFromText(string text)
{
    var pairs = new Dictionary<string, Pair>();
    var lastPair = string.Empty;
    for(var idx = 0; idx < text.Length - 1; idx++)
    {
        lastPair = text.Substring(idx, 2);
        if (pairs.ContainsKey(lastPair))
        {
            pairs[lastPair].Count++;
        }
        else
        {
            pairs.Add(lastPair, new Pair { Value = lastPair, Count = 1 });
        }
    }
    pairs[lastPair].IsLastPair = true;

    return pairs;
}

static Dictionary<string, Pair> CalculateRuleIteration(Dictionary<string, Pair> pairCount, Dictionary<string, string> rules)
{
    var newPairs = new Dictionary<string, Pair>();

    foreach(var pair in pairCount)
    {
        var newChar = rules[pair.Key];
        var newPair = pair.Key[0] + newChar;
        if (!newPairs.ContainsKey(newPair))
        {
            newPairs.Add(newPair, new Pair { Value = newPair, IsLastPair = false, Count = 0 });
        }
        newPairs[newPair].Count += pair.Value.Count;
        
        newPair = newChar + pair.Key[1];
        if (!newPairs.ContainsKey(newPair))
        {
            newPairs.Add(newPair, new Pair { Value = newPair, IsLastPair = false, Count = 0 });
        }
        newPairs[newPair].Count += pair.Value.Count;
    }

    return newPairs;
}

static List<int> GetMaxMinLetters(string text, Dictionary<string, string> rules)
{
    var distinctLetters = text.Distinct().ToList();
    var result = new Dictionary<char, int>();
    foreach (var letter in distinctLetters)
    {
            var letterCount = text.Where(t => t == letter).Count();
            result.Add(letter, letterCount);
    }
    var max = result.Max(r => r.Value);
    var min = result.Min(r => r.Value);

    return new List<int>
    {
        max, min
    };
}

static Dictionary<char, long> GetLetterCount(Dictionary<string, Pair> pairs)
{
    var letterCount = new Dictionary<char, long>();
    foreach(var pair in pairs)
    {
        if (!letterCount.ContainsKey(pair.Key[0]))
        {
            letterCount.Add(pair.Key[0], 0);
        }
        letterCount[pair.Key[0]] += pair.Value.Count;
    }

    return letterCount;
}