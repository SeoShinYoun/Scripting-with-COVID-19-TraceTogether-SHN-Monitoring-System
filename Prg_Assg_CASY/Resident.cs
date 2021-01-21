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

        public Resident() { }

        public Resident(string ad, string n, DateTime lc)
        {
            Address = ad;
            Name = n;
            LastLeftCountry = lc;
        }

        /*public double CalculateSHNCharges()
        {

        }*/

        public override string ToString()
        {
            return base.ToString() + "Name of resident: " + Name + "\nAddress of resident: " + Address + "\nDate last travelled Overseas: " + LastLeftCountry;
        }

    }
}
