namespace Chess.Models.Figures.Contracts
{
    using Chess.Models.Player.Contracts;
    using static Chess.Program;

    public interface IFigure
    {
        string Name { get; }

        Color Color { get; }

        char Symbol { get; }

        CoordinateY Row { get; set; }

        CoordinateX Col { get; set; }

        bool IsOccupied { get; set; }

        bool IsFirstMove { get; set; }

        bool IsLastMove { get; set; }

        void Draw(int row, int col);

        bool Move(IFigure[][] squares, CoordinateY toRow, CoordinateX toCol);

        bool Take(IFigure[][] squares, CoordinateY toRow, CoordinateX toCol);
    }
}
