namespace Chess
{
    using Models.Square;
    using Models.Square.Contracts;
    using Models.Player;
    using Models.Player.Contracts;
    using Models.Board.Contracts;
    using Models.Figures.Contracts;
    using Models.Board;
    using Models.Figures;
    using Models.Enums;
    using Chess.Models;

    public class Factory
    {
        public static IBoard GetBoard()
        {
            IBoard board = new Board();
            return board;
        }

        public static IPlayer GetPlayer(string name, Color color)
        {
            IPlayer player = new Player(name, color);
            return player;
        }

        public static IPiece GetPawn(Color color)
        {
            IPiece pawn = new Pawn(color);
            return pawn;
        }

        public static IPiece GetRook(Color color)
        {
            IPiece rook = new Rook(color);
            return rook;
        }

        public static IPiece GetKnight(Color color)
        {
            IPiece knight = new Knight(color);
            return knight;
        }

        public static IPiece GetBishop(Color color)
        {
            IPiece bishop = new Bishop(color);
            return bishop;
        }

        public static IPiece GetQueen(Color color)
        {
            IPiece queen = new Queen(color);
            return queen;
        }

        public static IPiece GetKing(Color color)
        {
            IPiece king = new King(color);
            return king;
        }

        public static IPiece GetEmpty()
        {
            IPiece empty = new Empty();
            return empty;
        }

        public static ISquare[][] GetMatrix(ISquare[][] matrix)
        {
            matrix = new ISquare[Globals.BoardRows][];

            for (int row = 0; row < Globals.BoardRows; row++)
            {
                matrix[row] = new ISquare[Globals.BoardCols];
            }

            return matrix;
        }

        public static ISquare GetSquare(Row row, Col col, IPiece empty)
        {
            ISquare square = new Square(row, col, empty);
            return square;
        }

        public static Position GetPosition (X x, Y y)
        {
            return new Position(x, y);
        }
    }
}
