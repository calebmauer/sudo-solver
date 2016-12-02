using System;
using System.Collections;

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
                if (!HasNumber)
                    throw new Exception("Attempting to read number from a blank square.");
                return number;
            }
            set
            {
                if (HasNumber)
                    throw new Exception("This square already has a number.");

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
                for (var i = 0; i < M; i++)
                {
                    isAvailable[i] = false;
                }
            }
        }

        public const int UNSET = 0;
        int number = UNSET; // Unset means it doesn't have a number

        BitArray isAvailable;
        int M;

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
            M = m;
            // Initially all numbers are available because the board starts blank
            isAvailable = new BitArray(m);
            isAvailable.SetAll(true);
        }

        public void Copy(Square original)
        {
            if (HasNumber == false)
            {
                if (original.HasNumber)
                {
                    Number = original.Number;
                }
                else
                {
                    isAvailable = (BitArray)original.isAvailable.Clone();
                }
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
            for (var i = 0; i < M; i++)
            { 
                if (isAvailable[i]) count++;
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

        public override string ToString()
        {
            return HasNumber ? Number.ToString() : "";
        }
    }
}
