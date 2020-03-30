namespace Chess
{
    using System;
    using Models.Square.Contracts;
    using Models.Figures.Contracts;
    using Models.Enums;

    public static class Draw
    {
        public static void Board()
        {
            for (int row = 0; row < Globals.BoardRows; row++)
            {
                for (int col = 0; col < Globals.BoardCols; col++)
                {
                    EmptySquare(row, col);
                }
            }

            Border();

            Paint.DefaultBackground();
        }

        public static void NewGame(ISquare[][] matrix)
        {
            for (int row = 0; row < Globals.BoardRows; row++)
            {
                for (int col = 0; col < Globals.BoardCols; col++)
                {
                    ISquare currentSquare = matrix[row][col];
                    Figure(row, col, currentSquare.Figure);
                }
            }

            Border();

            Paint.DefaultBackground();
        }

        public static void Figure(int row, int col, IPiece figure)
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

        public static void EmptySquare(int row, int col)
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

        private static void Border()
        {
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
                Console.WriteLine($"{Math.Abs(i - 8)}");

                Console.SetCursorPosition(horizontalMax, verticalMiddlePoints);
                Console.WriteLine($"{Math.Abs(i - 8)}");

                Console.SetCursorPosition(horizontalMiddlePoints, verticalMin - 1);
                Console.WriteLine($"{(char)(65 + i)}");

                Console.SetCursorPosition(horizontalMiddlePoints, verticalMax);
                Console.WriteLine($"{(char)(65 + i)}");

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
    }
}
