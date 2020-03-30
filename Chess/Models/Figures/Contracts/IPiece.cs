namespace Chess.Models.Figures.Contracts
{
    using System;

    using Square.Contracts;
    using Enums;

    public interface IPiece : ICloneable
    {
        string Name { get; }

        Color Color { get; }

        char Symbol { get; }

        bool[,] FigureMatrix { get; }

        Position Position { get; set; }

        bool IsFirstMove { get; set; }

        bool IsLastMove { get; set; }

        bool IsMoveable { get; set; }

        bool IsMoveAvailable(ISquare[][] matrix, int row, int col);

        void Attacking(ISquare[][] matrix, ISquare square, int row, int col);

        bool Move(ISquare[][] matrix, ISquare square, IPiece figure, Row toRow, Col toCol);

        bool Take(ISquare[][] matrix, ISquare square, IPiece figure, Row toRow, Col toCol);
    }
}
