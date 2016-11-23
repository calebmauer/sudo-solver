using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SudoSolver
{
    public partial class frmMain : Form
    {
        TextBox[,] boardTextBoxes;
        Panel boardPanel;

        public frmMain()
        {
            InitializeComponent();
        }

        private void generateBoard(int n, int m)
        {
            int txtBoxWidth = 25;
            int txtBoxHeight = 25;
            int squarePadding = 5;
            int startPosX = 20;
            int startPosY = 40;
            int panelPadding = 8;
            const int MAX_TEXT_LENGTH = 2;

            if (boardTextBoxes != null)
            {
                foreach (var box in boardTextBoxes)
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
                    boardTextBoxes[row, col].TextAlign = HorizontalAlignment.Center;

                    boardPanel.Controls.Add(boardTextBoxes[row, col]);
                }
            }

            // Allow the form to grow to accomodate larger puzzle sizes
            AutoSize = true;
        }

        int[,] makeIntFromBoard(Square[,] board)
        {
            int[,] returnArray = new int[board.GetLength(0), board.GetLength(1)];

            for (var row = 0; row < board.GetLength(0); row++)
            {
                for (var col = 0; col < board.GetLength(1); col++)
                {
                    returnArray[row, col] = board[row, col].Number;
                }
            }

            return returnArray;
        }

        private Board makeBoardFromText()
        {
            var m = boardTextBoxes.GetLength(0);
            var newBoard = new Board(Convert.ToInt32(Math.Sqrt(m)));

            for (var row = 0; row < m; row++)
            {
                for (var col = 0; col < m; col++)
                {
                    if (boardTextBoxes[row, col].Text != "")
                        newBoard[row, col].Number = Convert.ToInt32(boardTextBoxes[row, col].Text);
                }
            }

            return newBoard;
        }

        private void setTextFromStringArray(String[,] board, bool isSolution)
        {
            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int col = 0; col < board.GetLength(1); col++)
                {
                    if (isSolution && string.IsNullOrWhiteSpace(boardTextBoxes[row, col].Text))
                    {
                        boardTextBoxes[row, col].Text = board[row, col];
                        boardTextBoxes[row, col].ForeColor = Color.Blue;
                    }
                    else if (!isSolution)
                    {
                        boardTextBoxes[row, col].Text = board[row, col];
                        if (board[row, col] != "")
                        {
                            boardTextBoxes[row, col].Enabled = false;
                            boardTextBoxes[row, col].Font = new Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            boardTextBoxes[row, col].ForeColor = SystemColors.ControlText;
                        }
                    }
                }
            }
        }

        private void solveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (boardTextBoxes != null)
            {
                var newSolve = makeBoardFromText();
                var result = newSolve.solveBoard();

                MessageBox.Show(string.Format("Is Solved: {0}\nGreatest Search Depth: {1}", result ? "Yes" : "No", newSolve.GreatestDepth));

                setTextFromStringArray(newSolve.toStringArray(), true);
            }
        }

        private void checkProgressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var board = makeBoardFromText();
            MessageBox.Show(board.IsSolved().ToString());
        }

        private void loadTestBoardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            const int n = 3;
            const int m = n * n;

            // Evil, need to make guesses (level 5) to solve
            var evilBoard = new String[m, m] {{"8", "", "", "4", "", "", "", "", ""},
                                          {"2", "", "", "7", "", "6", "", "", ""},
                                          {"7", "", "5", "", "8", "", "", "9", ""},
                                          {"", "", "", "", "", "", "", "8", "6"},
                                          {"", "8", "", "", "9", "", "", "4", ""},
                                          {"6", "5", "", "", "", "", "", "", ""},
                                          {"", "1", "", "", "2", "", "5", "", "7"},
                                          {"", "", "", "1", "", "7", "", "", "9"},
                                          {"", "", "", "", "", "5", "", "", "3"}};

            // Hardest board I could find online, said to be the most difficult 9 x 9
            var hardestBoard = new String[m, m]
                                         {{"8", "", "", "", "", "", "", "", ""},
                                          {"", "", "3", "6", "", "", "", "", ""},
                                          {"", "7", "", "", "9", "", "2", "", ""},
                                          {"", "5", "", "", "", "7", "", "", ""},
                                          {"", "", "", "", "4", "5", "7", "", ""},
                                          {"", "", "", "1", "", "", "", "3", ""},
                                          {"", "", "1", "", "", "", "", "6", "8"},
                                          {"", "", "8", "5", "", "", "", "1", ""},
                                          {"", "9", "", "", "", "", "4", "", ""}};

            generateBoard(n, m);
            setTextFromStringArray(hardestBoard, false);
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var newBoardDialog = new NewBoardDialog();
            if (newBoardDialog.ShowDialog() == DialogResult.OK)
            {
                var n = newBoardDialog.ChosenSize;
                var m = n * n;
                generateBoard(n, m);
            }
        }

        private void loadPuzzleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(ofdOpenExisting.ShowDialog() == DialogResult.OK)
            {
                using (var reader = new StreamReader(ofdOpenExisting.FileName))
                {
                    // Read size number - square root of number of rows and columns (3 in a 9x9)
                    var n = Convert.ToInt32(reader.ReadLine());
                    var m = n * n;

                    var board = new string[m, m];

                    for (var i = 0; i < m; i++)
                    {
                        var rowText = reader.ReadLine();

                        // Skip lines that start with '#'. These are comment lines.
                        while(rowText.StartsWith("#"))
                        {
                            rowText = reader.ReadLine();
                        }

                        var split = rowText.Split('|');
                        
                        for (var j = 0; j < m; j++)
                        {
                            board[i, j] = split[j].Trim();
                        }
                    }

                    generateBoard(n, m);
                    setTextFromStringArray(board, false);
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (boardTextBoxes == null)
            {
                MessageBox.Show("You can't save until you've created a puzzle to save.");
                return;
            }

            if (sfdSave.ShowDialog() == DialogResult.OK)
            {
                var board = makeBoardFromText();
                using (var saveStream = new StreamWriter(sfdSave.FileName))
                {
                    // First line is the square root of the board dimension
                    saveStream.WriteLine(board.N);

                    for (var i = 0; i < board.M; i++)
                    {
                        for (var j = 0; j < board.M; j++)
                        {
                            saveStream.Write(board[i, j].HasNumber ? board[i, j].Number.ToString() : " ");
                            if (j != board.M - 1)
                            {
                                saveStream.Write("|");
                            }
                        }
                        saveStream.Write("\n");
                    }
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
