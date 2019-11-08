namespace Chess.Models.Figures
{
    using Square.Contracts;
    using Contracts;
    using Enums;

    public class Knight : IFigure
    {
        public Knight(Color color)
        {
            this.Name = "Knight";
            this.Color = color;
            this.Symbol = 'N';
            this.IsFirstMove = true;
            this.IsLastMove = false;
            this.FigureMatrix = new bool[Globals.CellRows, Globals.CellCols]
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

        public bool[,] FigureMatrix { get; }

        public bool IsFirstMove { get; set; }

        public bool IsLastMove { get; set; }

        public bool Move(ISquare[][] squares, ISquare square, IFigure figure, Row toRow, Col toCol)
        {
            if (toCol == square.Col - 1 && toRow == square.Row - 2)
            {
                square.Row -= 2;
                square.Col -= 1;
                return true;
            }

            if (toCol == square.Col + 1 && toRow == square.Row - 2)
            {
                square.Row -= 2;
                square.Col += 1;
                return true;
            }

            if (toCol == square.Col - 1 && toRow == square.Row + 2)
            {
                square.Row += 2;
                square.Col -= 1;
                return true;
            }

            if (toCol == square.Col + 1 && toRow == square.Row + 2)
            {
                square.Row += 2;
                square.Col += 1;
                return true;
            }

            if (toCol == square.Col - 2 && toRow == square.Row - 1)
            {
                square.Row -= 1;
                square.Col -= 2;
                return true;
            }

            if (toCol == square.Col - 2 && toRow == square.Row + 1)
            {
                square.Row += 1;
                square.Col -= 2;
                return true;
            }

            if (toCol == square.Col + 2 && toRow == square.Row - 1)
            {
                square.Row -= 1;
                square.Col += 2;
                return true;
            }

            if (toCol == square.Col + 2 && toRow == square.Row + 1)
            {
                square.Row += 1;
                square.Col += 2;
                return true;
            }

            return false;
        }

        public bool Take(ISquare[][] squares, ISquare square, IFigure figure, Row toRow, Col toCol)
        {
            if (toCol == square.Col - 1 && toRow == square.Row - 2)
            {
                square.Row -= 2;
                square.Col -= 1;
                return true;
            }

            if (toCol == square.Col + 1 && toRow == square.Row - 2)
            {
                square.Row -= 2;
                square.Col += 1;
                return true;
            }

            if (toCol == square.Col - 1 && toRow == square.Row + 2)
            {
                square.Row += 2;
                square.Col -= 1;
                return true;
            }

            if (toCol == square.Col + 1 && toRow == square.Row + 2)
            {
                square.Row += 2;
                square.Col += 1;
                return true;
            }

            if (toCol == square.Col - 2 && toRow == square.Row - 1)
            {
                square.Row -= 1;
                square.Col -= 2;
                return true;
            }

            if (toCol == square.Col - 2 && toRow == square.Row + 1)
            {
                square.Row += 1;
                square.Col -= 2;
                return true;
            }

            if (toCol == square.Col + 2 && toRow == square.Row - 1)
            {
                square.Row -= 1;
                square.Col += 2;
                return true;
            }

            if (toCol == square.Col + 2 && toRow == square.Row + 1)
            {
                square.Row += 1;
                square.Col += 2;
                return true;
            }

            return false;
        }
    }
}