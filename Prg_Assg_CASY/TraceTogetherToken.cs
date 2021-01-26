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
                Console.WriteLine("--------------------------------------------------------------------");
                Console.WriteLine("You are NOT eligible for a replacement of your Trace Together Token!");
                Console.WriteLine("--------------------------------------------------------------------");
                return false;
            }
            else /* Else if the current date is one month from the expiration date, the user would hence be available for the replacement of the Trace Together Token*/
            {
                Console.WriteLine("---------------------------------------------------------------");
                Console.WriteLine("You are eligble for a replacement of your Trace Together Token!");
                Console.WriteLine("---------------------------------------------------------------");
                return true;
            }
        }

        public void ReplaceToken(string sn, string cl)
        {
            System.Random ran5 = new System.Random();
            int newSN = ran5.Next(10000,99999); //Token Serial number where 5 diogits are generated randomly for the new token 
            SerialNo = "T" + newSN; // To create the new token number 
            Console.WriteLine("Enter the Location where you would like to collect your new token: ");
            CollectionLocation = Console.ReadLine(); // To store the location where the user would like to collect their new token 

            ExpiryDate = DateTime.Now.AddMonths(6); // To create the new expiry date to be given to the new token 

            TraceTogetherToken tt = new TraceTogetherToken(SerialNo, CollectionLocation, ExpiryDate); // A new string is made to store the new information of the token and to update the token details as a new token 
            Console.WriteLine("");
            Console.WriteLine("======================================================");
            Console.WriteLine("Here are the details of your new Trace Together Token!");
            Console.WriteLine("======================================================");
            Console.WriteLine(tt.ToString());// To print out the new data of the token 
            Console.WriteLine("");
        }

        public override string ToString()
        {
            return "Serial Number: " + SerialNo + "\nExpiry Date: " + ExpiryDate + "\nLocation last Collected: " + CollectionLocation; 
        }
    }
}
