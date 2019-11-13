namespace Chess.Models.Player.Contracts
{
    using Enums;

    public interface IPlayer
    {
        string Name { get; }

        Color Color { get; }

        bool IsCheck { get; set; }

        bool IsCheckmate { get; set; }

        bool IsMoveAvailable { get; set; }

        void TakeFigure(string figureName);

        int TakenFigures(string figureName);
    }
}
