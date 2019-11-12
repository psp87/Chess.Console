namespace Chess.Models.Board
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Contracts;
    using Enums;
    using Figures;
    using Figures.Contracts;
    using Player.Contracts;
    using Square.Contracts;

    public class Board : IBoard
    {
        public Board()
        {
            this.Matrix = Factory.GetMatrix(this.Matrix);
            this.MatrixInitializeAssign();
        }

        public ISquare[][] Matrix { get; set; }

        public static bool InBoardCheck(int row, int col)
        {
            if (row >= 0 && row <= 7 && col >= 0 && col <= 7)
            {
                return true;
            }

            return false;
        }

        public void MoveFigure(IPlayer currentPlayer, IPlayer otherPlayer)
        {
            Globals.TurnCounter++;

            IFigure emptyFigure = Factory.GetEmpty();

            bool successfulMove = false;

            while (!successfulMove || currentPlayer.isCheck == true)
            {
                try
                {
                    string text = Console.ReadLine();

                    string pattern = @"([A-Za-z])([A-Za-z])([1-8])([A-Za-z])([1-8])";
                    Regex regex = new Regex(pattern);
                    Match match = regex.Match(text);

                    char symbol = char.Parse(match.Groups[1].ToString().ToUpper());
                    int fromCol = Globals.ColMap[match.Groups[2].ToString().ToUpper()];
                    int fromRow = Math.Abs(int.Parse(match.Groups[3].ToString()) - 8);
                    int toCol = Globals.ColMap[match.Groups[4].ToString().ToUpper()];
                    int toRow = Math.Abs(int.Parse(match.Groups[5].ToString()) - 8);

                    var fromSquare = this.Matrix[fromRow][fromCol];
                    var toSquare = this.Matrix[toRow][toCol];

                    var tempSquare = this.Matrix[toRow][toCol];

                    if (!toSquare.IsOccupied && currentPlayer.Color == fromSquare.Figure.Color && symbol == fromSquare.Figure.Symbol)
                    {
                        if (fromSquare.Figure.Move(this.Matrix, this.Matrix[fromRow][fromCol], this.Matrix[fromRow][fromCol].Figure, (Row)toRow, (Col)toCol))
                        {
                            this.AssignNewValues(emptyFigure, fromCol, fromRow, toCol, toRow);
                            this.AttackedSquares();

                            if (this.IsKingAttacked(currentPlayer, fromCol, fromRow, toCol, toRow, tempSquare))
                            {
                                currentPlayer.isCheck = true;
                                this.AttackedSquares();
                                Print.KingIsCheck(currentPlayer);
                                continue;
                            }
                            currentPlayer.isCheck = false;

                            this.DrawNewFigures(fromCol, fromRow, toCol, toRow);

                            if (this.Matrix[toRow][toCol].Figure is Pawn && this.Matrix[toRow][toCol].Figure.IsLastMove)
                            {
                                this.Matrix[toRow][toCol].Figure = Pawn.Promotion(toRow, toCol, this.Matrix[toRow][toCol].Figure);
                                this.AttackedSquares();
                            }

                            if (IsCheck(currentPlayer, otherPlayer))
                            {
                                Print.CheckMessage(currentPlayer);

                                this.Checkmate(currentPlayer, Check.KingRow, Check.KingCol, this.Matrix[Check.AttackingRow][Check.AttackingCol], otherPlayer);
                            }

                            successfulMove = true;
                        }
                    }

                    if (toSquare.IsOccupied && toSquare.Figure.Color != fromSquare.Figure.Color &&
                        currentPlayer.Color == fromSquare.Figure.Color && symbol == fromSquare.Figure.Symbol)
                    {
                        if (fromSquare.Figure.Take(this.Matrix, this.Matrix[fromRow][fromCol], this.Matrix[fromRow][fromCol].Figure, (Row)toRow, (Col)toCol))
                        {
                            this.AssignNewValues(emptyFigure, fromCol, fromRow, toCol, toRow);
                            this.AttackedSquares();

                            if (this.IsKingAttacked(currentPlayer, fromCol, fromRow, toCol, toRow, tempSquare))
                            {
                                currentPlayer.isCheck = true;
                                this.AttackedSquares();
                                Print.KingIsCheck(currentPlayer);
                                continue;
                            }
                            currentPlayer.isCheck = false;

                            this.DrawNewFigures(fromCol, fromRow, toCol, toRow);

                            if (this.Matrix[toRow][toCol].Figure is Pawn && this.Matrix[toRow][toCol].Figure.IsLastMove)
                            {
                                this.Matrix[toRow][toCol].Figure = Pawn.Promotion(toRow, toCol, this.Matrix[toRow][toCol].Figure);
                                this.AttackedSquares();
                            }

                            if (IsCheck(currentPlayer, otherPlayer))
                            {
                                Print.CheckMessage(currentPlayer);

                                this.Checkmate(currentPlayer, Check.KingRow, Check.KingCol, this.Matrix[Check.AttackingRow][Check.AttackingCol], otherPlayer);
                            }

                            successfulMove = true;

                            currentPlayer.TakeFigure(tempSquare.Figure.Name);
                        }
                    }

                    if (EnPassant.Turn == Globals.TurnCounter && fromSquare.Figure is Pawn && toRow == EnPassant.Row && toCol == EnPassant.Col)
                    {
                        this.Matrix[toRow][toCol] = this.Matrix[fromRow][fromCol];
                        this.Matrix[toRow][toCol].Row = (Row)toRow;
                        this.Matrix[toRow][toCol].Col = (Col)toCol;
                        Draw.Figure(toRow, toCol, this.Matrix[toRow][toCol].Figure);

                        this.Matrix[fromRow][fromCol] = Factory.GetSquare((Row)fromRow, (Col)fromCol, emptyFigure);
                        Draw.EmptySquare(fromRow, fromCol);

                        int colCheck = toCol > fromCol ? 1 : -1;
                        this.Matrix[fromRow][fromCol + colCheck] = Factory.GetSquare((Row)fromRow, (Col)fromCol, emptyFigure);
                        this.Matrix[fromRow][fromCol + colCheck].Row = (Row)fromRow;
                        this.Matrix[fromRow][fromCol + colCheck].Col = (Col)fromCol;
                        Draw.EmptySquare(fromRow, fromCol + colCheck);

                        this.AttackedSquares();
                        successfulMove = true;
                    }

                    if (!successfulMove)
                    {
                        Print.InvalidMessage(currentPlayer);
                    }

                    Paint.DefaultColor();
                }
                catch (Exception)
                {
                    continue;
                }
            }
        }

        private bool Stalemate(IPlayer player)
        {
            throw new NotImplementedException();
        }

        private void Checkmate(IPlayer playerMoving, int kingRow, int kingCol, ISquare attackingSquare, IPlayer otherPlayer)
        {
            // To take attacking figure check
            if (attackingSquare.IsAttacked.Where(x => x.Figure.Color == otherPlayer.Color).Any())
            {
                if (!(attackingSquare.IsAttacked.Where(x => x.Figure.Color == otherPlayer.Color) is King))
                {
                    return;
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

                    if (InBoardCheck(kingRow + i, kingCol + k))
                    {
                        var checkedSquare = this.Matrix[kingRow + i][kingCol + k];

                        if (this.NeighbourSquareAvailable(checkedSquare, playerMoving))
                        {
                            return;
                        }
                    }
                }
            }

            // To move other figure check
            if (!(attackingSquare.Figure is Knight) && !(attackingSquare.Figure is Pawn))
            {
                if ((int)attackingSquare.Row == kingRow)
                {
                    int difference = Math.Abs((int)attackingSquare.Col - kingCol) - 1;

                    if ((int)attackingSquare.Col - kingCol < 0)
                    {
                        for (int i = 1; i <= difference; i++)
                        {
                            if (this.Matrix[kingRow][(int)attackingSquare.Col + i].IsAttacked.Where(x => x.Figure.Color == playerMoving.Color).Any())
                            {
                                return;
                            }
                        }
                    }
                    else
                    {
                        for (int i = 1; i <= difference; i++)
                        {
                            if (this.Matrix[kingRow][(int)attackingSquare.Col - i].IsAttacked.Where(x => x.Figure.Color == playerMoving.Color).Any())
                            {
                                return;
                            }
                        }
                    }
                }
                else if ((int)attackingSquare.Col == kingCol)
                {
                    int difference = Math.Abs((int)attackingSquare.Row - kingRow) - 1;

                    if ((int)attackingSquare.Row - kingRow < 0)
                    {
                        for (int i = 1; i <= difference; i++)
                        {
                            if (this.Matrix[kingRow + i][(int)attackingSquare.Col].IsAttacked.Where(x => x.Figure.Color == playerMoving.Color).Any())
                            {
                                return;
                            }
                        }
                    }
                    else
                    {
                        for (int i = 1; i <= difference; i++)
                        {
                            if (this.Matrix[kingRow + i][(int)attackingSquare.Col].IsAttacked.Where(x => x.Figure.Color == playerMoving.Color).Any())
                            {
                                return;
                            }
                        }
                    }
                }
                //else
                //{

                //}
            }

            otherPlayer.isCheckmate = true;
        }

        private bool NeighbourSquareAvailable(ISquare square, IPlayer player)
        {
            if ((square.IsOccupied == true &&
                square.Figure.Color != player.Color &&
                !square.IsAttacked.Where(x => x.Figure.Color != player.Color).Any()) ||
                (square.IsOccupied == false &&
                !square.IsAttacked.Where(x => x.Figure.Color != player.Color).Any()))
            {
                return true;
            }

            return false;
        }

        public void NewGame()
        {
            this.FiguresInitializeAssign();
            Draw.NewGame(this.Matrix);
        }

        public void AttackedSquares()
        {
            for (int row = 0; row < Globals.BoardRows; row++)
            {
                for (int col = 0; col < Globals.BoardCols; col++)
                {
                    this.Matrix[row][col].IsAttacked.Clear();
                }
            }

            for (int row = 0; row < Globals.BoardRows; row++)
            {
                for (int col = 0; col < Globals.BoardCols; col++)
                {
                    if (this.Matrix[row][col].IsOccupied == true)
                    {
                        this.Matrix[row][col].Figure.Attacking(this.Matrix, this.Matrix[row][col], row, col);
                    }
                }
            }
        }

        private void MatrixInitializeAssign()
        {
            IFigure empty = Factory.GetEmpty();

            ISquare square1 = Factory.GetSquare(Row.One, Col.A, empty);
            ISquare square2 = Factory.GetSquare(Row.One, Col.B, empty);
            ISquare square3 = Factory.GetSquare(Row.One, Col.C, empty);
            ISquare square4 = Factory.GetSquare(Row.One, Col.D, empty);
            ISquare square5 = Factory.GetSquare(Row.One, Col.E, empty);
            ISquare square6 = Factory.GetSquare(Row.One, Col.F, empty);
            ISquare square7 = Factory.GetSquare(Row.One, Col.G, empty);
            ISquare square8 = Factory.GetSquare(Row.One, Col.H, empty);

            ISquare square9 = Factory.GetSquare(Row.Two, Col.A, empty);
            ISquare square10 = Factory.GetSquare(Row.Two, Col.B, empty);
            ISquare square11 = Factory.GetSquare(Row.Two, Col.C, empty);
            ISquare square12 = Factory.GetSquare(Row.Two, Col.D, empty);
            ISquare square13 = Factory.GetSquare(Row.Two, Col.E, empty);
            ISquare square14 = Factory.GetSquare(Row.Two, Col.F, empty);
            ISquare square15 = Factory.GetSquare(Row.Two, Col.G, empty);
            ISquare square16 = Factory.GetSquare(Row.Two, Col.H, empty);

            ISquare square17 = Factory.GetSquare(Row.Three, Col.A, empty);
            ISquare square18 = Factory.GetSquare(Row.Three, Col.B, empty);
            ISquare square19 = Factory.GetSquare(Row.Three, Col.C, empty);
            ISquare square20 = Factory.GetSquare(Row.Three, Col.D, empty);
            ISquare square21 = Factory.GetSquare(Row.Three, Col.E, empty);
            ISquare square22 = Factory.GetSquare(Row.Three, Col.F, empty);
            ISquare square23 = Factory.GetSquare(Row.Three, Col.G, empty);
            ISquare square24 = Factory.GetSquare(Row.Three, Col.H, empty);

            ISquare square25 = Factory.GetSquare(Row.Four, Col.A, empty);
            ISquare square26 = Factory.GetSquare(Row.Four, Col.B, empty);
            ISquare square27 = Factory.GetSquare(Row.Four, Col.C, empty);
            ISquare square28 = Factory.GetSquare(Row.Four, Col.D, empty);
            ISquare square29 = Factory.GetSquare(Row.Four, Col.E, empty);
            ISquare square30 = Factory.GetSquare(Row.Four, Col.F, empty);
            ISquare square31 = Factory.GetSquare(Row.Four, Col.G, empty);
            ISquare square32 = Factory.GetSquare(Row.Four, Col.H, empty);

            ISquare square33 = Factory.GetSquare(Row.Five, Col.A, empty);
            ISquare square34 = Factory.GetSquare(Row.Five, Col.B, empty);
            ISquare square35 = Factory.GetSquare(Row.Five, Col.C, empty);
            ISquare square36 = Factory.GetSquare(Row.Five, Col.D, empty);
            ISquare square37 = Factory.GetSquare(Row.Five, Col.E, empty);
            ISquare square38 = Factory.GetSquare(Row.Five, Col.F, empty);
            ISquare square39 = Factory.GetSquare(Row.Five, Col.G, empty);
            ISquare square40 = Factory.GetSquare(Row.Five, Col.H, empty);

            ISquare square41 = Factory.GetSquare(Row.Six, Col.A, empty);
            ISquare square42 = Factory.GetSquare(Row.Six, Col.B, empty);
            ISquare square43 = Factory.GetSquare(Row.Six, Col.C, empty);
            ISquare square44 = Factory.GetSquare(Row.Six, Col.D, empty);
            ISquare square45 = Factory.GetSquare(Row.Six, Col.E, empty);
            ISquare square46 = Factory.GetSquare(Row.Six, Col.F, empty);
            ISquare square47 = Factory.GetSquare(Row.Six, Col.G, empty);
            ISquare square48 = Factory.GetSquare(Row.Six, Col.H, empty);

            ISquare square49 = Factory.GetSquare(Row.Seven, Col.A, empty);
            ISquare square50 = Factory.GetSquare(Row.Seven, Col.B, empty);
            ISquare square51 = Factory.GetSquare(Row.Seven, Col.C, empty);
            ISquare square52 = Factory.GetSquare(Row.Seven, Col.D, empty);
            ISquare square53 = Factory.GetSquare(Row.Seven, Col.E, empty);
            ISquare square54 = Factory.GetSquare(Row.Seven, Col.F, empty);
            ISquare square55 = Factory.GetSquare(Row.Seven, Col.G, empty);
            ISquare square56 = Factory.GetSquare(Row.Seven, Col.H, empty);

            ISquare square57 = Factory.GetSquare(Row.Eight, Col.A, empty);
            ISquare square58 = Factory.GetSquare(Row.Eight, Col.B, empty);
            ISquare square59 = Factory.GetSquare(Row.Eight, Col.C, empty);
            ISquare square60 = Factory.GetSquare(Row.Eight, Col.D, empty);
            ISquare square61 = Factory.GetSquare(Row.Eight, Col.E, empty);
            ISquare square62 = Factory.GetSquare(Row.Eight, Col.F, empty);
            ISquare square63 = Factory.GetSquare(Row.Eight, Col.G, empty);
            ISquare square64 = Factory.GetSquare(Row.Eight, Col.H, empty);

            this.Matrix[7][0] = square1;
            this.Matrix[7][1] = square2;
            this.Matrix[7][2] = square3;
            this.Matrix[7][3] = square4;
            this.Matrix[7][4] = square5;
            this.Matrix[7][5] = square6;
            this.Matrix[7][6] = square7;
            this.Matrix[7][7] = square8;

            this.Matrix[6][0] = square9;
            this.Matrix[6][1] = square10;
            this.Matrix[6][2] = square11;
            this.Matrix[6][3] = square12;
            this.Matrix[6][4] = square13;
            this.Matrix[6][5] = square14;
            this.Matrix[6][6] = square15;
            this.Matrix[6][7] = square16;

            this.Matrix[5][0] = square17;
            this.Matrix[5][1] = square18;
            this.Matrix[5][2] = square19;
            this.Matrix[5][3] = square20;
            this.Matrix[5][4] = square21;
            this.Matrix[5][5] = square22;
            this.Matrix[5][6] = square23;
            this.Matrix[5][7] = square24;

            this.Matrix[4][0] = square25;
            this.Matrix[4][1] = square26;
            this.Matrix[4][2] = square27;
            this.Matrix[4][3] = square28;
            this.Matrix[4][4] = square29;
            this.Matrix[4][5] = square30;
            this.Matrix[4][6] = square31;
            this.Matrix[4][7] = square32;

            this.Matrix[3][0] = square33;
            this.Matrix[3][1] = square34;
            this.Matrix[3][2] = square35;
            this.Matrix[3][3] = square36;
            this.Matrix[3][4] = square37;
            this.Matrix[3][5] = square38;
            this.Matrix[3][6] = square39;
            this.Matrix[3][7] = square40;

            this.Matrix[2][0] = square41;
            this.Matrix[2][1] = square42;
            this.Matrix[2][2] = square43;
            this.Matrix[2][3] = square44;
            this.Matrix[2][4] = square45;
            this.Matrix[2][5] = square46;
            this.Matrix[2][6] = square47;
            this.Matrix[2][7] = square48;

            this.Matrix[1][0] = square49;
            this.Matrix[1][1] = square50;
            this.Matrix[1][2] = square51;
            this.Matrix[1][3] = square52;
            this.Matrix[1][4] = square53;
            this.Matrix[1][5] = square54;
            this.Matrix[1][6] = square55;
            this.Matrix[1][7] = square56;

            this.Matrix[0][0] = square57;
            this.Matrix[0][1] = square58;
            this.Matrix[0][2] = square59;
            this.Matrix[0][3] = square60;
            this.Matrix[0][4] = square61;
            this.Matrix[0][5] = square62;
            this.Matrix[0][6] = square63;
            this.Matrix[0][7] = square64;
        }

        private void FiguresInitializeAssign()
        {
            IFigure whiteRook1 = Factory.GetRook(Color.Light);
            IFigure whiteKnight1 = Factory.GetKnight(Color.Light);
            IFigure whiteBishop1 = Factory.GetBishop(Color.Light);
            IFigure whiteQueen = Factory.GetQueen(Color.Light);
            IFigure whiteKing = Factory.GetKing(Color.Light);
            IFigure whiteBishop2 = Factory.GetBishop(Color.Light);
            IFigure whiteKnight2 = Factory.GetKnight(Color.Light);
            IFigure whiteRook2 = Factory.GetRook(Color.Light);
            IFigure whitePawn1 = Factory.GetPawn(Color.Light);
            IFigure whitePawn2 = Factory.GetPawn(Color.Light);
            IFigure whitePawn3 = Factory.GetPawn(Color.Light);
            IFigure whitePawn4 = Factory.GetPawn(Color.Light);
            IFigure whitePawn5 = Factory.GetPawn(Color.Light);
            IFigure whitePawn6 = Factory.GetPawn(Color.Light);
            IFigure whitePawn7 = Factory.GetPawn(Color.Light);
            IFigure whitePawn8 = Factory.GetPawn(Color.Light);

            IFigure blackRook1 = Factory.GetRook(Color.Dark);
            IFigure blackKnight1 = Factory.GetKnight(Color.Dark);
            IFigure blackBishop1 = Factory.GetBishop(Color.Dark);
            IFigure blackQueen = Factory.GetQueen(Color.Dark);
            IFigure blackKing = Factory.GetKing(Color.Dark);
            IFigure blackBishop2 = Factory.GetBishop(Color.Dark);
            IFigure blackKnight2 = Factory.GetKnight(Color.Dark);
            IFigure blackRook2 = Factory.GetRook(Color.Dark);
            IFigure blackPawn1 = Factory.GetPawn(Color.Dark);
            IFigure blackPawn2 = Factory.GetPawn(Color.Dark);
            IFigure blackPawn3 = Factory.GetPawn(Color.Dark);
            IFigure blackPawn4 = Factory.GetPawn(Color.Dark);
            IFigure blackPawn5 = Factory.GetPawn(Color.Dark);
            IFigure blackPawn6 = Factory.GetPawn(Color.Dark);
            IFigure blackPawn7 = Factory.GetPawn(Color.Dark);
            IFigure blackPawn8 = Factory.GetPawn(Color.Dark);

            this.Matrix[0][0].Figure = blackRook1;
            this.Matrix[0][1].Figure = blackKnight1;
            this.Matrix[0][2].Figure = blackBishop1;
            this.Matrix[0][3].Figure = blackQueen;
            this.Matrix[0][4].Figure = blackKing;
            this.Matrix[0][5].Figure = blackBishop2;
            this.Matrix[0][6].Figure = blackKnight2;
            this.Matrix[0][7].Figure = blackRook2;
            this.Matrix[1][0].Figure = blackPawn1;
            this.Matrix[1][1].Figure = blackPawn2;
            this.Matrix[1][2].Figure = blackPawn3;
            this.Matrix[1][3].Figure = blackPawn4;
            this.Matrix[1][4].Figure = blackPawn5;
            this.Matrix[1][5].Figure = blackPawn6;
            this.Matrix[1][6].Figure = blackPawn7;
            this.Matrix[1][7].Figure = blackPawn8;

            this.Matrix[6][0].Figure = whitePawn1;
            this.Matrix[6][1].Figure = whitePawn2;
            this.Matrix[6][2].Figure = whitePawn3;
            this.Matrix[6][3].Figure = whitePawn4;
            this.Matrix[6][4].Figure = whitePawn5;
            this.Matrix[6][5].Figure = whitePawn6;
            this.Matrix[6][6].Figure = whitePawn7;
            this.Matrix[6][7].Figure = whitePawn8;
            this.Matrix[7][0].Figure = whiteRook1;
            this.Matrix[7][1].Figure = whiteKnight1;
            this.Matrix[7][2].Figure = whiteBishop1;
            this.Matrix[7][3].Figure = whiteQueen;
            this.Matrix[7][4].Figure = whiteKing;
            this.Matrix[7][5].Figure = whiteBishop2;
            this.Matrix[7][6].Figure = whiteKnight2;
            this.Matrix[7][7].Figure = whiteRook2;

            this.Matrix[0][0].IsOccupied = true;
            this.Matrix[0][1].IsOccupied = true;
            this.Matrix[0][2].IsOccupied = true;
            this.Matrix[0][3].IsOccupied = true;
            this.Matrix[0][4].IsOccupied = true;
            this.Matrix[0][5].IsOccupied = true;
            this.Matrix[0][6].IsOccupied = true;
            this.Matrix[0][7].IsOccupied = true;
            this.Matrix[1][0].IsOccupied = true;
            this.Matrix[1][1].IsOccupied = true;
            this.Matrix[1][2].IsOccupied = true;
            this.Matrix[1][3].IsOccupied = true;
            this.Matrix[1][4].IsOccupied = true;
            this.Matrix[1][5].IsOccupied = true;
            this.Matrix[1][6].IsOccupied = true;
            this.Matrix[1][7].IsOccupied = true;

            this.Matrix[6][0].IsOccupied = true;
            this.Matrix[6][1].IsOccupied = true;
            this.Matrix[6][2].IsOccupied = true;
            this.Matrix[6][3].IsOccupied = true;
            this.Matrix[6][4].IsOccupied = true;
            this.Matrix[6][5].IsOccupied = true;
            this.Matrix[6][6].IsOccupied = true;
            this.Matrix[6][7].IsOccupied = true;
            this.Matrix[7][0].IsOccupied = true;
            this.Matrix[7][1].IsOccupied = true;
            this.Matrix[7][2].IsOccupied = true;
            this.Matrix[7][3].IsOccupied = true;
            this.Matrix[7][4].IsOccupied = true;
            this.Matrix[7][5].IsOccupied = true;
            this.Matrix[7][6].IsOccupied = true;
            this.Matrix[7][7].IsOccupied = true;
        }

        private void DrawNewFigures(int fromCol, int fromRow, int toCol, int toRow)
        {
            Draw.EmptySquare(toRow, toCol);
            Draw.Figure(toRow, toCol, this.Matrix[toRow][toCol].Figure);

            Draw.EmptySquare(fromRow, fromCol);
        }

        private void AssignNewValues(IFigure emptyFigure, int fromCol, int fromRow, int toCol, int toRow)
        {
            // Assigning the new value
            this.Matrix[toRow][toCol] = this.Matrix[fromRow][fromCol];

            // Assigning empty to the old square
            this.Matrix[fromRow][fromCol] = Factory.GetSquare((Row)fromRow, (Col)fromCol, emptyFigure);
        }

        private bool IsKingAttacked(IPlayer player, int fromCol, int fromRow, int toCol, int toRow, ISquare tempSquare)
        {
            for (int row = 0; row < Globals.BoardRows; row++)
            {
                for (int col = 0; col < Globals.BoardCols; col++)
                {
                    var currentSquare = this.Matrix[row][col];

                    if (currentSquare.Figure is King && currentSquare.Figure.Color == player.Color
                        && currentSquare.IsAttacked.Where(x => x.Figure.Color != player.Color).Any())
                    {
                        // Assigning the new value, Deleting the old drawn figure
                        this.Matrix[toRow][toCol].Row = this.Matrix[fromRow][fromCol].Row;
                        this.Matrix[toRow][toCol].Col = this.Matrix[fromRow][fromCol].Col;
                        this.Matrix[fromRow][fromCol] = this.Matrix[toRow][toCol];

                        // Assigning the old square
                        this.Matrix[toRow][toCol] = tempSquare;

                        return true;
                    }
                }
            }

            return false;
        }

        private bool IsCheck(IPlayer playerMoving, IPlayer otherPlayer)
        {
            for (int row = 0; row < Globals.BoardRows; row++)
            {
                for (int col = 0; col < Globals.BoardCols; col++)
                {
                    var currentSquare = this.Matrix[row][col];

                    if (currentSquare.Figure is King && currentSquare.Figure.Color == otherPlayer.Color
                        && currentSquare.IsAttacked.Where(x => x.Figure.Color == playerMoving.Color).Any())
                    {
                        Check.KingRow = (int)this.Matrix[row][col].Row;
                        Check.KingCol = (int)this.Matrix[row][col].Col;

                        Check.AttackingRow = (int)currentSquare.IsAttacked.Where(x => x.Figure.Color == otherPlayer.Color).FirstOrDefault().Row;
                        Check.AttackingCol = (int)currentSquare.IsAttacked.Where(x => x.Figure.Color == otherPlayer.Color).FirstOrDefault().Col;

                        return true;
                    }
                }
            }

            return false;
        }
    }
}