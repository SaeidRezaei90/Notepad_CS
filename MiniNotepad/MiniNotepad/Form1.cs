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

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(MainTextBox.Text) && MainTextBox.Text.CompareTo(_originalTextData) != 0)
            {
                DialogResult _save = MessageBox.Show("Do you want to save the file?", "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                switch (_save)
                {
                    case DialogResult.Cancel:
                        break;

                    case DialogResult.Yes:
                        Save();
                        MainTextBox.Text = string.Empty;
                        break;

                    case DialogResult.No:
                        MainTextBox.Text = "";
                        break;
                    default:
                        break;
                }

            }
        }

        private void Save(bool isSaveAs = false)
        {
            SaveFileDialog sve = new SaveFileDialog();
            sve.Filter = "All File Text|*.txt";
            sve.Title = "Save File Dialog";
            sve.FileName = "file1.txt";
            if (sve.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(sve.FileName, MainTextBox.Text);
                _originalTextData = MainTextBox.Text;
            }
            else
                return;

        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(MainTextBox.Text))
                return;
            Save();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MainTextBox.Text != "")
            {
                {
                    Save();
                }

            }
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = "All Text Files|*.txt";
                dlg.Title = "Open File Dialog";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    _originalTextData = MainTextBox.Text = File.ReadAllText(dlg.FileName);
                    FilePathToolStripStatusLabel.Text = dlg.FileName;
                }
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fnt = new FontDialog();
            if (fnt.ShowDialog() == DialogResult.OK)
            {
                MainTextBox.Font = fnt.Font;
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
                    case DialogResult.No:
                        break;

                    default:
                        e.Cancel = true;
                        break;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


    }
}
