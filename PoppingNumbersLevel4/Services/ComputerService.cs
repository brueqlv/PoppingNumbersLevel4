using PoppingNumbersLevel4.Models;
using PoppingNumbersLevel4.Services.Interfaces;

namespace PoppingNumbersLevel4.Services
{
    public class ComputerService(IGameService gameService, GameBoard gameBoard) : IComputerService
    {
        private readonly Random _gameRandom = new();

        public void ComputerTurn(int minGameNumber, int maxGameNumber, bool useAppearancePredictor)
        {
            var numbersPlaced = 0;

            while (numbersPlaced < 3 || gameService.IsGameOver())
            {
                var row = _gameRandom.Next(gameBoard.Height);
                var col = _gameRandom.Next(gameBoard.Width);

                if (gameBoard.Board[row, col] != null) continue;

                var placeTaker = "*";

                if (!useAppearancePredictor)
                {
                    placeTaker = _gameRandom.Next(minGameNumber, maxGameNumber + 1).ToString();
                }

                gameBoard.Board[row, col] = placeTaker;
                numbersPlaced++;
            }
        }
        public void ReplaceStarsWithRandomNumbers(int minGameNumber, int maxGameNumber)
        {
            for (var i = 0; i < gameBoard.Height; i++)
            {
                for (var j = 0; j < gameBoard.Width; j++)
                {
                    if (gameBoard.Board[i, j] == "*")
                    {
                        gameBoard.Board[i, j] = _gameRandom.Next(minGameNumber, maxGameNumber + 1).ToString();
                    }
                }
            }
        }
    }
}
