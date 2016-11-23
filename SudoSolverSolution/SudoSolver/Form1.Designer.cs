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
            this.cmbSize = new System.Windows.Forms.ComboBox();
            this.btnNewBoard = new System.Windows.Forms.Button();
            this.btnTestBoard = new System.Windows.Forms.Button();
            this.btnSolve = new System.Windows.Forms.Button();
            this.btnCheckProgress = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmbSize
            // 
            this.cmbSize.FormattingEnabled = true;
            this.cmbSize.Items.AddRange(new object[] {
            "3",
            "4",
            "5",
            "6",
            "7"});
            this.cmbSize.Location = new System.Drawing.Point(44, 48);
            this.cmbSize.Name = "cmbSize";
            this.cmbSize.Size = new System.Drawing.Size(121, 21);
            this.cmbSize.TabIndex = 0;
            this.cmbSize.Text = "3";
            // 
            // btnNewBoard
            // 
            this.btnNewBoard.Location = new System.Drawing.Point(185, 45);
            this.btnNewBoard.Name = "btnNewBoard";
            this.btnNewBoard.Size = new System.Drawing.Size(89, 23);
            this.btnNewBoard.TabIndex = 1;
            this.btnNewBoard.Text = "btnNewBoard";
            this.btnNewBoard.UseVisualStyleBackColor = true;
            this.btnNewBoard.Click += new System.EventHandler(this.btnNewBoard_Click);
            // 
            // btnTestBoard
            // 
            this.btnTestBoard.Location = new System.Drawing.Point(185, 16);
            this.btnTestBoard.Name = "btnTestBoard";
            this.btnTestBoard.Size = new System.Drawing.Size(75, 23);
            this.btnTestBoard.TabIndex = 2;
            this.btnTestBoard.Text = "Test Board";
            this.btnTestBoard.UseVisualStyleBackColor = true;
            this.btnTestBoard.Click += new System.EventHandler(this.btnTestBoard_Click_1);
            // 
            // btnSolve
            // 
            this.btnSolve.Location = new System.Drawing.Point(291, 45);
            this.btnSolve.Name = "btnSolve";
            this.btnSolve.Size = new System.Drawing.Size(75, 23);
            this.btnSolve.TabIndex = 3;
            this.btnSolve.Text = "Solve";
            this.btnSolve.UseVisualStyleBackColor = true;
            this.btnSolve.Click += new System.EventHandler(this.btnSolve_Click_1);
            // 
            // btnCheckProgress
            // 
            this.btnCheckProgress.Location = new System.Drawing.Point(291, 16);
            this.btnCheckProgress.Name = "btnCheckProgress";
            this.btnCheckProgress.Size = new System.Drawing.Size(104, 23);
            this.btnCheckProgress.TabIndex = 4;
            this.btnCheckProgress.Text = "Check Progress";
            this.btnCheckProgress.UseVisualStyleBackColor = true;
            this.btnCheckProgress.Click += new System.EventHandler(this.btnCheckProgress_Click_1);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(528, 445);
            this.Controls.Add(this.btnCheckProgress);
            this.Controls.Add(this.btnSolve);
            this.Controls.Add(this.btnTestBoard);
            this.Controls.Add(this.btnNewBoard);
            this.Controls.Add(this.cmbSize);
            this.Name = "frmMain";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbSize;
        private System.Windows.Forms.Button btnNewBoard;
        private System.Windows.Forms.Button btnTestBoard;
        private System.Windows.Forms.Button btnSolve;
        private System.Windows.Forms.Button btnCheckProgress;
    }
}

