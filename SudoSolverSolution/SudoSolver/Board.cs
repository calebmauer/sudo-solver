using System;
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

        void CopyBoard(Board orignal)
        {
            for (var row = 0; row < M; row++)
            {
                for (var col = 0; col < M; col++)
                {
                    if (orignal.board[row, col].HasNumber && this[row, col].HasNumber == false)
                        this[row, col].Number = orignal.board[row, col].Number;
                }
            }
        }

        public int SolveDepth = 1;
        public int GreatestDepth = 1;

        public bool solveBoard(int solveDepth = 1)
        {
            SolveDepth = solveDepth;

            changeMade = true;

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
                // Find the square with the least options and pick a number to go there. Search forward. If it returns invalid, 
                // try another number. One number has to go in each place, so eventually it will work.
                var square = SquareWithFewestOptions;
                for (int num = 1; num <= M; num++)
                {
                    if (square.IsBlocked(num) == false)
                    {
                        var attempt = new Board(this);
                        attempt.board[square.RowNumber, square.ColNumber].Number = num;

                        if (attempt.solveBoard(solveDepth+1))
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

            return IsSolved();
        }

        /// <summary>
        /// Get the first blank square with the fewest options.
        /// </summary>
        Square SquareWithFewestOptions
        {
            get
            {
                return (from Square s in board
                        where s.HasNumber == false
                        orderby s.CountAvailable()
                        select s).First();
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

        public string[,] toStringArray()
        {
            var stringArray = new string[M, M];

            for (var row = 0; row < M; row++)
            {
                for (var col = 0; col < M; col++)
                {
                    if (board[row, col].HasNumber)
                        stringArray[row, col] = board[row, col].Number.ToString();
                    else
                        stringArray[row, col] = "";
                }
            }

            return stringArray;
        }
    }
}
