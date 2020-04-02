namespace Chess.Models
{
    using System;

    using Enums;
    using EventArgs;

    public class Game
    {
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

        public void New()
        {
            this.ChessBoard.Initialize();
        }

        public void Move(Player movingPlayer, Player opponent)
        {
            this.ChessBoard.MakeMove(movingPlayer, opponent);
            this.ChangeTurns();

            if (Globals.GameOver.ToString() != GameOver.None.ToString())
            {
                this.OnGameOver?.Invoke(movingPlayer, new GameOverEventArgs(Globals.GameOver));
            }
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
