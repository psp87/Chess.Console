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
        private Dictionary<string, int> ColMap = new Dictionary<string, int>()
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
                    int fromCol = this.ColMap[match.Groups[2].ToString().ToUpper()];
                    int fromRow = Math.Abs(int.Parse(match.Groups[3].ToString()) - 8);
                    int toCol = this.ColMap[match.Groups[4].ToString().ToUpper()];
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
                                Print.KingIsCheck(movingPlayer);
                                continue;
                            }
                            else
                            {
                                movingPlayer.IsCheck = false;
                            }

                            // Draw the new figures to FROM and TO squares
                            Draw.NewFigures(fromCol, fromRow, toCol, toRow, this.Matrix[toRow][toCol].Piece);

                            // Clear the check message screen of the other player
                            Print.EmptyCheckScreen(opponent);

                            // Check for pawn promotion
                            if (this.Matrix[toRow][toCol].Piece is Pawn && this.Matrix[toRow][toCol].Piece.IsLastMove)
                            {
                                this.Matrix[toRow][toCol].Piece = Print.PawnPromotion(toPos, this.Matrix[toRow][toCol].Piece);
                                this.CalculateAttackedSquares();
                            }

                            // Print if the current player check the other player after movement. Check if the player is checkmate.
                            if (this.IsPlayerChecked(opponent))
                            {
                                Print.Check(movingPlayer);
                                this.IsOpponentCheckmate(movingPlayer, from, opponent);
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
                                Print.KingIsCheck(movingPlayer);
                                continue;
                            }
                            else
                            {
                                movingPlayer.IsCheck = false;
                            }

                            // Draw the new figures to FROM and TO squares
                            Draw.NewFigures(fromCol, fromRow, toCol, toRow, this.Matrix[toRow][toCol].Piece);

                            // Clear the check message screen of the other player
                            Print.EmptyCheckScreen(opponent);

                            // Check for pawn promotion
                            if (this.Matrix[toRow][toCol].Piece is Pawn && this.Matrix[toRow][toCol].Piece.IsLastMove)
                            {
                                this.Matrix[toRow][toCol].Piece = Print.PawnPromotion(toPos, this.Matrix[toRow][toCol].Piece);
                                this.CalculateAttackedSquares();
                            }

                            // Print if the current player check the other player after movement. Check if the player is checkmate.
                            if (this.IsPlayerChecked(opponent))
                            {
                                Print.Check(movingPlayer);
                                this.IsOpponentCheckmate(movingPlayer, from, opponent);
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
                        Draw.Figure(toRow, toCol, this.Matrix[toRow][toCol].Piece);

                        // Assign empty square to FROM. Draw the empty square.
                        this.Matrix[fromRow][fromCol].Piece = emptyFigure;
                        Draw.EmptySquare(fromRow, fromCol);

                        // Assign empty to the third square where is figure. Draw the empty square.
                        int colCheck = toCol > fromCol ? 1 : -1;
                        this.Matrix[fromRow][fromCol + colCheck].Piece = emptyFigure;
                        Draw.EmptySquare(fromRow, fromCol + colCheck);

                        // Calculation of attacked squares in the board
                        this.CalculateAttackedSquares();

                        // Print if the current player check the other player after movement. Check if the player is checkmate.
                        if (this.IsPlayerChecked(opponent))
                        {
                            Print.Check(movingPlayer);
                            this.IsOpponentCheckmate(movingPlayer, from, opponent);
                        }

                        successfulMove = true;
                    }

                    // Print invalid message if the move is unsuccessful
                    if (!successfulMove)
                    {
                        Print.Invalid(movingPlayer);
                    }
                }
                catch (Exception)
                {
                    Print.Invalid(movingPlayer);
                    continue;
                }
            }

            // Check for stalemate
            this.IsGameStalemate(opponent);

            // Clear the message screen
            Print.EmptyMessageScreen(movingPlayer);
        }

        public void IsOpponentCheckmate(Player currentPlayer, Square attackingSquare, Player otherPlayer)
        {
            var king = this.GetKingSquare(otherPlayer.Color);

            int kingRow = king.Position.Y;
            int kingCol = king.Position.X;

            // To take attacking figure check
            if (attackingSquare.IsAttacked.Where(x => x.Color == otherPlayer.Color).Any())
            {
                if (attackingSquare.IsAttacked.Count(x => x.Color == otherPlayer.Color) > 1)
                {
                    //otherPlayer.IsCheckmate = false;
                    Globals.GameOver = GameOver.None;
                    return;
                }
                else
                {
                    if (!(attackingSquare.IsAttacked.Where(x => x.Color == otherPlayer.Color).First() is King))
                    {
                        //otherPlayer.IsCheckmate = false;
                        Globals.GameOver = GameOver.None;
                        return;
                    }
                }
            }

            // To move king check
            for (int i = -1; i <= 1; i++)
            {
                for (int k = -1; k <= 1; k++)
                {
                    if (i == 0 && k == 0)
                    {
                        continue;
                    }

                    if (Position.IsInBoard(kingRow + i, kingCol + k))
                    {
                        var checkedSquare = this.Matrix[kingRow + i][kingCol + k];

                        if (this.NeighbourSquareAvailable(checkedSquare, currentPlayer))
                        {
                            IPiece currentFigure = this.Matrix[kingRow][kingCol].Piece;
                            IPiece empty = Factory.GetEmpty();

                            this.AssignNewValuesAndCalculate(kingRow, kingCol, i, k, currentFigure, empty);

                            if (!this.Matrix[kingRow + i][kingCol + k].IsAttacked.Where(x => x.Color == currentPlayer.Color).Any())
                            {
                                this.AssignOldValuesAndCalculate(kingRow, kingCol, i, k, currentFigure, empty);
                                Globals.GameOver = GameOver.None;
                                return;
                            }

                            this.AssignOldValuesAndCalculate(kingRow, kingCol, i, k, currentFigure, empty);
                        }
                    }
                }
            }

            // To move other figure check
            if (!(attackingSquare.Piece is Knight) && !(attackingSquare.Piece is Pawn))
            {
                int attackingRow = attackingSquare.Position.Y;
                int attackingCol = attackingSquare.Position.X;

                if (attackingRow == kingRow)
                {
                    int difference = Math.Abs(attackingCol - kingCol) - 1;

                    for (int i = 1; i <= difference; i++)
                    {
                        int sign = attackingCol - kingCol < 0 ? i : -i;

                        if (this.Matrix[kingRow][attackingCol + sign].IsAttacked.Where(x => x.Color == otherPlayer.Color).Any())
                        {
                            if (this.Matrix[kingRow][attackingCol + sign].IsAttacked.Count(x => x.Color == otherPlayer.Color) > 1)
                            {
                                Globals.GameOver = GameOver.None;
                                return;
                            }
                            else
                            {
                                if (!(this.Matrix[kingRow][attackingCol + sign].IsAttacked.Where(x => x.Color == otherPlayer.Color).First() is King))
                                {
                                    Globals.GameOver = GameOver.None;
                                    return;
                                }
                            }
                        }
                    }
                }

                if (attackingCol == kingCol)
                {
                    int difference = Math.Abs(attackingRow - kingRow) - 1;

                    for (int i = 1; i <= difference; i++)
                    {
                        int sign = attackingRow - kingRow < 0 ? i : -i;

                        if (this.Matrix[attackingRow + sign][kingCol].IsAttacked.Where(x => x.Color == otherPlayer.Color).Any())
                        {
                            if (this.Matrix[kingRow + sign][attackingCol].IsAttacked.Count(x => x.Color == otherPlayer.Color) > 1)
                            {
                                Globals.GameOver = GameOver.None;
                                return;
                            }
                            else
                            {
                                if (!(this.Matrix[kingRow + sign][attackingCol].IsAttacked.Where(x => x.Color == otherPlayer.Color).First() is King))
                                {
                                    Globals.GameOver = GameOver.None;
                                    return;
                                }
                            }
                        }
                    }
                }

                if (attackingRow != kingRow && attackingCol != kingCol)
                {
                    int difference = Math.Abs(attackingRow - kingRow) - 1;

                    for (int i = 1; i <= difference; i++)
                    {
                        int signRow = attackingRow - kingRow < 0 ? i : -i;
                        int signCol = attackingCol - kingCol < 0 ? i : -i;

                        if (this.Matrix[attackingRow + signRow][kingCol + signCol].IsAttacked.Where(x => x.Color == otherPlayer.Color).Any())
                        {
                            if (this.Matrix[attackingRow + signRow][attackingCol + signCol].IsAttacked.Count(x => x.Color == otherPlayer.Color) > 1)
                            {
                                Globals.GameOver = GameOver.None;
                                return;
                            }
                            else
                            {
                                if (!(this.Matrix[attackingRow + signRow][attackingCol + signCol].IsAttacked.Where(x => x.Color == otherPlayer.Color).First() is King))
                                {
                                    Globals.GameOver = GameOver.None;
                                    return;
                                }
                            }
                        }
                    }
                }
            }

            Globals.GameOver = GameOver.Checkmate;
        }

        private void AssignOldValuesAndCalculate(int kingRow, int kingCol, int i, int k, IPiece currentFigure, IPiece empty)
        {
            this.Matrix[kingRow][kingCol].Piece = currentFigure;
            this.Matrix[kingRow][kingCol].IsOccupied = true;
            this.Matrix[kingRow + i][kingCol + k].Piece = empty;
            this.Matrix[kingRow + i][kingCol + k].IsOccupied = false;
            this.CalculateAttackedSquares();
        }

        private void AssignNewValuesAndCalculate(int kingRow, int kingCol, int i, int k, IPiece currentFigure, IPiece empty)
        {
            this.Matrix[kingRow][kingCol].Piece = empty;
            this.Matrix[kingRow][kingCol].IsOccupied = false;
            this.Matrix[kingRow + i][kingCol + k].Piece = currentFigure;
            this.Matrix[kingRow + i][kingCol + k].IsOccupied = true;
            this.CalculateAttackedSquares();
        }

        private bool NeighbourSquareAvailable(Square square, Player currentPlayer)
        {
            if (square.IsOccupied &&
                square.Piece.Color == currentPlayer.Color &&
                !square.IsAttacked.Where(x => x.Color == currentPlayer.Color).Any())
            {
                return true;
            }

            if (!square.IsOccupied &&
                !square.IsAttacked.Where(x => x.Color == currentPlayer.Color).Any())
            {
                return true;
            }

            return false;
        }





        public void IsGameStalemate(Player player)
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
                            break;
                        }
                    }
                }
            }

            Globals.GameOver = GameOver.Stalemate;
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

        private bool IsPlayerChecked(Player player)
        {
            var kingSquare = this.GetKingSquare(player.Color);

            if (kingSquare.IsAttacked.Where(x => x.Color != kingSquare.Piece.Color).Any())
            {
                return true;
            }

            return false;
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
    }
}
