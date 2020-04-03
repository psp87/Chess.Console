namespace Chess.View
{
    using System;

    using Models;
    using Models.Enums;
    using Models.Pieces.Contracts;

    public class Draw
    {
        public void Board(Color color)
        {
            for (int row = 0; row < Globals.BoardRows; row++)
            {
                for (int col = 0; col < Globals.BoardCols; col++)
                {
                    EmptySquare(row, col);
                }
            }

            Border(color);

            Paint.DefaultBackground();
        }

        public void NewGame(Square[][] matrix, Player player)
        {
            for (int row = 0; row < Globals.BoardRows; row++)
            {
                for (int col = 0; col < Globals.BoardCols; col++)
                {
                    Square currentSquare = matrix[row][col];
                    Figure(row, col, currentSquare.Piece);
                }
            }

            Border(player.Color);

            Paint.DefaultBackground();
        }

        public void Figure(int row, int col, IPiece figure)
        {
            for (int cellRow = 1; cellRow < Globals.CellRows - 1; cellRow++)
            {
                for (int cellCol = 1; cellCol < Globals.CellCols - 1; cellCol++)
                {
                    if (figure.FigureMatrix[cellRow, cellCol] == true)
                    {
                        Console.SetCursorPosition(col * Globals.CellCols + Globals.OffsetHorizontal + cellCol,
                            row * Globals.CellRows + Globals.OffsetVertical + cellRow);

                        if (figure.Color == Color.Light)
                        {
                            Paint.LightFigure();
                            Console.Write(" ");
                        }
                        else
                        {
                            Paint.DarkFigure();
                            Console.Write(" ");
                        }
                    }
                }
            }
        }

        public void EmptySquare(int row, int col)
        {
            if ((row + col) % 2 == 0)
            {
                Paint.LightSquare();
            }
            else
            {
                Paint.DarkSquare();
            }

            for (int i = 0; i < Globals.CellRows; i++)
            {
                Console.SetCursorPosition(col * Globals.CellCols + Globals.OffsetHorizontal,
                    row * Globals.CellRows + Globals.OffsetVertical + i);
                Console.Write(new string(' ', 9));
            }
        }

        private void Border(Color color)
        {
            var value = color == Color.Light ? 0 : 9;

            Paint.BorderBackground();
            Paint.BorderText();

            var horizontalMin = Globals.OffsetHorizontal;
            var horizontalMax = Globals.OffsetHorizontal + (Globals.BoardCols * Globals.CellCols);

            var verticalMin = Globals.OffsetVertical;
            var verticalMax = Globals.OffsetVertical + (Globals.BoardRows * Globals.CellRows);

            // Draw lines up and down
            Console.SetCursorPosition(horizontalMin - 2, verticalMin - 2);
            Console.WriteLine(new string(' ', 76));

            Console.SetCursorPosition(horizontalMin - 2, verticalMax + 1);
            Console.WriteLine(new string(' ', 76));

            // Draw middle points and write letters and numbers
            for (int i = 0; i < Globals.BoardRows; i++)
            {
                var horizontalMiddlePoints = Globals.OffsetHorizontal + 4 + (i * Globals.CellCols);
                var verticalMiddlePoints = Globals.OffsetVertical + 4 + (i * Globals.CellRows);

                Console.SetCursorPosition(horizontalMin - 1, verticalMiddlePoints);
                Console.WriteLine($"{Math.Abs(8 - i - value)}");

                Console.SetCursorPosition(horizontalMax, verticalMiddlePoints);
                Console.WriteLine($"{Math.Abs(8 - i - value)}");

                if (color == Color.Light)
                {
                    Console.SetCursorPosition(horizontalMiddlePoints, verticalMin - 1);
                    Console.WriteLine($"{(char)(65 + i)}");

                    Console.SetCursorPosition(horizontalMiddlePoints, verticalMax);
                    Console.WriteLine($"{(char)(65 + i)}");
                }
                else
                {
                    Console.SetCursorPosition(horizontalMiddlePoints, verticalMin - 1);
                    Console.WriteLine($"{(char)(72 - i)}");

                    Console.SetCursorPosition(horizontalMiddlePoints, verticalMax);
                    Console.WriteLine($"{(char)(72 - i)}");
                }


                for (int k = 0; k < Globals.CellRows + 2; k++)
                {
                    // Draw lines left and right
                    var verticalRowByRow = Globals.OffsetVertical + (i * Globals.CellCols) + k;

                    Console.SetCursorPosition(horizontalMin - 2, verticalRowByRow - 1);
                    Console.WriteLine(" ");

                    Console.SetCursorPosition(horizontalMax + 1, verticalRowByRow - 1);
                    Console.WriteLine(" ");
                }
            }
        }

        public void NewFigures(int fromCol, int fromRow, int toCol, int toRow, IPiece figure)
        {
            EmptySquare(toRow, toCol);
            Figure(toRow, toCol, figure);

            EmptySquare(fromRow, fromCol);
        }

        public void NewFiguresTest(int fromCol, int fromRow, int toCol, int toRow, IPiece figure)
        {
            EmptySquare(7 - toRow, 7 - toCol);
            Figure(7 - toRow, 7 - toCol, figure);

            EmptySquare(7 - fromRow, 7 - fromCol);
        }

        public void BoardOrientation(Square[][] matrix, Color color)
        {
            var value = color == Color.Light ? 0 : 7;

            for (int row = 0; row < Globals.BoardRows; row++)
            {
                for (int col = 0; col < Globals.BoardCols; col++)
                {
                    Square currentSquare = matrix[row][col];
                    Figure(Math.Abs(row - value), Math.Abs(col - value), currentSquare.Piece);
                }
            }

            Border(color);

            Paint.DefaultBackground();
        }

        public IPiece PawnPromotion(Position toPos, IPiece piece)
        {
            Paint.DefaultBackground();

            if (piece.Color == Color.Light)
            {
                Print.SetCursorMinMax(-17, -4);
                Console.WriteLine("(Q,R,B,N)");
                Print.SetCursorMinMax(-17, -6);
                Console.Write("CHOOSE FIGURE:");
            }
            else
            {
                Print.SetCursorMinMax(78, -51);
                Console.WriteLine("(Q,R,B,N)");
                Print.SetCursorMinMax(78, -53);
                Console.Write("CHOOSE FIGURE:");
            }

            var figureChoose = Console.ReadKey().Key;

            switch (figureChoose)
            {
                case ConsoleKey.Q:
                    {
                        this.EmptySquare(toPos.Y, toPos.X);
                        IPiece queen = Factory.GetQueen(piece.Color);
                        this.Figure(toPos.Y, toPos.X, queen);
                        return queen;
                    }

                case ConsoleKey.R:
                    {
                        this.EmptySquare(toPos.Y, toPos.X);
                        IPiece rook = Factory.GetRook(piece.Color);
                        this.Figure(toPos.Y, toPos.X, rook);
                        return rook;
                    }

                case ConsoleKey.B:
                    {
                        this.EmptySquare(toPos.Y, toPos.X);
                        IPiece bishop = Factory.GetBishop(piece.Color);
                        this.Figure(toPos.Y, toPos.X, bishop);
                        return bishop;
                    }

                case ConsoleKey.N:
                    {
                        this.EmptySquare(toPos.Y, toPos.X);
                        IPiece knight = Factory.GetKnight(piece.Color);
                        this.Figure(toPos.Y, toPos.X, knight);
                        return knight;
                    }

                default: return this.PawnPromotion(toPos, piece);
            }
        }
    }
}
