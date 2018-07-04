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

    class Employee : Person
    {
        public int Salary { get; private set; }
        public string Profession { get; private set; }
        public Room room { get; private set; }

        public Employee(string Name, DateTime BirthDate, Genders Gender, int Salary, string Profession, Room room) : base(Name, BirthDate, Gender)
        {
            this.Salary = Salary;
            this.Profession = Profession;
            this.room = room;
        }

        public override string ToString()
        {
            return "Employee: " + Name + ", " + BirthDate + ", " + Gender + ", " + Salary;
        }
    }

    class Room
    {
        public int RoomNumber { get; private set; }
        
        public Room(int RoomNumber)
        {
            this.RoomNumber = RoomNumber;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Person Pistike = new Employee("Pistike", new DateTime(2000, 01, 02), Genders.Male, 2000, "Worker", new Room(200));
            Console.WriteLine(Pistike);
        }
    }

}
