namespace Chess.Models
{
    public class Move
    {
        public Move()
        {
        }

        public char Symbol { get; set; }

        public Square Start { get; set; }

        public Square End { get; set; }
    }
}
