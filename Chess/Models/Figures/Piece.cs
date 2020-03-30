namespace Chess.Models.Figures
{
    using System;

    using Chess.Models.Enums;
    using Chess.Models.Figures.Contracts;
    using Chess.Models.Square.Contracts;

    public abstract class Piece : IPiece, ICloneable
    {
        public Piece(Color color)
        {
            this.Color = color;
            this.IsFirstMove = true;
            this.IsLastMove = false;
        }

        public Piece()
        {
        }

        public string Name => this.GetType().Name.ToString();

        public Color Color { get; set; }

        public abstract char Symbol { get; }

        public abstract bool[,] FigureMatrix { get; }

        public Position Position { get; set; }

        public bool IsFirstMove { get; set; }

        public bool IsLastMove { get; set; }

        public bool IsMoveable { get; set; }

        public abstract bool IsMoveAvailable(ISquare[][] matrix, int row, int col);

        public abstract void Attacking(ISquare[][] matrix, ISquare square, int row, int col);

        public abstract bool Move(ISquare[][] matrix, ISquare square, IPiece figure, Row toRow, Col toCol);

        public abstract bool Take(ISquare[][] matrix, ISquare square, IPiece figure, Row toRow, Col toCol);

        public override string ToString()
        {
            return this.ToString();
        }

        public virtual object Clone()
        {
            return this.Clone();
        }
    }
}
