namespace Chess
{
    using Models.Enums;

    public class Globals
    {
        public const int BoardRows = 8;
        public const int BoardCols = 8;

        public const int CellRows = 9;
        public const int CellCols = 9;

        public const int OffsetHorizontal = 21;
        public const int OffsetVertical = 6;

        public const int HorizontalMin = OffsetHorizontal;
        public const int HorizontalMax = OffsetHorizontal + (BoardCols * CellCols);

        public const int VerticalMin = OffsetVertical;
        public const int VerticalMax = OffsetVertical + (BoardRows * CellRows);

        public const int HorizontalMinWithBorder = HorizontalMin - 2;
        public const int HorizontalMaxWithBorder = HorizontalMax + 2;

        public const int VerticalMinWithBorder = VerticalMin - 2;
        public const int VerticalMaxWithBorder = VerticalMax + 2;

        public static GameOver GameState { get; set; }

        public static int TurnCounter { get; set; }
    }
}
