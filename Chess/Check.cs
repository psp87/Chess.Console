namespace Chess
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public static class Check
    {
        public static int KingRow { get; set; }

        public static int KingCol { get; set; }

        public static int AttackingRow { get; set; }

        public static int AttackingCol { get; set; }
    }
}
