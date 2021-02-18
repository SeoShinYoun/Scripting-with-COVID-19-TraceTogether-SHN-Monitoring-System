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
    class Resident:Person  //Inherited class of Abstract Method and Parent Class, Person
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

        public Resident(string aName, string aAddress, DateTime aLastLeftCountry):base(aName) //retrieve name property from person class
        {
            Address = aAddress;
            LastLeftCountry = aLastLeftCountry;
        }

        public override double CalculateSHNCharges(TravelEntry te)
        {
            double charge = 0; //dummy value
            foreach(TravelEntry TE in TravelEntryList) 
            {
                if (te == TE)
                {
                    if (TE.LastCountyOfEmbarkation == "Vietnam" || TE.LastCountyOfEmbarkation == "New Zealand") // If came from Vietnam or New Zealand....
                    {
                        charge = 200; //No transportation fee only Swab Test Fee
                    }
                    else if (TE.LastCountyOfEmbarkation.ToUpper() == "MACAO SAR") //If came from Macao SAR....
                    {
                        charge = (200 + 20); //Swab Test Fee of $200 and Transportation fee of $20
                    }
                    else //If from other countries not stated....
                    {
                        charge = (200 + 20 + 1000); //Swab Test Fee of $200, Transportation Fee of $20 and SDF Charge of $1,000
                    }
                    break;
                }
            }
            return charge * 1.07; //return charge with 7% GST
        }

        public override string ToString()
        {
            return "Name of resident: " + Name + "\nAddress of resident: " + Address + "\nDate last travelled Overseas: " + LastLeftCountry;
        }
    }
}
