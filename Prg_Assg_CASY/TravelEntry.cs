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
    class TravelEntry
    {
        //Fields
        private string lastCountryOfEmbarkation;
        private string entryMode;
        private DateTime entryDate;
        private DateTime shnEndDate;
        private SHNFacility shnStay;
        private bool isPaid;

        //Properties
        public string LastCountyOfEmbarkation
        {
            get { return lastCountryOfEmbarkation; }
            set { lastCountryOfEmbarkation = value; }
        }
        public string EntryMode
        {
            get { return entryMode; }
            set { entryMode = value; }
        }
        public DateTime EntryDate
        {
            get { return entryDate; }
            set { entryDate = value; }
        }
        public DateTime ShnEndDate
        {
            get { return shnEndDate; }
            set { shnEndDate = value; }
        }
        public SHNFacility ShnStay 
        {
            get { return shnStay; }
            set { shnStay = value; }
        }
        public bool IsPaid
        {
            get { return isPaid; }
            set { isPaid = value; }
        }
        //Constructors
        public TravelEntry() { }

        public TravelEntry(string aLastCountryOfEmbarkation, string aEntryMode, DateTime aEntryDate)
        {
            LastCountyOfEmbarkation = aLastCountryOfEmbarkation;
            EntryMode = aEntryMode;
            EntryDate = aEntryDate;
        }
        
        public void AssignSHNFacility(SHNFacility facility)
        {
            ShnStay = facility; //Assign SHN Facility when User creates travel entry record and books a facility
        }

        public void CalculateSHNDuration()
        {
            int duration;
            if (LastCountyOfEmbarkation == "New Zealand" || LastCountyOfEmbarkation == "Vietnam") //if Person cam from New Zealand or Vietnam,
            {
                duration = 0; //Duration = 0 days as they do not need to serve SHN
            }
            else if (LastCountyOfEmbarkation == "Macao SAR")
            {
                duration = 7; //Duraion = 7 days as they would need to serve SHN at own accommodation for 7 days
            }
            else
            {
                duration = 14;  //Other countries' duration = 14 days 
            }
            Console.WriteLine("The Length of SHN Duration is: " + duration + " Days"); //Display Length of duration
            ShnEndDate = EntryDate.AddDays(duration); //Add the duration days to entry date to find end date of shn
            Console.WriteLine("The SHN End Date: " + ShnEndDate.ToString("dd/MM/yyyy")); //Format DateTime and Display End Date
        }

        public override string ToString()
        {
            return "Last Country of Embarkation: " + LastCountyOfEmbarkation 
                + "\nEntry Mode: " + EntryMode + "\nEntry Date: " + EntryDate + 
                "\nSHN End Date: " + shnEndDate + "\nSHN Stay: " + shnStay +
                "\nIs Paid: " + isPaid;
        }
    }
}
