using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SudoSolver
{

    class SudoSqr
    {
        public const int SET = 1;
        public const int BLOCKED = -1;
        public const int UNSET = 0;

        int[] states;
        int n;
        int m;

        public bool hasValue { get; set; }

        public SudoArea row { get; set; }
        public SudoArea col { get; set; }
        public SudoArea region { get; set; }
        public int rowNum { get; set; }
        public int colNum { get; set; }

        int value;

        int getState(int num)
        {
            return states[num];
        }

        public SudoSqr(int newN, int newValue = UNSET)
        {
            n = newN;
            m = newN * newN;
            hasValue = false;
            value = newValue;
            initializeSqr();
            if (newValue != UNSET)
            {
                set(newValue);
            }


        }
        public SudoSqr(SudoSqr original)
        {
            n = original.n;
            m = original.m;
            hasValue = original.hasValue;
            value = original.value;
            initializeSqr();

            if (value != UNSET)
            {
                set(value);
            }

            for (int i = 0; i < m; i++)
            {
                states[i] = original.states[i];
            }
        }

        public int getValue()
        {
            return value;
        }

        public void reset()
        {
            value = UNSET;
            hasValue = false;
            for (int i = 0; i < m; i++)
            {
                states[i] = UNSET;
            }
        }
        void initializeSqr()
        {
            states = new int[m];
            hasValue = false;
            for (int i = 0; i < m; i++)
            {
                states[i] = UNSET;
            }
        }
        public bool IsSet(int num)
        {
            if (states[num - 1] == SET)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsUnSet(int num)
        {
            return states[num - 1] == UNSET;
        }
        public bool IsBlocked(int num)
        {
            if (states[num - 1] == BLOCKED)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsAllSet()
        {
            // Used when a square is used to represent an entire row
            // column, or region. Returns true when all numbers are set.
            bool result = true;
            for (int i = 0; i < m; i++)
            {
                result = result && (states[i] == SET);
            }
            return result;
        }
        public bool IsAllBlocked()
        {
            // Used when a square is used to represent an entire row
            // column, or region. Returns true when all numbers are set.
            bool result = true;
            for (int i = 0; i < m; i++)
            {
                result = result && (states[i] == BLOCKED);
            }
            return result;
        }
        public void setNoBlock(int num)
        {
            if (IsNumValid(num))
            {
                states[num - 1] = SET;
                value = num;
            }
        }
        public void set(int num)
        {
            if (IsNumValid(num))
            {
                states[num - 1] = SET;
                value = num;
                hasValue = true;
                blockAllExcept(num);
            }
            else if (num == UNSET)
            {
                value = num;
                hasValue = false;
            }
        }
        void blockAllExcept(int num)
        {
            for (int i = 0; i < m; i++)
            {
                if (i != (num - 1))
                {
                    states[i] = BLOCKED;
                }
            }
        }
        public void block(int num)
        {
            if (IsNumValid(num))
            {
                states[num - 1] = BLOCKED;
            }
        }
        public void unset(int num)
        {
            if (IsNumValid(num))
            {
                states[num - 1] = UNSET;
            }
        }
        bool IsNumValid(int num)
        {
            return (num > 0) && (num <= m);
        }
    }
}
