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
            Paint.DefaultBackground();
            Paint.WhiteText();

            string header = "JUST CHESS BY PLAMEN PETROV";
            SetCursorMinMin((76 - header.Length) / 2 + 1, -3);
            Console.Write(header);
        }

        public static void Menu()
        {
            Paint.DefaultBackground();
            Paint.WhiteText();

            SetCursorMinMax(21, 2);
            Console.Write("(N)EW GAME");
            SetCursorMinMax(35, 2);
            Console.Write("(L)OAD GAME");
            SetCursorMinMax(50, 2);
            Console.Write("(E)XIT");
        }

        public static void Stats(IPlayer player1, IPlayer player2)
        {
            Paint.DefaultBackground();
            Paint.WhiteText();

            int lightHorizontalOffset = -12;
            int lightVerticalOffset = -24;
            int darkHorizontalOffset = 7;
            int darkVerticalOffset = 5;

            int playerOneNameCenterPosition = -(19 + player1.Name.Length) / 2;
            Print.LineMinMax(playerOneNameCenterPosition - 1, lightVerticalOffset, player1.Name.Length + 2, '-');
            SetCursorMinMax(playerOneNameCenterPosition, lightVerticalOffset + 1);
            Console.WriteLine($"{player1.Name}");
            Print.LineMinMax(playerOneNameCenterPosition - 1, lightVerticalOffset + 2, player1.Name.Length + 2, '-');
            SetCursorMinMax(lightHorizontalOffset, lightVerticalOffset + 4);
            Console.WriteLine("WHITE");
            SetCursorMinMax(lightHorizontalOffset, lightVerticalOffset + 7);
            Console.Write($"P x {player1.TakenFigures(nameof(Pawn))}");
            SetCursorMinMax(lightHorizontalOffset, lightVerticalOffset + 9);
            Console.Write($"N x {player1.TakenFigures(nameof(Knight))}");
            SetCursorMinMax(lightHorizontalOffset, lightVerticalOffset + 11);
            Console.Write($"B x {player1.TakenFigures(nameof(Bishop))}");
            SetCursorMinMax(lightHorizontalOffset, lightVerticalOffset + 13);
            Console.Write($"R x {player1.TakenFigures(nameof(Rook))}");
            SetCursorMinMax(lightHorizontalOffset, lightVerticalOffset + 15);
            Console.Write($"Q x {player1.TakenFigures(nameof(Queen))}");

            int playerTwoNameCenterPosition = (19 - player2.Name.Length) / 2;
            Print.LineMaxMin(playerTwoNameCenterPosition - 1, darkVerticalOffset, player2.Name.Length + 2, '-');
            SetCursorMaxMin(playerTwoNameCenterPosition, darkVerticalOffset + 1);
            Console.WriteLine($"{player2.Name}");
            Print.LineMaxMin(playerTwoNameCenterPosition - 1, darkVerticalOffset + 2, player2.Name.Length + 2, '-');
            SetCursorMaxMin(darkHorizontalOffset, darkVerticalOffset + 4);
            Console.WriteLine("BLACK");
            SetCursorMaxMin(darkHorizontalOffset, darkVerticalOffset + 7);
            Console.Write($"P x {player2.TakenFigures(nameof(Pawn))}");
            SetCursorMaxMin(darkHorizontalOffset, darkVerticalOffset + 9);
            Console.Write($"N x {player2.TakenFigures(nameof(Knight))}");
            SetCursorMaxMin(darkHorizontalOffset, darkVerticalOffset + 11);
            Console.Write($"B x {player2.TakenFigures(nameof(Bishop))}");
            SetCursorMaxMin(darkHorizontalOffset, darkVerticalOffset + 13);
            Console.Write($"R x {player2.TakenFigures(nameof(Rook))}");
            SetCursorMaxMin(darkHorizontalOffset, darkVerticalOffset + 15);
            Console.Write($"Q x {player2.TakenFigures(nameof(Queen))}");
        }

        public static void Turn(IPlayer player)
        {
            Paint.DefaultBackground();
            Paint.YellowText();

            if (player.Color == Color.Light)
            {
                int playerOneCenterNamePosition = -(19 + player.Name.Length) / 2;

                SetCursorMinMax(playerOneCenterNamePosition, -23);
                Console.Write($"{player.Name}");
                SetCursorMinMax(-16, -6);
                Console.Write("MOVE:      ");
                SetCursorMinMax(-11, -6);
            }
            else
            {
                int playerTwoCenterNamePosition = (19 - player.Name.Length) / 2;

                SetCursorMaxMin(playerTwoCenterNamePosition, 6);
                Console.Write($"{player.Name}");
                SetCursorMaxMin(3, 23);
                Console.Write("MOVE:      ");
                SetCursorMaxMin(8, 23);
            }
        }

        public static void Check(IPlayer player)
        {
            Paint.DefaultBackground();
            Paint.YellowText();

            switch (player.Color)
            {
                case Color.Light: SetCursorMinMax(-12, -27);
                    break;
                case Color.Dark: SetCursorMaxMin(7, 2);
                    break;
            }

            Console.Write("CHECK!");
        }

        public static void Won(IPlayer player)
        {
            Paint.DefaultBackground();
            Paint.YellowText();

            EmptyFinalScreen();
            if (player.Color == Color.Light)
            {
                SetCursorMinMax(-14, -27);
            }
            else
            {
                SetCursorMaxMin(5, 2);
            }
            Console.Write("CHECKMATE!");
            SetCursorMinMax(28, 2);
            Console.Write($"{player.Name.ToUpper()} WON THE GAME!");
        }

        public static void Stalemate()
        {
            Paint.DefaultBackground();
            Paint.YellowText();

            EmptyFinalScreen();
            SetCursorMinMax(28, 2);
            Console.Write("THE GAME IS STALEMATE!");
        }

        public static void KingIsCheck(IPlayer player)
        {
            Paint.DefaultBackground();
            Paint.YellowText();

            if (player.Color == Color.Light)
            {
                SetCursorMinMax(-16, -4);
                Console.Write("KING IS CHECK!");
                LineMinMax(-11, -6, 5, ' ');
                SetCursorMinMax(-11, -6);
            }
            else
            {
                SetCursorMaxMin(3, 25);
                Console.Write("KING IS CHECK!");
                LineMaxMin(8, 23, 5, ' ');
                SetCursorMaxMin(8, 23);
            }
        }

        public static void ExampleText()
        {
            Paint.DefaultBackground();
            Paint.GrayText();

            int horizontalOffset = -14;
            int verticalOffset = 7;

            SetCursorMinMin(horizontalOffset, verticalOffset);
            Console.Write("EXAMPLE:");
            SetCursorMinMin(horizontalOffset, verticalOffset + 2);
            Console.Write("PE2E4");
            SetCursorMinMin(horizontalOffset, verticalOffset + 4);
            Console.Write("P - PAWN");
            SetCursorMinMin(horizontalOffset, verticalOffset + 5);
            Console.Write("E2 - FROM");
            SetCursorMinMin(horizontalOffset, verticalOffset + 6);
            Console.Write("E4 - TO");

            SetCursorMinMin(horizontalOffset, verticalOffset + 9);
            Console.Write("LEGEND");
            SetCursorMinMin(horizontalOffset, verticalOffset + 10);
            Console.Write("P - PAWN");
            SetCursorMinMin(horizontalOffset, verticalOffset + 11);
            Console.Write("N - KNIGHT");
            SetCursorMinMin(horizontalOffset, verticalOffset + 12);
            Console.Write("B - BISHOP");
            SetCursorMinMin(horizontalOffset, verticalOffset + 13);
            Console.Write("R - ROOK");
            SetCursorMinMin(horizontalOffset, verticalOffset + 14);
            Console.Write("Q - QUEEN");
        }

        public static void GameMenu()
        {
            Paint.DefaultBackground();
            Paint.WhiteText();

            SetCursorMinMax(Globals.HorizontalMinWithBorder, 2);
            Console.Write(new string(' ', Globals.HorizontalMaxWithBorder));

            SetCursorMinMax(27, 2);
            Console.Write("(S)AVE GAME");
            SetCursorMinMax(42, 2);
            Console.Write("(E)XIT");
        }

        public static void Invalid(IPlayer player)
        {
            Paint.DefaultBackground();
            Paint.YellowText();

            if (player.Color == Color.Light)
            {
                LineMinMax(-16, -4, 15, ' ');
                SetCursorMinMax(-16, -4);
                Console.Write("INVALID!");
                LineMinMax(-11, -6, 5, ' ');
                SetCursorMinMax(-11, -6);
            }
            else
            {
                LineMaxMin(3, 25, 15, ' ');
                SetCursorMaxMin(3, 25);
                Console.Write("INVALID!");
                LineMaxMin(8, 23, 5, ' ');
                SetCursorMaxMin(8, 23);
            }
        }

        public static void PlayersMenu(int number)
        {
            Paint.DefaultBackground();
            Paint.WhiteText();

            LineMinMax(Globals.HorizontalMinWithBorder, 2, 50, ' ');
            SetCursorMinMax(Globals.HorizontalMinWithBorder, 2);
            Console.Write($"PLAYER {number} NAME: ");
        }

        public static void EmptyMessageScreen(IPlayer player)
        {
            Paint.DefaultBackground();

            if (player.Color == Color.Light)
            {
                LineMinMax(-19, -2, 19, ' ');
                LineMinMax(-19, -3, 19, ' ');
                LineMinMax(-19, -4, 19, ' ');
                LineMinMax(-19, -5, 19, ' ');
                LineMinMax(-19, -6, 19, ' ');
            }
            else
            {
                LineMaxMin(1, 22, 19, ' ');
                LineMaxMin(1, 23, 19, ' ');
                LineMaxMin(1, 24, 19, ' ');
                LineMaxMin(1, 25, 19, ' ');
                LineMaxMin(1, 26, 19, ' ');
            }
        }

        public static void EmptyCheckScreen(IPlayer player)
        {
            Paint.DefaultBackground();

            switch (player.Color)
            {
                case Color.Light:
                    LineMinMax(-12, -27, 6, ' ');

                    break;
                case Color.Dark:
                    LineMaxMin(7, 2, 6, ' ');
                    break;
            }
        }

        public static void EmptyFinalScreen()
        {
            Paint.DefaultBackground();

            LineMinMax(2, 1, 72, ' ');
            LineMinMax(2, 2, 72, ' ');
            LineMinMax(2, 3, 72, ' ');
            LineMinMax(2, 4, 72, ' ');
            LineMinMax(2, 5, 72, ' ');
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

        public static void SetCursorMinMin(int x, int y)
        {
            Console.SetCursorPosition(Globals.HorizontalMinWithBorder + x, Globals.VerticalMinWithBorder + y);
        }

        public static void SetCursorMinMax(int x, int y)
        {
            Console.SetCursorPosition(Globals.HorizontalMinWithBorder + x, Globals.VerticalMaxWithBorder + y);
        }

        public static void SetCursorMaxMin(int x, int y)
        {
            Console.SetCursorPosition(Globals.HorizontalMaxWithBorder + x, Globals.VerticalMinWithBorder + y);
        }

        public static void SetCursorMaxMax(int x, int y)
        {
            Console.SetCursorPosition(Globals.HorizontalMaxWithBorder + x, Globals.VerticalMaxWithBorder + y);
        }
    }
}
