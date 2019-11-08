namespace Chess.Models.Figures
{
    using System;
    using Square.Contracts;
    using Contracts;
    using Enums;

    public class Queen : IFigure
    {
        public Queen(Color color)
        {
            this.Name = "Queen";
            this.Color = color;
            this.Symbol = 'Q';
            this.IsFirstMove = true;
            this.IsLastMove = false;
            this.FigureMatrix = new bool[Globals.CellRows, Globals.CellCols]
            {
                { false, false, false, false, false, false, false, false, false },
                { false, false, false, false, true, false, false, false, false },
                { false, false, true, false, true, false, true, false, false },
                { false, false, false, true, false, true, false, false, false },
                { false, true, false, true, true, true, false, true, false },
                { false, false, true, false, true, false, true, false, false },
                { false, false, true, true, false, true, true, false, false },
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

        public bool Move(ISquare[][] matrix, ISquare square, IFigure figure, Row toRow, Col toCol)
        {
            if (toRow != square.Row && toCol == square.Col)
            {
                if (this.RookOccupiedSquaresCheck(toRow, toCol, matrix, square))
                {
                    square.Row = toRow;
                    return true;
                }
            }

            if (toRow == square.Row && toCol != square.Col)
            {
                if (this.RookOccupiedSquaresCheck(toRow, toCol, matrix, square))
                {
                    square.Col = toCol;
                    return true;
                }
            }

            int differenceRow = Math.Abs(toRow - square.Row);
            int differenceCol = Math.Abs(toCol - square.Col);

            if (differenceRow == differenceCol)
            {
                if (this.BishopOccupiedSquaresCheck(toRow, toCol, matrix, square))
                {
                    square.Row = toRow;
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
                if (this.RookOccupiedSquaresCheck(toRow, toCol, matrix, square))
                {
                    square.Row = toRow;
                    return true;
                }
            }

            if (toRow == square.Row && toCol != square.Col)
            {
                if (this.RookOccupiedSquaresCheck(toRow, toCol, matrix, square))
                {
                    square.Col = toCol;
                    return true;
                }
            }

            int differenceRow = Math.Abs(toRow - square.Row);
            int differenceCol = Math.Abs(toCol - square.Col);

            if (differenceRow == differenceCol)
            {
                if (this.BishopOccupiedSquaresCheck(toRow, toCol, matrix, square))
                {
                    square.Row = toRow;
                    square.Col = toCol;
                    return true;
                }
            }

            return false;
        }

        private bool RookOccupiedSquaresCheck(Row toRow, Col toCol, ISquare[][] matrix, ISquare square)
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

        private bool BishopOccupiedSquaresCheck(Row toRow, Col toCol, ISquare[][] matrix, ISquare square)
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