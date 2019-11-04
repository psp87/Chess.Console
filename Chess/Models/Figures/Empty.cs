namespace Chess
{
    using System;
    using Chess.Models.Figures.Contracts;
    using static Chess.Program;

    public class Empty : IFigure
    {
        public Empty(CoordinateY row, CoordinateX col)
        {
            this.Name = "Empty";
            this.Symbol = 'E';
            this.Col = col;
            this.Row = row;
            this.Color = Color.Empty;
            this.IsOccupied = false;
            this.IsFirstMove = false;
            this.IsLastMove = false;
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
        }

        public bool Move(IFigure[][] squares, CoordinateY toY, CoordinateX toX)
        {
            throw new NotImplementedException();
        }

        public bool Take(IFigure[][] squares, CoordinateY toRow, CoordinateX toCol)
        {
            throw new NotImplementedException();
        }
    }
}
