namespace Chess
{
    using System;
    using Chess.Models.Board.Contracts;
    using Chess.Models.Player.Contracts;

    public class Program
    {
        public enum Color { Light, Dark, Empty }

        public static void Main()
        {
            IBoard board = Factory.GetBoard();
            board.Draw();

            Print.Header();
            Print.Menu();

            var option = Console.ReadKey().Key;

            while (true)
            {
                switch (option)
                {
                    case ConsoleKey.N:
                        {
                            Print.MenuGame();
                            board.NewGame();
                            board.Draw();

                            IPlayer player1 = Factory.GetPlayer("Pepi", Color.Light);
                            IPlayer player2 = Factory.GetPlayer("Pati", Color.Dark);

                            while (true)
                            {
                                for (int turn = 1; turn <= 2; turn++)
                                {
                                    if (turn % 2 == 1)
                                    {
                                        Print.GameStats();

                                        board.FigureMove(player1);

                                        Paint.DefaultColor();
                                        Print.BlankLine(-13, -6);
                                    }
                                    else
                                    {
                                        Print.GameStats();

                                        board.FigureMove(player2);

                                        Paint.DefaultColor();
                                        Print.BlankLine(79, -53);
                                    }
                                }
                            }
                        }
                    case ConsoleKey.L:
                        break;
                    case ConsoleKey.S:
                        break;
                    case ConsoleKey.Escape:
                        return;
                }
            }
        }
    }
}
