namespace Chess
{
    using Chess.Models.Player.Contracts;
    using System;
    using static Chess.Program;

    public static class Print
    {
        public static void Header()
        {
            SetCursorMinMin(25, -3);
            Console.Write("JUST CHESS BY PLAMEN PETROV");
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

        public static void GameStats()
        {
            Console.ForegroundColor = ConsoleColor.White;

            SetCursorMinMax(-13, -24);
            Console.WriteLine(new string('-', 10));
            SetCursorMinMax(-12, -23);
            Console.WriteLine("PLAYER 1");
            SetCursorMinMax(-13, -22);
            Console.WriteLine(new string('-', 10));
            SetCursorMinMax(-11, -20);
            Console.WriteLine("WHITE");
            SetCursorMinMax(-11, -17);
            Console.Write("P x 0");
            SetCursorMinMax(-11, -15);
            Console.Write("N x 0");
            SetCursorMinMax(-11, -13);
            Console.Write("B x 0");
            SetCursorMinMax(-11, -11);
            Console.Write("R x 0");
            SetCursorMinMax(-11, -9);
            Console.Write("Q x 0");

            SetCursorMaxMin(3, 5);
            Console.WriteLine(new string('-', 10));
            SetCursorMaxMin(4, 6);
            Console.WriteLine("PLAYER 2");
            SetCursorMaxMin(3, 7);
            Console.WriteLine(new string('-', 10));
            SetCursorMaxMin(5, 9);
            Console.WriteLine("BLACK");
            SetCursorMaxMin(5, 12);
            Console.Write("P x 0");
            SetCursorMaxMin(5, 14);
            Console.Write("N x 0");
            SetCursorMaxMin(5, 16);
            Console.Write("B x 0");
            SetCursorMaxMin(5, 18);
            Console.Write("R x 0");
            SetCursorMaxMin(5, 20);
            Console.Write("Q x 0");
        }

        internal static void Values()
        {
            char symbol = char.Parse(Console.ReadLine());
            int fromX = int.Parse(Console.ReadLine());
            int fromY = int.Parse(Console.ReadLine());
            int toX = int.Parse(Console.ReadLine());
            int toY = int.Parse(Console.ReadLine());
        }

        public static void Turn(IPlayer player)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            if (player.Color == Color.Light)
            {
                SetCursorMinMax(-13, -23);
                Console.Write(">PLAYER 1<");

                SetCursorMinMax(-13, -6);
                Console.Write("MOVE:");
                SetCursorMinMax(-8, -6);
            }
            else
            {
                SetCursorMaxMin(3, 23);
                Console.Write("MOVE:");
                SetCursorMaxMin(8, 23);
            }
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

        public static void BlankLine(int x, int y)
        {
            SetCursorMinMax(x, y);
            Console.Write(new string(' ', 10));
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
