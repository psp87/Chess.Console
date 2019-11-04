namespace Chess
{
    using System;
    using Chess.Models.Figures.Contracts;
    using static Chess.Program;

    public class King : IFigure
    {
        private bool[,] figureMatrix;

        public King(Color color, CoordinateY row, CoordinateX col)
        {
            this.Name = "King";
            this.Color = color;
            this.Symbol = 'K';
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
                { false, true, true, false, true, false, true, true, false },
                { false, true, true, true, false, true, true, true, false },
                { false, true, true, true, true, true, true, true, false },
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
            if (toCol == this.Col && toRow == this.Row - 1)
            {
                this.Row -= 1;
                this.IsFirstMove = false;
                return true;
            }

            if (toCol == this.Col + 1 && toRow == this.Row - 1)
            {
                this.Row -= 1;
                this.Col += 1;
                this.IsFirstMove = false;
                return true;
            }

            if (toCol == this.Col + 1 && toRow == this.Row)
            {
                this.Col += 1;
                this.IsFirstMove = false;
                return true;
            }

            if (toCol == this.Col + 1 && toRow == this.Row + 1)
            {
                this.Row += 1;
                this.Col += 1;
                this.IsFirstMove = false;
                return true;
            }

            if (toCol == this.Col && toRow == this.Row + 1)
            {
                this.Row += 1;
                this.IsFirstMove = false;
                return true;
            }

            if (toCol == this.Col - 1 && toRow == this.Row + 1)
            {
                this.Row += 1;
                this.Col -= 1;
                this.IsFirstMove = false;
                return true;
            }

            if (toCol == this.Col - 1 && toRow == this.Row)
            {
                this.Col -= 1;
                this.IsFirstMove = false;
                return true;
            }

            if (toCol == this.Col - 1 && toRow == this.Row - 1)
            {
                this.Row -= 1;
                this.Col -= 1;
                this.IsFirstMove = false;
                return true;
            }

            if (this.IsFirstMove && toRow == this.Row)
            {
                if (toCol == this.Col + 2)
                {
                    if (this.OccupiedSquaresCheck(toRow, toCol, squares) && squares[(int)this.Row][7] is Rook && squares[(int)this.Row][7].IsFirstMove)
                    {
                        this.Col += 2;
                        this.IsFirstMove = false;

                        IFigure empty = Factory.GetEmpty(this.Row, (CoordinateX)7);
                        squares[(int)this.Row][(int)this.Col - 1] = squares[(int)this.Row][7];
                        Drawer.EmptySquare((int)this.Row, 7);
                        squares[(int)this.Row][(int)this.Col - 1].Draw((int)this.Row, (int)this.Col - 1);
                        squares[(int)this.Row][(int)this.Col - 1].Col -= 2;
                        squares[(int)this.Row][7] = empty;
                        Drawer.EmptySquare((int)this.Row, 7);

                        return true;
                    }
                }

                if (toCol == this.Col - 2)
                {
                    if (this.OccupiedSquaresCheck(toRow, toCol, squares) && squares[(int)this.Row][0] is Rook && squares[(int)this.Row][0].IsFirstMove)
                    {
                        this.Col -= 2;

                        IFigure empty = Factory.GetEmpty(this.Row, (CoordinateX)0);
                        squares[(int)this.Row][(int)this.Col + 1] = squares[(int)this.Row][0];
                        Drawer.EmptySquare((int)this.Row, 0);
                        squares[(int)this.Row][(int)this.Col + 1].Draw((int)this.Row, (int)this.Col + 1);
                        squares[(int)this.Row][(int)this.Col + 1].Col += 3;
                        squares[(int)this.Row][0] = empty;
                        Drawer.EmptySquare((int)this.Row, 0);

                        return true;
                    }
                }
            }

            return false;
        }

        public bool Take(IFigure[][] squares, CoordinateY toRow, CoordinateX toCol)
        {
            if (toCol == this.Col && toRow == this.Row - 1)
            {
                this.Row -= 1;
                this.IsFirstMove = false;
                return true;
            }

            if (toCol == this.Col + 1 && toRow == this.Row - 1)
            {
                this.Row -= 1;
                this.Col += 1;
                this.IsFirstMove = false;
                return true;
            }

            if (toCol == this.Col + 1 && toRow == this.Row)
            {
                this.Col += 1;
                this.IsFirstMove = false;
                return true;
            }

            if (toCol == this.Col + 1 && toRow == this.Row + 1)
            {
                this.Row += 1;
                this.Col += 1;
                this.IsFirstMove = false;
                return true;
            }

            if (toCol == this.Col && toRow == this.Row + 1)
            {
                this.Row += 1;
                this.IsFirstMove = false;
                return true;
            }

            if (toCol == this.Col - 1 && toRow == this.Row + 1)
            {
                this.Row += 1;
                this.Col -= 1;
                this.IsFirstMove = false;
                return true;
            }

            if (toCol == this.Col - 1 && toRow == this.Row)
            {
                this.Col -= 1;
                this.IsFirstMove = false;
                return true;
            }

            if (toCol == this.Col - 1 && toRow == this.Row - 1)
            {
                this.Row -= 1;
                this.Col -= 1;
                this.IsFirstMove = false;
                return true;
            }

            return false;
        }

        private bool OccupiedSquaresCheck(CoordinateY toRow, CoordinateX toCol, IFigure[][] squares)
        {
            int colDifference = Math.Abs((int)this.Col - (int)toCol) - 1;

            if ((int)this.Col > (int)toCol)
            {
                colDifference += 2;
            }

            for (int i = 1; i <= colDifference; i++)
            {
                int sign = this.Col < toCol? i : -i;

                int colCheck = (int)this.Col + sign;

                if (squares[(int)this.Row][colCheck].IsOccupied)
                {
                    return false;
                }
            }

            return true;
        }
    }
}