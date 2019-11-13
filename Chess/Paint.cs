namespace Chess
{
    using System;

    public static class Paint
    {
        public static void DefaultBackground()
        {
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public static void LightSquare()
        {
            Console.BackgroundColor = ConsoleColor.Gray;
        }

        public static void DarkSquare()
        {
            Console.BackgroundColor = ConsoleColor.DarkGray;
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

        public static void YellowText()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
        }

        public static void WhiteText()
        {
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void GrayText()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
