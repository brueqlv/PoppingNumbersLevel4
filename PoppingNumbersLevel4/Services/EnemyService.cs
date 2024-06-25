using PoppingNumbersLevel4.Models;
using PoppingNumbersLevel4.Services.Interfaces;

namespace PoppingNumbersLevel4.Services
{
    public class EnemyService(GameBoard gameBoard, IGameService gameService) : IEnemyService
    {
        private readonly Random _gameRandom = new();

        public void EnemyTurn(int minGameNumber, int maxGameNumber)
        {
            var validPositions = new List<(int row, int col)>();

            for (var i = 0; i < gameBoard.Height; i++)
            {
                for (var j = 0; j < gameBoard.Width; j++)
                {
                    if (gameBoard.Board[i, j] == null)
                    {
                        validPositions.Add((i, j));
                    }
                }
            }

            foreach (var pos in validPositions)
            {
                if (WouldCreateThreeInARow(pos.row, pos.col, minGameNumber, maxGameNumber))
                {
                    var chosenNumber = _gameRandom.Next(minGameNumber, maxGameNumber + 1).ToString();
                    gameBoard.Board[pos.row, pos.col] = chosenNumber;
                    return;
                }
            }

            if (validPositions.Any())
            {
                var randomPos = validPositions[_gameRandom.Next(validPositions.Count)];
                var randomNumber = _gameRandom.Next(minGameNumber, maxGameNumber + 1).ToString();
                gameBoard.Board[randomPos.row, randomPos.col] = randomNumber;
            }
        }

        private bool WouldCreateThreeInARow(int row, int col, int minGameNumber, int maxGameNumber)
        {
            for (var number = minGameNumber; number <= maxGameNumber; number++)
            {
                gameBoard.Board[row, col] = number.ToString();

                if (CreatesThreeInARow(row, col, number.ToString()))
                {
                    gameBoard.Board[row, col] = null;
                    return true;
                }
            }

            gameBoard.Board[row, col] = null;
            return false;
        }

        private bool CreatesThreeInARow(int row, int col, string number)
        {
            return CheckSequence(row, col, 0, 1, number) || // Horizontal
                   CheckSequence(row, col, 1, 0, number) || // Vertical
                   CheckSequence(row, col, 1, 1, number) || // Diagonal
                   CheckSequence(row, col, 1, -1, number);  // Reverse Diagonal
        }

        private bool CheckSequence(int startRow, int startCol, int rowIncrement, int colIncrement, string number)
        {
            var count = 0;

            for (var i = -2; i <= 2; i++)
            {
                var row = startRow + i * rowIncrement;
                var col = startCol + i * colIncrement;

                if (gameService.IsValidPosition(row, col) && gameBoard.Board[row, col] == number)
                {
                    count++;
                    if (count >= 3)
                    {
                        return true;
                    }
                }
                else
                {
                    count = 0;
                }
            }

            return false;
        }
    }
}
