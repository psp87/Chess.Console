namespace Chess.Models.Board.Contracts
{
    using Square.Contracts;
    using Player.Contracts;

    public interface IBoard
    {
        ISquare[][] Matrix { get; set; }

        void MoveFigure(IPlayer player);

        void NewGame();
    }
}
