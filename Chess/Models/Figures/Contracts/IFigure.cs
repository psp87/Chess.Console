namespace Chess.Models.Figures.Contracts
{
    using Square.Contracts;
    using Enums;

    public interface IFigure
    {
        string Name { get; }

        Color Color { get; }

        char Symbol { get; }

        bool[,] FigureMatrix { get; }

        bool IsFirstMove { get; set; }

        bool IsLastMove { get; set; }

        bool IsMoveAvailable(ISquare[][] matrix, int row, int col);

        void Attacking(ISquare[][] matrix, ISquare square, int row, int col);

        bool Move(ISquare[][] matrix, ISquare square, IFigure figure, Row toRow, Col toCol);

        bool Take(ISquare[][] matrix, ISquare square, IFigure figure, Row toRow, Col toCol);
    }
}
