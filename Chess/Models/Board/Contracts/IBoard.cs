namespace Chess.Models.Board.Contracts
{
    using Chess.Models.Player.Contracts;

    public interface IBoard
    {
        void Draw();

        void FigureMove(IPlayer player);

        void NewGame();
    }
}
