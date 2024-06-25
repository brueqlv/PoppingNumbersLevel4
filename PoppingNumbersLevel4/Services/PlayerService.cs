using PoppingNumbersLevel4.Helpers;
using PoppingNumbersLevel4.Models;
using PoppingNumbersLevel4.Services.Interfaces;

namespace PoppingNumbersLevel4.Services
{
    public class PlayerService(GameBoard gameBoard) : IPlayerService
    {
        public void PlayerTurn(int minGameNumber, int maxGameNumber)
        {
            while (true)
            {
                var number = UserInputHelper.GetValidUserInputNumber("Enter a number", maxGameNumber, minGameNumber).ToString();
                var row = UserInputHelper.GetValidUserInputNumber("Enter row", gameBoard.Height, 1);
                var col = UserInputHelper.GetValidUserInputNumber("Enter col", gameBoard.Width, 1);

                if (gameBoard.Board[row - 1, col - 1] == null || gameBoard.Board[row - 1, col - 1] == "*")
                {
                    gameBoard.Board[row - 1, col - 1] = number;
                    break;
                }

                Console.WriteLine("Field is already occupied, try again.");
            }
        }
    }
}
