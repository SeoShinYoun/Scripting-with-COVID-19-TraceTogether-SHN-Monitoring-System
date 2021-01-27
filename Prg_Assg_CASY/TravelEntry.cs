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
            
        }

        public void CalculateSHNDuration()
        {
            int duration;
            if (LastCountyOfEmbarkation == "New Zealand" || LastCountyOfEmbarkation == "Vietnam")
            {
                duration = 0;
            }
            else if (LastCountyOfEmbarkation == "Macao SAR")
            {
                duration = 7;
            }
            else
            {
                duration = 14;
            }
            Console.WriteLine("The Length of SHN Duration is: " + duration + " Days");
            ShnEndDate = EntryDate.AddDays(duration);
            Console.WriteLine("The SHN End Date: " + ShnEndDate.ToString("dd/MM/yyyy"));
        }

        public override string ToString()
        {
            return "Last Country of Embarkation: " + LastCountyOfEmbarkation 
                + "\tEntry Mode: " + EntryMode + "\tEntry Date" + EntryDate + 
                "\tSHN End Date: " + shnEndDate + "\tSHN Stay: " + shnStay +
                "\tIs Paid: " + isPaid;
        }
    }
}
