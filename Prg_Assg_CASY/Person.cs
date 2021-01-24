using System;
using System.Collections.Generic;
using System.Text;

//============================================================
// Student Number : S10205100, S10203193
// Student Name : Seo Shin Youn, Phua Cheng Ann
// Module Group : T09 
//============================================================

namespace Prg_Assg_CASY
{
    abstract class Person
    {
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public List<SafeEntry> SafeEntryList { get; set; }
        public List<TravelEntry> TravelEntryList { get; set; }

        public Person() { }

        public Person(string n)
        {
            Name = n;
        }

        public void AddTravelEntry(TravelEntry TE)
        {
            TravelEntryList = new List<TravelEntry>();
            TravelEntryList.Add(TE);
        }

        public void AddSafeEntry(SafeEntry SE)
        {
            SafeEntryList = new List<SafeEntry>();
            SafeEntryList.Add(SE);
        }

        public abstract double CalculateSHNCharges();

        public override string ToString()
        {
            return "\nName: " + Name; 
        }
    }
}