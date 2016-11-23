using System;

namespace SudoSolver
{
    /// <summary>
    /// A square on the Sudoku board that can have a number on it or be blank.
    /// </summary>
    public class Square
    {
        public bool HasNumber { get; set; }

        /// <summary>
        /// The number on the face of this square.
        /// </summary>
        public int Number
        {
            get
            {
                if (HasNumber == false)
                    throw new Exception("Attempting to read number from a blank square.");

                return number;
            }
            set
            {
                // Exception for debugging, this code should never set a square that already has a number.
                if (HasNumber)
                    throw new Exception("Overwriting number");

                // Set the number
                HasNumber = true;
                number = value;

                // Let all areas this square is in know about the number setting (TODO: Replace with events that the areas subscribe to.)
                Row.NotifySquareSet(this, value);
                Col.NotifySquareSet(this, value);
                Region.NotifySquareSet(this, value);

                // Block this number from all the areas the square is in (because the number is already in use).
                Row.blockInAllSquares(value);
                Col.blockInAllSquares(value);
                Region.blockInAllSquares(value);

                // Mark that this square can't take any numbers because it has a number now.
                for (var i = 0; i < isAvailable.Length; i++)
                {
                    isAvailable[i] = false;
                }
            }
        }

        public const int UNSET = 0;
        int number = UNSET; // Unset means it doesn't have a number

        bool[] isAvailable;

        /// <summary>
        /// The row on the board the square is in.
        /// </summary>
        public Area Row { get; set; }

        /// <summary>
        /// The column on the board the square is in.
        /// </summary>
        public Area Col { get; set; }

        /// <summary>
        /// The region on the board the square is in.
        /// </summary>
        public Area Region { get; set; }

        /// <summary>
        /// Column number on the board
        /// </summary>
        public int ColNumber { get { return Col.Number; } }

        /// <summary>
        /// Row number on the board
        /// </summary>
        public int RowNumber { get { return Row.Number; } }

        public Square(int m)
        {
            HasNumber = false;

            isAvailable = new bool[m];
            for (var i = 0; i < isAvailable.Length; i++)
            {
                isAvailable[i] = true;
            }
        }

        /// <summary>
        /// Returns true if this square can have the number on it (meaning it's not already blocked).
        /// </summary>
        /// <param name="num">Number between 1 and M inclusive.</param>
        /// <returns></returns>
        public bool IsAvailable(int num)
        {
            return isAvailable[num - 1];
        }

        /// <summary>
        /// Is number blocked from being placed on this square?
        /// </summary>
        /// <param name="num">Number between 1 and M inclusive.</param>
        /// <returns></returns>
        public bool IsBlocked(int num)
        {
            return isAvailable[num - 1] == false;
        }

        /// <summary>
        /// Count how many numbers can possibly go on this square. Will be zero when square has a number on it.
        /// </summary>
        public int CountAvailable()
        {
            int count = 0;
            foreach (var available in isAvailable)
            {
                if (available) count++;
            }
            return count;
        }

        /// <summary>
        /// Remove the number from the available numbers that can go on the face of this square.
        /// </summary>
        /// <param name="num">Number between 1 and M inclusive.</param>
        public void Block(int num)
        {
            isAvailable[num - 1] = false;
        }
    }
}
