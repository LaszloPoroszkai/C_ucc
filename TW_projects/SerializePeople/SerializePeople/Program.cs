using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

enum Genders
{
    Male,
    Female
}

namespace SerializePeople
{
    class Program
    {
        public static void Main(String[] args)
        {
            String fileName = "person.ser";
            IFormatter form = new BinaryFormatter();
//            SerializeStuff(fileName, form);
            DeserializeStuff(fileName, form);
        }

        public static void SerializeStuff(String fileName, IFormatter form)
        {
            try
            {
                Person bela = new Person("bela", new DateTime(1989, 08, 12), Genders.Male);
                FileStream str = new FileStream(fileName, FileMode.Create);
                form.Serialize(str, bela);
                str.Close();
            }catch (SerializationException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void DeserializeStuff(String fileName, IFormatter form)
        {
            FileStream str = new FileStream(fileName, FileMode.Open);
            Person geza = (Person)form.Deserialize(str);
            Console.WriteLine(geza);
        }
    }

    [Serializable]
    class Person : ISerializable
    {
        public string name { get; private set; }
        public DateTime birthDate { get; private set; }
        public Genders gender { get; private set; }

        public Person(string name, DateTime birthDate, Genders gender)
        {
            this.name = name;
            this.birthDate = birthDate;
            this.gender = gender;
        }

        protected Person(SerializationInfo si, StreamingContext context)
        {
            name = si.GetString("prop1");
            birthDate = si.GetDateTime("prop2");
            gender = (Genders)si.GetValue("prop3", typeof(Genders));
        }

        public void GetObjectData(SerializationInfo si, StreamingContext context)
        {
            si.AddValue("prop1", name, typeof(String));
            si.AddValue("prop2", birthDate, typeof(DateTime));
            si.AddValue("prop3", gender, typeof(Genders));
        }

        public override string ToString()
        {
            return "Person: " + name + ", " + birthDate + ", " + gender;
        }
    }

}
