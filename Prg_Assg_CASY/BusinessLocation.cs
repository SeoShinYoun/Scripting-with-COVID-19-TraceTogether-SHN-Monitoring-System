using System;
using System.Collections.Generic;
using System.Text;

//============================================================
// Student Number : S10205100, S10203193
// Student Name : Seo Shin Youn, Phua Cheng Ann
// Module Group : T09 //============================================================

namespace Prg_Assg_CASY
{
    class BusinessLocation
    {
        private string businessName;
        private string branchCode;
        private int maximumCapacity;
        private int visitorsNow; 

        public string BusinessName
        {
            get { return businessName; }
            set { businessName = value; }
        }

        public string BranchCode
        {
            get { return branchCode; }
            set { branchCode = value; }
        }

        public int MaximumCapacity
        {
            get { return maximumCapacity; }
            set { maximumCapacity = value; }
        }

        public int VisitorsNow
        {
            get { return visitorsNow; }
            set { visitorsNow = value; }
        }

        public BusinessLocation() { }

        public BusinessLocation(string bn, string c, int mc)
        {
            businessName = bn;
            branchCode = c;
            maximumCapacity = mc;
        }

        public bool IsFull()
        {
            if (VisitorsNow == MaximumCapacity) /* When the number of visitors is at the max capacity, the business would be 'full' */
            {
                Console.WriteLine("Business location is at maximum capacity");
                return true;
            }
            
            return false;
        }

        public override string ToString()
        {
            return base.ToString() + "Name of Business: " + BusinessName + "Branch Code: " + BranchCode + "Maximum Capacity: " + MaximumCapacity;
        }

    }
}
