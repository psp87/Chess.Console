namespace Chess.Models.Figures
{
    using Board;
    using Square.Contracts;
    using Contracts;
    using Enums;

    public class Knight : Piece
    {
        public Knight(Color color)
            : base(color)
        {
        }

        public override char Symbol => 'N';

        public override bool[,] FigureMatrix { get => new bool[Globals.CellRows, Globals.CellCols]
            {
                { false, false, false, false, false, false, false, false, false },
                { false, false, false, true, true, true, false, false, false },
                { false, false, true, false, true, true, true, false, false },
                { false, true, true, true, true, true, true, false, false },
                { false, true, true, false, false, true, true, false, false },
                { false, false, false, false, true, true, true, false, false },
                { false, false, false, true, true, true, false, false , false },
                { false, false, true, true, true, true, true, false, false },
                { false, false, false, false, false, false, false, false, false }
            };
        }

        public override bool IsMoveAvailable(ISquare[][] matrix, int row, int col)
        {
            if (Board.InBoardCheck(row - 2, col - 1))
            {
                var checkedSquare = matrix[row - 2][col - 1];

                if ((checkedSquare.IsOccupied && checkedSquare.Figure.Color != this.Color) || !checkedSquare.IsOccupied)
                {
                    return true;
                }
            }

            if (Board.InBoardCheck(row - 2, col + 1))
            {
                var checkedSquare = matrix[row - 2][col + 1];

                if ((checkedSquare.IsOccupied && checkedSquare.Figure.Color != this.Color) || !checkedSquare.IsOccupied)
                {
                    return true;
                }
            }

            if (Board.InBoardCheck(row + 2, col - 1))
            {
                var checkedSquare = matrix[row + 2][col - 1];

                if ((checkedSquare.IsOccupied && checkedSquare.Figure.Color != this.Color) || !checkedSquare.IsOccupied)
                {
                    return true;
                }
            }

            if (Board.InBoardCheck(row + 2, col + 1))
            {
                var checkedSquare = matrix[row + 2][col + 1];

                if ((checkedSquare.IsOccupied && checkedSquare.Figure.Color != this.Color) || !checkedSquare.IsOccupied)
                {
                    return true;
                }
            }

            if (Board.InBoardCheck(row - 1, col - 2))
            {
                var checkedSquare = matrix[row - 1][col - 2];

                if ((checkedSquare.IsOccupied && checkedSquare.Figure.Color != this.Color) || !checkedSquare.IsOccupied)
                {
                    return true;
                }
            }

            if (Board.InBoardCheck(row - 1, col + 2))
            {
                var checkedSquare = matrix[row - 1][col + 2];

                if ((checkedSquare.IsOccupied && checkedSquare.Figure.Color != this.Color) || !checkedSquare.IsOccupied)
                {
                    return true;
                }
            }

            if (Board.InBoardCheck(row + 1, col - 2))
            {
                var checkedSquare = matrix[row + 1][col - 2];

                if ((checkedSquare.IsOccupied && checkedSquare.Figure.Color != this.Color) || !checkedSquare.IsOccupied)
                {
                    return true;
                }
            }

            if (Board.InBoardCheck(row + 1, col + 2))
            {
                var checkedSquare = matrix[row + 1][col + 2];

                if ((checkedSquare.IsOccupied && checkedSquare.Figure.Color != this.Color) || !checkedSquare.IsOccupied)
                {
                    return true;
                }
            }

            return false;
        }

        public override void Attacking(ISquare[][] matrix, ISquare square, int row, int col)
        {
            if (Board.InBoardCheck(row - 2, col - 1))
            {
                matrix[row - 2][col - 1].IsAttacked.Add(square);
            }

            if (Board.InBoardCheck(row - 2, col + 1))
            {
                matrix[row - 2][col + 1].IsAttacked.Add(square);
            }

            if (Board.InBoardCheck(row + 2, col - 1))
            {
                matrix[row + 2][col - 1].IsAttacked.Add(square);
            }

            if (Board.InBoardCheck(row + 2, col + 1))
            {
                matrix[row + 2][col + 1].IsAttacked.Add(square);
            }

            if (Board.InBoardCheck(row - 1, col - 2))
            {
                matrix[row - 1][col - 2].IsAttacked.Add(square);
            }

            if (Board.InBoardCheck(row - 1, col + 2))
            {
                matrix[row - 1][col + 2].IsAttacked.Add(square);
            }

            if (Board.InBoardCheck(row + 1, col - 2))
            {
                matrix[row + 1][col - 2].IsAttacked.Add(square);
            }

            if (Board.InBoardCheck(row + 1, col + 2))
            {
                matrix[row + 1][col + 2].IsAttacked.Add(square);
            }
        }

        public override bool Move(ISquare[][] squares, ISquare square, IPiece figure, Row toRow, Col toCol)
        {
            if (toCol == square.Col - 1 && toRow == square.Row - 2)
            {
                square.Row -= 2;
                square.Col -= 1;
                return true;
            }

            if (toCol == square.Col + 1 && toRow == square.Row - 2)
            {
                square.Row -= 2;
                square.Col += 1;
                return true;
            }

            if (toCol == square.Col - 1 && toRow == square.Row + 2)
            {
                square.Row += 2;
                square.Col -= 1;
                return true;
            }

            if (toCol == square.Col + 1 && toRow == square.Row + 2)
            {
                square.Row += 2;
                square.Col += 1;
                return true;
            }

            if (toCol == square.Col - 2 && toRow == square.Row - 1)
            {
                square.Row -= 1;
                square.Col -= 2;
                return true;
            }

            if (toCol == square.Col - 2 && toRow == square.Row + 1)
            {
                square.Row += 1;
                square.Col -= 2;
                return true;
            }

            if (toCol == square.Col + 2 && toRow == square.Row - 1)
            {
                square.Row -= 1;
                square.Col += 2;
                return true;
            }

            if (toCol == square.Col + 2 && toRow == square.Row + 1)
            {
                square.Row += 1;
                square.Col += 2;
                return true;
            }

            return false;
        }

        public override bool Take(ISquare[][] squares, ISquare square, IPiece figure, Row toRow, Col toCol)
        {
            if (toCol == square.Col - 1 && toRow == square.Row - 2)
            {
                square.Row -= 2;
                square.Col -= 1;
                return true;
            }

            if (toCol == square.Col + 1 && toRow == square.Row - 2)
            {
                square.Row -= 2;
                square.Col += 1;
                return true;
            }

            if (toCol == square.Col - 1 && toRow == square.Row + 2)
            {
                square.Row += 2;
                square.Col -= 1;
                return true;
            }

            if (toCol == square.Col + 1 && toRow == square.Row + 2)
            {
                square.Row += 2;
                square.Col += 1;
                return true;
            }

            if (toCol == square.Col - 2 && toRow == square.Row - 1)
            {
                square.Row -= 1;
                square.Col -= 2;
                return true;
            }

            if (toCol == square.Col - 2 && toRow == square.Row + 1)
            {
                square.Row += 1;
                square.Col -= 2;
                return true;
            }

            if (toCol == square.Col + 2 && toRow == square.Row - 1)
            {
                square.Row -= 1;
                square.Col += 2;
                return true;
            }

            if (toCol == square.Col + 2 && toRow == square.Row + 1)
            {
                square.Row += 1;
                square.Col += 2;
                return true;
            }

            return false;
        }
    }
}