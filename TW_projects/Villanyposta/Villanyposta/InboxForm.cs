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
using Limilabs.Mail;
using Limilabs.Mail.Headers;

namespace Villanyposta
{
    public partial class InboxForm : Form
    {
        DataSource ds = DataSource.Instance;

        public InboxForm()
        {
            InitializeComponent();
            foreach (long uid in ds.Uids)
            {
                IMail email = new MailBuilder()
                    .CreateFromEml(ds.Ic.GetMessageByUID(uid));
                listBox1.Text = email.Subject;
                listBox1.ValueMember = uid.ToString();
            }
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            if (listBox1.ValueMember != null)
            {
                long mailId = Convert.ToInt64(listBox1.ValueMember);
                foreach (long uid in ds.Uids)
                {
                    if (mailId == uid)
                    {
                        IMail email = new MailBuilder()
                            .CreateFromEml(ds.Ic.GetMessageByUID(uid));
                        textBox1.Text = email.Text;
                        foreach (MailBox mb in email.From)
                        {
                            textBox2.Text = textBox2.Text + mb.Address;
                        }
                    }
                }
            }
        }
    }
}
