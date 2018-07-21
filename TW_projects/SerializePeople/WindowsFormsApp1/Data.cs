using System.Collections.Generic;

namespace WindowsFormsApp1
{
    public sealed class Data
    {
        private List<Person> allPersons = new List<Person>();
        private Person activePerson = null;
        private int currIndex = 0;

        internal List<Person> AllPersons { get => allPersons; }

        public int CurrIndex { get => currIndex; }

        internal void addToPersons(Person person)
        {
            allPersons.Add(person);
        }

        private static Data instance = null;

        private Data()
        {
        }

        public static Data Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Data();
                }
                return instance;
            }
        }

        internal Person getFirstPerson()
        {
            currIndex = 0;
            activePerson = allPersons[0];
            return activePerson;
        }

        internal Person getLastPerson()
        {
            currIndex = AllPersons.Count - 1;
            activePerson = allPersons[AllPersons.Count - 1];
            return activePerson;
        }
        
        internal bool updateIndex(int by)
        {
            if ((currIndex + by) >= 0 && (currIndex + by) <= (allPersons.Count)-1)
            {
                currIndex = currIndex + by;
                return true;
            }else{
                return false;
            }
        }
    }
}
