namespace Chess.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Enums;
    using Pieces;
    using Pieces.Contracts;
    using Pieces.Helpers;
    using View;

    public class Board : ICloneable
    {
        Print printer = Factory.GetPrint();
        Draw drawer = Factory.GetDraw();

        private Dictionary<string, int> colMap = new Dictionary<string, int>()
        {
            { "A", 0 },
            { "B", 1 },
            { "C", 2 },
            { "D", 3 },
            { "E", 4 },
            { "F", 5 },
            { "G", 6 },
            { "H", 7 },
        };

        private string[] letters = new string[] { "A", "B", "C", "D", "E", "F", "G", "H" };

        private Dictionary<string, Piece> setup = new Dictionary<string, Piece>()
        {
            { "A1", new Rook(Color.Light) },  { "B1", new Knight(Color.Light) }, { "C1", new Bishop(Color.Light) }, { "D1", new Queen(Color.Light) },
            { "E1", new King(Color.Light) }, { "F1", new Bishop(Color.Light) }, { "G1", new Knight(Color.Light) }, { "H1", new Rook(Color.Light) },
            { "A2", new Pawn(Color.Light) },  { "B2", new Pawn(Color.Light) },   { "C2", new Pawn(Color.Light) },   { "D2", new Pawn(Color.Light) },
            { "E2", new Pawn(Color.Light) },  { "F2", new Pawn(Color.Light) },   { "G2", new Pawn(Color.Light) },   { "H2", new Pawn(Color.Light) },

            { "A7", new Pawn(Color.Dark) },  { "B7", new Pawn(Color.Dark) },   { "C7", new Pawn(Color.Dark) },   { "D7", new Pawn(Color.Dark) },
            { "E7", new Pawn(Color.Dark) },  { "F7", new Pawn(Color.Dark) },   { "G7", new Pawn(Color.Dark) },   { "H7", new Pawn(Color.Dark) },
            { "A8", new Rook(Color.Dark) },  { "B8", new Knight(Color.Dark) }, { "C8", new Bishop(Color.Dark) }, { "D8", new Queen(Color.Dark) },
            { "E8", new King(Color.Dark) }, { "F8", new Bishop(Color.Dark) }, { "G8", new Knight(Color.Dark) }, { "H8", new Rook(Color.Dark) },
        };

        public Board()
        {
            this.Matrix = Factory.GetMatrix();
        }

        public Square[][] Matrix { get; set; }

        public void MakeMove(Player movingPlayer, Player opponent)
        {
            Globals.TurnCounter++;

            IPiece emptyFigure = Factory.GetEmpty();

            bool successfulMove = false;

            while (!successfulMove || movingPlayer.IsCheck == true)
            {
                try
                {
                    // Get command arguments
                    string text = Console.ReadLine();

                    string pattern = @"([A-Za-z])([A-Za-z])([1-8])([A-Za-z])([1-8])";
                    Regex regex = new Regex(pattern);
                    Match match = regex.Match(text);

                    char symbol = char.Parse(match.Groups[1].ToString().ToUpper());
                    int fromCol = this.colMap[match.Groups[2].ToString().ToUpper()];
                    int fromRow = Math.Abs(int.Parse(match.Groups[3].ToString()) - 8);
                    int toCol = this.colMap[match.Groups[4].ToString().ToUpper()];
                    int toRow = Math.Abs(int.Parse(match.Groups[5].ToString()) - 8);

                    // Get FROM square and TO square to simplify if conditions
                    var from = this.Matrix[fromRow][fromCol];
                    var to = this.Matrix[toRow][toCol];

                    // Get FROM position and TO position
                    var fromPos = Factory.GetPosition(fromRow, fromCol);
                    var toPos = Factory.GetPosition(toRow, toCol);

                    // Main logic for movement and taking
                    if (!to.IsOccupied && movingPlayer.Color == from.Piece.Color && symbol == from.Piece.Symbol)
                    {
                        if (from.Piece.Move(toPos, this.Matrix))
                        {
                            // Assign new values to squares
                            this.Matrix[toRow][toCol].Piece = this.Matrix[fromRow][fromCol].Piece;
                            this.Matrix[fromRow][fromCol].Piece = emptyFigure;

                            // Calculation of attacked squares in the board
                            this.CalculateAttackedSquares();

                            // Check is the king of the current player is attacked when making the move
                            if (this.IsPlayerChecked(movingPlayer))
                            {
                                // Assigning the new value, Deleting the old drawn figure
                                this.Matrix[toRow][toCol].Position.Y = this.Matrix[fromRow][fromCol].Position.Y;
                                this.Matrix[toRow][toCol].Position.X = this.Matrix[fromRow][fromCol].Position.X;
                                this.Matrix[fromRow][fromCol] = this.Matrix[toRow][toCol];

                                // Assigning the old square
                                this.Matrix[toRow][toCol] = to;

                                movingPlayer.IsCheck = true;
                                this.CalculateAttackedSquares();
                                printer.KingIsCheck(movingPlayer);
                                continue;
                            }
                            else
                            {
                                movingPlayer.IsCheck = false;
                            }

                            // Draw the new figures to FROM and TO squares
                            if (movingPlayer.Color == Color.Light)
                            {
                                drawer.NewFigures(fromCol, fromRow, toCol, toRow, this.Matrix[toRow][toCol].Piece);
                            }
                            else
                            {
                                drawer.NewFiguresTest(fromCol, fromRow, toCol, toRow, this.Matrix[toRow][toCol].Piece);
                            }


                            // Clear the check message screen of the other player
                            printer.EmptyCheckScreen(opponent);

                            // Check for pawn promotion
                            if (this.Matrix[toRow][toCol].Piece is Pawn && this.Matrix[toRow][toCol].Piece.IsLastMove)
                            {
                                this.Matrix[toRow][toCol].Piece = drawer.PawnPromotion(toPos, this.Matrix[toRow][toCol].Piece);
                                this.CalculateAttackedSquares();
                            }

                            // Print if the current player check the other player after movement. Check if the player is checkmate.
                            if (this.IsPlayerChecked(opponent))
                            {
                                printer.Check(movingPlayer);
                                this.IsOpponentCheckmate(movingPlayer, opponent, from);
                            }

                            successfulMove = true;
                        }
                    }
                    else if (to.IsOccupied && to.Piece.Color != from.Piece.Color &&
                        movingPlayer.Color == from.Piece.Color && symbol == from.Piece.Symbol)
                    {
                        if (from.Piece.Take(toPos, this.Matrix))
                        {
                            // Assign new values to squares
                            this.Matrix[toRow][toCol].Piece = this.Matrix[fromRow][fromCol].Piece;
                            this.Matrix[fromRow][fromCol].Piece = emptyFigure;

                            // Calculation of attacked squares in the board
                            this.CalculateAttackedSquares();

                            // Check is the king of the current player is attacked when making the move
                            if (this.IsPlayerChecked(movingPlayer))
                            {
                                // Assigning the new value, Deleting the old drawn figure
                                this.Matrix[toRow][toCol].Position.Y = this.Matrix[fromRow][fromCol].Position.Y;
                                this.Matrix[toRow][toCol].Position.X = this.Matrix[fromRow][fromCol].Position.X;
                                this.Matrix[fromRow][fromCol] = this.Matrix[toRow][toCol];

                                // Assigning the old square
                                this.Matrix[toRow][toCol] = to;

                                movingPlayer.IsCheck = true;
                                this.CalculateAttackedSquares();
                                printer.KingIsCheck(movingPlayer);
                                continue;
                            }
                            else
                            {
                                movingPlayer.IsCheck = false;
                            }

                            // Draw the new figures to FROM and TO squares
                            drawer.NewFigures(fromCol, fromRow, toCol, toRow, this.Matrix[toRow][toCol].Piece);

                            // Clear the check message screen of the other player
                            printer.EmptyCheckScreen(opponent);

                            // Check for pawn promotion
                            if (this.Matrix[toRow][toCol].Piece is Pawn && this.Matrix[toRow][toCol].Piece.IsLastMove)
                            {
                                this.Matrix[toRow][toCol].Piece = drawer.PawnPromotion(toPos, this.Matrix[toRow][toCol].Piece);
                                this.CalculateAttackedSquares();
                            }

                            // Print if the current player check the other player after movement. Check if the player is checkmate.
                            if (this.IsPlayerChecked(opponent))
                            {
                                printer.Check(movingPlayer);
                                this.IsOpponentCheckmate(movingPlayer, opponent, from);
                            }

                            // Update the dictionary with the newly taken figure
                            movingPlayer.TakeFigure(to.Piece.Name);

                            successfulMove = true;
                        }
                    }

                    // Main logic for en passant take
                    if (EnPassant.Turn == Globals.TurnCounter && toPos.X == EnPassant.Position.X && toPos.Y == EnPassant.Position.Y &&
                        from.Piece is Pawn)
                    {
                        // Assign new values to TO square and update row and col because you do not enter take method of pawn. Draw the new figure.
                        this.Matrix[toRow][toCol].Piece = this.Matrix[fromRow][fromCol].Piece;
                        drawer.Figure(toRow, toCol, this.Matrix[toRow][toCol].Piece);

                        // Assign empty square to FROM. Draw the empty square.
                        this.Matrix[fromRow][fromCol].Piece = emptyFigure;
                        drawer.EmptySquare(fromRow, fromCol);

                        // Assign empty to the third square where is figure. Draw the empty square.
                        int colCheck = toCol > fromCol ? 1 : -1;
                        this.Matrix[fromRow][fromCol + colCheck].Piece = emptyFigure;
                        drawer.EmptySquare(fromRow, fromCol + colCheck);

                        // Calculation of attacked squares in the board
                        this.CalculateAttackedSquares();

                        // Print if the current player check the other player after movement. Check if the player is checkmate.
                        if (this.IsPlayerChecked(opponent))
                        {
                            printer.Check(movingPlayer);
                            this.IsOpponentCheckmate(movingPlayer, opponent, from);
                        }

                        successfulMove = true;
                    }

                    // Print invalid message if the move is unsuccessful
                    if (!successfulMove)
                    {
                        printer.Invalid(movingPlayer);
                    }
                }
                catch (Exception)
                {
                    printer.Invalid(movingPlayer);
                    continue;
                }
            }

            // Check for stalemate
            this.IsGameStalemate(opponent);

            // Clear the message screen
            printer.EmptyMessageScreen(movingPlayer);
        }

        public void Initialize()
        {
            var toggle = Color.Light;

            for (int row = 0; row < Globals.BoardRows; row++)
            {
                for (int col = 0; col < Globals.BoardCols; col++)
                {
                    var name = this.letters[col] + (8 - row);
                    var square = new Square()
                    {
                        Position = Factory.GetPosition(row, col),
                        Piece = this.setup.FirstOrDefault(x => x.Key.Equals(name)).Value,
                        Color = toggle,
                        Name = name,
                    };

                    if (square.Piece == null)
                    {
                        square.Piece = Factory.GetEmpty();
                        square.IsOccupied = false;
                    }

                    if (col != 7)
                    {
                        toggle = toggle == Color.Light ? Color.Dark : Color.Light;
                    }

                    this.Matrix[row][col] = square;
                }
            }
        }

        public object Clone()
        {
            var board = Factory.GetBoard();

            for (int row = 0; row <= 7; row++)
            {
                for (int col = 0; col <= 7; col++)
                {
                    board.Matrix[row][col] = this.Matrix[row][col].Clone() as Square;
                }
            }

            return board;
        }

        private bool IsPlayerChecked(Player player)
        {
            var kingSquare = this.GetKingSquare(player.Color);

            if (kingSquare.IsAttacked.Where(x => x.Color != kingSquare.Piece.Color).Any())
            {
                return true;
            }

            return false;
        }

        private void IsOpponentCheckmate(Player movingPlayer, Player opponent, Square attackingSquare)
        {
            var king = this.GetKingSquare(opponent.Color);

            if (this.IsKingAbleToMove(king, movingPlayer) ||
                this.AttackingPieceCanBeTaken(attackingSquare, opponent) ||
                this.OtherPieceCanBlockTheCheck(king, attackingSquare, opponent))
            {
                Globals.GameOver = GameOver.None;
            }
            else
            {
                Globals.GameOver = GameOver.Checkmate;
            }
        }

        private void IsGameStalemate(Player player)
        {
            for (int y = 0; y < Globals.BoardRows; y++)
            {
                for (int x = 0; x < Globals.BoardCols; x++)
                {
                    var currentFigure = this.Matrix[y][x].Piece;

                    if (currentFigure.Color == player.Color)
                    {
                        currentFigure.IsMoveAvailable(this.Matrix);
                        if (currentFigure.IsMovable)
                        {
                            Globals.GameOver = GameOver.None;
                            return;
                        }
                    }
                }
            }

            Globals.GameOver = GameOver.Stalemate;
        }

        private void CalculateAttackedSquares()
        {
            for (int y = 0; y < Globals.BoardRows; y++)
            {
                for (int x = 0; x < Globals.BoardCols; x++)
                {
                    this.Matrix[y][x].IsAttacked.Clear();
                }
            }

            for (int y = 0; y < Globals.BoardRows; y++)
            {
                for (int x = 0; x < Globals.BoardCols; x++)
                {
                    if (this.Matrix[y][x].IsOccupied == true)
                    {
                        this.Matrix[y][x].Piece.Attacking(this.Matrix);
                    }
                }
            }
        }

        private Square GetKingSquare(Color color)
        {
            for (int y = 0; y < Globals.BoardRows; y++)
            {
                var kingSquare = this.Matrix[y].FirstOrDefault(x => x.Piece is King && x.Piece.Color == color);

                if (kingSquare != null)
                {
                    return kingSquare;
                }
            }

            return null;
        }

        #region IsOpponentCheckmate Methods
        private bool IsKingAbleToMove(Square king, Player movingPlayer)
        {
            int kingY = king.Position.Y;
            int kingX = king.Position.X;

            for (int y = -1; y <= 1; y++)
            {
                for (int x = -1; x <= 1; x++)
                {
                    if (y == 0 && x == 0)
                    {
                        continue;
                    }

                    if (Position.IsInBoard(kingY + y, kingX + x))
                    {
                        var checkedSquare = this.Matrix[kingY + y][kingX + x];

                        if (this.NeighbourSquareAvailable(checkedSquare, movingPlayer))
                        {
                            var currentFigure = this.Matrix[kingY][kingX].Piece;
                            var empty = Factory.GetEmpty();

                            this.AssignNewValuesAndCalculate(kingY, kingX, y, x, currentFigure, empty);

                            if (!this.Matrix[kingY + y][kingX + x].IsAttacked.Where(k => k.Color == movingPlayer.Color).Any())
                            {
                                this.AssignOldValuesAndCalculate(kingY, kingX, y, x, currentFigure, empty);
                                return true;
                            }

                            this.AssignOldValuesAndCalculate(kingY, kingX, y, x, currentFigure, empty);
                        }
                    }
                }
            }

            return false;
        }

        private bool AttackingPieceCanBeTaken(Square attackingSquare, Player opponent)
        {
            if (attackingSquare.IsAttacked.Where(x => x.Color == opponent.Color).Any())
            {
                if (attackingSquare.IsAttacked.Count(x => x.Color == opponent.Color) > 1)
                {
                    return true;
                }
                else if (!(attackingSquare.IsAttacked.Where(x => x.Color == opponent.Color).First() is King))
                {
                    return true;
                }
            }

            return false;
        }

        private bool OtherPieceCanBlockTheCheck(Square king, Square attackingSquare, Player opponent)
        {
            if (!(attackingSquare.Piece is Knight) && !(attackingSquare.Piece is Pawn))
            {
                int kingY = king.Position.Y;
                int kingX = king.Position.X;

                int attackingRow = attackingSquare.Position.Y;
                int attackingCol = attackingSquare.Position.X;

                if (attackingRow == kingY)
                {
                    int difference = Math.Abs(attackingCol - kingX) - 1;

                    for (int i = 1; i <= difference; i++)
                    {
                        int sign = attackingCol - kingX < 0 ? i : -i;

                        if (this.Matrix[kingY][attackingCol + sign].IsAttacked.Where(x => x.Color == opponent.Color).Any())
                        {
                            if (this.Matrix[kingY][attackingCol + sign].IsAttacked.Count(x => x.Color == opponent.Color) > 1)
                            {
                                return true;
                            }
                            else
                            {
                                if (!(this.Matrix[kingY][attackingCol + sign].IsAttacked.Where(x => x.Color == opponent.Color).First() is King))
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }

                if (attackingCol == kingX)
                {
                    int difference = Math.Abs(attackingRow - kingY) - 1;

                    for (int i = 1; i <= difference; i++)
                    {
                        int sign = attackingRow - kingY < 0 ? i : -i;

                        if (this.Matrix[attackingRow + sign][kingX].IsAttacked.Where(x => x.Color == opponent.Color).Any())
                        {
                            if (this.Matrix[kingY + sign][attackingCol].IsAttacked.Count(x => x.Color == opponent.Color) > 1)
                            {
                                return true;
                            }
                            else
                            {
                                if (!(this.Matrix[kingY + sign][attackingCol].IsAttacked.Where(x => x.Color == opponent.Color).First() is King))
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }

                if (attackingRow != kingY && attackingCol != kingX)
                {
                    int difference = Math.Abs(attackingRow - kingY) - 1;

                    for (int i = 1; i <= difference; i++)
                    {
                        int signRow = attackingRow - kingY < 0 ? i : -i;
                        int signCol = attackingCol - kingX < 0 ? i : -i;

                        if (this.Matrix[attackingRow + signRow][kingX + signCol].IsAttacked.Where(x => x.Color == opponent.Color).Any())
                        {
                            if (this.Matrix[attackingRow + signRow][attackingCol + signCol].IsAttacked.Count(x => x.Color == opponent.Color) > 1)
                            {
                                return true;
                            }
                            else
                            {
                                if (!(this.Matrix[attackingRow + signRow][attackingCol + signCol].IsAttacked.Where(x => x.Color == opponent.Color).First() is King))
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }

            return false;
        }

        private bool NeighbourSquareAvailable(Square square, Player movingPlayer)
        {
            if (square.IsOccupied &&
                square.Piece.Color == movingPlayer.Color &&
                !square.IsAttacked.Where(x => x.Color == movingPlayer.Color).Any())
            {
                return true;
            }

            if (!square.IsOccupied &&
                !square.IsAttacked.Where(x => x.Color == movingPlayer.Color).Any())
            {
                return true;
            }

            return false;
        }

        private void AssignNewValuesAndCalculate(int kingRow, int kingCol, int i, int k, IPiece currentFigure, IPiece empty)
        {
            this.Matrix[kingRow][kingCol].Piece = empty;
            this.Matrix[kingRow][kingCol].IsOccupied = false;
            this.Matrix[kingRow + i][kingCol + k].Piece = currentFigure;
            this.Matrix[kingRow + i][kingCol + k].IsOccupied = true;
            this.CalculateAttackedSquares();
        }

        private void AssignOldValuesAndCalculate(int kingRow, int kingCol, int i, int k, IPiece currentFigure, IPiece empty)
        {
            this.Matrix[kingRow][kingCol].Piece = currentFigure;
            this.Matrix[kingRow][kingCol].IsOccupied = true;
            this.Matrix[kingRow + i][kingCol + k].Piece = empty;
            this.Matrix[kingRow + i][kingCol + k].IsOccupied = false;
            this.CalculateAttackedSquares();
        }
        #endregion
    }
}
