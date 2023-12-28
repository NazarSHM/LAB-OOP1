using System;
using System.Collections.Generic;

//  клас для гравців
class Player
{
    public string UserName { get; private set; }
    public int CurrentRating { get; private set; }
    public int GamesCount { get; private set; }
    private List<(string opponentName, string result, int ratingChange, int gameIndex)> gameHistory;

    public Player(string username, int currentRating = 1000)
    {
        UserName = username;
        CurrentRating = currentRating;
        GamesCount = 0;
        gameHistory = new List<(string opponentName, string result, int ratingChange, int gameIndex)>();
    }

    public void WinGame(Game game)
    {
        int ratingChange = game.CalculateRatingChange(true);
        CurrentRating += ratingChange;
        GamesCount++;
        gameHistory.Add((game.GetOpponent(UserName), "Win", ratingChange, GamesCount));
    }

    public void LoseGame(Game game)
    {
        int ratingChange = game.CalculateRatingChange(false);
        CurrentRating -= ratingChange;
        if (CurrentRating < 1)
        {
            CurrentRating = 1;
        }
        GamesCount++;
        gameHistory.Add((game.GetOpponent(UserName), "Lose", ratingChange, GamesCount));
    }

    public void GetStats()
    {
        Console.WriteLine($"Game history for {UserName}:");
        Console.WriteLine("Opponent  | Result | Rating Change | Game Index");
        foreach (var game in gameHistory)
        {
            Console.WriteLine($"{game.opponentName,-8} | {game.result,-6} | {game.ratingChange,13} | {game.gameIndex,10}");
        }
    }
}

//  клас для ігор
abstract class Game
{
    public abstract int CalculateRatingChange(bool isWin);
    public abstract string GetOpponent(string playerName);
}

// Клас для стандартної гри
class StandardGame : Game
{
    public override int CalculateRatingChange(bool isWin)
    {
        return isWin ? 50 : 20;
    }

    public override string GetOpponent(string playerName)
    {
        
        return "Opponent";
    }
}

// Клас для гри без рейтингу
class TrainingGame : Game
{
    public override int CalculateRatingChange(bool isWin)
    {
        return 0;
    }

    public override string GetOpponent(string playerName)
    {
        
        return "";
    }
}

class Program
{
    static void Main()
    {
        Player player1 = new Player("Alice");
        Player player2 = new Player("Bob");

        Game standardGame = new StandardGame();
        Game trainingGame = new TrainingGame();

        player1.WinGame(standardGame);
        player2.LoseGame(standardGame);

        player1.WinGame(trainingGame);
        player2.LoseGame(trainingGame);

        player1.GetStats();
        player2.GetStats();
    }
}