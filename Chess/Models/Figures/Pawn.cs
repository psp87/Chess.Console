namespace Chess
{
    using System;
    using Chess.Models.Figures.Contracts;
    using Chess.Models.Player.Contracts;

    public class Pawn : IFigure
    {
        private bool[,] figureMatrix;

        public Pawn(Color color, CoordinateY row, CoordinateX col)
        {
            this.Name = "Pawn";
            this.Color = color;
            this.Symbol = 'P';
            this.Col = col;
            this.Row = row;
            this.IsOccupied = true;
            this.IsFirstMove = true;
            this.IsLastMove = false;
            this.figureMatrix = new bool[Globals.CellRows, Globals.CellCols]
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

        public CoordinateY Row { get; set; }

        public CoordinateX Col { get; set; }

        public bool IsOccupied { get; set; }

        public bool IsFirstMove { get; set; }

        public bool IsLastMove { get; set; }

        public void Draw(int row, int col)
        {
            for (int cellRow = 1; cellRow < Globals.CellRows - 1; cellRow++)
            {
                for (int cellCol = 1; cellCol < Globals.CellCols - 1; cellCol++)
                {
                    if (this.figureMatrix[cellRow, cellCol] == true)
                    {
                        Console.SetCursorPosition(col * Globals.CellCols + Globals.OffsetHorizontal + cellCol,
                            row * Globals.CellRows + Globals.OffsetVertical + cellRow);

                        if (this.Color == Color.Light)
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

        public bool Move(IFigure[][] squares, CoordinateY toRow, CoordinateX toCol)
        {
            if (this.Color == Color.Light)
            {
                if (!this.IsFirstMove && toCol == this.Col && toRow == this.Row - 1)
                {
                    if (toRow == CoordinateY.Eight)
                    {
                        this.IsLastMove = true;
                    }

                    this.Row -= 1;
                    return true;
                }
                else if (this.IsFirstMove && toCol == this.Col && toRow == this.Row - 1)
                {
                    this.Row -= 1;
                    this.IsFirstMove = false;
                    return true;
                }
                else if (this.IsFirstMove && toCol == this.Col && toRow == this.Row - 2)
                {
                    this.Row -= 2;
                    this.IsFirstMove = false;
                    this.EnPassantCheck(squares, toRow);
                    return true;
                }
            }
            else
            {
                if (!this.IsFirstMove && toCol == this.Col && toRow == this.Row + 1)
                {
                    if (toRow == CoordinateY.One)
                    {
                        this.IsLastMove = true;
                    }

                    this.Row += 1;
                    return true;
                }
                else if (this.IsFirstMove && toCol == this.Col && toRow == this.Row + 1)
                {
                    this.Row += 1;
                    this.IsFirstMove = false;
                    return true;
                }
                else if (this.IsFirstMove && toCol == this.Col && toRow == this.Row + 2)
                {
                    this.Row += 2;
                    this.IsFirstMove = false;
                    this.EnPassantCheck(squares, toRow);
                    return true;
                }
            }

            return false;
        }

        public bool Take(IFigure[][] squares, CoordinateY toRow, CoordinateX toCol)
        {
            if (this.Color == Color.Light)
            {
                if (toRow == this.Row - 1 && toCol == this.Col - 1)
                {
                    if (toRow == CoordinateY.Eight)
                    {
                        this.IsLastMove = true;
                    }

                    this.Row -= 1;
                    this.Col -= 1;
                    return true;
                }
                else if (toRow == this.Row - 1 && toCol == this.Col + 1)
                {
                    if (toRow == CoordinateY.Eight)
                    {
                        this.IsLastMove = true;
                    }

                    this.Row -= 1;
                    this.Col += 1;
                    return true;
                }
            }
            else
            {
                if (toRow == this.Row + 1 && toCol == this.Col - 1)
                {
                    if (toRow == CoordinateY.One)
                    {
                        this.IsLastMove = true;
                    }

                    this.Row += 1;
                    this.Col -= 1;
                    return true;
                }
                else if (toRow == this.Row + 1 && toCol == this.Col + 1)
                {
                    if (toRow == CoordinateY.One)
                    {
                        this.IsLastMove = true;
                    }

                    this.Row += 1;
                    this.Col += 1;
                    return true;
                }
            }

            return false;
        }

        public static void LastMoveCheck(int toRow, int toCol, IFigure[][] squares, IPlayer player)
        {
            if (squares[toRow][toCol].IsLastMove)
            {
                string figureChoose = Console.ReadLine();

                switch (figureChoose.ToUpper())
                {
                    case "Q":
                        {
                            Drawer.EmptySquare(toRow, toCol);
                            IFigure queen = Factory.GetQueen(player.Color, (CoordinateY)toRow, (CoordinateX)toCol);
                            squares[toRow][toCol] = queen;
                            squares[toRow][toCol].Draw(toRow, toCol);
                            break;
                        }
                    case "R":
                        {
                            Drawer.EmptySquare(toRow, toCol);
                            IFigure rook = Factory.GetRook(player.Color, (CoordinateY)toRow, (CoordinateX)toCol);
                            squares[toRow][toCol] = rook;
                            squares[toRow][toCol].Draw(toRow, toCol);
                            break;
                        }
                    case "B":
                        {
                            Drawer.EmptySquare(toRow, toCol);
                            IFigure bishop = Factory.GetBishop(player.Color, (CoordinateY)toRow, (CoordinateX)toCol);
                            squares[toRow][toCol] = bishop;
                            squares[toRow][toCol].Draw(toRow, toCol);
                            break;
                        }
                    case "N":
                        {
                            Drawer.EmptySquare(toRow, toCol);
                            IFigure knight = Factory.GetKnight(player.Color, (CoordinateY)toRow, (CoordinateX)toCol);
                            squares[toRow][toCol] = knight;
                            squares[toRow][toCol].Draw(toRow, toCol);
                            break;
                        }
                }
            }
        }

        private void EnPassantCheck(IFigure[][] squares, CoordinateY toRow)
        {
            int rowSign = this.Color == Color.Light ? 1 : -1;

            var currentRow = (int)this.Row;
            var currentCol = (int)this.Col;
            
            switch (currentCol)
            {
                case 0:
                    {
                        var rightNeighbor = squares[(int)toRow][currentCol + 1];

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
                        var leftNeighbor = squares[(int)toRow][currentCol - 1];

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
                        var rightNeighbor = squares[(int)toRow][currentCol + 1];
                        var leftNeighbor = squares[(int)toRow][currentCol - 1];

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