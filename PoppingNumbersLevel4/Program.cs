using PoppingNumbersLevel4.Helpers;
using PoppingNumbersLevel4.Models;
using PoppingNumbersLevel4.Services;

namespace PoppingNumbersLevel4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int minDeskSize = 5;
            const int maxDeskSize = 25;
            const int minGameNumber = 0;
            const int maxGameNumber = 9;
            var score = 0;
            var enemyScore = 0;

            var userName = UserInputHelper.GetValidUserName();

            var deskWidth = UserInputHelper.GetValidUserInputNumber("Enter board desk width", maxDeskSize, minDeskSize);
            var deskHeight = UserInputHelper.GetValidUserInputNumber("Enter board desk height", maxDeskSize, minDeskSize);

            var gameBoard = new GameBoard(deskWidth, deskHeight);
            var gameService = new GameService(gameBoard);
            var playerService = new PlayerService(gameBoard);

            var gameNumbersFrom = UserInputHelper.GetValidUserInputNumber("Enter min game number", maxGameNumber, minGameNumber);
            var gameNumbersTo = UserInputHelper.GetValidUserInputNumber("Enter max game number", maxGameNumber, minGameNumber);

            var playAgainstEnemy = UserInputHelper.GetValidUserInputBoolean("Do you want to play against an enemy?");

            if (!playAgainstEnemy)
            {
                var computerService = new ComputerService(gameService, gameBoard);
                var appearancePredictor = UserInputHelper.GetValidUserInputBoolean("Do you want to use number appearance predictor?");

                while (true)
                {
                    Console.Clear();
                    Console.WriteLine($"{userName} score - {score}");
                    
                    if (appearancePredictor)
                    {
                        computerService.ComputerTurn(gameNumbersFrom, gameNumbersTo, appearancePredictor);
                    }

                    gameService.PrintBoard();

                    playerService.PlayerTurn(gameNumbersFrom, gameNumbersTo);

                    if (gameService.IsGameOver())
                    {
                        break;
                    }

                    if (appearancePredictor)
                    {
                        computerService.ReplaceStarsWithRandomNumbers(gameNumbersFrom, gameNumbersTo);
                    }
                    else
                    {
                        computerService.ComputerTurn(gameNumbersFrom, gameNumbersTo, appearancePredictor);
                    }

                    score += gameService.ClearConnectedNumbers();

                    if (gameService.IsGameOver())
                    {
                        break;
                    }
                }

                Console.WriteLine("Game Over! No more spaces left.");
                Console.ReadLine();
            }
            else
            {
                var enemyService = new EnemyService(gameBoard, gameService);
                const string enemyName = "Darth Vader";
                var winner = userName;

                while (true)
                {
                    Console.Clear();
                    Console.WriteLine($"{userName} score - {score}");
                    Console.WriteLine($"{enemyName}'s score = {enemyScore}");

                    gameService.PrintBoard();

                    playerService.PlayerTurn(gameNumbersFrom, gameNumbersTo);

                    score += gameService.ClearConnectedNumbers();

                    if (gameService.IsGameOver())
                    {
                        break;
                    }

                    enemyService.EnemyTurn(gameNumbersFrom, gameNumbersTo);

                    enemyScore += gameService.ClearConnectedNumbers();

                    if (gameService.IsGameOver())
                    {
                        break;
                    }
                }

                if (enemyScore > score)
                {
                    winner = enemyName;
                }

                Console.WriteLine("Game Over! No more spaces left.");
                Console.WriteLine($"Winner - {winner}");
                Console.ReadLine();
            }
        }
    }
}
