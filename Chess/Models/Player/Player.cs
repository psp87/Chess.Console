namespace Chess.Models.Player
{
    using Chess.Models.Player.Contracts;
    using System.Collections.Generic;

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
            this.isCastlingAvailable = true;
        }

        public string Name { get; }

        public Color Color { get; }

        public bool isChess { get; set; }

        public bool isMoveAvailable { get; set; }

        public bool isCastlingAvailable { get; set; }

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
