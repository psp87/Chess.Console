namespace Chess
{
    using System;
    using Models.Figures;
    using Models.Player.Contracts;
    using Models.Enums;

    public static class Print
    {
        public static void Header()
        {
            string header = "JUST CHESS BY PLAMEN PETROV";
            SetCursorMinMin((76 - header.Length)/2 + 1, -3);
            Console.Write(header);
        }

        public static void Menu()
        {
            SetCursorMinMax(21, 2);
            Console.Write("(N)EW GAME");

            SetCursorMinMax(35, 2);
            Console.Write("(L)OAD GAME");

            SetCursorMinMax(50, 2);
            Console.Write("(E)XIT");
        }

        public static void GameStats(IPlayer player1, IPlayer player2)
        {
            Console.ForegroundColor = ConsoleColor.White;

            int playerOneNameCenterPosition = -(15 + player1.Name.Length) / 2;
            Print.LineMinMax(playerOneNameCenterPosition - 1, -24, player1.Name.Length + 2, '-');
            SetCursorMinMax(playerOneNameCenterPosition, -23);
            Console.WriteLine($"{player1.Name}");
            Print.LineMinMax(playerOneNameCenterPosition - 1, -22, player1.Name.Length + 2, '-');
            SetCursorMinMax(-10, -20);
            Console.WriteLine("WHITE");
            SetCursorMinMax(-10, -17);
            Console.Write($"P x {player1.TakenFigures(nameof(Pawn))}");
            SetCursorMinMax(-10, -15);
            Console.Write($"N x {player1.TakenFigures(nameof(Knight))}");
            SetCursorMinMax(-10, -13);
            Console.Write($"B x {player1.TakenFigures(nameof(Bishop))}");
            SetCursorMinMax(-10, -11);
            Console.Write($"R x {player1.TakenFigures(nameof(Rook))}");
            SetCursorMinMax(-10, -9);
            Console.Write($"Q x {player1.TakenFigures(nameof(Queen))}");


            int playerTwoNameCenterPosition = (15 - player2.Name.Length) / 2;
            Print.LineMaxMin(playerTwoNameCenterPosition - 1, 5, player2.Name.Length + 2, '-');
            SetCursorMaxMin(playerTwoNameCenterPosition, 6);
            Console.WriteLine($"{player2.Name}");
            Print.LineMaxMin(playerTwoNameCenterPosition - 1, 7, player2.Name.Length + 2, '-');
            SetCursorMaxMin(5, 9);
            Console.WriteLine("BLACK");
            SetCursorMaxMin(5, 12);
            Console.Write($"P x {player2.TakenFigures(nameof(Pawn))}");
            SetCursorMaxMin(5, 14);
            Console.Write($"N x {player2.TakenFigures(nameof(Knight))}");
            SetCursorMaxMin(5, 16);
            Console.Write($"B x {player2.TakenFigures(nameof(Bishop))}");
            SetCursorMaxMin(5, 18);
            Console.Write($"R x {player2.TakenFigures(nameof(Rook))}");
            SetCursorMaxMin(5, 20);
            Console.Write($"Q x {player2.TakenFigures(nameof(Queen))}");
        }

        public static void Turn(IPlayer player)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            if (player.Color == Color.Light)
            {
                int playerOneCenterNamePosition = -(15 + player.Name.Length) / 2;

                SetCursorMinMax(playerOneCenterNamePosition, -23);
                Console.Write($"{player.Name}");

                SetCursorMinMax(-13, -6);
                Console.Write("MOVE:      ");
                SetCursorMinMax(-8, -6);
            }
            else
            {
                int playerTwoCenterNamePosition = (15 - player.Name.Length) / 2;

                SetCursorMaxMin(playerTwoCenterNamePosition, 6);
                Console.Write($"{player.Name}");

                SetCursorMaxMin(3, 23);
                Console.Write("MOVE:      ");
                SetCursorMaxMin(8, 23);
            }
        }

        public static void ExampleText()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            SetCursorMinMin(-12, 7);
            Console.Write("EXAMPLE:");
            SetCursorMinMin(-12, 9);
            Console.Write("PE2E4");
            SetCursorMinMin(-12, 11);
            Console.Write("P - PAWN");
            SetCursorMinMin(-12, 12);
            Console.Write("E2 - FROM");
            SetCursorMinMin(-12, 13);
            Console.Write("E4 - TO");

            SetCursorMinMin(-12, 16);
            Console.Write("LEGEND");
            SetCursorMinMin(-12, 17);
            Console.Write("P - PAWN");
            SetCursorMinMin(-12, 18);
            Console.Write("N - KNIGHT");
            SetCursorMinMin(-12, 19);
            Console.Write("B - BISHOP");
            SetCursorMinMin(-12, 20);
            Console.Write("R - ROOK");
            SetCursorMinMin(-12, 21);
            Console.Write("Q - QUEEN");
        }

        public static void MenuGame()
        {
            SetCursorMinMax(Globals.HorizontalMinWithBorder, 2);
            Console.Write(new string(' ', Globals.HorizontalMaxWithBorder));

            SetCursorMinMax(27, 2);
            Console.Write("(S)AVE GAME");

            SetCursorMinMax(42, 2);
            Console.Write("(E)XIT");
        }

        public static void MenuPlayers(int playerNumber)
        {
            LineMinMax(Globals.HorizontalMinWithBorder, 2, 50, ' ');

            SetCursorMinMax(Globals.HorizontalMinWithBorder, 2);
            Console.Write($"PLAYER {playerNumber} NAME: ");
        }

        public static void LineMinMax(int x, int y, int count, char symbol)
        {
            SetCursorMinMax(x, y);
            Console.Write(new string(symbol, count));
        }

        public static void LineMaxMin(int x, int y, int count, char symbol)
        {
            SetCursorMaxMin(x, y);
            Console.Write(new string(symbol, count));
        }

        public static void LineMinMin(int x, int y, int count, char symbol)
        {
            SetCursorMinMin(x, y);
            Console.Write(new string(symbol, count));
        }

        public static void LineMaxMax(int x, int y, int count, char symbol)
        {
            SetCursorMaxMax(x, y);
            Console.Write(new string(symbol, count));
        }

        private static void SetCursorMinMin(int x, int y)
        {
            Console.SetCursorPosition(Globals.HorizontalMinWithBorder + x, Globals.VerticalMinWithBorder + y);
        }

        private static void SetCursorMinMax(int x, int y)
        {
            Console.SetCursorPosition(Globals.HorizontalMinWithBorder + x, Globals.VerticalMaxWithBorder + y);
        }

        private static void SetCursorMaxMin(int x, int y)
        {
            Console.SetCursorPosition(Globals.HorizontalMaxWithBorder + x, Globals.VerticalMinWithBorder + y);
        }

        private static void SetCursorMaxMax(int x, int y)
        {
            Console.SetCursorPosition(Globals.HorizontalMaxWithBorder + x, Globals.VerticalMaxWithBorder + y);
        }
    }
}
