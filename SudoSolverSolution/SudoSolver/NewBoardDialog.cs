using System;
using System.Windows.Forms;

namespace SudoSolver
{
    public partial class NewBoardDialog : Form
    {
        /// <summary>
        /// The size of the new board/puzzle that the user selected.
        /// </summary>
        public int ChosenSize
        {
            get
            {
                return ((PuzzleSize)lstSizes.SelectedItem).Value;
            }
        }

        /// <summary>
        /// Displays a dialog asking the user to pick a puzzle size.
        /// </summary>
        public NewBoardDialog()
        {
            InitializeComponent();

            lstSizes.Items.Add(new PuzzleSize("  1 x 1", 1));
            lstSizes.Items.Add(new PuzzleSize("  4 x 4", 2));
            lstSizes.Items.Add(new PuzzleSize("  9 x 9", 3));
            lstSizes.Items.Add(new PuzzleSize("16 x 16", 4));
            lstSizes.Items.Add(new PuzzleSize("25 x 25", 5));
            lstSizes.Items.Add(new PuzzleSize("36 x 36", 6));
            lstSizes.Items.Add(new PuzzleSize("49 x 49", 7));
            lstSizes.SelectedIndex = 1;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void lstSizes_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }

    public class PuzzleSize
    {
        public readonly int Value;
        public readonly string Label;
        public PuzzleSize(string label, int value)
        {
            Label = label;
            Value = value;
        }
        public override string ToString()
        {
            return Label;
        }
    }
}
