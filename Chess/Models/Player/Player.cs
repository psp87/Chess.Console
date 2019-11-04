namespace Chess.Models.Player
{
    using Chess.Models.Figures.Contracts;
    using Chess.Models.Player.Contracts;
    using System.Collections.Generic;
    using System.Linq;
    using static Chess.Program;

    public class Player : IPlayer
    {
        Dictionary<IFigure, int> takenFigures;

        public Player(string name, Color color)
        {
            new Dictionary<IFigure, int>();
            this.Name = name;
            this.Color = color;
            this.isCastlingAvailable = true;
        }

        public string Name { get; }

        public Color Color { get; }

        public bool isChess { get; set; }

        public bool isMoveAvailable { get; set; }

        public bool isCastlingAvailable { get; set; }

        public int TakenFigures(IFigure figure)
        {
            if (!takenFigures.ContainsKey(figure))
            {
                this.takenFigures[figure] = 0;
            }

            takenFigures[figure]++;

            return takenFigures.Where(x => x.Key.Name == figure.Name).Select(x => x.Value).Count();
        }
    }
}
