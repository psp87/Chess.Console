namespace Chess.Models.Figures
{
    using System;
    using Board;
    using Square.Contracts;
    using Contracts;
    using Enums;

    public class Rook : IFigure
    {
        public Rook(Color color)
        {
            this.Name = "Rook";
            this.Color = color;
            this.Symbol = 'R';
            this.IsFirstMove = true;
            this.IsLastMove = false;
            this.FigureMatrix = new bool[Globals.CellRows, Globals.CellCols]
            {
                { false, false, false, false, false, false, false, false, false },
                { false, false, true, false, true, false, true, false, false },
                { false, false, false, true, true, true, false, false, false },
                { false, false, false, true, true, true, false, false, false },
                { false, false, false, true, true, true, false, false, false },
                { false, false, false, true, true, true, false, false, false },
                { false, false, true, true, true, true, true, false, false },
                { false, false, true, true, true, true, true, false, false },
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
            if (Board.InBoardCheck(row + 1, col))
            {
                var checkedSquare = matrix[row + 1][col];

                if ((checkedSquare.IsOccupied && checkedSquare.Figure.Color != this.Color) || !checkedSquare.IsOccupied)
                {
                    return true;
                }
            }

            if (Board.InBoardCheck(row - 1, col))
            {
                var checkedSquare = matrix[row - 1][col];

                if ((checkedSquare.IsOccupied && checkedSquare.Figure.Color != this.Color) || !checkedSquare.IsOccupied)
                {
                    return true;
                }
            }

            if (Board.InBoardCheck(row, col - 1))
            {
                var checkedSquare = matrix[row][col - 1];

                if ((checkedSquare.IsOccupied && checkedSquare.Figure.Color != this.Color) || !checkedSquare.IsOccupied)
                {
                    return true;
                }
            }

            if (Board.InBoardCheck(row, col + 1))
            {
                var checkedSquare = matrix[row][col + 1];

                if ((checkedSquare.IsOccupied && checkedSquare.Figure.Color != this.Color) || !checkedSquare.IsOccupied)
                {
                    return true;
                }
            }

            return false;
        }

        public void Attacking(ISquare[][] matrix, ISquare square, int row, int col)
        {
            for (int i = 1; i <= 7; i++)
            {
                if (Board.InBoardCheck(row - i, col))
                {
                    matrix[row - i][col].IsAttacked.Add(square);

                    if (matrix[row - i][col].IsOccupied)
                    {
                        break;
                    }
                }
            }

            for (int i = 1; i <= 7; i++)
            {
                if (Board.InBoardCheck(row + i, col))
                {
                    matrix[row + i][col].IsAttacked.Add(square);

                    if (matrix[row + i][col].IsOccupied)
                    {
                        break;
                    }
                }
            }

            for (int i = 1; i <= 7; i++)
            {
                if (Board.InBoardCheck(row, col - i))
                {
                    matrix[row][col - i].IsAttacked.Add(square);

                    if (matrix[row][col - i].IsOccupied)
                    {
                        break;
                    }
                }
            }

            for (int i = 1; i <= 7; i++)
            {
                if (Board.InBoardCheck(row, col + i))
                {
                    matrix[row][col + i].IsAttacked.Add(square);

                    if (matrix[row][col + i].IsOccupied)
                    {
                        break;
                    }
                }
            }
        }

        public bool Move(ISquare[][] matrix, ISquare square, IFigure figure, Row toRow, Col toCol)
        {
            if (toRow != square.Row && toCol == square.Col)
            {
                if (this.OccupiedSquaresCheck(toRow, toCol, matrix, square))
                {
                    square.Row = toRow;
                    return true;
                }
            }

            if (toRow == square.Row && toCol != square.Col)
            {
                if (this.OccupiedSquaresCheck(toRow, toCol, matrix, square))
                {
                    square.Col = toCol;
                    return true;
                }
            }

            return false;
        }

        public bool Take(ISquare[][] matrix, ISquare square, IFigure figure, Row toRow, Col toCol)
        {
            if (toRow != square.Row && toCol == square.Col)
            {
                if (this.OccupiedSquaresCheck(toRow, toCol, matrix, square))
                {
                    square.Row = toRow;
                    return true;
                }
            }

            if (toRow == square.Row && toCol != square.Col)
            {
                if (this.OccupiedSquaresCheck(toRow, toCol, matrix, square))
                {
                    square.Col = toCol;
                    return true;
                }
            }

            return false;
        }

        private bool OccupiedSquaresCheck(Row toRow, Col toCol, ISquare[][] matrix, ISquare square)
        {
            if (toRow != square.Row)
            {
                int rowDifference = Math.Abs((int)square.Row - (int)toRow) - 1;

                for (int i = 1; i <= rowDifference; i++)
                {
                    int sign = square.Row < toRow ? i : -i;

                    int rowCheck = (int)square.Row + sign;

                    if (matrix[rowCheck][(int)square.Col].IsOccupied)
                    {
                        return false;
                    }
                }
            }
            else
            {
                int colDifference = Math.Abs((int)square.Col - (int)toCol) - 1;

                for (int i = 1; i <= colDifference; i++)
                {
                    int sign = square.Col < toCol ? i : -i;

                    int colCheck = (int)square.Col + sign;

                    if (matrix[(int)square.Row][colCheck].IsOccupied)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}