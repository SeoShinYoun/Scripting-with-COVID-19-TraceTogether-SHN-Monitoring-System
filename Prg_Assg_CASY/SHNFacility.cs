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
    class SHNFacility
    {
        //Fields
        private string facilityName;
        private int facilityCapacity;
        private int facilityVacancy;
        private double distFromAirCheckpoint;
        private double distFromSeaCheckpoint;
        private double distFromLandCheckpoint;

        //Properties
        public string FacilityName
        {
            get { return facilityName; }
            set { facilityName = value; }
        }
        public int FacilityCapacity
        {
            get { return facilityCapacity; }
            set { facilityCapacity = value; }
        }
        public int FacilityVacancy
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

        public SHNFacility(string aFacilityName, int aFacilityCapacity, double aDistFromAirCheckpoint, double aDistFromSeaCheckpoint, double aDistFromLandCheckpoint)
        {
            FacilityName = aFacilityName;
            FacilityCapacity = aFacilityCapacity;
            DistFromAirCheckpoint = aDistFromAirCheckpoint;
            DistFromSeaCheckpoint = aDistFromSeaCheckpoint;
            DistFromLandCheckpoint = aDistFromLandCheckpoint;
        }

        //Methods
        public double CalculateTravelCost(string entryMode, DateTime entryDate)
        {
            double cost = 50 ; //Base Fare Default Value before Distance Cost
            // Base Fare and for further calculations
            if (entryMode == "Air")
            {
                cost += DistFromAirCheckpoint * 0.22; // Calculate Travel Cost based on if Entry Mode is Air...
               
            }
            else if (entryMode == "Sea")
            {
                cost += DistFromSeaCheckpoint * 0.22; // Calculate Travel Cost based on if Entry Mode is Sea...
            }
            else if (entryMode == "Land")
            {
                cost += DistFromLandCheckpoint * 0.22; // Calculate Travel Cost based on if Entry Mode is Land...
            } 
            else
            {
                Console.WriteLine("Entry Mode cannot be determined...");
            }
            DateTime aDate = DateTime.Now; //
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
            if ((entryDate.TimeOfDay >= timerangea1.TimeOfDay && entryDate.TimeOfDay <= timerangea2.TimeOfDay) || (entryDate.TimeOfDay >= timerangeb1.TimeOfDay && entryDate.TimeOfDay <= timerangeb2.TimeOfDay))
            {
                return cost * 1.25; //25% surcharge of basefare between 6am to 8.59am OR 6pm to 11.59pm
            }
            else if (entryDate.TimeOfDay >= timerangec1.TimeOfDay && entryDate.TimeOfDay <= timerangec2.TimeOfDay)
            {
                return cost * 1.5; //50% surcharge of basefare between Midnight to 5.59am
            }
            else
            {
                return cost; //No surcharge for other timings
            }
        }
 
        public bool IsAvailable() //Check if Facility has vacancy 
        {
            if (facilityVacancy >= 1)
            {
                return true; //If there is 1 or more vacancy, the facility would be available for booking
            }
            else
            {
                facilityVacancy = 0;  // If there 0 vacancies left, vacancy would be set to 0 to prevent negative numbers 
                return false;  //If false, facility would not be available for booking and program will display message accordingly
            }
        }

        public override string ToString()
        {
            return "\nFacility Name: " + FacilityName + "\nFacility Capacity: " + FacilityCapacity +
                "\nFacility Vacancy: " + FacilityVacancy + "\nDistance From Air Checkpoint: " + DistFromAirCheckpoint +
                "\nDistance From Sea Checkpoint: " + DistFromSeaCheckpoint + "\nDistance From Land Checkpoint: " + DistFromLandCheckpoint;
        }
    }
}
