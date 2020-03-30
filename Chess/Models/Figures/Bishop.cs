namespace Chess.Models.Figures
{
    using System;
    using Board;
    using Square.Contracts;
    using Figures.Contracts;
    using Enums;

    public class Bishop : Piece
    {
        public Bishop(Color color)
            : base(color)
        {
        }

        public override char Symbol => 'B';

        public override bool[,] FigureMatrix { get => new bool[Globals.CellRows, Globals.CellCols]
            {
                { false, false, false, false, false, false, false, false, false },
                { false, false, false, false, true, false, false, false, false },
                { false, false, false, true, true, true, false, false, false },
                { false, false, true, true, false, true, true, false, false },
                { false, false, true, false, false, false, true, false, false },
                { false, false, false, true, false, true, false, false, false },
                { false, false, false, false, true, false, false, false, false },
                { false, false, true, true, false, true, true, false, false },
                { false, false, false, false, false, false, false, false, false }
            };
        }

        public override bool IsMoveAvailable(ISquare[][] matrix, int row, int col)
        {
            for (int i = -1; i <= 1; i += 2)
            {
                for (int k = -1; k <= 1; k += 2)
                {
                    if (Board.InBoardCheck(row + i, col + k))
                    {
                        var checkedSquare = matrix[row + i][col + k];

                        if ((checkedSquare.IsOccupied && checkedSquare.Figure.Color != this.Color) || !checkedSquare.IsOccupied)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public override void Attacking(ISquare[][] matrix, ISquare square, int row, int col)
        {
            for (int i = 1; i <= 7; i++)
            {
                if (Board.InBoardCheck(row - i, col - i))
                {
                    matrix[row - i][col - i].IsAttacked.Add(square);

                    if (matrix[row - i][col - i].IsOccupied)
                    {
                        break;
                    }
                }
            }

            for (int i = 1; i <= 7; i++)
            {
                if (Board.InBoardCheck(row - i, col + i))
                {
                    matrix[row - i][col + i].IsAttacked.Add(square);

                    if (matrix[row - i][col + i].IsOccupied)
                    {
                        break;
                    }
                }
            }

            for (int i = 1; i <= 7; i++)
            {
                if (Board.InBoardCheck(row + i, col - i))
                {
                    matrix[row + i][col - i].IsAttacked.Add(square);

                    if (matrix[row + i][col - i].IsOccupied)
                    {
                        break;
                    }
                }
            }

            for (int i = 1; i <= 7; i++)
            {
                if (Board.InBoardCheck(row + i, col + i))
                {
                    matrix[row + i][col + i].IsAttacked.Add(square);

                    if (matrix[row + i][col + i].IsOccupied)
                    {
                        break;
                    }
                }
            }
        }

        public override bool Move(ISquare[][] matrix, ISquare square, IPiece figure, Row toRow, Col toCol)
        {
            int differenceRow = Math.Abs(toRow - square.Row);
            int differenceCol = Math.Abs(toCol - square.Col);

            if (differenceRow == differenceCol)
            {
                if (this.OccupiedSquaresCheck(toRow, toCol, matrix, square))
                {
                    square.Row = toRow;
                    square.Col = toCol;
                    return true;
                }
            }

            return false;
        }

        public override bool Take(ISquare[][] matrix, ISquare square, IPiece figure, Row toRow, Col toCol)
        {
            int differenceRow = Math.Abs(toRow - square.Row);
            int differenceCol = Math.Abs(toCol - square.Col);

            if (differenceRow == differenceCol)
            {
                if (this.OccupiedSquaresCheck(toRow, toCol, matrix, square))
                {
                    square.Row = toRow;
                    square.Col = toCol;
                    return true;
                }
            }

            return false;
        }

        private bool OccupiedSquaresCheck(Row toRow, Col toCol, ISquare[][] matrix, ISquare square)
        {
            int squaresCount = Math.Abs((int)square.Row - (int)toRow) - 1;

            if (toRow < square.Row && toCol < square.Col)
            {
                for (int i = 1; i <= squaresCount; i++)
                {
                    int rowCheck = (int)square.Row - i;
                    int colCheck = (int)square.Col - i;

                    if (matrix[rowCheck][colCheck].IsOccupied)
                    {
                        return false;
                    }
                }
            }

            if (toRow > square.Row && toCol > square.Col)
            {
                for (int i = 1; i <= squaresCount; i++)
                {
                    int rowCheck = (int)square.Row + i;
                    int colCheck = (int)square.Col + i;

                    if (matrix[rowCheck][colCheck].IsOccupied)
                    {
                        return false;
                    }
                }
            }

            if (toRow < square.Row && toCol > square.Col)
            {
                for (int i = 1; i <= squaresCount; i++)
                {
                    int rowCheck = (int)square.Row - i;
                    int colCheck = (int)square.Col + i;

                    if (matrix[rowCheck][colCheck].IsOccupied)
                    {
                        return false;
                    }
                }
            }

            if (toRow > square.Row && toCol < square.Col)
            {
                for (int i = 1; i <= squaresCount; i++)
                {
                    int rowCheck = (int)square.Row + i;
                    int colCheck = (int)square.Col - i;

                    if (matrix[rowCheck][colCheck].IsOccupied)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
