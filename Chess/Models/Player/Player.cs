namespace Chess.Models.Player
{
    using System.Collections.Generic;
    using Figures;
    using Contracts;
    using Enums;

    public class Player : IPlayer
    {
        Dictionary<string, int> takenFigures;

        public Player(string name, Color color)
        {
            this.takenFigures = new Dictionary<string, int>()
            {
                { nameof(Pawn), 0 },
                { nameof(Knight), 0 },
                { nameof(Bishop), 0 },
                { nameof(Rook), 0 },
                { nameof(Queen), 0 },
            };
            this.Name = name;
            this.Color = color;
            this.IsMoveAvailable = true;
            this.IsCheck = false;
            this.IsCheckmate = false;
        }

        public string Name { get; }

        public Color Color { get; }

        public bool IsCheck { get; set; }

        public bool IsCheckmate { get; set; }

        public bool IsMoveAvailable { get; set; }

        public void TakeFigure(string figureName)
        {
            takenFigures[figureName]++;
        }

        public int TakenFigures(string figureName)
        {
            return takenFigures[figureName];
        }
    }
}
