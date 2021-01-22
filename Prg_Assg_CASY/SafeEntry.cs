using System;
using System.Collections.Generic;
using System.Text;

//============================================================
// Student Number : S10205100, S10203193
// Student Name : Seo Shin Youn, Phua Cheng Ann
// Module Group : T09 //============================================================

namespace Prg_Assg_CASY
{
    class SafeEntry
    {
        private DateTime checkIn;
        private DateTime checkOut;
        private BusinessLocation location;

        public DateTime CheckIn
        {
            get { return checkIn; }
            set { checkIn = value; }
        }

        public DateTime CheckOut
        { 
            get { return checkOut; }
            set { checkOut = value; }
        }

        public BusinessLocation Location
        {
            get { return location; }
            set { location = value; }
        }

        public SafeEntry() { }

        public SafeEntry(DateTime In, BusinessLocation L )
        {
            CheckIn = In;
            Location = L;
        }

        public virtual void PerformCheckOut()
        {
            CheckOut = DateTime.Now; 
        }

        public override string ToString()
        {
            return base.ToString() + "Location: " + Location + "\nCheck in time: " + CheckIn + "\ncheck out time: " + CheckOut;
        }
    }
}
