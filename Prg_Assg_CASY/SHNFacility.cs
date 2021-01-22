using System;
using System.Collections.Generic;
using System.Text;

//============================================================
// Student Number : S10205100, S10203193
// Student Name : Seo Shin Youn, Phua Cheng Ann
// Module Group : T09 //============================================================

namespace Prg_Assg_CASY
{
    class SHNFacility
    {
        //Fields
        private string facilityName;
        private string facilityCapacity;
        private string facilityVacancy;
        private double distFromAirCheckpoint;
        private double distFromSeaCheckpoint;
        private double distFromLandCheckpoint;

        //Properties
        public string FacilityName
        {
            get { return facilityName; }
            set { facilityName = value; }
        }
        public string FacilityCapacity
        {
            get { return facilityCapacity; }
            set { facilityCapacity = value; }
        }
        public string FacilityVacancy
        {
            get { return facilityVacancy; }
            set { facilityVacancy = value; }
        }
        public double DistFromAirCheckpoint
        {
            get { return distFromAirCheckpoint; }
            set { distFromAirCheckpoint = value; }
        }
        public double DistFromSeaCheckpoint
        {
            get { return distFromSeaCheckpoint; }
            set { distFromSeaCheckpoint = value; }
        }
        public double DistFromLandCheckpoint
        {
            get { return distFromLandCheckpoint; }
            set { distFromLandCheckpoint = value; }
        }
        //Constuctors
        public SHNFacility() { }

        public SHNFacility(string aFacilityName, string aFacilityCapacity, string aFacilityVacancy, double aDistFromAirCheckpoint, double aDistFromSeaCheckpoint, double aDistFromLandCheckpoint)
        {
            FacilityName = aFacilityName;
            FacilityCapacity = aFacilityCapacity;
            FacilityVacancy = aFacilityVacancy;
            DistFromAirCheckpoint = aDistFromAirCheckpoint;
            DistFromSeaCheckpoint = aDistFromSeaCheckpoint;
            DistFromLandCheckpoint = aDistFromLandCheckpoint;
        }

        //Methods
        public double CalculateTravelCost(string entryMode, DateTime entryDate)
        {
            double cost = 50 ; // Base Fare and for further calculations
            if (entryMode == "Air")
            {
                cost = cost * DistFromAirCheckpoint * 0.22;
               
            }
            else if (entryMode == "Sea")
            {
                cost = cost * DistFromSeaCheckpoint * 0.22;
            }
            else if (entryMode == "Land")
            {
                cost = cost * DistFromLandCheckpoint * 0.22;
            }
            else
            {
                Console.WriteLine("Entry Mode was cannot be determined");
            }
            DateTime aDate = DateTime.Now;
            //Condition checks for entry that falls between 6am to 8.59am
            DateTime timerangea1 = new DateTime(aDate.Year, aDate.Month, aDate.Day, 6, 0, 0);
            DateTime timerangea2 = new DateTime(aDate.Year, aDate.Month, aDate.Day, 8, 59, 0);

            //Condition Checks if entry falls beween 6pm to 11.59pm
            DateTime timerangeb1 = new DateTime(aDate.Year, aDate.Month, aDate.Day, 18, 0, 0);
            DateTime timerangeb2 = new DateTime(aDate.Year, aDate.Month, aDate.Day, 23, 59, 0);

            //Condition chceks is entry falls between Midnight to 5.59am
            DateTime timerangec1 = new DateTime(aDate.Year, aDate.Month, aDate.Day, 0, 0, 0);
            DateTime timerangec2 = new DateTime(aDate.Year, aDate.Month, aDate.Day, 5, 59, 0);

            // Check if entry falls within 6am to 8.59am or 6pm to 11.59pm
            if ((entryDate >= timerangea1 && entryDate <= timerangea2) || (entryDate >= timerangeb1 && entryDate <= timerangeb2))
            {
                return cost * 1.25; //25% surcharge of basefare
            }
            else if (entryDate >= timerangec1 && entryDate <= timerangec2)
            {
                return cost * 1.5; //50% surcharge of basefare
            }
            else
            {
                return cost;
            }
        }

        public bool IsAvailable()
        {
            if ((Convert.ToInt32(FacilityCapacity) - Convert.ToInt32(FacilityVacancy)) < Convert.ToInt32(FacilityCapacity))
            {
                Console.WriteLine("The Facility - " + FacilityName + " is available.");
                return true;
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            return "Facility Name: " + FacilityName + "\tFacility Capacity: " + FacilityCapacity +
                "\tFacility Vacancy: " + FacilityVacancy + "\tDistance by Air: " + DistFromAirCheckpoint +
                "\tDistance by Sea: " + DistFromSeaCheckpoint + "\tDistance by Land: " + DistFromLandCheckpoint;
        }
    }
}
