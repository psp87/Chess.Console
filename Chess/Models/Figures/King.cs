namespace Chess.Models.Figures
{
    using System;
    using Board;
    using Square.Contracts;
    using Contracts;
    using Enums;
    using System.Linq;

    public class King : IFigure
    {
        public King(Color color)
        {
            this.Name = "King";
            this.Color = color;
            this.Symbol = 'K';
            this.IsFirstMove = true;
            this.IsLastMove = false;
            this.FigureMatrix = new bool[Globals.CellRows, Globals.CellCols]
            {
                { false, false, false, false, false, false, false, false, false },
                { false, false, false, false, true, false, false, false, false },
                { false, false, false, true, true, true, false, false, false },
                { false, true, true, false, true, false, true, true, false },
                { false, true, true, true, false, true, true, true, false },
                { false, true, true, true, true, true, true, true, false },
                { false, false, true, true, true, true, true, false, false },
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
            if (Board.InBoardCheck(row - 1, col))
            {
                matrix[row - 1][col].IsAttacked.Add(square);
            }

            if (Board.InBoardCheck(row - 1, col + 1))
            {
                matrix[row - 1][col + 1].IsAttacked.Add(square);
            }

            if (Board.InBoardCheck(row, col + 1))
            {
                matrix[row][col + 1].IsAttacked.Add(square);
            }

            if (Board.InBoardCheck(row + 1, col + 1))
            {
                matrix[row + 1][col + 1].IsAttacked.Add(square);
            }

            if (Board.InBoardCheck(row + 1, col))
            {
                matrix[row + 1][col].IsAttacked.Add(square);
            }

            if (Board.InBoardCheck(row + 1, col - 1))
            {
                matrix[row + 1][col - 1].IsAttacked.Add(square);
            }

            if (Board.InBoardCheck(row, col - 1))
            {
                matrix[row][col - 1].IsAttacked.Add(square);
            }

            if (Board.InBoardCheck(row - 1, col - 1))
            {
                matrix[row - 1][col - 1].IsAttacked.Add(square);
            }
        }

        public bool Move(ISquare[][] matrix, ISquare square, IFigure figure, Row toRow, Col toCol)
        {
            if (!matrix[(int)toRow][(int)toCol].IsAttacked.Where(x => x.Figure.Color != this.Color).Any())
            {
                if (toCol == square.Col && toRow == square.Row - 1)
                {
                    square.Row -= 1;
                    this.IsFirstMove = false;
                    return true;
                }

                if (toCol == square.Col + 1 && toRow == square.Row - 1)
                {
                    square.Row -= 1;
                    square.Col += 1;
                    this.IsFirstMove = false;
                    return true;
                }

                if (toCol == square.Col + 1 && toRow == square.Row)
                {
                    square.Col += 1;
                    this.IsFirstMove = false;
                    return true;
                }

                if (toCol == square.Col + 1 && toRow == square.Row + 1)
                {
                    square.Row += 1;
                    square.Col += 1;
                    this.IsFirstMove = false;
                    return true;
                }

                if (toCol == square.Col && toRow == square.Row + 1)
                {
                    square.Row += 1;
                    this.IsFirstMove = false;
                    return true;
                }

                if (toCol == square.Col - 1 && toRow == square.Row + 1)
                {
                    square.Row += 1;
                    square.Col -= 1;
                    this.IsFirstMove = false;
                    return true;
                }

                if (toCol == square.Col - 1 && toRow == square.Row)
                {
                    square.Col -= 1;
                    this.IsFirstMove = false;
                    return true;
                }

                if (toCol == square.Col - 1 && toRow == square.Row - 1)
                {
                    square.Row -= 1;
                    square.Col -= 1;
                    this.IsFirstMove = false;
                    return true;
                }

                if (this.IsFirstMove && toRow == square.Row)
                {
                    if (toCol == square.Col + 2)
                    {
                        if (this.OccupiedSquaresCheck(toRow, toCol, matrix, square) && matrix[(int)square.Row][7].Figure is Rook && matrix[(int)square.Row][7].Figure.IsFirstMove)
                        {
                            square.Col += 2;
                            this.IsFirstMove = false;

                            IFigure emptyFigure = Factory.GetEmpty();
                            ISquare emptySquare = Factory.GetSquare(square.Row, (Col)7, emptyFigure);
                            matrix[(int)square.Row][(int)square.Col - 1] = matrix[(int)square.Row][7];
                            Draw.EmptySquare((int)square.Row, 7);
                            Draw.Figure((int)square.Row, (int)square.Col - 1, matrix[(int)square.Row][(int)square.Col - 1].Figure);
                            matrix[(int)square.Row][(int)square.Col - 1].Col -= 2;
                            matrix[(int)square.Row][7] = emptySquare;
                            Draw.EmptySquare((int)square.Row, 7);

                            return true;
                        }
                    }

                    if (toCol == square.Col - 2)
                    {
                        if (this.OccupiedSquaresCheck(toRow, toCol, matrix, square) && matrix[(int)square.Row][0].Figure is Rook && matrix[(int)square.Row][0].Figure.IsFirstMove)
                        {
                            square.Col -= 2;

                            IFigure emptyFigure = Factory.GetEmpty();
                            ISquare emptySquare = Factory.GetSquare(square.Row, (Col)0, emptyFigure);
                            matrix[(int)square.Row][(int)square.Col + 1] = matrix[(int)square.Row][0];
                            Draw.EmptySquare((int)square.Row, 0);
                            Draw.Figure((int)square.Row, (int)square.Col + 1, matrix[(int)square.Row][(int)square.Col + 1].Figure);
                            matrix[(int)square.Row][(int)square.Col + 1].Col += 3;
                            matrix[(int)square.Row][0] = emptySquare;
                            Draw.EmptySquare((int)square.Row, 0);

                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public bool Take(ISquare[][] matrix, ISquare square, IFigure figure, Row toRow, Col toCol)
        {
            if (toCol == square.Col && toRow == square.Row - 1)
            {
                square.Row -= 1;
                this.IsFirstMove = false;
                return true;
            }

            if (toCol == square.Col + 1 && toRow == square.Row - 1)
            {
                square.Row -= 1;
                square.Col += 1;
                this.IsFirstMove = false;
                return true;
            }

            if (toCol == square.Col + 1 && toRow == square.Row)
            {
                square.Col += 1;
                this.IsFirstMove = false;
                return true;
            }

            if (toCol == square.Col + 1 && toRow == square.Row + 1)
            {
                square.Row += 1;
                square.Col += 1;
                this.IsFirstMove = false;
                return true;
            }

            if (toCol == square.Col && toRow == square.Row + 1)
            {
                square.Row += 1;
                this.IsFirstMove = false;
                return true;
            }

            if (toCol == square.Col - 1 && toRow == square.Row + 1)
            {
                square.Row += 1;
                square.Col -= 1;
                this.IsFirstMove = false;
                return true;
            }

            if (toCol == square.Col - 1 && toRow == square.Row)
            {
                square.Col -= 1;
                this.IsFirstMove = false;
                return true;
            }

            if (toCol == square.Col - 1 && toRow == square.Row - 1)
            {
                square.Row -= 1;
                square.Col -= 1;
                this.IsFirstMove = false;
                return true;
            }

            return false;
        }

        private bool OccupiedSquaresCheck(Row toRow, Col toCol, ISquare[][] matrix, ISquare square)
        {
            int colDifference = Math.Abs((int)square.Col - (int)toCol) - 1;

            if ((int)square.Col > (int)toCol)
            {
                colDifference += 2;
            }

            for (int i = 1; i <= colDifference; i++)
            {
                int sign = square.Col < toCol ? i : -i;

                int colCheck = (int)square.Col + sign;

                if (matrix[(int)square.Row][colCheck].IsOccupied)
                {
                    return false;
                }
            }

            return true;
        }
    }
}