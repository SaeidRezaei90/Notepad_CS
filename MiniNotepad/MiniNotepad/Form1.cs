using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniNotepad
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string _originalTextData = string.Empty;

        private void Save(bool isSaveAs = false)
        {
            string fileName = string.Empty;
            if ((FilePathToolStripStatusLabel.Text == string.Empty) || (isSaveAs))
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Filter = "All Text Files|*.txt";
                if (dlg.ShowDialog() == DialogResult.OK)

                    fileName = dlg.FileName;
            }
            else
                fileName = FilePathToolStripStatusLabel.Text;
            if(fileName != string.Empty)
                File.WriteAllText(fileName, MainTextBox.Text);

        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(MainTextBox.Text))
                return;
            Save();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "All Text Files|*.txt";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string fileName = dlg.FileName;
                _originalTextData = MainTextBox.Text = File.ReadAllText(fileName);
                FilePathToolStripStatusLabel.Text = fileName;
                
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(MainTextBox.Text))
                return;
            Save(true);
            
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog dlg = new FontDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                MainTextBox.Font = dlg.Font;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_originalTextData.CompareTo(MainTextBox.Text) != 0)
            {
                DialogResult result = MessageBox.Show("Do you want to save changes", "Error", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                switch (result)
                {
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                    case DialogResult.Yes:
                        Save();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
