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
        public string Name { get; private set; }
        public DateTime BirthDate { get; private set; }
        public Genders Gender { get; private set; }

        public Person(string Name, DateTime BirthDate, Genders Gender)
        {
            this.Name = Name;
            this.BirthDate = BirthDate;
            this.Gender = Gender;
        }

        public override string ToString()
        {
            return "Person: " + Name + ", " + BirthDate + ", " + Gender;
        }
    }

    class Employee : Person, ICloneable
    {
        public int Salary { get; private set; }
        public string Profession { get; private set; }
        public Room Room { get; set; }

        public Employee(string Name, DateTime BirthDate, Genders Gender, int Salary, string Profession, Room Room) : base(Name, BirthDate, Gender)
        {
            this.Salary = Salary;
            this.Profession = Profession;
            this.Room = Room;
        }

        public override string ToString()
        {
            return "Employee: " + Name + ", " + BirthDate + ", " + Gender + ", " + Salary + ", " + Room.Number;
        }

        public object Clone()
        {
            Employee newEmployee = (Employee)this.MemberwiseClone();
            newEmployee.Room = new Room(Room.Number);
            return newEmployee;
        }
    }

    class Room
    {
        public int Number { get; set; }
        
        public Room(int Number)
        {
            this.Number = Number;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Employee Kovacs = new Employee("Géza", DateTime.Now, Genders.Male, 1000, "léhűtő", new Room(111));
            Employee Kovacs2 = (Employee)Kovacs.Clone();
            Kovacs2.Room.Number = 112;
            Console.WriteLine(Kovacs.ToString());
            Console.WriteLine(Kovacs2.ToString());
            Console.ReadKey();
        }
    }

}
