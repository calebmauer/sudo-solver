using System;
using System.Collections.Generic;
using System.Linq;

namespace SudoSolver
{
    /// <summary>
    /// A Row, Column, or Region on an m by m sudoku board, where each area has m squares.
    /// </summary>
    public class Area
    {
        protected Square[] squares;
        protected Board board;

        /// <summary>
        /// Squares in this area that don't have a number on them.
        /// </summary>
        protected List<Square> blankSquares;

        /// <summary>
        /// Numbers that have not been placed on any squares in this area.
        /// </summary>
        protected List<int> remainingNumbers;

        /// <summary>
        /// Number of squares in an area
        /// </summary>
        protected int M;

        /// <summary>
        /// Number of the area (row number for rows, col number for columns, etc)
        /// </summary>
        public int Number;

        public Area(int m, Board newBoard, int number)
        {
            M = m;
            board = newBoard;
            Number = number;
            squares = new Square[M];
        }

        protected void initAvailabilityLists()
        {
            // All squares start blank and all numbers start available
            blankSquares = squares.ToList();
            remainingNumbers = Enumerable.Range(1, M).ToList();
        }

        public void NotifySquareSet(Square square, int num)
        {
            remainingNumbers.Remove(num);
            blankSquares.Remove(square);
            board.setChangeMade();
        }

        /// <summary>
        /// Returns true when an area (row, col, or region) has numbers on each square
        /// and none of the numbers repeat.
        /// </summary>
        public bool IsComplete()
        {
            return remainingNumbers.Count == 0;
        }

        /// <summary>
        /// Block in all squares in the area except those in the excluded array.
        /// </summary>
        /// <param name="num">A number between 1 and m inclusive to block from all squares in the area.</param>
        /// <param name="excluded">Squares that the number should not be blocked from.</param>
        public void blockInAllSquares(int num, params Square[] excluded)
        {
            foreach (var square in blankSquares.Except(excluded))
            {
                // If square isn't already blocked, block it, and record it as a change made
                if (square.IsAvailable(num))
                {
                    square.Block(num);
                    board.setChangeMade();
                }
            }
        }

        public void checkEachEmptySquare()
        { 
            // Scans for each number missing from the area and tries to place it.
            var openingsByNumber = new Dictionary<int, List<Square>>();

            foreach (var num in remainingNumbers)
            {
                var openings = new List<Square>();

                foreach (var square in blankSquares)
                {
                    // Collect list of squares the number can go on
                    if (square.IsAvailable(num))
                    {
                        openings.Add(square);
                    }
                }

                // If there is only one square the number can go on, put it there
                if (openings.Count == 1)
                {
                    openings[0].Number = num;
                    checkEachEmptySquare(); // Restart check to re-enumerate remainingNumbers
                    return;
                }
                else
                {
                    // Save squares each number can go on for later analysis
                    openingsByNumber[num] = openings;

                    if (openings.Count == 2)
                    {
                        var a = openings[0];
                        var b = openings[1];

                        // If it can go in only two squares in the current area, the current area isn't a row, and the two squares
                        // are in the same row, block the number in all squares in the row except these two squares because on this row 
                        // it has to be in one of these two squares and no others.
                        if (!(this is Row) && a.Row == b.Row)
                        {
                            a.Row.blockInAllSquares(num, a, b);
                        }
                        // Same as above but for Col
                        else if (!(this is Col) && a.Col == b.Col)
                        {
                            a.Col.blockInAllSquares(num, a, b);
                        }
                        // Same as above but for Region
                        else if (!(this is Region) && a.Region == b.Region)
                        {
                            a.Region.blockInAllSquares(num, a, b);
                        }
                    }
                    else if (openings.Count == 3)
                    {
                        var a = openings[0];
                        var b = openings[1];
                        var c = openings[2];

                        // If it can go in only three squares in the current area, the current area isn't a row, and the squares
                        // are in the same row, block the number in all squares in the row except these squares because on this row 
                        // it has to be in one of these squares and no others.
                        if (!(this is Row) && a.Row == b.Row && b.Row == c.Row)
                        {
                            a.Row.blockInAllSquares(num, a, b, c);
                        }
                        // Same as above but for Column
                        else if (!(this is Col) && a.Col == b.Col && b.Col == c.Col)
                        {
                            a.Col.blockInAllSquares(num, a, b, c);
                        }
                        // Same as above but for Region
                        else if (!(this is Region) && a.Region == b.Region && b.Region == c.Region)
                        {
                            a.Region.blockInAllSquares(num, a, b, c);
                        }
                    }
                }
            }

           // checkForChains(openingsByNumber);
        }

        /// <summary>
        /// Given a list of the squares that each unused number can go on in this area, check if
        /// any group of numbers monopolizes a set of squares. If so, exclude all other numbers from
        /// those squares since they can't possibly go there.
        /// </summary>
        /// <param name="openingsByNumber">A dictionary mapping numbers to the squares they can go on.</param>
        public void checkForChains(Dictionary<int,List<Square>> openingsByNumber)
        {
            var numberList = openingsByNumber.Keys.ToList();

            // For each combination of the numbers (from length 2 to length n where n is the number of unset numbers)
            for (var i = 2; i <= numberList.Count; i++)
            {
                foreach (var combination in Combinations<int>.Get(numberList, i))
                {
                    var cmbList = combination.ToList();

                    var squareUnion = (from c in combination
                                       from square in openingsByNumber[c]
                                       select square).Distinct().ToList();

                    if (squareUnion.Count == i)
                    {
                        foreach (var num in numberList)
                        {
                            if (!cmbList.Contains(num))
                            {
                                foreach(var square in squareUnion)
                                {
                                    // If square isn't already blocked, block it, and record it as a change made
                                    if (square.IsAvailable(num))
                                    {
                                        square.Block(num);
                                        board.setChangeMade();
                                    }
                                }
                            }
                        }

                        return;
                    }
                }
            }
        }
    }

    /// <summary>
    /// Represents a row on an m by m square sudoku puzzle.
    /// </summary>
    class Row : Area
    {
        public Row(int m, Board board, int row) : base(m, board, row)
        {
            for (var col = 0; col < m; col++)
            {
                squares[col] = board[row, col];
                squares[col].Row = this;
            }

            initAvailabilityLists();
        }
    }

    /// <summary>
    /// Represents a column on an m by m square sudoku puzzle.
    /// </summary>
    class Col : Area
    {
        public Col(int m, Board board, int col) : base(m, board, col)
        {
            for (var row = 0; row < m; row++)
            {
                squares[row] = board[row, col];
                squares[row].Col = this;
            }

            initAvailabilityLists();
        }
    }

    /// <summary>
    /// Represents a region (aka sub-region, sub-block, or block) on an m by m square sudoku puzzle.
    /// </summary>
    class Region : Area
    {
        public Region(int n, int m, Board board, int region) : base(m, board, region)
        {
            // Makes this SudoArea into a container for a region (sub-square).
            // This code uses integer divides and mods to use the region number to get
            // the specific 9 squares belonging to that region.
            int counter = 0;
            for (var row = (region / n) * n; row < ((region / n) + 1) * n; row++)
            {
                for (var col = (region % n) * n; col < ((region % n) + 1) * n; col++)
                {
                    squares[counter] =  board[row, col];
                    squares[counter].Region = this;
                    counter++;
                }
            }

            initAvailabilityLists();
        }
    }
}
