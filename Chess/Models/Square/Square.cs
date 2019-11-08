namespace Chess.Models.Square
{
    using System.Collections.Generic;
    using Contracts;
    using Figures.Contracts;
    using Enums;

    public class Square : ISquare
    {
        public Square(Row row, Col col, Color color, IFigure empty)
        {
            this.Row = row;
            this.Col = col;
            this.Color = color;
            this.Figure = empty;
            this.IsOccupied = false;
            this.IsAttacked = new Dictionary<IFigure, int>();
        }

        public Row Row { get; set; }

        public Col Col { get; set; }

        public Color Color { get; set; }

        public IFigure Figure { get ; set ; }

        public bool IsOccupied { get; set; }

        public Dictionary<IFigure, int> IsAttacked { get; set; }
    }
}
