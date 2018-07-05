using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections;

namespace ProcessForms
{
    public partial class Form1 : Form
    {
        Process[] p = Process.GetProcesses().OrderBy(m => m.ProcessName).ToArray();
        List<Comment> Comments = new List<Comment>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateProcList();
        }

        private void UpdateProcList()
        {
            p = Process.GetProcesses();
            processList.DataSource = p;
            processList.DisplayMember = "ProcessName";
            processList.ValueMember = "Id";
        }

        private void UpdateProcList(object sender, EventArgs e)
        {
            UpdateProcList();
        }

        private void DisplayValues(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(processList.SelectedItem.ToString()))
            {
                foreach (Process proc in p)
                {
                    if (proc.Id == Int32.Parse(processList.SelectedValue.ToString()))
                    {
                        PerformanceCounter myAppCpu = new PerformanceCounter("Process", "% Processor Time", proc.ProcessName, true);
                        textBox1.Text = "";
                        textBox2.Text = proc.Id.ToString();
                        textBox3.Text = myAppCpu.NextValue().ToString();
                        textBox4.Text = proc.WorkingSet64.ToString();
                        try
                        {
                            textBox5.Text = (DateTime.Now - proc.StartTime).ToString();
                            textBox6.Text = proc.StartTime.ToString();
                        }catch(System.ComponentModel.Win32Exception)
                        {
                            textBox5.Text = "Access Denied";
                            textBox6.Text = "Access Denied";
                        }
                        foreach(Comment comm in Comments)
                        {
                            if (comm.Id == Int32.Parse(processList.SelectedValue.ToString()))
                            {
                                textBox1.Text = comm.Comm;
                            }
                        }
                    }
                }
            }
        }

        private void SaveComment(object sender, EventArgs e)
        {
            Comments.Add(new Comment(textBox1.Text, Int32.Parse(processList.SelectedValue.ToString())));
        }
    }

    public class Comment
    {
        public string Comm { get; set; }
        public int Id { get; set; }

        public Comment(String Comm, int Id)
        {
            this.Comm = Comm;
            this.Id = Id;
        }
    }
}
