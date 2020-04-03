namespace Chess.View
{
    using System;

    using Models;
    using Models.Enums;
    using Models.Pieces;
    using Models.Pieces.Contracts;

    public class Print
    {
        public Print()
        {
        }

        public void Header()
        {
            Paint.DefaultBackground();
            Paint.WhiteText();

            string header = "JUST CHESS BY PLAMEN PETROV";
            this.SetCursorMinMin((76 - header.Length) / 2 + 1, -3);
            Console.Write(header);
        }

        public void Menu()
        {
            Paint.DefaultBackground();
            Paint.WhiteText();

            this.SetCursorMinMax(21, 2);
            Console.Write("(N)EW GAME");
            this.SetCursorMinMax(35, 2);
            Console.Write("(L)OAD GAME");
            this.SetCursorMinMax(50, 2);
            Console.Write("(E)XIT");
        }

        public void Stats(Player player1, Player player2)
        {
            Paint.DefaultBackground();
            Paint.WhiteText();

            int lightHorizontalOffset = -12;
            int lightVerticalOffset = -24;
            int darkHorizontalOffset = 7;
            int darkVerticalOffset = 5;

            int playerOneNameCenterPosition = -(19 + player1.Name.Length) / 2;
            this.LineMinMax(playerOneNameCenterPosition - 1, lightVerticalOffset, player1.Name.Length + 2, '-');
            this.SetCursorMinMax(playerOneNameCenterPosition, lightVerticalOffset + 1);
            Console.WriteLine($"{player1.Name}");
            this.LineMinMax(playerOneNameCenterPosition - 1, lightVerticalOffset + 2, player1.Name.Length + 2, '-');
            this.SetCursorMinMax(lightHorizontalOffset, lightVerticalOffset + 4);
            Console.WriteLine("WHITE");
            this.SetCursorMinMax(lightHorizontalOffset, lightVerticalOffset + 7);
            Console.Write($"P x {player1.TakenFigures(nameof(Pawn))}");
            this.SetCursorMinMax(lightHorizontalOffset, lightVerticalOffset + 9);
            Console.Write($"N x {player1.TakenFigures(nameof(Knight))}");
            this.SetCursorMinMax(lightHorizontalOffset, lightVerticalOffset + 11);
            Console.Write($"B x {player1.TakenFigures(nameof(Bishop))}");
            this.SetCursorMinMax(lightHorizontalOffset, lightVerticalOffset + 13);
            Console.Write($"R x {player1.TakenFigures(nameof(Rook))}");
            this.SetCursorMinMax(lightHorizontalOffset, lightVerticalOffset + 15);
            Console.Write($"Q x {player1.TakenFigures(nameof(Queen))}");

            int playerTwoNameCenterPosition = (19 - player2.Name.Length) / 2;
            this.LineMaxMin(playerTwoNameCenterPosition - 1, darkVerticalOffset, player2.Name.Length + 2, '-');
            this.SetCursorMaxMin(playerTwoNameCenterPosition, darkVerticalOffset + 1);
            Console.WriteLine($"{player2.Name}");
            this.LineMaxMin(playerTwoNameCenterPosition - 1, darkVerticalOffset + 2, player2.Name.Length + 2, '-');
            this.SetCursorMaxMin(darkHorizontalOffset, darkVerticalOffset + 4);
            Console.WriteLine("BLACK");
            this.SetCursorMaxMin(darkHorizontalOffset, darkVerticalOffset + 7);
            Console.Write($"P x {player2.TakenFigures(nameof(Pawn))}");
            this.SetCursorMaxMin(darkHorizontalOffset, darkVerticalOffset + 9);
            Console.Write($"N x {player2.TakenFigures(nameof(Knight))}");
            this.SetCursorMaxMin(darkHorizontalOffset, darkVerticalOffset + 11);
            Console.Write($"B x {player2.TakenFigures(nameof(Bishop))}");
            this.SetCursorMaxMin(darkHorizontalOffset, darkVerticalOffset + 13);
            Console.Write($"R x {player2.TakenFigures(nameof(Rook))}");
            this.SetCursorMaxMin(darkHorizontalOffset, darkVerticalOffset + 15);
            Console.Write($"Q x {player2.TakenFigures(nameof(Queen))}");
        }

        public void Turn(Player player)
        {
            Paint.DefaultBackground();
            Paint.YellowText();

            if (player.Color == Color.Light)
            {
                int playerOneCenterNamePosition = -(19 + player.Name.Length) / 2;

                this.SetCursorMinMax(playerOneCenterNamePosition, -23);
                Console.Write($"{player.Name}");
                this.SetCursorMinMax(-16, -6);
                Console.Write("MOVE:           ");
                this.SetCursorMinMax(-11, -6);
            }
            else
            {
                int playerTwoCenterNamePosition = (19 - player.Name.Length) / 2;

                this.SetCursorMaxMin(playerTwoCenterNamePosition, 6);
                Console.Write($"{player.Name}");
                this.SetCursorMaxMin(3, 23);
                Console.Write("MOVE:           ");
                this.SetCursorMaxMin(8, 23);
            }
        }

        public void Check(Player player)
        {
            Paint.DefaultBackground();
            Paint.YellowText();

            switch (player.Color)
            {
                case Color.Light: this.SetCursorMinMax(-12, -27);
                    break;
                case Color.Dark: this.SetCursorMaxMin(7, 2);
                    break;
            }

            Console.Write("CHECK!");
        }

        public IPiece PawnPromotion(Position toPos, IPiece piece)
        {
            Paint.DefaultBackground();

            if (piece.Color == Color.Light)
            {
                this.SetCursorMinMax(-17, -4);
                Console.WriteLine("(Q,R,B,N)");
                this.SetCursorMinMax(-17, -6);
                Console.Write("CHOOSE FIGURE:");
            }
            else
            {
                this.SetCursorMinMax(78, -51);
                Console.WriteLine("(Q,R,B,N)");
                this.SetCursorMinMax(78, -53);
                Console.Write("CHOOSE FIGURE:");
            }

            var figureChoose = Console.ReadKey().Key;

            switch (figureChoose)
            {
                case ConsoleKey.Q:
                    {
                        Draw.EmptySquare(toPos.Y, toPos.X);
                        IPiece queen = Factory.GetQueen(piece.Color);
                        Draw.Figure(toPos.Y, toPos.X, queen);
                        return queen;
                    }

                case ConsoleKey.R:
                    {
                        Draw.EmptySquare(toPos.Y, toPos.X);
                        IPiece rook = Factory.GetRook(piece.Color);
                        Draw.Figure(toPos.Y, toPos.X, rook);
                        return rook;
                    }

                case ConsoleKey.B:
                    {
                        Draw.EmptySquare(toPos.Y, toPos.X);
                        IPiece bishop = Factory.GetBishop(piece.Color);
                        Draw.Figure(toPos.Y, toPos.X, bishop);
                        return bishop;
                    }

                case ConsoleKey.N:
                    {
                        Draw.EmptySquare(toPos.Y, toPos.X);
                        IPiece knight = Factory.GetKnight(piece.Color);
                        Draw.Figure(toPos.Y, toPos.X, knight);
                        return knight;
                    }

                default: return this.PawnPromotion(toPos, piece);
            }
        }

        public void Won(Player player, string message)
        {
            Paint.DefaultBackground();
            Paint.YellowText();

            this.EmptyFinalScreen();
            if (player.Color == Color.Light)
            {
                this.SetCursorMinMax(-14, -27);
            }
            else
            {
                this.SetCursorMaxMin(5, 2);
            }

            Console.Write($"{message.ToUpper()}!");
            this.SetCursorMinMax(28, 2);
            Console.Write($"{player.Name.ToUpper()} WON THE GAME!");
        }

        public void Stalemate()
        {
            Paint.DefaultBackground();
            Paint.YellowText();

            this.EmptyFinalScreen();
            this.SetCursorMinMax(28, 2);
            Console.Write("THE GAME IS STALEMATE!");
        }

        public void KingIsCheck(Player player)
        {
            Paint.DefaultBackground();
            Paint.YellowText();

            if (player.Color == Color.Light)
            {
                this.SetCursorMinMax(-16, -4);
                Console.Write("KING IS CHECK!");
                this.LineMinMax(-11, -6, 5, ' ');
                this.SetCursorMinMax(-11, -6);
            }
            else
            {
                this.SetCursorMaxMin(3, 25);
                Console.Write("KING IS CHECK!");
                this.LineMaxMin(8, 23, 5, ' ');
                this.SetCursorMaxMin(8, 23);
            }
        }

        public void ExampleText()
        {
            Paint.DefaultBackground();
            Paint.GrayText();

            int horizontalOffset = -14;
            int verticalOffset = 7;

            this.SetCursorMinMin(horizontalOffset, verticalOffset);
            Console.Write("EXAMPLE:");
            this.SetCursorMinMin(horizontalOffset, verticalOffset + 2);
            Console.Write("PE2E4");
            this.SetCursorMinMin(horizontalOffset, verticalOffset + 4);
            Console.Write("P - PAWN");
            this.SetCursorMinMin(horizontalOffset, verticalOffset + 5);
            Console.Write("E2 - FROM");
            this.SetCursorMinMin(horizontalOffset, verticalOffset + 6);
            Console.Write("E4 - TO");

            this.SetCursorMinMin(horizontalOffset, verticalOffset + 9);
            Console.Write("LEGEND");
            this.SetCursorMinMin(horizontalOffset, verticalOffset + 10);
            Console.Write("P - PAWN");
            this.SetCursorMinMin(horizontalOffset, verticalOffset + 11);
            Console.Write("N - KNIGHT");
            this.SetCursorMinMin(horizontalOffset, verticalOffset + 12);
            Console.Write("B - BISHOP");
            this.SetCursorMinMin(horizontalOffset, verticalOffset + 13);
            Console.Write("R - ROOK");
            this.SetCursorMinMin(horizontalOffset, verticalOffset + 14);
            Console.Write("Q - QUEEN");
        }

        public void GameMenu()
        {
            Paint.DefaultBackground();
            Paint.WhiteText();

            this.SetCursorMinMax(Globals.HorizontalMinWithBorder, 2);
            Console.Write(new string(' ', Globals.HorizontalMaxWithBorder));

            this.SetCursorMinMax(27, 2);
            Console.Write("(S)AVE GAME");
            this.SetCursorMinMax(42, 2);
            Console.Write("(E)XIT");
        }

        public void Invalid(Player player)
        {
            Paint.DefaultBackground();
            Paint.YellowText();

            if (player.Color == Color.Light)
            {
                this.LineMinMax(-16, -4, 15, ' ');
                this.SetCursorMinMax(-16, -4);
                Console.Write("INVALID!");
                this.LineMinMax(-11, -6, 10, ' ');
                this.SetCursorMinMax(-11, -6);
            }
            else
            {
                this.LineMaxMin(3, 25, 15, ' ');
                this.SetCursorMaxMin(3, 25);
                Console.Write("INVALID!");
                this.LineMaxMin(8, 23, 10, ' ');
                this.SetCursorMaxMin(8, 23);
            }
        }

        public void PlayersMenu(Color color)
        {
            int number = color == Color.Light ? 1 : 2;

            Paint.DefaultBackground();
            Paint.WhiteText();

            this.LineMinMax(Globals.HorizontalMinWithBorder, 2, 50, ' ');
            this.SetCursorMinMax(Globals.HorizontalMinWithBorder, 2);
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
            this.SetCursorMinMax(x, y);
            Console.Write(new string(symbol, count));
        }

        public void LineMaxMin(int x, int y, int count, char symbol)
        {
            this.SetCursorMaxMin(x, y);
            Console.Write(new string(symbol, count));
        }

        public void LineMinMin(int x, int y, int count, char symbol)
        {
            this.SetCursorMinMin(x, y);
            Console.Write(new string(symbol, count));
        }

        public void LineMaxMax(int x, int y, int count, char symbol)
        {
            this.SetCursorMaxMax(x, y);
            Console.Write(new string(symbol, count));
        }

        public void SetCursorMinMin(int x, int y)
        {
            Console.SetCursorPosition(Globals.HorizontalMinWithBorder + x, Globals.VerticalMinWithBorder + y);
        }

        public void SetCursorMinMax(int x, int y)
        {
            Console.SetCursorPosition(Globals.HorizontalMinWithBorder + x, Globals.VerticalMaxWithBorder + y);
        }

        public void SetCursorMaxMin(int x, int y)
        {
            Console.SetCursorPosition(Globals.HorizontalMaxWithBorder + x, Globals.VerticalMinWithBorder + y);
        }

        public void SetCursorMaxMax(int x, int y)
        {
            Console.SetCursorPosition(Globals.HorizontalMaxWithBorder + x, Globals.VerticalMaxWithBorder + y);
        }
    }
}
