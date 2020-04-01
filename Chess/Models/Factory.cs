﻿namespace Chess.Models
{
    using Enums;
    using Pieces;
    using Pieces.Contracts;
    using Pieces.Helpers;

    public class Factory
    {
        public static Board GetBoard()
        {
            return new Board();
        }

        public static Player GetPlayer(string name, Color color)
        {
            return new Player(name, color);
        }

        public static IPiece GetPawn(Color color)
        {
            return new Pawn(color);
        }

        public static IPiece GetRook(Color color)
        {
            return new Rook(color);
        }

        public static IPiece GetKnight(Color color)
        {
            return new Knight(color);
        }

        public static IPiece GetBishop(Color color)
        {
            return new Bishop(color);
        }

        public static IPiece GetQueen(Color color)
        {
            return new Queen(color);
        }

        public static IPiece GetKing(Color color)
        {
             return new King(color);
        }

        public static IPiece GetEmpty()
        {
            return new Empty();
        }

        public static Position GetPosition(int y, int x)
        {
            return new Position(y, x);
        }

        public static Position GetPositionEmpty()
        {
            return new Position();
        }

        public static Square GetSquare(int x, int y)
        {
            return new Square(x, y);
        }

        public static Square[][] GetMatrix()
        {
            Square[][] matrix = new Square[Globals.BoardRows][];

            for (int row = 0; row < Globals.BoardRows; row++)
            {
                matrix[row] = new Square[Globals.BoardCols];
            }

            return matrix;
        }

        public static RookBehaviour GetRookBehaviour()
        {
            return new RookBehaviour();
        }

        public static BishopBahaviour GetBishopBehaviour()
        {
            return new BishopBahaviour();
        }
    }
}