namespace Chess
{
    using System;
    using System.Threading;

    using Models;
    using Models.Enums;
    using Models.EventArgs;
    using View;

    public class StartUp
    {
        public static void Main()
        {
            Print printer = Factory.GetPrint();
            Draw drawer = Factory.GetDraw();

            printer.Header();
            drawer.Board(Color.Light);

            while (true)
            {
                printer.Menu();

                var option = Console.ReadKey().Key;
                switch (option)
                {
                    case ConsoleKey.N:
                        {
                            printer.PlayersMenu(Color.Light);
                            var namePlayer1 = Console.ReadLine();
                            printer.PlayersMenu(Color.Dark);
                            var namePlayer2 = Console.ReadLine();

                            Player player1 = Factory.GetPlayer(namePlayer1.ToUpper(), Color.Light);
                            Player player2 = Factory.GetPlayer(namePlayer2.ToUpper(), Color.Dark);

                            Game game = Factory.GetGame(player1, player2);
                            game.OnGameOver += Game_OnGameOver;

                            printer.GameMenu();
                            printer.ExampleText();
                            drawer.NewGame(game.ChessBoard.Matrix, game.MovingPlayer);

                            while (Globals.GameOver.ToString() == GameOver.None.ToString())
                            {
                                printer.Stats(game.MovingPlayer, game.Opponent);
                                printer.Turn(game.MovingPlayer);

                                game.Move(game.MovingPlayer, game.Opponent);

                                Thread.Sleep(500);
                                drawer.Board(game.MovingPlayer.Color);
                                drawer.BoardOrientation(game.ChessBoard.Matrix, game.MovingPlayer.Color);
                            }

                            Console.ReadLine();
                            Console.Clear();
                            drawer.Board(Color.Light);
                        }

                        break;
                    case ConsoleKey.L:
                        break;
                    case ConsoleKey.S:
                        break;
                    case ConsoleKey.Escape: Console.Clear();
                        return;
                }
            }

            void Game_OnGameOver(object sender, EventArgs e)
            {
                var player = sender as Player;
                var gameOver = e as GameOverEventArgs;

                switch (gameOver.GameOver)
                {
                    case GameOver.Checkmate: printer.Won(player, gameOver.GameOver.ToString());
                        break;
                    case GameOver.Stalemate: printer.Stalemate();
                        break;
                }
            }
        }
    }
}
