using DayTen;
// See https://aka.ms/new-console-template for more information

ProblemOne();
Console.WriteLine("-----------------------------");
ProblemTwo();
Console.WriteLine("-----------------------------");
Console.WriteLine("Completed Day 10");

static void ProblemOne()
{
    Console.WriteLine("Day 10 Problem 1");
    var data = File.ReadAllLines("navigation.txt");
    var badTokens = new List<Token>();

    foreach(var line in data)
    {
        var parsedLine = ParseLine(line.Trim());
        var badToken = ValidateTokens(parsedLine);
        if (badToken != null)
        {
            badTokens.Add(badToken);
        }
    }
    var total = badTokens.Sum(t => t.ErrorPoints);
    Console.WriteLine($"Bad token points {total}");
}

static void ProblemTwo()
{
    Console.WriteLine("Day 10 Problem 2");
    var data = File.ReadAllLines("navigation.txt");
    var scores = new List<long>();
    foreach (var line in data)
    {
        long score = 0;

        var parsedLine = ParseLine(line.Trim());
        var badToken = ValidateTokens(parsedLine);
        if (badToken == null)
        {
            var remainingTokens = GetRemainingTokens(parsedLine);
            remainingTokens.Reverse();
            foreach(var token in remainingTokens)
            {
                score = (5 * score) + Token.GetById(token.Id + 1).ClosingPoints;
            }
            scores.Add(score);
            
        }
    }
    scores.Sort();
    var middle = scores[scores.Count / 2];
    Console.WriteLine($"Final score {middle}");
}

static List<Token> ParseLine(string line)
{
    var tokens = new List<Token>();
    foreach(var token in line)
    {
        tokens.Add(Token.GetByValue(token));
    }
    return tokens;
}

static Token ValidateTokens(List<Token> tokens)
{
    var remainingTokens = new List<Token>();
    foreach (var token in tokens)
    {
        if (token.Id % 2 == 0)
        {
            remainingTokens.Add(token);
        }
        else if (remainingTokens[remainingTokens.Count - 1].Id == token.Id - 1)
        {
            remainingTokens.RemoveAt(remainingTokens.Count - 1);
        }
        else { return token; }
    }
    return null;
}

static List<Token> GetRemainingTokens(List<Token> tokens)
{
    var remainingTokens = new List<Token>();
    foreach (var token in tokens)
    {
        if (token.Id % 2 == 0)
        {
            remainingTokens.Add(token);
        }
        else if (remainingTokens[remainingTokens.Count - 1].Id == token.Id - 1)
        {
            remainingTokens.RemoveAt(remainingTokens.Count - 1);
        }
    }

    return remainingTokens;
}