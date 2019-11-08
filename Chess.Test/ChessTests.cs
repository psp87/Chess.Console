namespace Tests
{
    using Chess;
    using Chess.Models.Board.Contracts;
    using Chess.Models.Figures.Contracts;
    using Chess.Models.Player.Contracts;
    using NUnit.Framework;

    public class ChessTests
    {
        IBoard board = Factory.GetBoard();
        IPlayer player1 = Factory.GetPlayer("PEPPY", Color.Light);
        IPlayer player2 = Factory.GetPlayer("PATTY", Color.Dark);


        [SetUp]
        public void Setup()
        {
            //board.NewGame();
        }

        [Test]
        [TestCase(Col.A, Row.Two, Col.A, Row.Three)]
        [TestCase(Col.A, Row.Two, Col.A, Row.Four)]
        public void LightPawnShouldMoveOneSquareOrTwoSquaresOnFirstMove(Col fromCol, Row fromRow, Col toCol, Row toRow)
        {
            // Arrange
            IFigure pawn = Factory.GetPawn(Color.Light, fromRow, fromCol);
            board.Matrix[(int)fromRow][(int)fromCol] = pawn;

            // Act
            bool result = board.Matrix[(int)fromRow][(int)fromCol].Move(board.Matrix, toRow, toCol);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        [TestCase(Col.A, Row.Seven, Col.A, Row.Six)]
        [TestCase(Col.A, Row.Seven, Col.A, Row.Five)]
        public void BlackPawnShouldMoveOneSquareOrTwoSquaresOnFirstMove(Col fromCol, Row fromRow, Col toCol, Row toRow)
        {
            // Arrange
            IFigure pawn = Factory.GetPawn(Color.Dark, fromRow, fromCol);
            board.Matrix[(int)fromRow][(int)fromCol] = pawn;

            // Act
            bool result = board.Matrix[(int)fromRow][(int)fromCol].Move(board.Matrix, toRow, toCol);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        [TestCase(Col.A, Row.Three, Col.A, Row.Four)]
        public void LightPawnShouldThrowsExceptionOnInvalidMove(Col fromCol, Row fromRow, Col toCol, Row toRow)
        {
            // Arrange
            IFigure pawn = Factory.GetPawn(Color.Light, fromRow, fromCol);
            board.Matrix[(int)fromRow][(int)fromCol] = pawn;

            // Act
            bool result = board.Matrix[(int)fromRow][(int)fromCol].Move(board.Matrix, toRow, toCol);

            // Assert
            Assert.That(() => result, Throws.Exception);
        }
    }
}