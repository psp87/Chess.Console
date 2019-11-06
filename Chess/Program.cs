namespace Chess
{
    using System;
    using Chess.Models.Board.Contracts;
    using Chess.Models.Player.Contracts;

    public class Program
    {
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

                            IPlayer player1 = Factory.GetPlayer("PEPPY", Color.Light);
                            IPlayer player2 = Factory.GetPlayer("PATTY", Color.Dark);

                            while (true)
                            {
                                Print.ExampleText();

                                for (int turn = 1; turn <= 2; turn++)
                                {
                                    if (turn % 2 == 1)
                                    {
                                        Print.GameStats(player1, player2);
                                        Print.Turn(player1);
                                        board.FigureMove(player1);
                                        Print.LineMinMax(-13, -6, 10, ' ');
                                    }
                                    else
                                    {
                                        Print.GameStats(player1, player2);
                                        Print.Turn(player2);
                                        board.FigureMove(player2);
                                        Print.LineMinMax(79, -53, 10, ' ');
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
