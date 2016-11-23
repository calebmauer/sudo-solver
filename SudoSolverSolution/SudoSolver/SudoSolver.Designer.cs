namespace SudoSolver
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadTestBoardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadPuzzleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sudokuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dheckProgressToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.solveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkProgressToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.solveToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ofdOpenExisting = new System.Windows.Forms.OpenFileDialog();
            this.sfdSave = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.sudokuToolStripMenuItem,
            this.checkProgressToolStripMenuItem,
            this.solveToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(528, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.loadTestBoardToolStripMenuItem,
            this.loadPuzzleToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // loadTestBoardToolStripMenuItem
            // 
            this.loadTestBoardToolStripMenuItem.Name = "loadTestBoardToolStripMenuItem";
            this.loadTestBoardToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.loadTestBoardToolStripMenuItem.Text = "Load the Test Board";
            this.loadTestBoardToolStripMenuItem.Click += new System.EventHandler(this.loadTestBoardToolStripMenuItem_Click);
            // 
            // loadPuzzleToolStripMenuItem
            // 
            this.loadPuzzleToolStripMenuItem.Name = "loadPuzzleToolStripMenuItem";
            this.loadPuzzleToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.loadPuzzleToolStripMenuItem.Text = "Load Puzzle";
            this.loadPuzzleToolStripMenuItem.Click += new System.EventHandler(this.loadPuzzleToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.saveToolStripMenuItem.Text = "Save As";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // sudokuToolStripMenuItem
            // 
            this.sudokuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dheckProgressToolStripMenuItem,
            this.solveToolStripMenuItem});
            this.sudokuToolStripMenuItem.Name = "sudokuToolStripMenuItem";
            this.sudokuToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.sudokuToolStripMenuItem.Text = "Sudoku";
            // 
            // dheckProgressToolStripMenuItem
            // 
            this.dheckProgressToolStripMenuItem.Name = "dheckProgressToolStripMenuItem";
            this.dheckProgressToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.dheckProgressToolStripMenuItem.Text = "Check Progress";
            this.dheckProgressToolStripMenuItem.Click += new System.EventHandler(this.checkProgressToolStripMenuItem_Click);
            // 
            // solveToolStripMenuItem
            // 
            this.solveToolStripMenuItem.Name = "solveToolStripMenuItem";
            this.solveToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.solveToolStripMenuItem.Text = "Solve";
            this.solveToolStripMenuItem.Click += new System.EventHandler(this.solveToolStripMenuItem_Click);
            // 
            // checkProgressToolStripMenuItem
            // 
            this.checkProgressToolStripMenuItem.Name = "checkProgressToolStripMenuItem";
            this.checkProgressToolStripMenuItem.Size = new System.Drawing.Size(100, 20);
            this.checkProgressToolStripMenuItem.Text = "Check Progress";
            this.checkProgressToolStripMenuItem.Click += new System.EventHandler(this.checkProgressToolStripMenuItem_Click);
            // 
            // solveToolStripMenuItem1
            // 
            this.solveToolStripMenuItem1.Name = "solveToolStripMenuItem1";
            this.solveToolStripMenuItem1.Size = new System.Drawing.Size(47, 20);
            this.solveToolStripMenuItem1.Text = "Solve";
            this.solveToolStripMenuItem1.Click += new System.EventHandler(this.solveToolStripMenuItem_Click);
            // 
            // ofdOpenExisting
            // 
            this.ofdOpenExisting.Filter = "Sudoku File|*.sudo";
            // 
            // sfdSave
            // 
            this.sfdSave.Filter = "Sudoku File|*.sudo";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(528, 445);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.Text = "Sudo Solver";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadPuzzleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sudokuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dheckProgressToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem solveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadTestBoardToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog ofdOpenExisting;
        private System.Windows.Forms.SaveFileDialog sfdSave;
        private System.Windows.Forms.ToolStripMenuItem checkProgressToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem solveToolStripMenuItem1;
    }
}

