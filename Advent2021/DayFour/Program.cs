// See https://aka.ms/new-console-template for more information
using DayFour;

ProblemOne();
Console.WriteLine("-----------------------------");
ProblemTwo();
Console.WriteLine("-----------------------------");
Console.WriteLine("Completed Day 4");

static void ProblemOne()
{
    Console.WriteLine("Day 4 Problem 1");

    var data = File.ReadAllLines("game.txt");

    var playedNumbers = data[0].Split(',').Select(d => int.Parse(d)).ToList();
    var gameCard = new GameCard();
    var gameCards = new List<GameCard>(); ;
    for (var idx = 2; idx < data.Length; idx++)
    {
        if (String.IsNullOrWhiteSpace(data[idx]))
        {
            if (gameCard.IsValidCard())
            {
                gameCards.Add(gameCard);
            }
            gameCard = new GameCard();
            continue;
        }

        var row = data[idx].Split(' ').Where(d => d.Trim().Length > 0).Select(d => int.Parse(d)).ToList();
        gameCard.AddRow(row);
    }

    if (gameCard.IsValidCard())
    {
        gameCards.Add(gameCard);
    }

    var winningCards = new List<GameCard>();
    var winningNumber = -1;
    foreach (var num in playedNumbers)
    {
        foreach (var card in gameCards)
        {
            if (card.CheckForNumber(num))
            {
                winningCards.Add(card);
            }
        }
        if (winningCards.Count > 0)
        {
            winningNumber = num;
            break;
        }
    }
    foreach (var card in winningCards) {
        Console.WriteLine($"Winning card score {card.CalculateScore(winningNumber)}");
    }
}

static void ProblemTwo()
{
    Console.WriteLine("Day 4 Problem 2");

    var data = File.ReadAllLines("game.txt");

    var playedNumbers = data[0].Split(',').Select(d => int.Parse(d)).ToList();
    var gameCard = new GameCard();
    var gameCards = new List<GameCard>(); ;
    for (var idx = 2; idx < data.Length; idx++)
    {
        if (String.IsNullOrWhiteSpace(data[idx]))
        {
            if (gameCard.IsValidCard())
            {
                gameCards.Add(gameCard);
            }
            gameCard = new GameCard();
            continue;
        }

        var row = data[idx].Split(' ').Where(d => d.Trim().Length > 0).Select(d => int.Parse(d)).ToList();
        gameCard.AddRow(row);
    }

    if (gameCard.IsValidCard())
    {
        gameCards.Add(gameCard);
    }

    var winningCards = new List<GameCard>();
    var winningNumber = -1;
    foreach (var num in playedNumbers)
    {
        foreach (var card in gameCards)
        {
            if (card.CheckForNumber(num))
            {
                winningCards.Add(card);
            }
        }
        foreach(var winner in winningCards)
        {
            gameCards.Remove(winner);
        }
        
        if (gameCards.Count == 0)
        {
            winningNumber = num;
            break;
        }
        winningCards.Clear();
    }
    foreach (var card in winningCards)
    {
        Console.WriteLine($"Winning card score {card.CalculateScore(winningNumber)}");
    }
}