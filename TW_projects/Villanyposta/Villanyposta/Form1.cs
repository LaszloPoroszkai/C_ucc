using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Limilabs.Client.IMAP;

namespace Villanyposta
{
    public partial class Form1 : Form
    {
        DataSource ds = DataSource.Instance;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            User user = new User(textBox1.Text + "@gmail.com", textBox2.Text);

            using (Imap ic = new Imap())
            {
                ic.ConnectSSL("imap.gmail.com");
                try
                {
                    ic.Login(user.UserName, user.Password);
                    ds.Ic = ic;
                    ds.User = user;
                    InboxForm ibf = new InboxForm();
                    this.Hide();
                    ibf.Show();
                }catch (ImapResponseException er)
                {
                    MessageBox.Show("Gmail refused connection!\nPassword might be incorrect.", "Error", MessageBoxButtons.OK);
                }
                ic.Close(true);
            }
        }
    }
}
