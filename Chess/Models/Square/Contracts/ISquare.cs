namespace Chess.Models.Square.Contracts
{
    using System.Collections.Generic;
    using Figures.Contracts;
    using Enums;

    public interface ISquare
    {
        Row Row { get; set; }

        Col Col { get; set; }

        IFigure Figure { get; set; }

        bool IsOccupied { get; set; }

        List<ISquare> IsAttacked { get; set; }
    }
}
