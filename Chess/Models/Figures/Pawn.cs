namespace Chess.Models.Figures
{
    using System;
    using Contracts;
    using Square.Contracts;
    using Enums;
    using Board;

    public class Pawn : Piece
    {
        public Pawn(Color color)
            : base(color)
        {
        }

        public override char Symbol => 'P';

        public override bool[,] FigureMatrix { get => new bool[Globals.CellRows, Globals.CellCols]
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

        public override bool IsMoveAvailable(ISquare[][] matrix, int row, int col)
        {
            if (this.Color == Color.Light)
            {
                if (Board.InBoardCheck(row - 1, col))
                {
                    var checkedSquare = matrix[row - 1][col];

                    if (!checkedSquare.IsOccupied)
                    {
                        return true;
                    }
                }

                if (Board.InBoardCheck(row - 1, col - 1))
                {
                    var checkedSquare = matrix[row - 1][col - 1];

                    if (checkedSquare.IsOccupied && checkedSquare.Figure.Color != this.Color)
                    {
                        return true;
                    }
                }

                if (Board.InBoardCheck(row - 1, col + 1))
                {
                    var checkedSquare = matrix[row - 1][col + 1];

                    if (checkedSquare.IsOccupied && checkedSquare.Figure.Color != this.Color)
                    {
                        return true;
                    }
                }
            }
            else
            {
                if (Board.InBoardCheck(row + 1, col))
                {
                    var checkedSquare = matrix[row + 1][col];

                    if (!checkedSquare.IsOccupied)
                    {
                        return true;
                    }
                }

                if (Board.InBoardCheck(row + 1, col - 1))
                {
                    var checkedSquare = matrix[row + 1][col - 1];

                    if (checkedSquare.IsOccupied && checkedSquare.Figure.Color != this.Color)
                    {
                        return true;
                    }
                }

                if (Board.InBoardCheck(row + 1, col + 1))
                {
                    var checkedSquare = matrix[row + 1][col + 1];

                    if (checkedSquare.IsOccupied && checkedSquare.Figure.Color != this.Color)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public override void Attacking(ISquare[][] matrix, ISquare square, int row, int col)
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

        public override bool Move(ISquare[][] matrix, ISquare square, IPiece figure, Row toRow, Col toCol)
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

        public override bool Take(ISquare[][] matrix, ISquare square, IPiece figure, Row toRow, Col toCol)
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

        public static IPiece Promotion(int toRow, int toCol, IPiece figure)
        {
            Paint.DefaultBackground();

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
                        IPiece queen = Factory.GetQueen(figure.Color);
                        Draw.Figure(toRow, toCol, queen);
                        return queen;
                    }
                case ConsoleKey.R:
                    {
                        Draw.EmptySquare(toRow, toCol);
                        IPiece rook = Factory.GetRook(figure.Color);
                        Draw.Figure(toRow, toCol, rook);
                        return rook;
                    }
                case ConsoleKey.B:
                    {
                        Draw.EmptySquare(toRow, toCol);
                        IPiece bishop = Factory.GetBishop(figure.Color);
                        Draw.Figure(toRow, toCol, bishop);
                        return bishop;
                    }
                case ConsoleKey.N:
                    {
                        Draw.EmptySquare(toRow, toCol);
                        IPiece knight = Factory.GetKnight(figure.Color);
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