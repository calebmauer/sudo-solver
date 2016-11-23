using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SudoSolver
{
    public partial class frmMain : Form
    {
        public int n;
        public int m;
        TextBox[,] boardTextBoxes;
        Panel boardPanel;
        bool IsBoardInitialized = false;

        public frmMain()
        {
            InitializeComponent();
        }

        private void generateBoard()
        {
            int txtBoxWidth = 25;
            int txtBoxHeight = 25;
            int squarePadding = 5;
            int startPosX = 35;
            int startPosY = 100;
            int panelPadding = 10;
            const int MAX_TEXT_LENGTH = 2;

            IsBoardInitialized = true;

            if (boardTextBoxes != null)
            {
                foreach (TextBox box in boardTextBoxes)
                {
                    box.Dispose();
                }
            }
            if (boardPanel != null)
            {
                boardPanel.Dispose();
            }
            boardPanel = new Panel();
            boardPanel.Location = new Point(startPosX, startPosY);
            boardPanel.Size = new Size(panelPadding * 2 + squarePadding * (n - 1) + m * txtBoxWidth,
                                        panelPadding * 2 + squarePadding * (n - 1) + m * txtBoxHeight);
            boardPanel.BackColor = Color.PowderBlue;
            this.Controls.Add(boardPanel);
            boardTextBoxes = new TextBox[m, m];

            for (int row = 0; row < boardTextBoxes.GetLength(0); row++)
            {
                for (int col = 0; col < boardTextBoxes.GetLength(1); col++)
                {
                    boardTextBoxes[row, col] = new TextBox();
                    boardTextBoxes[row, col].Multiline = true;
                    boardTextBoxes[row, col].Size = new Size(txtBoxWidth, txtBoxHeight);

                    boardTextBoxes[row, col].Location = new Point(
                                            panelPadding + (txtBoxWidth * col) + (squarePadding * (col / n))
                                            , panelPadding + (txtBoxHeight * row) + (squarePadding * (row / n)));
                    boardTextBoxes[row, col].MaxLength = MAX_TEXT_LENGTH;
                    boardTextBoxes[row, col].TextAlign = System.Windows.Forms.HorizontalAlignment.Center;

                    boardPanel.Controls.Add(boardTextBoxes[row, col]);
                }
            }
        }

        int[,] makeIntFromBoard(SudoSqr[,] board)
        {
            int[,] returnArray = new int[board.GetLength(0), board.GetLength(1)];

            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int col = 0; col < board.GetLength(1); col++)
                {
                    returnArray[row, col] = board[row, col].getValue();
                }
            }

            return returnArray;
        }
        private SudoBoard makeBoardFromText()
        {
            SudoSqr[,] newBoard = new SudoSqr[m, m];
            for (int row = 0; row < m; row++)
            {
                for (int col = 0; col < m; col++)
                {
                    if (boardTextBoxes[row, col].Text != "")
                        newBoard[row, col] = new SudoSqr(n, Convert.ToInt32(boardTextBoxes[row, col].Text));
                    else newBoard[row, col] = new SudoSqr(n, SudoSqr.UNSET);
                }
            }
            return new SudoBoard(n, newBoard);
        }

        private String[,] makeStringArray(int[,] numArray)
        {
            String[,] stringArray = new String[numArray.GetLength(0), numArray.GetLength(1)];

            for (int row = 0; row < numArray.GetLength(0); row++)
            {
                for (int col = 0; col < numArray.GetLength(1); col++)
                {
                    stringArray[row, col] = numArray[row, col].ToString();
                    if (numArray[row, col] == 0)
                    {
                        stringArray[row, col] = "";
                    }
                }
            }

            return stringArray;
        }
        private void setTextFromStringArray(String[,] board)
        {
            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int col = 0; col < board.GetLength(1); col++)
                {
                    boardTextBoxes[row, col].Text = board[row, col];
                    if (board[row, col] != "")
                    {
                        boardTextBoxes[row, col].Enabled = false;
                        boardTextBoxes[row, col].Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        boardTextBoxes[row, col].ForeColor = SystemColors.ControlText;
                    }
                }
            }
        }

        private void btnNewBoard_Click(object sender, EventArgs e)
        {
            n = Convert.ToInt32(cmbSize.Text);
            m = n * n;
            generateBoard();
            btnSolve.Enabled = true;
        }

        private void btnTestBoard_Click_1(object sender, EventArgs e)
        {
            String[,] testBoard = new String[9, 9] {{"", "2", "7", "", "1", "", "", "", "4"},
                                          {"", "6", "", "3", "", "8", "", "", ""},
                                          {"1", "", "3", "7", "4", "", "", "5", ""},
                                          {"", "", "6", "", "", "", "8", "", "5"},
                                          {"5", "8", "", "", "7", "", "", "1", "3"},
                                          {"3", "", "2", "", "", "", "9", "", ""},
                                          {"", "7", "", "", "5", "4", "6", "", "9"},
                                          {"", "", "", "8", "", "7", "", "4", ""},
                                          {"6", "", "", "", "3", "", "1", "8", ""}};

           /* String[,] testBoard = new String[9, 9] {{"", "5", "8", "7", "", "", "", "", "4"},
                                          {"", "", "", "", "", "", "", "8", "3"},
                                          {"3", "", "", "9", "8", "", "6", "", "7"},
                                          {"", "", "", "", "1", "", "", "", ""},
                                          {"8", "7", "", "", "", "", "", "4", "6"},
                                          {"", "", "", "", "7", "", "", "", ""},
                                          {"2", "", "3", "", "4", "1", "", "", "8"},
                                          {"1", "8", "", "", "", "", "", "", ""},
                                          {"9", "", "", "", "", "8", "3", "5", ""}};*/
            n = 3;
            m = n * n;
            generateBoard();
            btnSolve.Enabled = true;
            setTextFromStringArray(testBoard);
        }

        private void btnSolve_Click_1(object sender, EventArgs e)
        {
            SudoBoard newSolve = makeBoardFromText();
            bool result = newSolve.solveBoard();
            SudoSqr[,] newResult = newSolve.board;
            int[,] intArray = makeIntFromBoard(newResult);
            setTextFromStringArray(makeStringArray(intArray));

        }

        private void btnCheckProgress_Click_1(object sender, EventArgs e)
        {
            SudoBoard board = makeBoardFromText();
            MessageBox.Show(board.IsSolved().ToString());
        }


    }
}
