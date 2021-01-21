using System;
using System.Collections.Generic;
using System.Text;

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

        public Resident(string ad, string n, DateTime lc):base(n)
        {
            Address = ad;
            LastLeftCountry = lc;
        }

        public override double CalculateSHNCharges()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return base.ToString() + "Name of resident: " + Name + "\tAddress of resident: " + Address + "\nDate last travelled Overseas: " + LastLeftCountry;
        }
    }
}
