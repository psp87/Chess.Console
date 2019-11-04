namespace Chess
{
    using System;

    public static class Drawer
    {
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

        public static void Border()
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
