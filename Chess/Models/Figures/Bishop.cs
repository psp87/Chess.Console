namespace Chess
{
    using System;
    using Chess.Models.Figures.Contracts;
    using static Chess.Program;

    public class Bishop : IFigure
    {
        private bool[,] figureMatrix;

        public Bishop(Color color, CoordinateY row, CoordinateX col)
        {
            this.Name = "Bishop";
            this.Color = color;
            this.Symbol = 'B';
            this.Col = col;
            this.Row = row;
            this.IsOccupied = true;
            this.IsFirstMove = true;
            this.IsLastMove = false;
            this.figureMatrix = new bool[Globals.CellRows, Globals.CellCols]
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
            int differenceRow = Math.Abs(toRow - this.Row);
            int differenceCol = Math.Abs(toCol - this.Col);

            if (differenceRow == differenceCol)
            {
                if (this.OccupiedSquaresCheck(toRow, toCol, squares))
                {
                    this.Row = toRow;
                    this.Col = toCol;
                    return true;
                }
            }

            return false;
        }

        public bool Take(IFigure[][] squares, CoordinateY toRow, CoordinateX toCol)
        {
            int differenceRow = Math.Abs(toRow - this.Row);
            int differenceCol = Math.Abs(toCol - this.Col);

            if (differenceRow == differenceCol)
            {
                if (this.OccupiedSquaresCheck(toRow, toCol, squares))
                {
                    this.Row = toRow;
                    this.Col = toCol;
                    return true;
                }
            }

            return false;
        }

        private bool OccupiedSquaresCheck(CoordinateY toRow, CoordinateX toCol, IFigure[][] squares)
        {
            int squaresCount = Math.Abs((int)this.Row - (int)toRow) - 1;

            if (toRow < this.Row && toCol < this.Col)
            {
                for (int i = 1; i <= squaresCount; i++)
                {
                    int rowCheck = (int)this.Row - i;
                    int colCheck = (int)this.Col - i;

                    if (squares[rowCheck][colCheck].IsOccupied)
                    {
                        return false;
                    }
                }
            }

            if (toRow > this.Row && toCol > this.Col)
            {
                for (int i = 1; i <= squaresCount; i++)
                {
                    int rowCheck = (int)this.Row + i;
                    int colCheck = (int)this.Col + i;

                    if (squares[rowCheck][colCheck].IsOccupied)
                    {
                        return false;
                    }
                }
            }

            if (toRow < this.Row && toCol > this.Col)
            {
                for (int i = 1; i <= squaresCount; i++)
                {
                    int rowCheck = (int)this.Row - i;
                    int colCheck = (int)this.Col + i;

                    if (squares[rowCheck][colCheck].IsOccupied)
                    {
                        return false;
                    }
                }
            }

            if (toRow > this.Row && toCol < this.Col)
            {
                for (int i = 1; i <= squaresCount; i++)
                {
                    int rowCheck = (int)this.Row + i;
                    int colCheck = (int)this.Col - i;

                    if (squares[rowCheck][colCheck].IsOccupied)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
