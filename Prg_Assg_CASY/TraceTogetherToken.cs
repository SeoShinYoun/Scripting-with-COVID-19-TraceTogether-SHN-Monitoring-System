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
    class TraceTogetherToken
    {
        private string serialNo;
        private string collectionLocation;
        private DateTime expiryDate;

        public string SerialNo
        {
            get { return serialNo; }
            set { serialNo = value; }
        }

        public string CollectionLocation
        {
            get { return collectionLocation; }
            set { collectionLocation = value; }
        }

        public DateTime ExpiryDate
        {
            get { return expiryDate; }
            set { expiryDate = value; }
        }

        public TraceTogetherToken() { }

        public TraceTogetherToken(string sn, string cl, DateTime ed)
        {
            SerialNo = sn;
            CollectionLocation = cl;
            ExpiryDate = ed;
        }

        public bool IsEligibleForReplacement()
        {
            DateTime currentDate = DateTime.Now;
            DateTime diffOfOneMonth = ExpiryDate.AddMonths(-1); /* subtracting one month from the expiration date, where 'diffofonemore' would be store the value*/

            if (currentDate < diffOfOneMonth) /* If The current date is 'less than' which is not one month from the expiration date, the user would not be eligible for a trace together token replacement*/
            {
                Console.WriteLine("You are not eligible for a replacement of your Trace Together Token!");
                return false;
            }
            else /* Else if the current date is one month from the expiration date, the user would hence be available for the replacement of the Trace Together Token*/
            {
                Console.WriteLine("You are eligble for a replacement of your Trace Together Token!");
                return true;
            }
            
        }

        public void ReplaceToken(string sn, string cl)
        {
            System.Random ran5 = new System.Random();
            sn = "T" + ran5.Next(10000,99999); //Token Serial number where 5 diogits are generated randomly for the new token 

            Console.WriteLine("Enter the Location where you would like to collect your new token: ");
            CollectionLocation = Console.ReadLine();

            ExpiryDate = DateTime.Now.AddMonths(6);

            TraceTogetherToken tt = new TraceTogetherToken(SerialNo, CollectionLocation, ExpiryDate);
        }
    }
}
