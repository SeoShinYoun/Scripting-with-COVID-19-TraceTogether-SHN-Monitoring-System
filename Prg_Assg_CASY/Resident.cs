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
    class Resident:Person
    {
        private string address;
        private DateTime lastLeftCountry;
        private TraceTogetherToken token;

        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        public DateTime LastLeftCountry
        {
            get { return lastLeftCountry; }
            set { lastLeftCountry = value; }
        }

        public TraceTogetherToken Token
        {
            get { return token; }
            set { token = value; }
        }

        public Resident(string aName, string aAddress, DateTime aLastLeftCountry):base(aName)
        {
            Address = aAddress;
            LastLeftCountry = aLastLeftCountry;
        }

        public override double CalculateSHNCharges()
        {
            double charge = 0;
            foreach(TravelEntry TE in TravelEntryList)
            {
                if (TE.LastCountyOfEmbarkation == "Vietnam" || TE.LastCountyOfEmbarkation == "New Zealand")
                {
                    charge = 200;
                }
                else if (TE.LastCountyOfEmbarkation == "MACAO SAR")
                {
                     charge = (200 + 20);
                }
                else
                {
                    charge = (200 + 20 + 1000);
                }
            }
            return charge * 1.07;
        }

        public override string ToString()
        {
            return "Name of resident: " + Name + "\nAddress of resident: " + Address + "\nDate last travelled Overseas: " + LastLeftCountry;
        }
    }
}
