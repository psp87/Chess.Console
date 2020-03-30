namespace Chess.Models.Figures
{
    using System;
    using Square.Contracts;
    using Contracts;
    using Enums;

    public class Empty : Piece
    {
        public Empty()
        {
            this.Color = Color.Empty;
        }

        public override char Symbol => 'E';

        public override bool[,] FigureMatrix { get => new bool[Globals.CellRows, Globals.CellCols]
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

        public override bool IsMoveAvailable(ISquare[][] matrix, int row, int col)
        {
            return true;
        }

        public override void Attacking(ISquare[][] matrix, ISquare square, int row, int col)
        {

        }

        public override bool Move(ISquare[][] matrix, ISquare square, IPiece figure, Row toRow, Col toCol)
        {
            throw new NotImplementedException();
        }

        public override bool Take(ISquare[][] matrix, ISquare square, IPiece figure, Row toRow, Col toCol)
        {
            throw new NotImplementedException();
        }
    }
}
