using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Limilabs.Client.IMAP;

namespace Villanyposta
{

    internal sealed class DataSource : INotifyPropertyChanged
    {
        Imap ic = null;
        User user = null;
        List<long> uids = new List<long>();

        private static DataSource instance = null;

        private DataSource()
        {
            this.PropertyChanged += ConnectionAdded;
        }

        public static DataSource Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DataSource();
                }
                return instance;
            }
        }

        public Imap Ic
        {
            get
            {
                return ic;
            }
            set
            {
                ic = value;
                OnConnectionAdded(new PropertyChangedEventArgs("ic"));
            }
        }

        internal User User { get => user; set => user = value; }
        public List<long> Uids { get => uids; set => uids = value; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnConnectionAdded(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

        private void ConnectionAdded(object sender, EventArgs e)
        {
            if (ic != null)
            {
                ic.SelectInbox();
                Uids = ic.Search(Flag.All);
            }
            else
            {
                Uids = null;
            }
        }

    }
}