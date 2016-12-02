using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SudoSolver
{
    public partial class frmMain : Form
    {
        TextBox[,] boardTextBoxes;
        Panel boardPanel;

        string appTitle = "Sudo Solver";
        string puzzleName = "";

        public frmMain()
        {
            InitializeComponent();
            updateTitle();
            // Allow the form to grow to accomodate larger puzzle sizes
            AutoSize = true;
        }

        void updateTitle(string puzzleName = "")
        {
            this.puzzleName = puzzleName;
            Text = appTitle + (puzzleName.Length > 0 ? " - " + puzzleName : "");
        }

        private void generateBoard(int n, int m)
        {
            const int TxtBoxWidth = 25;
            const int TxtBoxHeight = 25;
            const int SquarePadding = 5;
            const int StartPosX = 20;
            const int StartPosY = 40;
            const int PanelPadding = 8;
            const int MaxTextLength = 2;

            if (boardTextBoxes != null && boardTextBoxes.GetLength(0) == m)
            {
                // We can just re-use the existing board after clearing the squares
                foreach (var box in boardTextBoxes)
                {
                    box.Text = "";
                    styleAsDefault(box);
                }
            }
            else
            {
                SuspendLayout();

                // Dispose the existing controls
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
                boardPanel.Location = new Point(StartPosX, StartPosY);
                boardPanel.Size = new Size(PanelPadding * 2 + SquarePadding * (n - 1) + m * TxtBoxWidth,
                                            PanelPadding * 2 + SquarePadding * (n - 1) + m * TxtBoxHeight);
                boardPanel.BackColor = Color.PowderBlue;
                Controls.Add(boardPanel);
                boardTextBoxes = new TextBox[m, m];

                for (var row = 0; row < boardTextBoxes.GetLength(0); row++)
                {
                    for (var col = 0; col < boardTextBoxes.GetLength(1); col++)
                    {
                        boardTextBoxes[row, col] = new TextBox();
                        boardTextBoxes[row, col].Multiline = true;
                        boardTextBoxes[row, col].Size = new Size(TxtBoxWidth, TxtBoxHeight);

                        boardTextBoxes[row, col].Location = new Point(
                                                PanelPadding + (TxtBoxWidth * col) + (SquarePadding * (col / n))
                                                , PanelPadding + (TxtBoxHeight * row) + (SquarePadding * (row / n)));
                        boardTextBoxes[row, col].MaxLength = MaxTextLength;
                        boardTextBoxes[row, col].TextAlign = HorizontalAlignment.Center;

                        boardPanel.Controls.Add(boardTextBoxes[row, col]);
                    }
                }

                ResumeLayout();
            }
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

        private void setTextFromBoard(Board board, bool isSolution = false)
        {
            // If we are loading a board for the first time (not just filling in the solution) draw the board
            if (!isSolution)
                generateBoard(board.N, board.M);

            for (var row = 0; row < board.M; row++)
            {
                for (var col = 0; col < board.M; col++)
                {
                    if (isSolution && string.IsNullOrWhiteSpace(boardTextBoxes[row, col].Text))
                    {
                        boardTextBoxes[row, col].Text = board[row, col].ToString();
                        styleAsSolution(boardTextBoxes[row, col]);
                    }
                    else if (!isSolution)
                    {
                        boardTextBoxes[row, col].Text = board[row, col].ToString();
                        if (boardTextBoxes[row, col].Text != "")
                        {
                            styleAsGiven(boardTextBoxes[row, col]);
                        }
                    }
                }
            }
        }

        private void styleAsSolution(TextBox box)
        {
            box.ReadOnly = false;
            box.ForeColor = Color.Blue;
        }

        private void styleAsDefault(TextBox box)
        {
            box.ReadOnly = false;
            box.ForeColor = Color.Black;
        }

        private void styleAsGiven(TextBox box)
        {
            box.ReadOnly = true;
            box.ForeColor = Color.Black;
        }

        private void solveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (boardTextBoxes != null)
            {
                var newSolve = makeBoardFromText();
                var result = newSolve.solveBoard();

                MessageBox.Show(string.Format("Is Solved: {0}\nGreatest Search Depth: {1}\nSolve Time: {2}ms", result ? "Yes" : "No", newSolve.GreatestDepth, newSolve.SolveTimeMs));

                setTextFromBoard(newSolve, true);
            }
        }

        private void checkProgressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var board = makeBoardFromText();
            MessageBox.Show(board.IsSolved().ToString());
        }

        private void loadTestBoardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadBoardFromFile(@"..\..\..\..\Saved Puzzles\HardestBoard.sudo", "HardestBoard.sudo");
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var newBoardDialog = new NewBoardDialog();
            if (newBoardDialog.ShowDialog() == DialogResult.OK)
            {
                var board = new Board(newBoardDialog.ChosenSize);
                setTextFromBoard(board);
            }
        }

        private void loadPuzzleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(ofdOpenExisting.ShowDialog() == DialogResult.OK)
            {
                loadBoardFromFile(ofdOpenExisting.FileName, ofdOpenExisting.SafeFileName);
            }
        }

        private void loadBoardFromFile(string fileName, string safeFileName)
        {
            updateTitle(safeFileName);

            using (var reader = new StreamReader(fileName))
            {
                // Read size number - square root of number of rows and columns (3 in a 9x9)
                var n = Convert.ToInt32(reader.ReadLine());
                var m = n * n;

                var board = new Board(n);

                for (var i = 0; i < m; i++)
                {
                    var rowText = reader.ReadLine();

                    // Skip lines that start with '#'. These are comment lines.
                    while (rowText.Trim().StartsWith("#"))
                    {
                        rowText = reader.ReadLine();
                    }

                    var split = rowText.Split('|');

                    for (var j = 0; j < m; j++)
                    {
                        try
                        {
                            board[i, j].Number = Convert.ToInt32(split[j].Trim());
                        }
                        catch (FormatException) { } // Ignore format exception for blanks
                    }
                }
                
                setTextFromBoard(board);
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
                var spaces = (board.M > 9) ? "  " : " "; // To help make the columns line up propery in the text file.
                using (var saveStream = new StreamWriter(sfdSave.FileName))
                {
                    // First line is the square root of the board dimension
                    saveStream.WriteLine(board.N);

                    for (var i = 0; i < board.M; i++)
                    {
                        for (var j = 0; j < board.M; j++)
                        {
                            saveStream.Write(board[i, j].HasNumber ? board[i, j].Number.ToString() : spaces);
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
