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

        public static IFigure GetPawn(Color color)
        {
            IFigure pawn = new Pawn(color);
            return pawn;
        }

        public static IFigure GetRook(Color color)
        {
            IFigure rook = new Rook(color);
            return rook;
        }

        public static IFigure GetKnight(Color color)
        {
            IFigure knight = new Knight(color);
            return knight;
        }

        public static IFigure GetBishop(Color color)
        {
            IFigure bishop = new Bishop(color);
            return bishop;
        }

        public static IFigure GetQueen(Color color)
        {
            IFigure queen = new Queen(color);
            return queen;
        }

        public static IFigure GetKing(Color color)
        {
            IFigure king = new King(color);
            return king;
        }

        public static IFigure GetEmpty()
        {
            IFigure empty = new Empty();
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

        public static ISquare GetSquare(Row row, Col col, IFigure empty)
        {
            ISquare square = new Square(row, col, empty);
            return square;
        }
    }
}
