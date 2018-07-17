using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;

namespace SArchiver1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "Choose file to process";
            fdlg.InitialDirectory = @"c:\";
            fdlg.Filter = "All files (*.*)|*.*|All files (*.*)|*.*";
            fdlg.FilterIndex = 2;
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = fdlg.FileName;
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Please select a file first!", "Error", MessageBoxButtons.OK);
            }
            else if (!File.Exists(textBox1.Text))
            {
                MessageBox.Show("File does not exist! Please select a valid file!", "Error", MessageBoxButtons.OK);
            }
            else
            {
                String zippedName = textBox1.Text + "_backup.zip";
                String fileName = Path.GetFileName(textBox1.Text);
                String toBeZipped = textBox1.Text;
                try
                {
                    using (var zip = ZipFile.Open(zippedName, ZipArchiveMode.Create))
                    {
                        zip.CreateEntryFromFile(toBeZipped, fileName);
                    }
                    MessageBox.Show("File zipped!", "Notification", MessageBoxButtons.OK);
                }
                catch (IOException ex)
                {
                    MessageBox.Show("Folder does not exist!", "Error", MessageBoxButtons.OK);
                }
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Please select a file first!", "Error", MessageBoxButtons.OK);
            }
            else if (!File.Exists(textBox1.Text))
            {
                MessageBox.Show("File does not exist! Please select a valid file!", "Error", MessageBoxButtons.OK);
            }
            else
            {
                String toEncrypt = textBox1.Text;
                File.Encrypt(toEncrypt);
                MessageBox.Show("File encrypted!", "Notification", MessageBoxButtons.OK);
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            SettingsForm settingsForm = new SettingsForm();
            settingsForm.setTextBoxText(textBox1.Text);

            FileAttributes attribs = File.GetAttributes(textBox1.Text);

            if((attribs & FileAttributes.Archive) == FileAttributes.Archive)
            {
                settingsForm.Archive();
            }

            if ((attribs & FileAttributes.Hidden) == FileAttributes.Hidden)
            {
                settingsForm.Hidden();
            }

            if ((attribs & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
            {
                settingsForm.ReadOnly();
            }

            settingsForm.ShowDialog();
        }
    }
}
