using Chess.Models.Player.Contracts;

namespace Chess.Models.Board.Contracts
{
    public interface IBoard
    {
        void Draw();

        void FigureMove(IPlayer player);

        void NewGame();
    }
}
