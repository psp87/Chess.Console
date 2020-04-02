namespace Chess.Models
{
    using System;
    using Chess.Models.Pieces.Helpers;
    using Enums;
    using EventArgs;
    using View;

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

        public Player WaitingPlayer => Player1?.HasToMove ?? false ? Player1 : Player2;

        public void New()
        {
            this.ChessBoard.Initialize();
            Draw.NewGame(this.ChessBoard.Matrix);
        }

        public void Move(Player movingPlayer, Player waitingPlayer)
        {
            Print.Stats(movingPlayer, waitingPlayer);
            Print.Turn(movingPlayer);

            this.ChessBoard.MakeMove(movingPlayer, waitingPlayer);
            this.ChangeTurns();

            if (Globals.GameOver.ToString() == GameOver.Checkmate.ToString())
            {
                OnGameOver?.Invoke(movingPlayer, new GameOverEventArgs(Globals.GameOver));
            }
            
            this.IsStalemate(movingPlayer);
        }

        private void IsStalemate(Player MovingPlayer)
        {
            var stalemate = this.ChessBoard.Stalemate(MovingPlayer);
            if (stalemate == GameOver.Stalemate)
            {
                OnGameOver?.Invoke(MovingPlayer, new GameOverEventArgs(stalemate));
                Globals.GameOver = stalemate;
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
