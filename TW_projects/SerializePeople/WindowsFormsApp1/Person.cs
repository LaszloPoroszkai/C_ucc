using System;
using System.Runtime.Serialization;

namespace WindowsFormsApp1
{
    [Serializable]
    class Person : ISerializable, IComparable
    {
        private String name;
        private String address;
        private long phoneNumber;
        private DateTime recordedOn;
        private int id;

        public string Name { get => name; }
        public string Address { get => address; }
        public long PhoneNumber { get => phoneNumber; }
        public DateTime RecordedOn { get => recordedOn; }
        public int Id { get => id; set => id = value; }
        public static int Counter { get => counter; set => counter = value; }

        private static int counter;

        public Person(String name, String address, long phoneNumber)
        {
            this.name = name;
            this.address = address;
            this.phoneNumber = phoneNumber;
            this.recordedOn = DateTime.Now;
            Counter += 1;
            this.Id = Counter;
        }

        protected Person(SerializationInfo si, StreamingContext context)
        {
            name = si.GetString("prop1");
            address = si.GetString("prop2");
            phoneNumber = si.GetInt64("prop3");
            recordedOn = si.GetDateTime("prop4");
        }

        public void GetObjectData(SerializationInfo si, StreamingContext context)
        {
            si.AddValue("prop1", name, typeof(String));
            si.AddValue("prop2", address, typeof(String));
            si.AddValue("prop3", phoneNumber, typeof(int));
            si.AddValue("prop4", recordedOn, typeof(DateTime));
        }

        public override string ToString()
        {
            return "Person: " + name + ", " + address + ", " + phoneNumber;
        }

        public int CompareTo(Object o)
        {
            if (o == null) return 1;

            Person person = o as Person;
            if (person != null)
                return this.id.CompareTo(person.id);
            else
                throw new ArgumentException("Object is not a Temperature");
        }
    }
}
