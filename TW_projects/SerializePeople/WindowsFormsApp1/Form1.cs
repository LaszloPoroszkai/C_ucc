using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        Data db = Data.Instance;

        public Form1()
        {
            InitializeComponent();
            LoadFirst();
        }

        public void LoadFirst()
        {
            IFormatter form = new BinaryFormatter();
            String path = Directory.GetCurrentDirectory();
            var ext = new List<string> { ".dat" };
            var files = Directory.GetFiles(path).Where(s => ext.Contains(Path.GetExtension(s)));
            
            if (files.LongCount() < 1)
            { return; }
            else
            {
                foreach (String file in files)
                {
                    var match = new Regex(@"^Person(\d{1,2})\.dat").Match(Path.GetFileName(file));
                    int id = int.Parse(match.Groups[1].Value);
                    Person newPerson = DeserializePerson(match.Groups[0].ToString(), form);
                    newPerson.Id = id;
                    db.addToPersons(newPerson);
                }

                db.AllPersons.Sort();

                Person.Counter = db.AllPersons[db.AllPersons.Count-1].Id;

                Person person = db.getFirstPerson();

                updateForm(person);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (Control tb in this.Controls)
            {
                if(tb is TextBox)
                {
                    ((TextBox)tb).Text = String.Empty;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                Person person = new Person(textBox1.Text, textBox2.Text, Convert.ToInt64(textBox3.Text));
                String fileName = "Person" + person.Id + ".dat";
                IFormatter form = new BinaryFormatter();
                SerializePerson(fileName, form, person);
            }
        }

        private void SerializePerson(string fileName, IFormatter form, Person person)
        {
            FileStream str = new FileStream(fileName, FileMode.Create);
            form.Serialize(str, person);
            str.Close();
        }

        private Person DeserializePerson(string fileName, IFormatter form)
        {
            FileStream str = new FileStream(fileName, FileMode.Open);
            return (Person)form.Deserialize(str);
        }

        public bool ValidateInput()
        {
            Regex reg1 = new Regex(@"^[A-Z]*\s[A-Z]*");
            Regex reg2 = new Regex(@"^[A-Z]*[,]\s[A-Z]*[,]\s\d+");
            Regex reg3 = new Regex(@"\d{9}");

            if(!reg1.IsMatch(textBox1.Text))
            {
                MessageBox.Show("Name format invalid.\nCorrect it and try again!", "Error", MessageBoxButtons.OK);
                return false;
            }
            else if(!reg2.IsMatch(textBox2.Text))
            {
                MessageBox.Show("Address format invalid.\nMake sure it is City, Address, Number!", "Error", MessageBoxButtons.OK);
                return false;
            }
            else if (!reg3.IsMatch(textBox3.Text))
            {
                MessageBox.Show("Not a valid Hungarian cell phone number.\nMake sure it is numbers only, 9 digits!", "Error", MessageBoxButtons.OK);
                return false;
            }
            else
            {
                return true;
            }
        }

        private void updateForm(Person person)
        {
            textBox1.Text = person.Name;
            textBox2.Text = person.Address;
            textBox3.Text = person.PhoneNumber.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Person person = db.getFirstPerson();
            updateForm(person);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (db.updateIndex(-1))
            {
                Person person = db.AllPersons[db.CurrIndex];
                updateForm(person);
            }
            else
            {
                MessageBox.Show("No more users available!", "Warning", MessageBoxButtons.OK);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (db.updateIndex(1))
            {
                Person person = db.AllPersons[db.CurrIndex];
                updateForm(person);
            }
            else
            {
                MessageBox.Show("No more users available!", "Warning", MessageBoxButtons.OK);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Person person = db.getLastPerson();
            updateForm(person);
        }
    }
}
