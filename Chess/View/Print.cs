namespace Chess.View
{
    using System;
    using Chess.Models;
    using Models.Enums;
    using Models.Pieces;

    public class Print
    {
        public Print()
        {
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

        public void Header()
        {
            Paint.DefaultBackground();
            Paint.WhiteText();

            string header = "CONSOLE CHESS BY PLAMEN PETROV";
            SetCursorMinMin((76 - header.Length) / 2, -3);
            Console.Write(header);
        }

        public void Menu()
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

        public void Stats(Player playerOne, Player playerTwo)
        {
            Paint.DefaultBackground();
            Paint.WhiteText();

            int lightHorizontalOffset = -12;
            int lightVerticalOffset = -24;
            int darkHorizontalOffset = 7;
            int darkVerticalOffset = 5;

            int playerOneNameCenterPosition = -(19 + playerOne.Name.Length) / 2;
            this.LineMinMax(playerOneNameCenterPosition - 1, lightVerticalOffset, playerOne.Name.Length + 2, '-');
            SetCursorMinMax(playerOneNameCenterPosition, lightVerticalOffset + 1);
            Console.WriteLine($"{playerOne.Name}");
            this.LineMinMax(playerOneNameCenterPosition - 1, lightVerticalOffset + 2, playerOne.Name.Length + 2, '-');
            SetCursorMinMax(lightHorizontalOffset, lightVerticalOffset + 4);
            Console.WriteLine("WHITE");
            SetCursorMinMax(lightHorizontalOffset, lightVerticalOffset + 7);
            Console.Write($"P x {playerOne.TakenFigures(nameof(Pawn))}");
            SetCursorMinMax(lightHorizontalOffset, lightVerticalOffset + 9);
            Console.Write($"N x {playerOne.TakenFigures(nameof(Knight))}");
            SetCursorMinMax(lightHorizontalOffset, lightVerticalOffset + 11);
            Console.Write($"B x {playerOne.TakenFigures(nameof(Bishop))}");
            SetCursorMinMax(lightHorizontalOffset, lightVerticalOffset + 13);
            Console.Write($"R x {playerOne.TakenFigures(nameof(Rook))}");
            SetCursorMinMax(lightHorizontalOffset, lightVerticalOffset + 15);
            Console.Write($"Q x {playerOne.TakenFigures(nameof(Queen))}");

            int playerTwoNameCenterPosition = (19 - playerTwo.Name.Length) / 2;
            this.LineMaxMin(playerTwoNameCenterPosition - 1, darkVerticalOffset, playerTwo.Name.Length + 2, '-');
            SetCursorMaxMin(playerTwoNameCenterPosition, darkVerticalOffset + 1);
            Console.WriteLine($"{playerTwo.Name}");
            this.LineMaxMin(playerTwoNameCenterPosition - 1, darkVerticalOffset + 2, playerTwo.Name.Length + 2, '-');
            SetCursorMaxMin(darkHorizontalOffset, darkVerticalOffset + 4);
            Console.WriteLine("BLACK");
            SetCursorMaxMin(darkHorizontalOffset, darkVerticalOffset + 7);
            Console.Write($"P x {playerTwo.TakenFigures(nameof(Pawn))}");
            SetCursorMaxMin(darkHorizontalOffset, darkVerticalOffset + 9);
            Console.Write($"N x {playerTwo.TakenFigures(nameof(Knight))}");
            SetCursorMaxMin(darkHorizontalOffset, darkVerticalOffset + 11);
            Console.Write($"B x {playerTwo.TakenFigures(nameof(Bishop))}");
            SetCursorMaxMin(darkHorizontalOffset, darkVerticalOffset + 13);
            Console.Write($"R x {playerTwo.TakenFigures(nameof(Rook))}");
            SetCursorMaxMin(darkHorizontalOffset, darkVerticalOffset + 15);
            Console.Write($"Q x {playerTwo.TakenFigures(nameof(Queen))}");
        }

        public void FinalMessage(Player player, GameOver gameOver)
        {
            if (gameOver == GameOver.Checkmate)
            {
                Paint.DefaultBackground();
                Paint.YellowText();

                this.EmptyFinalScreen();
                if (player.Color == Color.Light)
                {
                    SetCursorMinMax(-14, -27);
                }
                else
                {
                    SetCursorMaxMin(5, 2);
                }

                Console.Write($"{gameOver.ToString().ToUpper()}!");
                SetCursorMinMax(28, 2);
                Console.Write($"{player.Name.ToUpper()} WON THE GAME!");
            }
            else if (gameOver == GameOver.Stalemate)
            {
                Paint.DefaultBackground();
                Paint.YellowText();

                this.EmptyFinalScreen();
                SetCursorMinMax(28, 2);
                Console.Write("THE GAME IS STALEMATE!");
            }
            else if (gameOver == GameOver.Repetition)
            {
                Paint.DefaultBackground();
                Paint.YellowText();

                this.EmptyFinalScreen();
                SetCursorMinMax(22, 2);
                Console.Write("THE GAME IS DRAW BY REPETITION!");
            }
        }

        public void Turn(Player player)
        {
            Paint.DefaultBackground();
            Paint.YellowText();

            if (player.Color == Color.Light)
            {
                int playerOneCenterNamePosition = -(19 + player.Name.Length) / 2;

                SetCursorMinMax(playerOneCenterNamePosition, -23);
                Console.Write($"{player.Name}");
                SetCursorMinMax(-16, -6);
                Console.Write("MOVE:           ");
                SetCursorMinMax(-11, -6);
            }
            else
            {
                int playerTwoCenterNamePosition = (19 - player.Name.Length) / 2;

                SetCursorMaxMin(playerTwoCenterNamePosition, 6);
                Console.Write($"{player.Name}");
                SetCursorMaxMin(3, 23);
                Console.Write("MOVE:           ");
                SetCursorMaxMin(8, 23);
            }
        }

        public void Check(Player player)
        {
            Paint.DefaultBackground();
            Paint.YellowText();

            switch (player.Color)
            {
                case Color.Light:
                    SetCursorMinMax(-12, -27);
                    break;
                case Color.Dark:
                    SetCursorMaxMin(7, 2);
                    break;
            }

            Console.Write("CHECK!");
        }

        public void KingIsCheck(Player player)
        {
            Paint.DefaultBackground();
            Paint.YellowText();

            if (player.Color == Color.Light)
            {
                SetCursorMinMax(-16, -4);
                Console.Write("KING IS CHECK!");
                this.LineMinMax(-11, -6, 5, ' ');
                SetCursorMinMax(-11, -6);
            }
            else
            {
                SetCursorMaxMin(3, 25);
                Console.Write("KING IS CHECK!");
                this.LineMaxMin(8, 23, 5, ' ');
                SetCursorMaxMin(8, 23);
            }
        }

        public void ExampleText()
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

        public void GameMenu()
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

        public void Invalid(Player player)
        {
            Paint.DefaultBackground();
            Paint.YellowText();

            if (player.Color == Color.Light)
            {
                this.LineMinMax(-16, -4, 15, ' ');
                SetCursorMinMax(-16, -4);
                Console.Write("INVALID!");
                this.LineMinMax(-11, -6, 10, ' ');
                SetCursorMinMax(-11, -6);
            }
            else
            {
                this.LineMaxMin(3, 25, 15, ' ');
                SetCursorMaxMin(3, 25);
                Console.Write("INVALID!");
                this.LineMaxMin(8, 23, 10, ' ');
                SetCursorMaxMin(8, 23);
            }
        }

        public void Exception(Player player)
        {
            Paint.DefaultBackground();
            Paint.YellowText();

            if (player.Color == Color.Light)
            {
                this.LineMinMax(-16, -4, 15, ' ');
                SetCursorMinMax(-16, -4);
                Console.Write("EXCEPTION!");
                this.LineMinMax(-11, -6, 10, ' ');
                SetCursorMinMax(-11, -6);
            }
            else
            {
                this.LineMaxMin(3, 25, 15, ' ');
                SetCursorMaxMin(3, 25);
                Console.Write("EXCEPTION!");
                this.LineMaxMin(8, 23, 10, ' ');
                SetCursorMaxMin(8, 23);
            }
        }

        public void PlayersMenu(Color color)
        {
            int number = color == Color.Light ? 1 : 2;

            Paint.DefaultBackground();
            Paint.WhiteText();

            this.LineMinMax(Globals.HorizontalMinWithBorder, 2, 50, ' ');
            SetCursorMinMax(Globals.HorizontalMinWithBorder, 2);
            Console.Write($"PLAYER {number} NAME: ");
        }

        public void EmptyMessageScreen(Player player)
        {
            Paint.DefaultBackground();

            if (player.Color == Color.Light)
            {
                this.LineMinMax(-19, -2, 19, ' ');
                this.LineMinMax(-19, -3, 19, ' ');
                this.LineMinMax(-19, -4, 19, ' ');
                this.LineMinMax(-19, -5, 19, ' ');
                this.LineMinMax(-19, -6, 19, ' ');
            }
            else
            {
                this.LineMaxMin(1, 22, 19, ' ');
                this.LineMaxMin(1, 23, 19, ' ');
                this.LineMaxMin(1, 24, 19, ' ');
                this.LineMaxMin(1, 25, 19, ' ');
                this.LineMaxMin(1, 26, 19, ' ');
            }
        }

        public void EmptyCheckScreen(Player player)
        {
            Paint.DefaultBackground();

            switch (player.Color)
            {
                case Color.Light:
                    this.LineMinMax(-12, -27, 6, ' ');

                    break;
                case Color.Dark:
                    this.LineMaxMin(7, 2, 6, ' ');
                    break;
            }
        }

        public void EmptyFinalScreen()
        {
            Paint.DefaultBackground();

            this.LineMinMax(2, 1, 72, ' ');
            this.LineMinMax(2, 2, 72, ' ');
            this.LineMinMax(2, 3, 72, ' ');
            this.LineMinMax(2, 4, 72, ' ');
            this.LineMinMax(2, 5, 72, ' ');
        }

        public void LineMinMax(int x, int y, int count, char symbol)
        {
            SetCursorMinMax(x, y);
            Console.Write(new string(symbol, count));
        }

        public void LineMaxMin(int x, int y, int count, char symbol)
        {
            SetCursorMaxMin(x, y);
            Console.Write(new string(symbol, count));
        }

        public void LineMinMin(int x, int y, int count, char symbol)
        {
            SetCursorMinMin(x, y);
            Console.Write(new string(symbol, count));
        }

        public void LineMaxMax(int x, int y, int count, char symbol)
        {
            SetCursorMaxMax(x, y);
            Console.Write(new string(symbol, count));
        }

        public void ErrorWindow()
        {
            Console.WriteLine("\nAPP WORKS WITH THE FOLLOWING WINDOW SETTINGS:\n");
            Console.WriteLine("Font: Raster Fonts");
            Console.WriteLine("Size: 8x8");
            Console.WriteLine("Window Size Width: 114");
            Console.WriteLine("Window Size Height: 87");
        }
    }
}
