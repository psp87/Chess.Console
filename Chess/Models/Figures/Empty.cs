namespace Chess.Models.Figures
{
    using System;
    using Square.Contracts;
    using Contracts;
    using Enums;

    public class Empty : IFigure
    {
        public Empty()
        {
            this.Name = "Empty";
            this.Symbol = 'E';
            this.Color = Color.Empty;
            this.IsFirstMove = false;
            this.IsLastMove = false;
            this.FigureMatrix = new bool[Globals.CellRows, Globals.CellCols]
            {
                { false, false, false, false, false, false, false, false, false },
                { false, false, false, false, false, false, false, false, false },
                { false, false, false, false, false, false, false, false, false },
                { false, false, false, false, false, false, false, false, false },
                { false, false, false, false, false, false, false, false, false },
                { false, false, false, false, false, false, false, false, false },
                { false, false, false, false, false, false, false, false, false },
                { false, false, false, false, false, false, false, false, false },
                { false, false, false, false, false, false, false, false, false }
            };
        }

        public string Name { get; }

        public Color Color { get; }

        public char Symbol { get; }

        public bool[,] FigureMatrix { get; }

        public bool IsFirstMove { get; set; }

        public bool IsLastMove { get; set; }

        public bool IsMoveAvailable(ISquare[][] matrix, int row, int col)
        {
            return true;
        }

        public void Attacking(ISquare[][] matrix, ISquare square, int row, int col)
        {

        }

        public bool Move(ISquare[][] matrix, ISquare square, IFigure figure, Row toRow, Col toCol)
        {
            throw new NotImplementedException();
        }

        public bool Take(ISquare[][] matrix, ISquare square, IFigure figure, Row toRow, Col toCol)
        {
            throw new NotImplementedException();
        }
    }
}
