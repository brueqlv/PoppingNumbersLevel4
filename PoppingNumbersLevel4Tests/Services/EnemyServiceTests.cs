using PoppingNumbersLevel4.Models;
using PoppingNumbersLevel4.Services;

namespace PoppingNumbersLevel4Tests.Services
{
    public class EnemyServiceTests
    {
        private GameBoard _gameBoard;
        private GameService _gameService;
        private EnemyService _enemyService;

        [SetUp]
        public void Setup()
        {
            _gameBoard = new GameBoard(5, 5);
            _gameService = new GameService(_gameBoard);
            _enemyService = new EnemyService(_gameBoard, _gameService);
        }

        [Test]
        public void EnemyTurn_ShouldPlaceNumberOnEmptyBoard()
        {
            // Act
            _enemyService.EnemyTurn(1, 9);

            // Assert
            Assert.That(_gameBoard.Board.Cast<string>().Count(cell => cell != null), Is.EqualTo(1));
        }

        [Test]
        public void EnemyTurn_ShouldPreferPlacementCreatingThreeInARow()
        {
            // Arrange
            _gameBoard.Board[0, 0] = "1";
            _gameBoard.Board[0, 1] = "1";

            // Act
            _enemyService.EnemyTurn(1, 1);

            // Assert
            Assert.That(_gameBoard.Board[0, 2], Is.EqualTo("1"));
        }

        [Test]
        public void EnemyTurn_ShouldNotThrowExceptionOnFullBoard()
        {
            // Arrange
            for (var i = 0; i < _gameBoard.Height; i++)
            {
                for (var j = 0; j < _gameBoard.Width; j++)
                {
                    _gameBoard.Board[i, j] = "1";
                }
            }

            // Act & Assert
            Assert.DoesNotThrow(() => _enemyService.EnemyTurn(1, 9));
        }

        [Test]
        public void WouldCreateThreeInARow_ShouldReturnTrueWhenCreatingThreeInARow()
        {
            // Arrange
            _gameBoard.Board[0, 0] = "1";
            _gameBoard.Board[0, 1] = "1";

            // Act
            var result = _enemyService.GetType()
                                      .GetMethod("WouldCreateThreeInARow", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                                      .Invoke(_enemyService, new object[] { 0, 2, 1, 1 });

            // Assert
            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public void WouldCreateThreeInARow_ShouldReturnFalseWhenNotCreatingThreeInARow()
        {
            // Act
            var result = _enemyService.GetType()
                                      .GetMethod("WouldCreateThreeInARow", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                                      .Invoke(_enemyService, new object[] { 0, 0, 1, 1 });

            // Assert
            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void CreatesThreeInARow_ShouldReturnTrueForHorizontalSequence()
        {
            // Arrange
            _gameBoard.Board[0, 0] = "1";
            _gameBoard.Board[0, 1] = "1";
            _gameBoard.Board[0, 2] = "1";

            // Act
            var result = _enemyService.GetType()
                                      .GetMethod("CreatesThreeInARow", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                                      .Invoke(_enemyService, new object[] { 0, 1, "1" });

            // Assert
            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public void CreatesThreeInARow_ShouldReturnTrueForVerticalSequence()
        {
            // Arrange
            _gameBoard.Board[0, 0] = "1";
            _gameBoard.Board[1, 0] = "1";
            _gameBoard.Board[2, 0] = "1";

            // Act
            var result = _enemyService.GetType()
                                      .GetMethod("CreatesThreeInARow", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                                      .Invoke(_enemyService, new object[] { 1, 0, "1" });

            // Assert
            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public void CreatesThreeInARow_ShouldReturnTrueForDiagonalSequence()
        {
            // Arrange
            _gameBoard.Board[0, 0] = "1";
            _gameBoard.Board[1, 1] = "1";
            _gameBoard.Board[2, 2] = "1";

            // Act
            var result = _enemyService.GetType()
                                      .GetMethod("CreatesThreeInARow", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                                      .Invoke(_enemyService, new object[] { 1, 1, "1" });

            // Assert
            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public void CreatesThreeInARow_ShouldReturnTrueForReverseDiagonalSequence()
        {
            // Arrange
            _gameBoard.Board[0, 2] = "1";
            _gameBoard.Board[1, 1] = "1";
            _gameBoard.Board[2, 0] = "1";

            // Act
            var result = _enemyService.GetType()
                                      .GetMethod("CreatesThreeInARow", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                                      .Invoke(_enemyService, new object[] { 1, 1, "1" });

            // Assert
            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public void CheckSequence_ShouldReturnTrueForMatchingSequence()
        {
            // Arrange
            _gameBoard.Board[0, 0] = "1";
            _gameBoard.Board[0, 1] = "1";
            _gameBoard.Board[0, 2] = "1";

            // Act
            var result = _enemyService.GetType()
                                      .GetMethod("CheckSequence", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                                      .Invoke(_enemyService, new object[] { 0, 1, 0, 1, "1" });

            // Assert
            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public void CheckSequence_ShouldReturnFalseForNonMatchingSequence()
        {
            // Arrange
            _gameBoard.Board[0, 0] = "1";
            _gameBoard.Board[0, 1] = "1";
            _gameBoard.Board[0, 2] = "2";

            // Act
            var result = _enemyService.GetType()
                                      .GetMethod("CheckSequence", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                                      .Invoke(_enemyService, new object[] { 0, 1, 0, 1, "1" });

            // Assert
            Assert.That(result, Is.EqualTo(false));
        }
    }
}
