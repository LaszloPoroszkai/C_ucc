using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

enum Genders
{
    Male,
    Female
}

namespace CreateClass
{
    class Person
    {
        private string Name { get; set; }
        private DateTime BirthDate { get; set; }
        private Genders Gender { get; set; }

        public Person(string Name, DateTime BirthDate, Genders Gender)
        {
            this.Name = Name;
            this.BirthDate = BirthDate;
            this.Gender = Gender;
        }

        static void Main(string[] args)
        {
            Person Pistike = new Person("Pistike", new DateTime(2000, 01, 02), Genders.Male);
            Console.WriteLine(Pistike);
        }

        public override string ToString()
        {
            return "Person: " + Name + ", " + BirthDate + ", " + Gender;
        }

    }
}
