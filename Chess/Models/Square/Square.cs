namespace Chess.Models.Square
{
    using System.Collections.Generic;
    using Contracts;
    using Figures.Contracts;
    using Enums;

    public class Square : ISquare
    {
        public Square(Row row, Col col, IFigure empty)
        {
            this.Row = row;
            this.Col = col;
            this.Figure = empty;
            this.IsOccupied = false;
            this.IsAttacked = new List<ISquare>();
        }

        public Row Row { get; set; }

        public Col Col { get; set; }

        public IFigure Figure { get ; set ; }

        public bool IsOccupied { get; set; }

        public List<ISquare> IsAttacked { get; set; }
    }
}
