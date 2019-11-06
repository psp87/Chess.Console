namespace Chess
{
    using Chess.Models.Board.Contracts;
    using Chess.Models.Figures.Contracts;
    using Chess.Models.Player;
    using Chess.Models.Player.Contracts;

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

        public static IFigure GetPawn(Color color, CoordinateY row, CoordinateX col)
        {
            IFigure pawn = new Pawn(color, row, col);
            return pawn;
        }

        public static IFigure GetRook(Color color, CoordinateY row, CoordinateX col)
        {
            IFigure rook = new Rook(color, row, col);
            return rook;
        }

        public static IFigure GetKnight(Color color, CoordinateY row, CoordinateX col)
        {
            IFigure knight = new Knight(color, row, col);
            return knight;
        }

        public static IFigure GetBishop(Color color, CoordinateY row, CoordinateX col)
        {
            IFigure bishop = new Bishop(color, row, col);
            return bishop;
        }

        public static IFigure GetQueen(Color color, CoordinateY row, CoordinateX col)
        {
            IFigure queen = new Queen(color, row, col);
            return queen;
        }

        public static IFigure GetKing(Color color, CoordinateY row, CoordinateX col)
        {
            IFigure king = new King(color, row, col);
            return king;
        }

        public static IFigure GetEmpty(CoordinateY row, CoordinateX col)
        {
            IFigure empty = new Empty(row, col);
            return empty;
        }

        public static IFigure[][] GetSquares(IFigure[][] squares)
        {
            squares = new IFigure[Globals.BoardRows][];

            for (int row = 0; row < Globals.BoardRows; row++)
            {
                squares[row] = new IFigure[Globals.BoardCols];
            }

            return squares;
        }
    }
}
