using System;
using System.Collections.Generic;
using System.Text;

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
            return base.ToString() + "Name of visitor: " + Name + "\tPassport Number: " + PassportNo + "\tNationality: " + Nationality;
        }
    }
}
