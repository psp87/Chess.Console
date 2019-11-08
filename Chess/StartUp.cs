namespace Chess
{
    using System;
    using Models.Board;
    using Models.Enums;
    using Models.Player.Contracts;

    public class StartUp
    {
        public static void Main()
        {
            Board board = new Board();

            Print.Header();
            Print.Menu();

            var option = Console.ReadKey().Key;

            while (true)
            {
                switch (option)
                {
                    case ConsoleKey.N:
                        {
                            Print.MenuPlayers(1);
                            string playerOneName = Console.ReadLine();
                            Print.MenuPlayers(2);
                            string playerTwoName = Console.ReadLine();

                            IPlayer player1 = Factory.GetPlayer(playerOneName, Color.Light);
                            IPlayer player2 = Factory.GetPlayer(playerTwoName, Color.Dark);

                            Print.MenuGame();
                            Print.ExampleText();

                            board.NewGame();

                            while (true)
                            {
                                for (int turn = 1; turn <= 2; turn++)
                                {
                                    if (turn % 2 == 1)
                                    {
                                        Print.GameStats(player1, player2);
                                        Print.Turn(player1);
                                        board.MoveFigure(player1);
                                        Print.LineMinMax(-13, -6, 10, ' ');
                                    }
                                    else
                                    {
                                        Print.GameStats(player1, player2);
                                        Print.Turn(player2);
                                        board.MoveFigure(player2);
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
