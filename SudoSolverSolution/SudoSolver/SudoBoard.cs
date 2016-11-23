using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace SudoSolver
{
    class SudoBoard
    {
        public SudoSqr[,] board;
        SudoArea[] boardRows;
        SudoArea[] boardCols;
        SudoArea[] boardRegions;
        int n;
        int m;
        bool changeMade;
        bool error; // Used to indicate an error was found, meaning the board is invalid in its current state.

        public SudoBoard(int newN)
        {
            n = newN;
            m = n * n;
            board = new SudoSqr[m, m];
            initRowsColsRegions();
        }
        public SudoBoard(int newN, SudoSqr[,] newBoard)
        {
            n = newN;
            m = n * n;
            board = newBoard;
            initRowsColsRegions();
        }
        public SudoBoard(SudoBoard original)
        {
            n = original.n;
            m = original.m;
            board = new SudoSqr[m, m];
            for (int row = 0; row < m; row++)
            {
                for (int col = 0; col < m; col++)
                {
                    board[row, col] = new SudoSqr(n, original.board[row,col].getValue());
                }
            }

            initRowsColsRegions();
        }

        private void initializeBoard()
        {
            for (int row = 0; row < m; row++)
            {
                for (int col = 0; col < m; col++)
                {
                    board[row, col] = new SudoSqr(n, SudoSqr.UNSET);
                }
            }
        }
        public SudoSqr getSquare(int row, int col)
        {
            return board[row, col];
        }
        private void initRowsColsRegions()
        {
            // Initializes boardRows, boardCols, and boardRegions to point to the squares
            // that go with each row, column, or region.
            boardRows = new SudoArea[m];

            for (int row = 0; row < boardRows.Length; row++)
            {
                boardRows[row] = new SudoArea(n, this);
                boardRows[row].makeRow(row);
                boardRows[row].areaNum = row;
            }

            boardCols = new SudoArea[m];
            for (int col = 0; col < boardCols.Length; col++)
            {
                boardCols[col] = new SudoArea(n, this);
                boardCols[col].makeCol(col);
                boardCols[col].areaNum = col;
            }

            boardRegions = new SudoArea[m];
            for (int region = 0; region < boardRegions.Length; region++)
            {
                boardRegions[region] = new SudoArea(n, this);
                boardRegions[region].makeRegion(region);
                boardRegions[region].areaNum = region;
            }
        }

        public bool solveBoard()
        {
            bool result = false;

            changeMade = true;
            error = false;
            foreach (SudoArea area in boardRows)
            {
                area.scanSet();
            }
            foreach (SudoArea area in boardCols)
            {
                area.scanSet();
            }
            foreach (SudoArea area in boardRegions)
            {
                area.scanSet();
            }

            while (changeMade && (error == false ))
            {
                changeMade = false;

                // Then for each area, check which numbers aren't set. For each unset number, if it only has
                // one place to go, set it there.
                foreach (SudoArea row in boardRows)
                {
                    row.checkEachUnset();
                }
                foreach (SudoArea col in boardCols)
                {
                    col.checkEachUnset();
                }
                foreach (SudoArea region in boardRegions)
                {
                    region.checkEachUnset();
                }

                // Then find squares that only have one unblocked number and set that number.
                for (int row = 0; row < m; row++)
                {
                    for (int col = 0; col < m; col++)
                    {
                        SudoSqr square = board[row, col];
                        if (square.hasValue == false)
                        {
                            int allowedNumber = 0;
                            bool allowedFound = false;
                            bool twoFound = false;
                            for (int i = 1; i <= m; i++)
                            {
                                if (square.IsBlocked(i) == false)
                                {
                                    if (allowedFound == false)
                                    {
                                        allowedFound = true;
                                        allowedNumber = i;
                                    }
                                    else
                                    {
                                        twoFound = true;
                                        break;
                                    }
                                }
                            }
                            if ((twoFound == false) && (allowedFound == true))
                            {
                                square.set(allowedNumber);
                                square.row.blockInAllAreas(square, allowedNumber);
                                setChangeMade();
                            }
                        }
                    }
                }

                // If there are no 2-ways, go to 3-way, etc. Maybe make the guess() function adaptable.
            }
            bool solutionFound = false;
            //MessageBox.Show(IsSolved().ToString());
            
            if ((error == false) && (IsSolved() == false))
            {
                SudoSqr square = getFirstEmptySquare();
                if (square != null)
                {
                   // MessageBox.Show("Predicting");
                        for (int num = 1; (num <= m) && (solutionFound == false); num++)
                        {
                            if (square.IsBlocked(num) == false)
                            {
                                SudoBoard attempt = new SudoBoard(this);
                                attempt.board[square.rowNum, square.colNum].set(num);

                                if (attempt.solveBoard() == true)
                                {
                                    board = attempt.board;
                                    MessageBox.Show("Board Solved with prediction");
                                    solutionFound = true;
                                }
                                else
                                {
                                    MessageBox.Show("Attempt failed");
                                } 
                            }
                        }
                }
            } 

            // Now once the previous 3 steps making results, find a number that only has two possible locations.
            // Pick one and do a recursive search. If that versions makes a conflict, it will return and you can
            // set it the other way, then continue on.

            // Find first non-set square. Pick a number to go there. Search forward. If it returns invalid, 
            // try another number. One number has to go in each place, so eventually it will work. May need
            // to be more efficient.

            if (error == false)
            {
                result = true;
            }
            return result;
        }
        SudoSqr getFirstEmptySquare()
        {
            foreach (SudoSqr square in board)
            {
                if (square.hasValue == false)
                {
                    return square;
                }
            }
            return null;
        }

        public void setChangeMade()
        {
            changeMade = true;
            //MessageBox.Show("Change Made");

        }

        public void setError()
        {
            error = true;
            //MessageBox.Show("Error Made");
        }


        public bool IsSolved()
        {
            SudoSqr holdingSquare = new SudoSqr(n);
            bool result = true;

            // This block checks each row for having one and only one of each number.
            for (int row = 0; (row < m) && (result == true); row++)
            {
                holdingSquare.reset();
                for (int col = 0; col < m; col++)
                {
                    holdingSquare.setNoBlock(board[row, col].getValue());
                }
                result = result && holdingSquare.IsAllSet();
            }

            // This block checks each column for having one and only one of each number.
            for (int col = 0; (col < m) && (result == true); col++)
            {
                holdingSquare.reset();
                for (int row = 0; row < m; row++)
                {
                    holdingSquare.setNoBlock(board[row, col].getValue());
                }
                result = result && holdingSquare.IsAllSet();
            }

            // This block checks each region (n by n) for have each number.
            for (int subRow = 0; subRow < n; subRow++)
            {
                for (int subCol = 0; (subCol < n) && (result == true); subCol++)
                {
                    holdingSquare.reset();
                    for (int row = subRow * n; row < (subRow + 1) * n; row++)
                    {
                        for (int col = subCol * n; col < (subCol + 1) * n; col++)
                        {
                            holdingSquare.setNoBlock(board[row, col].getValue());
                        }
                    }
                    result = result && holdingSquare.IsAllSet();
                }
            }

            return result;
        }
        int getRegionNumber(int row, int col)
        {   // Translates row and column into a region number.
            return ((row / n) * n) + (col / 3);
        }
    }
}
