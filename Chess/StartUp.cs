namespace Chess
{
    using System;
    using System.Collections.Generic;
    using Models.Board;
    using Models.Enums;
    using Models.Player.Contracts;

    public class StartUp
    {
        public static void Main()
        {
            Print.Header();
            Draw.Board();

            while (true)
            {
                Print.Menu();
                var option = Console.ReadKey().Key;

                switch (option)
                {
                    case ConsoleKey.N:
                        {
                            Board board = new Board();

                            List<string> playerNames = new List<string>();
                            for (int i = 1; i <= 2; i++)
                            {
                                Print.MenuPlayers(i);
                                playerNames.Add(Console.ReadLine());
                            }

                            IPlayer player1 = Factory.GetPlayer(playerNames[0].ToUpper(), Color.Light);
                            IPlayer player2 = Factory.GetPlayer(playerNames[1].ToUpper(), Color.Dark);

                            Print.MenuGame();
                            Print.ExampleText();

                            board.NewGame();

                            bool checkmate = false;
                            bool stalemate = false;

                            while (!checkmate && !stalemate)
                            {
                                for (int turn = 1; turn <= 2; turn++)
                                {
                                    if (turn % 2 == 1)
                                    {
                                        if (player1.isCheckmate)
                                        {
                                            Print.BlackWon(player2);
                                            checkmate = true;
                                            break;
                                        }
                                        if (!player1.isMoveAvailable)
                                        {
                                            Print.Stalemate();
                                            stalemate = true;
                                            break;
                                        }

                                        Print.GameStats(player1, player2);
                                        Print.Turn(player1);
                                        board.MoveFigure(player1, player2);
                                        Print.LightEmptyMessageScreen();
                                    }
                                    else
                                    {
                                        if (player2.isCheckmate)
                                        {
                                            Print.WhiteWon(player1);
                                            checkmate = true;
                                            break;
                                        }
                                        if (!player2.isMoveAvailable)
                                        {
                                            Print.Stalemate();
                                            stalemate = true;
                                            break;
                                        }

                                        Print.GameStats(player1, player2);
                                        Print.Turn(player2);
                                        board.MoveFigure(player2, player1);
                                        Print.DarkEmptyMessageScreen();
                                    }
                                }
                            }

                            Console.ReadLine();
                        }

                        break;
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