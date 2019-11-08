namespace Chess.Models.Player.Contracts
{
    using Enums;

    public interface IPlayer
    {
        string Name { get; }

        Color Color { get; }

        bool isChess { get; set; }

        bool isMoveAvailable { get; set; }

        bool isCastlingAvailable { get; set; }

        void TakeFigure(string figureName);

        int TakenFigures(string figureName);
    }
}
