namespace Chess.Models.Pieces
{
    using System;
    using System.Linq;

    using Contracts;
    using Enums;
    using View;

    public class King : Piece
    {
        Draw drawer = Factory.GetDraw();

        public King(Color color)
            : base(color)
        {
        }

        public override char Symbol => 'K';

        public override bool[,] FigureMatrix
        {
            get => new bool[Globals.CellRows, Globals.CellCols]
{
                { false, false, false, false, false, false, false, false, false },
                { false, false, false, false, true, false, false, false, false },
                { false, false, false, true, true, true, false, false, false },
                { false, true, true, false, true, false, true, true, false },
                { false, true, true, true, false, true, true, true, false },
                { false, true, true, true, true, true, true, true, false },
                { false, false, true, true, true, true, true, false, false },
                { false, false, true, true, true, true, true, false, false },
                { false, false, false, false, false, false, false, false, false }
};
        }

        public override void IsMoveAvailable(Square[][] matrix)
        {
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0)
                    {
                        continue;
                    }

                    if (Position.IsInBoard(this.Position.X + x, this.Position.Y + y))
                    {
                        var checkedSquare = matrix[this.Position.Y + y][this.Position.X + x];

                        if ((checkedSquare.IsOccupied &&
                            checkedSquare.Piece.Color != this.Color &&
                            !checkedSquare.IsAttacked.Where(p => p.Color != this.Color).Any()) ||
                            (!checkedSquare.IsOccupied &&
                            !checkedSquare.IsAttacked.Where(p => p.Color != this.Color).Any()))
                        {
                            this.IsMovable = true;
                        }
                    }
                }
            }

            this.IsMovable = false;
        }

        public override void Attacking(Square[][] matrix)
        {
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0)
                    {
                        continue;
                    }

                    if (Position.IsInBoard(this.Position.X + x, this.Position.Y + y))
                    {
                        matrix[this.Position.Y + y][this.Position.X + x].IsAttacked.Add(this);
                    }
                }
            }
        }

        public override bool Move(Position to, Square[][] matrix)
        {
            if (!matrix[to.Y][to.X].IsAttacked.Where(x => x.Color != this.Color).Any())
            {
                for (int x = -1; x <= 1; x++)
                {
                    for (int y = -1; y <= 1; y++)
                    {
                        if (x == 0 && y == 0)
                        {
                            continue;
                        }

                        if (to.X == this.Position.X + x && to.Y == this.Position.Y + y)
                        {
                            this.Position.Y += x;
                            this.Position.X += y;
                            this.IsFirstMove = false;
                            return true;
                        }
                    }
                }

                if (this.IsFirstMove && to.Y == this.Position.Y && to.X == this.Position.X + 2)
                {
                    var rook = matrix[this.Position.Y][7].Piece;
                    if (this.OccupiedSquaresCheck(to, matrix) && rook is Rook && rook.IsFirstMove)
                    {
                        this.Position.X += 2;
                        this.IsFirstMove = false;

                        matrix[this.Position.Y][this.Position.X - 1].Piece = matrix[this.Position.Y][7].Piece;

                        drawer.EmptySquare(this.Position.Y, 7);
                        drawer.Figure(this.Position.Y, this.Position.X - 1, matrix[this.Position.Y][this.Position.X - 1].Piece);

                        matrix[this.Position.Y][this.Position.X - 1].Position.X -= 2;
                        IPiece emptyFigure = Factory.GetEmpty();
                        matrix[this.Position.Y][7].Piece = emptyFigure;

                        drawer.EmptySquare(this.Position.Y, 7);

                        return true;
                    }
                }

                if (this.IsFirstMove && to.Y == this.Position.Y && to.X == this.Position.X - 2)
                {
                    var rook = matrix[this.Position.Y][0].Piece;
                    if (this.OccupiedSquaresCheck(to, matrix) && rook is Rook && rook.IsFirstMove)
                    {
                        this.Position.X -= 2;
                        this.IsFirstMove = false;

                        matrix[this.Position.Y][this.Position.X + 1].Piece = matrix[this.Position.Y][0].Piece;

                        drawer.EmptySquare(this.Position.Y, 0);
                        drawer.Figure(this.Position.Y, this.Position.X + 1, matrix[this.Position.Y][this.Position.X + 1].Piece);

                        matrix[this.Position.Y][this.Position.X + 1].Position.X += 3;
                        IPiece emptyFigure = Factory.GetEmpty();
                        matrix[this.Position.Y][0].Piece = emptyFigure;
                        drawer.EmptySquare(this.Position.Y, 0);

                        return true;
                    }
                }
            }

            return false;
        }

        public override bool Take(Position to, Square[][] matrix)
        {
            return this.Move(to, matrix);
        }

        private bool OccupiedSquaresCheck(Position to, Square[][] matrix)
        {
            int colDifference = Math.Abs(this.Position.X - to.X) - 1;

            if (this.Position.X > to.X)
            {
                colDifference += 2;
            }

            for (int i = 1; i <= colDifference; i++)
            {
                int sign = this.Position.X < to.X ? i : -i;

                if (matrix[this.Position.Y][this.Position.X + sign].IsOccupied)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
