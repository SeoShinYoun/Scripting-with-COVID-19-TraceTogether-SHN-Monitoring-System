using System;
using System.Collections.Generic;
using System.Text;

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
        //public double CalculateTravelCost(string entryMode, DateTime entryDate)
        //{
        //    double cost = 50; // Base Fare and for further calculations
        //    if (entryMode == "Air")
        //    {

        //    }

        //    DateTime aDate = DateTime.Now;
        //    //Condition checks for entry that falls between 6am to 8.59am
        //    DateTime timerangea1 = new DateTime(aDate.Year, aDate.Month, aDate.Day, 6, 0, 0);
        //    DateTime timerangea2 = new DateTime(aDate.Year, aDate.Month, aDate.Day, 8, 59, 0);

        //    //Condition Checks if entry falls beween 6pm to 11.59pm
        //    DateTime timerangeb1 = new DateTime(aDate.Year, aDate.Month, aDate.Day, 18, 0, 0);
        //    DateTime timerangeb2 = new DateTime(aDate.Year, aDate.Month, aDate.Day, 23, 59, 0);

        //    //Condition chceks is entry falls between Midnight to 5.59am
        //    DateTime timerangec1 = new DateTime(aDate.Year, aDate.Month, aDate.Day, 0, 0, 0);
        //    DateTime timerangec2 = new DateTime(aDate.Year, aDate.Month, aDate.Day, 5, 59, 0);


        //}

        //public bool IsAvailable()
        //{

        //}

        public override string ToString()
        {
            return "Facility Name: " + FacilityName + "\tFacility Capacity: " + FacilityCapacity +
                "\tFacility Vacancy: " + FacilityVacancy + "\tDistance by Air: " + DistFromAirCheckpoint +
                "\tDistance by Sea: " + DistFromSeaCheckpoint + "\tDistance by Land: " + DistFromLandCheckpoint;
        }
    }
}
