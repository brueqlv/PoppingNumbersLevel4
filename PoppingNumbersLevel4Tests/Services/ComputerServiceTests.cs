using Moq;
using PoppingNumbersLevel4.Models;
using PoppingNumbersLevel4.Services;
using PoppingNumbersLevel4.Services.Interfaces;

namespace PoppingNumbersLevel4Tests.Services
{
    public class ComputerServiceTests
    {
        private Mock<IGameService> _mockGameService;
        private GameBoard _gameBoard;
        private IComputerService _computerService;

        [SetUp]
        public void Setup()
        {
            _gameBoard = new GameBoard(5, 5);
            _mockGameService = new Mock<IGameService>();
            _computerService = new ComputerService(_mockGameService.Object, _gameBoard);
        }

        [Test]
        public void ComputerTurn_ShouldPlaceThreeNumbers_OnEmptyBoard()
        {
            // Act
            _computerService.ComputerTurn(1, 9, false);

            // Assert
            var count = 0;
            foreach (var cell in _gameBoard.Board)
            {
                if (cell != null) count++;
            }
            Assert.AreEqual(3, count);
        }

        [Test]
        public void ComputerTurn_ShouldPlaceStars_WhenUseAppearancePredictorIsTrue()
        {
            // Arrange
            _mockGameService.Setup(gs => gs.IsGameOver()).Returns(false);

            // Act
            _computerService.ComputerTurn(1, 9, true);

            // Assert
            var count = 0;
            foreach (var cell in _gameBoard.Board)
            {
                if (cell == "*") count++;
            }
            Assert.AreEqual(3, count);
        }

        [Test]
        public void ReplaceStarsWithRandomNumbers_ShouldReplaceAllStars()
        {
            // Arrange
            _gameBoard.Board[0, 0] = "*";
            _gameBoard.Board[1, 1] = "*";
            _gameBoard.Board[2, 2] = "*";

            // Act
            _computerService.ReplaceStarsWithRandomNumbers(1, 9);

            // Assert
            Assert.AreNotEqual("*", _gameBoard.Board[0, 0]);
            Assert.AreNotEqual("*", _gameBoard.Board[1, 1]);
            Assert.AreNotEqual("*", _gameBoard.Board[2, 2]);
        }

        [Test]
        public void ReplaceStarsWithRandomNumbers_ShouldNotAffectOtherCells()
        {
            // Arrange
            _gameBoard.Board[0, 0] = "5";
            _gameBoard.Board[1, 1] = "*";
            _gameBoard.Board[2, 2] = "8";

            // Act
            _computerService.ReplaceStarsWithRandomNumbers(1, 9);

            // Assert
            Assert.AreEqual("5", _gameBoard.Board[0, 0]);
            Assert.AreNotEqual("*", _gameBoard.Board[1, 1]);
            Assert.AreEqual("8", _gameBoard.Board[2, 2]);
        }
    }
}
