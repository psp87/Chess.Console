namespace Chess
{
    using System;
    using System.Collections.Generic;

    using Models;
    using Models.Enums;
    using Models.EventArgs;
    using View;

    public class StartUp
    {
        public static void Main()
        {
            Print.Header();

            while (true)
            {
                Print.Menu();
                var option = Console.ReadKey().Key;

                switch (option)
                {
                    case ConsoleKey.N:
                        {
                            Draw.Board();
                            List<string> playerNames = new List<string>();
                            for (int i = 1; i <= 2; i++)
                            {
                                Print.PlayersMenu(i);
                                playerNames.Add(Console.ReadLine());
                            }

                            Player player1 = Factory.GetPlayer(playerNames[0].ToUpper(), Color.Light);
                            Player player2 = Factory.GetPlayer(playerNames[1].ToUpper(), Color.Dark);

                            Game game = Factory.GetGame(player1, player2);
                            game.OnGameOver += Game_OnGameOver;

                            Print.GameMenu();
                            Print.ExampleText();

                            game.New();
                            Draw.NewGame(game.ChessBoard.Matrix);

                            while (Globals.GameOver.ToString() == GameOver.None.ToString())
                            {
                                Print.Stats(game.MovingPlayer, game.Opponent);
                                Print.Turn(game.MovingPlayer);

                                game.Move(game.MovingPlayer, game.Opponent);
                            }

                            Console.ReadLine();
                            Console.Clear();
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

            void Game_OnGameOver(object sender, EventArgs e)
            {
                var player = sender as Player;
                var gameOver = e as GameOverEventArgs;

                switch (gameOver.GameOver)
                {
                    case GameOver.Checkmate: Print.Won(player, gameOver.GameOver.ToString());
                        break;
                    case GameOver.Stalemate: Print.Stalemate();
                        break;
                }
            }
        }
    }
}
