namespace Chess
{
    using System;
    using Chess.Models.Figures.Contracts;
    using static Chess.Program;

    public class Knight : IFigure
    {
        private bool[,] figureMatrix;

        public Knight(Color color, CoordinateY row, CoordinateX col)
        {
            this.Name = "Knight";
            this.Color = color;
            this.Symbol = 'N';
            this.Col = col;
            this.Row = row;
            this.IsOccupied = true;
            this.IsFirstMove = true;
            this.IsLastMove = false;
            this.figureMatrix = new bool[Globals.CellRows, Globals.CellCols]
            {
                { false, false, false, false, false, false, false, false, false },
                { false, false, false, true, true, true, false, false, false },
                { false, false, true, false, true, true, true, false, false },
                { false, true, true, true, true, true, true, false, false },
                { false, true, true, false, false, true, true, false, false },
                { false, false, false, false, true, true, true, false, false },
                { false, false, false, true, true, true, false, false , false },
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
            if (toCol == this.Col - 1 && toRow == this.Row - 2)
            {
                this.Row -= 2;
                this.Col -= 1;
                return true;
            }

            if (toCol == this.Col + 1 && toRow == this.Row - 2)
            {
                this.Row -= 2;
                this.Col += 1;
                return true;
            }

            if (toCol == this.Col - 1 && toRow == this.Row + 2)
            {
                this.Row += 2;
                this.Col -= 1;
                return true;
            }

            if (toCol == this.Col + 1 && toRow == this.Row + 2)
            {
                this.Row += 2;
                this.Col += 1;
                return true;
            }

            if (toCol == this.Col - 2 && toRow == this.Row - 1)
            {
                this.Row -= 1;
                this.Col -= 2;
                return true;
            }

            if (toCol == this.Col - 2 && toRow == this.Row + 1)
            {
                this.Row += 1;
                this.Col -= 2;
                return true;
            }

            if (toCol == this.Col + 2 && toRow == this.Row - 1)
            {
                this.Row -= 1;
                this.Col += 2;
                return true;
            }

            if (toCol == this.Col + 2 && toRow == this.Row + 1)
            {
                this.Row += 1;
                this.Col += 2;
                return true;
            }

            return false;
        }

        public bool Take(IFigure[][] squares, CoordinateY toRow, CoordinateX toCol)
        {
            if (toCol == this.Col - 1 && toRow == this.Row - 2)
            {
                this.Row -= 2;
                this.Col -= 1;
                return true;
            }

            if (toCol == this.Col + 1 && toRow == this.Row - 2)
            {
                this.Row -= 2;
                this.Col += 1;
                return true;
            }

            if (toCol == this.Col - 1 && toRow == this.Row + 2)
            {
                this.Row += 2;
                this.Col -= 1;
                return true;
            }

            if (toCol == this.Col + 1 && toRow == this.Row + 2)
            {
                this.Row += 2;
                this.Col += 1;
                return true;
            }

            if (toCol == this.Col - 2 && toRow == this.Row - 1)
            {
                this.Row -= 1;
                this.Col -= 2;
                return true;
            }

            if (toCol == this.Col - 2 && toRow == this.Row + 1)
            {
                this.Row += 1;
                this.Col -= 2;
                return true;
            }

            if (toCol == this.Col + 2 && toRow == this.Row - 1)
            {
                this.Row -= 1;
                this.Col += 2;
                return true;
            }

            if (toCol == this.Col + 2 && toRow == this.Row + 1)
            {
                this.Row += 1;
                this.Col += 2;
                return true;
            }

            return false;
        }
    }
}