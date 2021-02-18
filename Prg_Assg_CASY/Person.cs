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

        public Person()
        {
            SafeEntryList = new List<SafeEntry>();
            TravelEntryList = new List<TravelEntry>();
        }

        public Person(string n)
        {
            Name = n;
            SafeEntryList = new List<SafeEntry>();
            TravelEntryList = new List<TravelEntry>();
        }

        public void AddTravelEntry(TravelEntry TE)
        {
            TravelEntryList.Add(TE); //Add Travel Entry Record To Person's travel Entry List
        }

        public void AddSafeEntry(SafeEntry SE)
        {
            SafeEntryList.Add(SE); //Add Safe Entry Record to Person's Safe Entry List
        }

        public abstract double CalculateSHNCharges(TravelEntry te);

        public override string ToString()
        {
            return "\nName: " + Name; 
        }
    }
}