namespace Chess
{
    using System;

    public static class Paint
    {
        public static void DefaultColor()
        {
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public static void LightSquare()
        {
            Console.BackgroundColor = ConsoleColor.Green;
        }

        public static void DarkSquare()
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
        }

        public static void LightFigure()
        {
            Console.BackgroundColor = ConsoleColor.White;
        }

        public static void DarkFigure()
        {
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public static void BorderBackground()
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
        }

        public static void BorderText()
        {
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
