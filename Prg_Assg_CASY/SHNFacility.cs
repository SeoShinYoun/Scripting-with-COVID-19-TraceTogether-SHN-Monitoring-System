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
    }
}
