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

namespace SArchiver1
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        public void setTextBoxText(String path)
        {
            textBox1.Text = path;
        }

        public void Archive()
        {
            checkBox1.CheckState = CheckState.Checked;
        }

        public void Hidden()
        {
            checkBox2.CheckState = CheckState.Checked;
        }

        public void ReadOnly()
        {
            checkBox3.CheckState = CheckState.Checked;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FileAttributes attr = File.GetAttributes(textBox1.Text);

            if(checkBox1.Checked)
            {
                File.SetAttributes(textBox1.Text, File.GetAttributes(textBox1.Text) | FileAttributes.Archive);
            }
            else
            {
                File.SetAttributes(textBox1.Text, File.GetAttributes(textBox1.Text) & ~FileAttributes.Archive);
            }

            if (checkBox2.Checked)
            {
                File.SetAttributes(textBox1.Text, File.GetAttributes(textBox1.Text) | FileAttributes.Hidden);
            }
            else
            {
                File.SetAttributes(textBox1.Text, File.GetAttributes(textBox1.Text) & ~FileAttributes.Hidden);
            }

            if (checkBox3.Checked)
            {
                File.SetAttributes(textBox1.Text, File.GetAttributes(textBox1.Text) | FileAttributes.ReadOnly);
            }
            else
            {
                File.SetAttributes(textBox1.Text, File.GetAttributes(textBox1.Text) & ~FileAttributes.ReadOnly);
            }
            this.Close();
        }
    }
}
