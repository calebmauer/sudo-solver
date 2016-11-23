using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SudoSolver
{
    class SudoArea
    {
        SudoSqr[] squares;
        SudoBoard board;
        int n;
        int m;
        public int areaNum;

        public SudoArea(int newN, SudoBoard newBoard)
        {
            n = newN;
            m = n * n;
            board = newBoard;
        }

        public bool scanSet()
        {
            bool success = true;
            foreach (SudoSqr square in squares)
            {
                if (square.hasValue) // If a number is set on the board, it blocks that number in all areas it intersects with.
                {
                    blockInAllAreas(square, square.getValue());
                }
            }
            return success;
        }
        public bool blockInAllAreas(SudoSqr square, int num)
        { //Returns false if more than one instance of the number is found in any area. Indicates operation failed.
            bool success = true;

            square.row.blockInAllSquares(num);
            square.col.blockInAllSquares(num);
            square.row.blockInAllSquares(num);

            return success;
        }
        public void blockInAllSquares(int num)
        {
            bool foundOne = false;
            foreach (SudoSqr square in squares)
            {
                if (square.IsSet(num))
                {
                    if (foundOne == false)
                    {
                        foundOne = true;
                    }
                    else
                    {
                        board.setError(); // Indicates board invalid
                        return; // If it reaches this point the number is present twice in
                        // this area, and the board is unsolvable in the current state.
                    }
                }
                else
                {
                    bool IsntBlocked = square.IsBlocked(num);
                    if (IsntBlocked == false)
                    {
                        board.setChangeMade(); // To indicate to the main solve loop that something happened this turn.
                        square.block(num);
                    }
                }
            }
        }

        public bool checkEachUnset()
        { // Scans for each number missing from the area and tries to place it.
            bool[] open = new bool[m];
            int blocksFound;
            for (int num = 1; num <= m; num++)
            {
                for (int tile = 0; tile < m; tile++) { open[tile] = true; }

                blocksFound = 0;
                for (int i = 0; i < m; i++)
                {
                    if (squares[i].IsSet(num))
                    {
                        blocksFound = -1; // Lets it exit the outer loop also.
                        break; // Number already set in area, break.
                    }
                    else if (squares[i].IsBlocked(num))
                    {
                        open[i] = false;
                        blocksFound += 1;
                    }
                }
                if (blocksFound == (m - 1))
                {
                    for (int j = 0; j < m; j++)
                    {
                        if (open[j] == true) // This is the one open space
                        {
                            squares[j].set(num);
                            blockInAllAreas(squares[j], num);
                            board.setChangeMade();
                        }
                    }
                }
                else if (blocksFound >= m)
                {
                    board.setError();
                    return false; // Board is invalid, return false.
                }
            }
            return true;
        }

        public void makeRow(int row)
        {
            squares = new SudoSqr[m];
            for (int col = 0; col < m; col++)
            {
                squares[col] = board.getSquare(row, col);
                squares[col].row = this;
                squares[col].rowNum = areaNum;
            }
        }
        public void makeCol(int col)
        {
            // Makes this SudoeArea into a container for a region.
            squares = new SudoSqr[m];
            for (int row = 0; row < m; row++)
            {
                squares[row] = board.getSquare(row, col);
                squares[row].col = this;
                squares[row].colNum = areaNum;
            }
        }
        public void makeRegion(int region)
        {
            // Makes this SudoArea into a container for a region (sub-square).
            squares = new SudoSqr[m];
            /* This code uses integer divides and mods to use the region number to get
             * the specific 9 squares belonging to that region. */
            int counter = 0;
            for (int row = (region / n) * n; row < ((region / n) + 1) * n; row++)
            {
                for (int col = (region % n) * n; col < ((region % n) + 1) * n; col++)
                {
                    squares[counter] = board.getSquare(row, col);
                    squares[counter].region = this;
                    counter++;
                }
            }
        }
    }
}
