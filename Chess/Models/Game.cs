namespace Chess.Models
{
    using System;
    using System.Threading;

    using Enums;
    using EventArgs;
    using View;

    public class Game
    {
        Print printer = Factory.GetPrint();
        Draw drawer = Factory.GetDraw();

        public Game(Player player1, Player player2)
        {
            this.ChessBoard = Factory.GetBoard();
            this.ChessBoard.Initialize();

            this.Player1 = player1;
            this.Player2 = player2;
        }

        public event EventHandler OnGameOver;

        public Board ChessBoard { get; set; }

        public Player Player1 { get; set; }

        public Player Player2 { get; set; }

        public Player MovingPlayer => Player1?.HasToMove ?? false ? Player2 : Player1;

        public Player Opponent => Player1?.HasToMove ?? false ? Player1 : Player2;

        public void Start()
        {
            while (Globals.GameOver.ToString() == GameOver.None.ToString())
            {
                printer.Stats(this.MovingPlayer, this.Opponent);
                printer.Turn(this.MovingPlayer);

                this.ChessBoard.MakeMove(this.MovingPlayer, this.Opponent);

                if (Globals.GameOver.ToString() != GameOver.None.ToString())
                {
                    this.OnGameOver?.Invoke(this.MovingPlayer, new GameOverEventArgs(Globals.GameOver));
                }

                this.ChangeTurns();

                Thread.Sleep(500);
                drawer.Board(this.MovingPlayer.Color);
                drawer.BoardOrientation(this.ChessBoard.Matrix, this.MovingPlayer.Color);
            }
        }

        public void New()
        {
            this.ChessBoard.Initialize();
        }

        private void ChangeTurns()
        {
            if (Player1.HasToMove)
            {
                Player1.HasToMove = false;
                Player2.HasToMove = true;
            }
            else
            {
                Player2.HasToMove = false;
                Player1.HasToMove = true;
            }
        }
    }
}
