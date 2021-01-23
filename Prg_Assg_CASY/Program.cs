using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

//============================================================
// Student Number : S10205100, S10203193
// Student Name : Seo Shin Youn, Phua Cheng Ann
// Module Group : T09 
//============================================================

namespace Prg_Assg_CASY
{
    class Program
    {
        static void Main(string[] args)
        {
            // Calling SHNFacility class API (Basic Feature 2)
            // Required to load data at the start of program
            List<SHNFacility> shnfacilityList = new List<SHNFacility>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://covidmonitoringapiprg2.azurewebsites.net");
                Task<HttpResponseMessage> responseTask = client.GetAsync("/facility");
                responseTask.Wait();
                HttpResponseMessage result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    Task<string> readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    string data = readTask.Result;
                    shnfacilityList = JsonConvert.DeserializeObject<List<SHNFacility>>(data);
                }
            }
            MainMenu();
        }

        // Creation of the MainMenu for users to navigate through other functions 
        static void MainMenu()
        {

            bool display = true;
            while (display == true)
            {
                int choice = 50; //Dummy value
                try
                {
                    Console.WriteLine("***************************************************************");
                    Console.WriteLine("*                                                             *");
                    Console.WriteLine("*                 COVID-19 Monitoring System                  *");
                    Console.WriteLine("*                                                             *");
                    Console.WriteLine("***************************************************************");
                    Console.WriteLine("==========Main Menu==========");
                    Console.WriteLine("(1) General ");
                    Console.WriteLine("(2) SafeEntry/TraceTogether");
                    Console.WriteLine("(3) TravelEntry");
                    Console.WriteLine("(4) Exit");
                    Console.WriteLine("=============================");
                    Console.Write("Options: ");
                    choice = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine();
                }
                catch (FormatException ex)
                {
                    Console.Write("Exception details: ");
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Choose from either Options 1, 2, 3, or 4...");
                    Console.WriteLine();
                }
                if (choice != 4)
                {
                    if (choice == 1)
                    {
                        GeneralMenu();
                    }
                    else if (choice == 2)
                    {

                    }
                    else if (choice == 3)
                    {

                    }
                    else
                    {
                        Console.WriteLine("Invalid option selected!");
                        Console.WriteLine("Choose from either Options 1, 2, 3, or 4...");
                    }
                }
                else
                {
                    display = false;
                    Console.WriteLine("Thank you! Bye...");
                }
            }


        }
        static void GeneralMenu()
        {
            bool display = true;
            while (display == true)
            {
                int choice = 50; //Dummy value
                try
                {
                    Console.WriteLine("***************************************************************");
                    Console.WriteLine("*                                                             *");
                    Console.WriteLine("*                         General Menu                        *");
                    Console.WriteLine("*                                                             *");
                    Console.WriteLine("***************************************************************");
                    Console.WriteLine("======== Menu Options =======");
                    Console.WriteLine("(1) Load Person and Business Location Data ");
                    Console.WriteLine("(2) Load SHN Facility Data");
                    Console.WriteLine("(3) List all Visitors");
                    Console.WriteLine("(4) List Person Details");
                    Console.WriteLine("(5) Back to Main Menu");
                    Console.WriteLine("=============================");
                    Console.Write("Options: ");
                    choice = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine();
                }
                catch (FormatException ex)
                {
                    Console.Write("Exception details: ");
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Choose from either Options 1, 2, 3, 4 or 5...");
                    Console.WriteLine();
                }
                if (choice != 5)
                {
                    if (choice == 1)
                    {
                        //Creation of list to store csv file 
                        List<Person> personList = new List<Person>();
                        List<BusinessLocation> businessLocationList = new List<BusinessLocation>();

                        //IncludePerson(personList, shnfacilityList);
                        //IncludeBusinessLocation(businessLocationList);
                    }
                    else if (choice == 2)
                    {

                    }
                    else if (choice == 3)
                    {

                    }
                    else if (choice == 4)
                    {

                    }
                    else
                    {

                    }
                }
                else
                {
                    display = false;
                    Console.WriteLine("Returning back to Main Menu...");
                    Task.Delay(1000).Wait();
                    MainMenu();
                }

            }
        }
        //Reading of BusinessLocation csv file using system.IO 
        static void IncludeBusinessLocation(List<BusinessLocation> bList)
        {
            string[] csvLines = File.ReadAllLines("BusinessLocation.csv");

            // reading business location csv file, from the second line onwards according to interpretation from csv file (without headings of attributes) 
            for (int i = 1; i < csvLines.Length; i++)
            {
                string[] properties = csvLines[i].Split(','); //splitting the different attributes into its own individual table 
                string businessName = properties[0];
                string branchCode = properties[1];
                int capacity = Convert.ToInt32(properties[2]);
                BusinessLocation businessLocation = new BusinessLocation(businessName, branchCode, capacity);
                bList.Add(businessLocation);
            }
        }

        static void SafeEntryMenu()
        {
            Console.WriteLine("***************************************************************");
            Console.WriteLine("*                                                             *");
            Console.WriteLine("*                          SafeEntry                          *");
            Console.WriteLine("*                                                             *");
            Console.WriteLine("***************************************************************");
            Console.WriteLine("");
            Console.WriteLine("==========Safe Entry==========");
            Console.WriteLine("Please Enter your name: ");
            Console.WriteLine("");
        }

    }

}
//Reading of person csv file using system.IO 
        //static void IncludePerson(List<Person> pList, List<SHNFacility> shnList)
        //{
        //    // reading person location csv file, from the second line onwards according to interpretation from csv file (without headings of attributes)
        //    string[] csvLines = File.ReadAllLines("Person.csv");
        //    for (int i = 1; i < csvLines.Length; i++)
        //    {
        //        string[] attributes = csvLines[i].Split(',');  //splitting the different attributes into its own individual table 
        //        if (attributes[0] == "resident") // When the attribute under the heading "type" is a resident 
        //        {
        //            Resident r = new Resident(attributes[1], attributes[2], DateTime.ParseExact(attributes[3], "dd/MM/yyyy", null)); // last left country date using ParseExact to convert the string in csv to a datetime in program 
        //            pList.Add(r);
        //            if (attributes[6] != null) //When token serial number is present 
        //            {
        //                r.Token = new TraceTogetherToken(attributes[6], attributes[7], DateTime.ParseExact(attributes[8], "dd/MM/yyyy", null)); // Name, address, Tokenexpiry date using ParseExact to convert the string in csv to a datetime in program 
        //            }
        //            if (attributes[9] != null) //When Travel location is present 
        //            {
        //                TravelEntry t = new TravelEntry(attributes[9], attributes[10], DateTime.ParseExact(attributes[11], "dd/MM/yyyy H:mm", null)); // TokenExpiry , TravelEntryMode, TravelEntry date and time (in hours and minutes) using ParseExact to convert the string in csv to a datetime in program 
        //                t.ShnEndDate = DateTime.ParseExact(attributes[12], "dd/MM/yyyy H:mm", null); //SHN end date and time (in hours and minutes) using ParseExact to convert the string in csv to a datetime in program 
        //                if (attributes[13] != null)
        //                {
        //                    t.IsPaid = Convert.ToBoolean(attributes[13]);
        //                }
        //                if (attributes[14] != null)
        //                {
        //                    t.AssignSHNFacility(SearchFacility(shnList, attributes[14]));
        //                }
        //            }
        //        }
        //        else if (attributes[0] == "visitor") // When the attribute under the heading "type" is a visitor  
        //        {
        //            pList.Add(new Visitor(attributes[1], attributes[4], attributes[5]));

        //        }
        //    }
        //}

    //static void LoadSHNFacility()
    //{

    //}
    //static SHNFacility SearchFacility(List<SHNFacility> shnList, string n)
    //{
    //    foreach (SHNFacility s in shnList)
    //    {
    //        if (n == s.FacilityName)
    //        {
    //            return s;
    //        }
    //    }
    //    return null;
    //}