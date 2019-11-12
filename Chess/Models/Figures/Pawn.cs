namespace Chess.Models.Figures
{
    using System;
    using Contracts;
    using Square.Contracts;
    using Enums;
    using Board;

    public class Pawn : IFigure
    {
        public Pawn(Color color)
        {
            this.Name = "Pawn";
            this.Color = color;
            this.Symbol = 'P';
            this.IsFirstMove = true;
            this.IsLastMove = false;
            this.FigureMatrix = new bool[Globals.CellRows, Globals.CellCols]
            {
                { false, false, false, false, false, false, false, false, false },
                { false, false, false, false, false, false, false, false, false },
                { false, false, false, false, true, false, false, false, false },
                { false, false, false, true, true, true, false, false, false },
                { false, false, false, true, true, true, false, false, false },
                { false, false, false, false, true, false, false, false, false },
                { false, false, false, true, true, true, false, false, false },
                { false, false, true, true, true, true, true, false, false },
                { false, false, false, false, false, false, false, false, false }
            };
        }

        public string Name { get; }

        public Color Color { get; }

        public char Symbol { get; }

        public bool[,] FigureMatrix { get; }

        public bool IsFirstMove { get; set; }

        public bool IsLastMove { get; set; }

        public void Attacking(ISquare[][] matrix, ISquare square, int row, int col)
        {
            if (this.Color == Color.Light)
            {
                if (Board.InBoardCheck(row - 1, col - 1))
                {
                    matrix[row - 1][col - 1].IsAttacked.Add(square);
                }

                if (Board.InBoardCheck(row - 1, col + 1))
                {
                    matrix[row - 1][col + 1].IsAttacked.Add(square);
                }
            }
            else
            {
                if (Board.InBoardCheck(row + 1, col - 1))
                {
                    matrix[row + 1][col - 1].IsAttacked.Add(square);
                }

                if (Board.InBoardCheck(row + 1, col + 1))
                {
                    matrix[row + 1][col + 1].IsAttacked.Add(square);
                }
            }
        }

        public bool Move(ISquare[][] matrix, ISquare square, IFigure figure, Row toRow, Col toCol)
        {
            if (this.Color == Color.Light)
            {
                if (!this.IsFirstMove && toCol == square.Col && toRow == square.Row - 1)
                {
                    if (toRow == Row.Eight)
                    {
                        this.IsLastMove = true;
                    }

                    square.Row -= 1;
                    return true;
                }
                else if (this.IsFirstMove && toCol == square.Col && toRow == square.Row - 1)
                {
                    square.Row -= 1;
                    this.IsFirstMove = false;
                    return true;
                }
                else if (this.IsFirstMove && toCol == square.Col && toRow == square.Row - 2)
                {
                    square.Row -= 2;
                    this.IsFirstMove = false;
                    this.EnPassantCheck(matrix, square, toRow);
                    return true;
                }
            }
            else
            {
                if (!this.IsFirstMove && toCol == square.Col && toRow == square.Row + 1)
                {
                    if (toRow == Row.One)
                    {
                        this.IsLastMove = true;
                    }

                    square.Row += 1;
                    return true;
                }
                else if (this.IsFirstMove && toCol == square.Col && toRow == square.Row + 1)
                {
                    square.Row += 1;
                    this.IsFirstMove = false;
                    return true;
                }
                else if (this.IsFirstMove && toCol == square.Col && toRow == square.Row + 2)
                {
                    square.Row += 2;
                    this.IsFirstMove = false;
                    this.EnPassantCheck(matrix, square, toRow);
                    return true;
                }
            }

            return false;
        }

        public bool Take(ISquare[][] matrix, ISquare square, IFigure figure, Row toRow, Col toCol)
        {
            if (this.Color == Color.Light)
            {
                if (toRow == square.Row - 1 && toCol == square.Col - 1)
                {
                    if (toRow == Row.Eight)
                    {
                        this.IsLastMove = true;
                    }

                    square.Row -= 1;
                    square.Col -= 1;
                    return true;
                }
                else if (toRow == square.Row - 1 && toCol == square.Col + 1)
                {
                    if (toRow == Row.Eight)
                    {
                        this.IsLastMove = true;
                    }

                    square.Row -= 1;
                    square.Col += 1;
                    return true;
                }
            }
            else
            {
                if (toRow == square.Row + 1 && toCol == square.Col - 1)
                {
                    if (toRow == Row.One)
                    {
                        this.IsLastMove = true;
                    }

                    square.Row += 1;
                    square.Col -= 1;
                    return true;
                }
                else if (toRow == square.Row + 1 && toCol == square.Col + 1)
                {
                    if (toRow == Row.One)
                    {
                        this.IsLastMove = true;
                    }

                    square.Row += 1;
                    square.Col += 1;
                    return true;
                }
            }

            return false;
        }

        public static IFigure Promotion(int toRow, int toCol, IFigure figure)
        {
            Paint.DefaultColor();

            if (figure.Color == Color.Light)
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
                        Draw.EmptySquare(toRow, toCol);
                        IFigure queen = Factory.GetQueen(figure.Color);
                        Draw.Figure(toRow, toCol, queen);
                        return queen;
                    }
                case ConsoleKey.R:
                    {
                        Draw.EmptySquare(toRow, toCol);
                        IFigure rook = Factory.GetRook(figure.Color);
                        Draw.Figure(toRow, toCol, rook);
                        return rook;
                    }
                case ConsoleKey.B:
                    {
                        Draw.EmptySquare(toRow, toCol);
                        IFigure bishop = Factory.GetBishop(figure.Color);
                        Draw.Figure(toRow, toCol, bishop);
                        return bishop;
                    }
                case ConsoleKey.N:
                    {
                        Draw.EmptySquare(toRow, toCol);
                        IFigure knight = Factory.GetKnight(figure.Color);
                        Draw.Figure(toRow, toCol, knight);
                        return knight;
                    }
                default: return figure;
            }
        }

        private void EnPassantCheck(ISquare[][] matrix, ISquare square, Row toRow)
        {
            int rowSign = this.Color == Color.Light ? 1 : -1;

            var currentRow = (int)square.Row;
            var currentCol = (int)square.Col;

            switch (currentCol)
            {
                case 0:
                    {
                        var rightNeighbor = matrix[(int)toRow][currentCol + 1].Figure;

                        if (rightNeighbor is Pawn && rightNeighbor.Color != this.Color)
                        {
                            EnPassant.Turn = Globals.TurnCounter + 1;
                            EnPassant.Row = currentRow + rowSign;
                            EnPassant.Col = currentCol;
                        }
                    }
                    break;
                case 7:
                    {
                        var leftNeighbor = matrix[(int)toRow][currentCol - 1].Figure;

                        if (leftNeighbor is Pawn && leftNeighbor.Color != this.Color)
                        {
                            EnPassant.Turn = Globals.TurnCounter + 1;
                            EnPassant.Row = currentRow + rowSign;
                            EnPassant.Col = currentCol;
                        }
                    }
                    break;
                default:
                    {
                        var rightNeighbor = matrix[(int)toRow][currentCol + 1].Figure;
                        var leftNeighbor = matrix[(int)toRow][currentCol - 1].Figure;

                        if (leftNeighbor is Pawn && leftNeighbor.Color != this.Color || rightNeighbor is Pawn && rightNeighbor.Color != this.Color)
                        {
                            EnPassant.Turn = Globals.TurnCounter + 1;
                            EnPassant.Row = currentRow + rowSign;
                            EnPassant.Col = currentCol;
                        }
                    }
                    break;
            }
        }
    }
}