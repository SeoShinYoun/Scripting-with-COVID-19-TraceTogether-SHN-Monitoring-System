using System;
using System.Collections.Generic;
using System.Text;

//============================================================
// Student Number : S10205100, S10203193
// Student Name : Seo Shin Youn, Phua Cheng Ann
// Module Group : T09 //============================================================

namespace Prg_Assg_CASY
{
    abstract class Person
    {
        private string name;
        private List<SafeEntry> safeEntryList;
        private List<TravelEntry> travelEntryList;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public Person() { }

        public Person(string n)
        {
            Name = n;
        }

        public void AddTravelEntry(TravelEntry TE)
        {
            travelEntryList.Add(TE);
        }

        public void AddSafeEntry(SafeEntry SE)
        {
            safeEntryList.Add(SE);
        }

        public abstract double CalculateSHNCharges();

        public override string ToString()
        {
            return base.ToString() + "\nName of person: " + Name; 
        }


    }


}
