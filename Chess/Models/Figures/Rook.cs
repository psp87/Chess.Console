namespace Chess
{
    using System;
    using Chess.Models.Figures.Contracts;

    public class Rook : IFigure
    {
        private bool[,] figureMatrix;

        public Rook(Color color, CoordinateY row, CoordinateX col)
        {
            this.Name = "Rook";
            this.Color = color;
            this.Symbol = 'R';
            this.Col = col;
            this.Row = row;
            this.IsOccupied = true;
            this.IsFirstMove = true;
            this.IsLastMove = false;
            this.figureMatrix = new bool[Globals.CellRows, Globals.CellCols]
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

        public CoordinateY Row { get; set; }

        public CoordinateX Col { get; set; }

        public bool IsOccupied { get; set; }

        public bool IsFirstMove { get; set; }

        public bool IsLastMove { get; set; }

        public void Draw(int row, int col)
        {
            for (int cellRow = 1; cellRow < Globals.CellRows - 1; cellRow++)
            {
                for (int cellCol = 1; cellCol < Globals.CellCols - 1; cellCol++)
                {
                    if (this.figureMatrix[cellRow, cellCol] == true)
                    {
                        Console.SetCursorPosition(col * Globals.CellCols + Globals.OffsetHorizontal + cellCol,
                            row * Globals.CellRows + Globals.OffsetVertical + cellRow);

                        if (this.Color == Color.Light)
                        {
                            Paint.LightFigure();
                            Console.Write(" ");
                        }
                        else
                        {
                            Paint.DarkFigure();
                            Console.Write(" ");
                        }
                    }
                }
            }
        }

        public bool Move(IFigure[][] squares, CoordinateY toRow, CoordinateX toCol)
        {
            if (toRow != this.Row && toCol == this.Col)
            {
                if (this.OccupiedSquaresCheck(toRow, toCol, squares))
                {
                    this.Row = toRow;
                    return true;
                }
            }

            if (toRow == this.Row && toCol != this.Col)
            {
                if (this.OccupiedSquaresCheck(toRow, toCol, squares))
                {
                    this.Col = toCol;
                    return true;
                }
            }

            return false;
        }

        public bool Take(IFigure[][] squares, CoordinateY toRow, CoordinateX toCol)
        {
            if (toRow != this.Row && toCol == this.Col)
            {
                if (this.OccupiedSquaresCheck(toRow, toCol, squares))
                {
                    this.Row = toRow;
                    return true;
                }
            }

            if (toRow == this.Row && toCol != this.Col)
            {
                if (this.OccupiedSquaresCheck(toRow, toCol, squares))
                {
                    this.Col = toCol;
                    return true;
                }
            }

            return false;
        }

        private bool OccupiedSquaresCheck(CoordinateY toRow, CoordinateX toCol, IFigure[][] squares)
        {
            if (toRow != this.Row)
            {
                int rowDifference = Math.Abs((int)this.Row - (int)toRow) - 1;

                for (int i = 1; i <= rowDifference; i++)
                {
                    int sign = this.Row < toRow ? i : -i;

                    int rowCheck = (int)this.Row + sign;

                    if (squares[rowCheck][(int)this.Col].IsOccupied)
                    {
                        return false;
                    }
                }
            }
            else
            {
                int colDifference = Math.Abs((int)this.Col - (int)toCol) - 1;

                for (int i = 1; i <= colDifference; i++)
                {
                    int sign = this.Col < toCol ? i : -i;

                    int colCheck = (int)this.Col + sign;

                    if (squares[(int)this.Row][colCheck].IsOccupied)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}