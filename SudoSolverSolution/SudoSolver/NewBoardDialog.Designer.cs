namespace SudoSolver
{
    partial class NewBoardDialog
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
            this.lstSizes = new System.Windows.Forms.ListBox();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstSizes
            // 
            this.lstSizes.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstSizes.FormattingEnabled = true;
            this.lstSizes.ItemHeight = 18;
            this.lstSizes.Location = new System.Drawing.Point(13, 13);
            this.lstSizes.Name = "lstSizes";
            this.lstSizes.Size = new System.Drawing.Size(165, 184);
            this.lstSizes.TabIndex = 0;
            this.lstSizes.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstSizes_MouseDoubleClick);
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(13, 226);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(75, 23);
            this.btnGenerate.TabIndex = 1;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(103, 226);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // NewBoardDialog
            // 
            this.AcceptButton = this.btnGenerate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(199, 261);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.lstSizes);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewBoardDialog";
            this.Text = "New Puzzle";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstSizes;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button btnCancel;
    }
}