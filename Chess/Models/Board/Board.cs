namespace Chess
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using Chess.Models.Board.Contracts;
    using Chess.Models.Figures.Contracts;
    using Chess.Models.Player.Contracts;

    public enum Color { Light, Dark, Empty }

    public enum CoordinateX
    {
        A,
        B,
        C,
        D,
        E,
        F,
        G,
        H
    }

    public enum CoordinateY
    {
        Eight,
        Seven,
        Six,
        Five,
        Four,
        Three,
        Two,
        One
    }

    public class Board : IBoard
    {
        private IFigure[][] squares;

        Dictionary<string, int> colMapping = new Dictionary<string, int>()
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

        public Board()
        {
            this.squares = Factory.GetSquares(squares);
            this.SquaresInitializeAssign(this.squares);
        }

        public void Draw()
        {
            for (int row = 0; row < Globals.BoardRows; row++)
            {
                for (int col = 0; col < Globals.BoardCols; col++)
                {
                    IFigure currentFigure = this.squares[row][col];

                    if ((row + col) % 2 == 0)
                    {
                        Drawer.EmptySquare(row, col);
                        currentFigure.Draw(row, col);
                    }
                    else
                    {
                        Drawer.EmptySquare(row, col);
                        currentFigure.Draw(row, col);
                    }
                }
            }

            Drawer.Border();

            Paint.DefaultColor();

            Console.SetCursorPosition(0, Globals.OffsetVertical + Globals.BoardRows * Globals.CellRows + 4);
            Console.WriteLine();
        }

        public void FigureMove(IPlayer player)
        {
            Globals.TurnCounter++;

            bool successfulMove = false;

            while (!successfulMove)
            {
                string text = Console.ReadLine();

                string pattern = @"([A-Za-z])([A-Za-z])([1-8])([A-Za-z])([1-8])";
                Regex regex = new Regex(pattern);
                Match match = regex.Match(text);

                char symbol = char.Parse(match.Groups[1].ToString().ToUpper());
                int fromCol = colMapping[match.Groups[2].ToString().ToUpper()];
                int fromRow = Math.Abs(int.Parse(match.Groups[3].ToString()) - 8);
                int toCol = colMapping[match.Groups[4].ToString().ToUpper()];
                int toRow = Math.Abs(int.Parse(match.Groups[5].ToString()) - 8);

                IFigure empty = Factory.GetEmpty((CoordinateY)fromRow, (CoordinateX)fromCol);

                if (this.squares[toRow][toCol].IsOccupied == false)
                {
                    if (player.Color == this.squares[fromRow][fromCol].Color && symbol == this.squares[fromRow][fromCol].Symbol)
                    {
                        if (this.squares[fromRow][fromCol].Move(this.squares, (CoordinateY)toRow, (CoordinateX)toCol))
                        {
                            // Assigning the new value, Deleting the old drawn figure, Drawing the new figure
                            this.squares[toRow][toCol] = this.squares[fromRow][fromCol];
                            Drawer.EmptySquare(toRow, toCol);
                            this.squares[toRow][toCol].Draw(toRow, toCol);

                            // Assigning and drawing empty to the old square
                            this.squares[fromRow][fromCol] = empty;
                            Drawer.EmptySquare(fromRow, fromCol);

                            // Check the pawn if it is last move
                            if (this.squares[toRow][toCol] is Pawn)
                            {
                                Pawn.LastMoveCheck(toRow, toCol, squares, player);
                            }

                            successfulMove = true;
                        }
                    }
                }
                else
                {
                    if (this.squares[toRow][toCol].Color != this.squares[fromRow][fromCol].Color)
                    {
                        if (player.Color == this.squares[fromRow][fromCol].Color && symbol == this.squares[fromRow][fromCol].Symbol)
                        {
                            if (this.squares[fromRow][fromCol].Take(this.squares, (CoordinateY)toRow, (CoordinateX)toCol))
                            {
                                player.TakeFigure(this.squares[toRow][toCol].Name);

                                // Assigning the new value, Deleting the old drawn figure, Drawing the new figure
                                this.squares[toRow][toCol] = this.squares[fromRow][fromCol];
                                Drawer.EmptySquare(toRow, toCol);
                                this.squares[toRow][toCol].Draw(toRow, toCol);

                                // Assigning and drawing empty to the old square
                                this.squares[fromRow][fromCol] = empty;
                                Drawer.EmptySquare(fromRow, fromCol);

                                // Check the pawn if it is last move
                                if (this.squares[toRow][toCol] is Pawn)
                                {
                                    Pawn.LastMoveCheck(toRow, toCol, squares, player);
                                }

                                successfulMove = true;
                            }
                        }
                    }
                }

                if (EnPassant.Turn == Globals.TurnCounter && squares[fromRow][fromCol] is Pawn && toRow == EnPassant.Row && toCol == EnPassant.Col)
                {
                    //player.TakenFigures(this.squares[toRow][toCol]);

                    this.squares[toRow][toCol] = this.squares[fromRow][fromCol];
                    this.squares[toRow][toCol].Row = (CoordinateY)toRow;
                    this.squares[toRow][toCol].Col = (CoordinateX)toCol;
                    this.squares[toRow][toCol].Draw(toRow, toCol);

                    this.squares[fromRow][fromCol] = empty;
                    Drawer.EmptySquare(fromRow, fromCol);

                    int colCheck = toCol > fromCol ? 1 : -1;
                    this.squares[fromRow][fromCol + colCheck] = empty;
                    this.squares[fromRow][fromCol + colCheck].Row = (CoordinateY)fromRow;
                    this.squares[fromRow][fromCol + colCheck].Col = (CoordinateX)fromCol;
                    Drawer.EmptySquare(fromRow, fromCol + colCheck);

                    successfulMove = true;
                }

                Paint.DefaultColor();
            }
        }

        public void NewGame()
        {
            IFigure whiteRook1 = Factory.GetRook(Color.Light, CoordinateY.One, CoordinateX.A);
            IFigure whiteKnight1 = Factory.GetKnight(Color.Light, CoordinateY.One, CoordinateX.B);
            IFigure whiteBishop1 = Factory.GetBishop(Color.Light, CoordinateY.One, CoordinateX.C);
            IFigure whiteQueen = Factory.GetQueen(Color.Light, CoordinateY.One, CoordinateX.D);
            IFigure whiteKing = Factory.GetKing(Color.Light, CoordinateY.One, CoordinateX.E);
            IFigure whiteBishop2 = Factory.GetBishop(Color.Light, CoordinateY.One, CoordinateX.F);
            IFigure whiteKnight2 = Factory.GetKnight(Color.Light, CoordinateY.One, CoordinateX.G);
            IFigure whiteRook2 = Factory.GetRook(Color.Light, CoordinateY.One, CoordinateX.H);

            IFigure whitePawn1 = Factory.GetPawn(Color.Light, CoordinateY.Two, CoordinateX.A);
            IFigure whitePawn2 = Factory.GetPawn(Color.Light, CoordinateY.Two, CoordinateX.B);
            IFigure whitePawn3 = Factory.GetPawn(Color.Light, CoordinateY.Two, CoordinateX.C);
            IFigure whitePawn4 = Factory.GetPawn(Color.Light, CoordinateY.Two, CoordinateX.D);
            IFigure whitePawn5 = Factory.GetPawn(Color.Light, CoordinateY.Two, CoordinateX.E);
            IFigure whitePawn6 = Factory.GetPawn(Color.Light, CoordinateY.Two, CoordinateX.F);
            IFigure whitePawn7 = Factory.GetPawn(Color.Light, CoordinateY.Two, CoordinateX.G);
            IFigure whitePawn8 = Factory.GetPawn(Color.Light, CoordinateY.Two, CoordinateX.H);

            IFigure blackRook1 = Factory.GetRook(Color.Dark, CoordinateY.Eight, CoordinateX.A);
            IFigure blackKnight1 = Factory.GetKnight(Color.Dark, CoordinateY.Eight, CoordinateX.B);
            IFigure blackBishop1 = Factory.GetBishop(Color.Dark, CoordinateY.Eight, CoordinateX.C);
            IFigure blackQueen = Factory.GetQueen(Color.Dark, CoordinateY.Eight, CoordinateX.D);
            IFigure blackKing = Factory.GetKing(Color.Dark, CoordinateY.Eight, CoordinateX.E);
            IFigure blackBishop2 = Factory.GetBishop(Color.Dark, CoordinateY.Eight, CoordinateX.F);
            IFigure blackKnight2 = Factory.GetKnight(Color.Dark, CoordinateY.Eight, CoordinateX.G);
            IFigure blackRook2 = Factory.GetRook(Color.Dark, CoordinateY.Eight, CoordinateX.H);

            IFigure blackPawn1 = Factory.GetPawn(Color.Dark, CoordinateY.Seven, CoordinateX.A);
            IFigure blackPawn2 = Factory.GetPawn(Color.Dark, CoordinateY.Seven, CoordinateX.B);
            IFigure blackPawn3 = Factory.GetPawn(Color.Dark, CoordinateY.Seven, CoordinateX.C);
            IFigure blackPawn4 = Factory.GetPawn(Color.Dark, CoordinateY.Seven, CoordinateX.D);
            IFigure blackPawn5 = Factory.GetPawn(Color.Dark, CoordinateY.Seven, CoordinateX.E);
            IFigure blackPawn6 = Factory.GetPawn(Color.Dark, CoordinateY.Seven, CoordinateX.F);
            IFigure blackPawn7 = Factory.GetPawn(Color.Dark, CoordinateY.Seven, CoordinateX.G);
            IFigure blackPawn8 = Factory.GetPawn(Color.Dark, CoordinateY.Seven, CoordinateX.H);

            this.squares[0][0] = blackRook1;
            this.squares[0][1] = blackKnight1;
            this.squares[0][2] = blackBishop1;
            this.squares[0][3] = blackQueen;
            this.squares[0][4] = blackKing;
            this.squares[0][5] = blackBishop2;
            this.squares[0][6] = blackKnight2;
            this.squares[0][7] = blackRook2;

            this.squares[1][0] = blackPawn1;
            this.squares[1][1] = blackPawn2;
            this.squares[1][2] = blackPawn3;
            this.squares[1][3] = blackPawn4;
            this.squares[1][4] = blackPawn5;
            this.squares[1][5] = blackPawn6;
            this.squares[1][6] = blackPawn7;
            this.squares[1][7] = blackPawn8;

            this.squares[6][0] = whitePawn1;
            this.squares[6][1] = whitePawn2;
            this.squares[6][2] = whitePawn3;
            this.squares[6][3] = whitePawn4;
            this.squares[6][4] = whitePawn5;
            this.squares[6][5] = whitePawn6;
            this.squares[6][6] = whitePawn7;
            this.squares[6][7] = whitePawn8;

            this.squares[7][0] = whiteRook1;
            this.squares[7][1] = whiteKnight1;
            this.squares[7][2] = whiteBishop1;
            this.squares[7][3] = whiteQueen;
            this.squares[7][4] = whiteKing;
            this.squares[7][5] = whiteBishop2;
            this.squares[7][6] = whiteKnight2;
            this.squares[7][7] = whiteRook2;
        }

        private void SquaresInitializeAssign(IFigure[][] squares)
        {
            IFigure emptyDummy = Factory.GetEmpty(CoordinateY.One, CoordinateX.A);

            IFigure empty1 = Factory.GetEmpty(CoordinateY.Three, CoordinateX.A);
            IFigure empty2 = Factory.GetEmpty(CoordinateY.Three, CoordinateX.B);
            IFigure empty3 = Factory.GetEmpty(CoordinateY.Three, CoordinateX.C);
            IFigure empty4 = Factory.GetEmpty(CoordinateY.Three, CoordinateX.D);
            IFigure empty5 = Factory.GetEmpty(CoordinateY.Three, CoordinateX.E);
            IFigure empty6 = Factory.GetEmpty(CoordinateY.Three, CoordinateX.F);
            IFigure empty7 = Factory.GetEmpty(CoordinateY.Three, CoordinateX.G);
            IFigure empty8 = Factory.GetEmpty(CoordinateY.Three, CoordinateX.H);

            IFigure empty9 = Factory.GetEmpty(CoordinateY.Four, CoordinateX.A);
            IFigure empty10 = Factory.GetEmpty(CoordinateY.Four, CoordinateX.B);
            IFigure empty11 = Factory.GetEmpty(CoordinateY.Four, CoordinateX.C);
            IFigure empty12 = Factory.GetEmpty(CoordinateY.Four, CoordinateX.D);
            IFigure empty13 = Factory.GetEmpty(CoordinateY.Four, CoordinateX.E);
            IFigure empty14 = Factory.GetEmpty(CoordinateY.Four, CoordinateX.F);
            IFigure empty15 = Factory.GetEmpty(CoordinateY.Four, CoordinateX.G);
            IFigure empty16 = Factory.GetEmpty(CoordinateY.Four, CoordinateX.H);

            IFigure empty17 = Factory.GetEmpty(CoordinateY.Five, CoordinateX.A);
            IFigure empty18 = Factory.GetEmpty(CoordinateY.Five, CoordinateX.B);
            IFigure empty19 = Factory.GetEmpty(CoordinateY.Five, CoordinateX.C);
            IFigure empty20 = Factory.GetEmpty(CoordinateY.Five, CoordinateX.D);
            IFigure empty21 = Factory.GetEmpty(CoordinateY.Five, CoordinateX.E);
            IFigure empty22 = Factory.GetEmpty(CoordinateY.Five, CoordinateX.F);
            IFigure empty23 = Factory.GetEmpty(CoordinateY.Five, CoordinateX.G);
            IFigure empty24 = Factory.GetEmpty(CoordinateY.Five, CoordinateX.H);

            IFigure empty25 = Factory.GetEmpty(CoordinateY.Six, CoordinateX.A);
            IFigure empty26 = Factory.GetEmpty(CoordinateY.Six, CoordinateX.B);
            IFigure empty27 = Factory.GetEmpty(CoordinateY.Six, CoordinateX.C);
            IFigure empty28 = Factory.GetEmpty(CoordinateY.Six, CoordinateX.D);
            IFigure empty29 = Factory.GetEmpty(CoordinateY.Six, CoordinateX.E);
            IFigure empty30 = Factory.GetEmpty(CoordinateY.Six, CoordinateX.F);
            IFigure empty31 = Factory.GetEmpty(CoordinateY.Six, CoordinateX.G);
            IFigure empty32 = Factory.GetEmpty(CoordinateY.Six, CoordinateX.H);

            for (int i = 0; i <= 1; i++)
            {
                for (int k = 0; k < Globals.BoardCols; k++)
                {
                    this.squares[i][k] = emptyDummy;
                }
            }

            this.squares[5][0] = empty1;
            this.squares[5][1] = empty2;
            this.squares[5][2] = empty3;
            this.squares[5][3] = empty4;
            this.squares[5][4] = empty5;
            this.squares[5][5] = empty6;
            this.squares[5][6] = empty7;
            this.squares[5][7] = empty8;

            this.squares[4][0] = empty9;
            this.squares[4][1] = empty10;
            this.squares[4][2] = empty11;
            this.squares[4][3] = empty12;
            this.squares[4][4] = empty13;
            this.squares[4][5] = empty14;
            this.squares[4][6] = empty15;
            this.squares[4][7] = empty16;

            this.squares[3][0] = empty17;
            this.squares[3][1] = empty18;
            this.squares[3][2] = empty19;
            this.squares[3][3] = empty20;
            this.squares[3][4] = empty21;
            this.squares[3][5] = empty22;
            this.squares[3][6] = empty23;
            this.squares[3][7] = empty24;

            this.squares[2][0] = empty25;
            this.squares[2][1] = empty26;
            this.squares[2][2] = empty27;
            this.squares[2][3] = empty28;
            this.squares[2][4] = empty29;
            this.squares[2][5] = empty30;
            this.squares[2][6] = empty31;
            this.squares[2][7] = empty32;

            for (int i = 6; i <= 7; i++)
            {
                for (int k = 0; k < Globals.BoardCols; k++)
                {
                    this.squares[i][k] = emptyDummy;
                }
            }
        }
    }
}