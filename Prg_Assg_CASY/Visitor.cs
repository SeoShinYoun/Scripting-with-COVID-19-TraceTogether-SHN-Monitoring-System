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
    class Visitor: Person //Inherited class of Abstract Method and Parent Class, Person
    {
        private string passportNo;
        private string nationality;

        public string PassportNo
        {
            get { return passportNo; }
            set { passportNo = value; }
        }
        public string Nationality
        {
            get { return nationality; }
            set { nationality = value; }
        }

        public Visitor(string aName, string aPassportNo, string aNationality):base(aName) //retrieve name property from person class
        {
            PassportNo = aPassportNo;
            Nationality = aNationality;
        }

        public override double CalculateSHNCharges()
        {
            double charge = 0; //dummy value
            foreach (TravelEntry TE in TravelEntryList)
            {
                if (TE.LastCountyOfEmbarkation == "Vietnam" || TE.LastCountyOfEmbarkation == "New Zealand" || TE.LastCountyOfEmbarkation.ToUpper() == "MACAO SAR") //If Visitor cam from Vietnam, New Zealand, or Macao SAR
                {
                    charge = 200 + 80; //Swab Test Fee of $200 and Transportation Cost of $80
                }
                else
                {
                    charge = 200 + 2000 + TE.ShnStay.CalculateTravelCost(TE.EntryMode, TE.EntryDate); //Swab Test Fee of $200, SDF charge of $2,000 and Calling CalculateTravelCost(...) to calculate Transporation Fee
                }
            }
            return charge *= 1.07; //return charge with 7% GST
        }

        public override string ToString()
        {
            return base.ToString() + "\tPassport Number: " + PassportNo + "\tNationality: " + Nationality;
        }
    }
}
