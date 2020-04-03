namespace Chess
{
    using System;

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
                            Game game = Factory.GetGame();

                            game.GetPlayers();
                            game.New();
                            game.OnGameOver += Game_OnGameOver;
                            game.Start();
                            game.End();
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
                    case GameOver.Checkmate:
                        printer.Won(player, gameOver.GameOver.ToString());
                        break;
                    case GameOver.Stalemate:
                        printer.Stalemate();
                        break;
                }
            }
        }
    }
}
