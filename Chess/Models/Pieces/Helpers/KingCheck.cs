namespace Chess.Models.Pieces.Helpers
{
    public static class KingCheck
    {
        public static int KingRow { get; set; }

        public static int KingCol { get; set; }

        public static int AttackingRow { get; set; }

        public static int AttackingCol { get; set; }
    }
}
