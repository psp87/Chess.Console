namespace Chess.Models.Player.Contracts
{
    using Chess.Models.Figures.Contracts;
    using static Chess.Program;

    public interface IPlayer
    {
        string Name { get; }

        Color Color { get; }

        bool isChess { get; set; }

        bool isMoveAvailable { get; set; }

        bool isCastlingAvailable { get; set; }

        int TakenFigures(IFigure figure);
    }
}
