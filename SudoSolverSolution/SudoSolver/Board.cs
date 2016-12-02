using System;
using System.Diagnostics;
using System.Linq;

namespace SudoSolver
{
    public class Board
    {
        Square[,] board;

        Area[] Rows;
        Area[] Columns;
        Area[] Regions;

        /// <summary>
        /// The length and width of the board (like 9 on a 9x9)
        /// </summary>
        public int M;

        /// <summary>
        /// Square root of M. This is the length and width of the regions/sub-blocks.
        /// </summary>
        public int N;

        bool changeMade;

        public Square this[int index, int y]
        {
            get { return board[index, y]; }
            set { board[index, y] = value; }
        }

        public Board(int n)
        {
            initializeBoard(n);
        }

        public Board(Board original)
        {
            initializeBoard(original.N);
            CopyBoard(original);
        }

        private void initializeBoard(int n)
        {
            N = n;
            M = n * n;

            board = new Square[M, M];
            for (var row = 0; row < M; row++)
            {
                for (var col = 0; col < M; col++)
                {
                    board[row, col] = new Square(M);
                }
            }

            initRowsColsRegions();
        }

        private void initRowsColsRegions()
        {
            // Initializes boardRows, boardCols, and boardRegions to point to the squares
            // that go with each row, column, or region.
            Rows = new Area[M];

            for (int row = 0; row < Rows.Length; row++)
            {
                Rows[row] = new Row(M, this, row);
                Rows[row].Number = row;
            }

            Columns = new Area[M];
            for (int col = 0; col < Columns.Length; col++)
            {
                Columns[col] = new Col(M, this, col);
                Columns[col].Number = col;
            }

            Regions = new Area[M];
            for (int region = 0; region < Regions.Length; region++)
            {
                Regions[region] = new Region(N, M, this, region);
                Regions[region].Number = region;
            }
        }

        void CopyBoard(Board original)
        {
            for (var row = 0; row < M; row++)
            {
                for (var col = 0; col < M; col++)
                {
                    this[row, col].Copy(original[row, col]);
                }
            }
        }

        public int SolveDepth = 1;
        public int GreatestDepth = 1;
        DateTime startTime;
        DateTime endTime;

        // How long it took to solve the puzzle in milliseconds
        public double SolveTimeMs
        {
            get
            {
                Debug.Assert(startTime != null && endTime != null);
                return (endTime - startTime).Milliseconds;
            }
        }

        public bool solveBoard(int solveDepth = 1)
        {
            SolveDepth = solveDepth;

            changeMade = true;
            startTime = DateTime.Now;
            var iterations = 0;

            // Loop until logical methods stop providing results
            while (changeMade && IsSolved() == false)
            {
                iterations += 1;

                changeMade = false;

                // Then for each area, check which numbers aren't set. For each unset number, if it only has
                // one place to go, set it there.
                foreach (var row in Rows)
                {
                    row.checkEachEmptySquare();
                }
                foreach (var col in Columns)
                {
                    col.checkEachEmptySquare();
                }
                foreach (var region in Regions)
                {
                    region.checkEachEmptySquare();
                }

                // Then find squares that only have one available number and set the square to that number.
                foreach (var square in board)
                {
                    if (square.HasNumber == false && square.CountAvailable() == 1)
                    {
                        for (var i = 1; i <= M; i++)
                        {
                            if (!square.IsBlocked(i))
                            {
                                square.Number = i;
                                setChangeMade();
                                break;
                            }
                        }
                    }
                }
            }
            
            // If logical methods didn't provide a solution, move on to depth-first search of solutions
            if (IsSolved() == false)
            {
                var square = SquareWithFewestOptions;

                // The board isn't solved yet and there are no valid options to try, so this board is unsolvable
                if (square == null)
                {
                    return false;
                }
                else
                {
                    // Try placing every number on the square that can go there. If it fails, keep trying till there are no options.
                    for (int num = 1; num <= M; num++)
                    {
                        if (square.IsBlocked(num) == false)
                        {
                            var attempt = new Board(this);
                            attempt[square.RowNumber, square.ColNumber].Number = num;

                            if (attempt.solveBoard(solveDepth + 1))
                            {
                                GreatestDepth = attempt.SolveDepth;
                                if (SolveDepth != 1)
                                    SolveDepth = attempt.SolveDepth;
                                CopyBoard(attempt);
                                break;
                            }
                        }
                    }
                }
            }

            endTime = DateTime.Now;
            return IsSolved();
        }

        /// <summary>
        /// Get the first blank square with the fewest options. Null if no squares are blank
        /// or if no blank squares have any valid numbers that can go on them.
        /// </summary>
        Square SquareWithFewestOptions
        {
            get
            {
                Square candidate = null;

                foreach(var square in board)
                {
                    if (square.HasNumber == false && (candidate == null || square.CountAvailable() < candidate.CountAvailable()))
                        candidate = square;
                }

                return candidate;
            }
        }

        public void setChangeMade()
        {
            changeMade = true;
        }

        public bool IsSolved()
        {
            return allNumbersInAreas(Rows) && allNumbersInAreas(Columns) && allNumbersInAreas(Regions);
        }

        bool allNumbersInAreas(Area[] areas)
        {
            foreach (var area in areas)
            {
                if (!area.IsComplete()) return false;
            }
            return true;
        }
    }
}
