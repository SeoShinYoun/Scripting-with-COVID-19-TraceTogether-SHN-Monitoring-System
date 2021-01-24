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
    class Visitor:Person
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

        public Visitor(string aName, string aPassportNo, string aNationality):base(aName)
        {
            PassportNo = aPassportNo;
            Nationality = aNationality;
        }

        public override double CalculateSHNCharges()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return base.ToString() + "\tPassport Number: " + PassportNo + "\tNationality: " + Nationality;
        }
    }
}
