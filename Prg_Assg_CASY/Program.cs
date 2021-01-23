using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Globalization; // CultureInfo ParseExact
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
            // Required for Basic Feature 1 and 2 to be loaded at the start of the program
            // Basic Feature 2 - Calling SHNFacility class API 
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
            
            //Basic Feature 1 - Loading of Person and Business Location Data
            //Creation of list to store csv file 
            List<Person> personList = new List<Person>();
            List<BusinessLocation> businessLocationList = new List<BusinessLocation>();

            //IncludePerson(personList, shnfacilityList);
            IncludeBusinessLocation(businessLocationList);
            IncludePerson(personList, shnfacilityList);

            // Load MainMenu page 
            MainMenu();

            //Load SafeEntry 
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
                    Console.WriteLine("***************************************************************");// Format of the Main Menu 
                    Console.WriteLine("*                                                             *");
                    Console.WriteLine("*                 COVID-19 Monitoring System                  *");
                    Console.WriteLine("*                                                             *");
                    Console.WriteLine("***************************************************************");
                    Console.WriteLine("==========Main Menu==========");
                    Console.WriteLine("(1) General "); // A GeneralMenu method is created to display a sepoerate menu 
                    Console.WriteLine("(2) SafeEntry/TraceTogether"); // A SafeEntryMenu method is created to display a seperate menu 
                    Console.WriteLine("(3) TravelEntry"); // A TravelntryMenu method is created to display a seperate menu 
                    Console.WriteLine("(4) Exit");// Exit the application 
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
                        SafeEntryMenu();
                    }
                    else if (choice == 3)
                    {
                        TravelEntryMenu();
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
                    Console.WriteLine("(1) List all Visitors");
                    Console.WriteLine("(2) List Person Details");
                    Console.WriteLine("(3) Back to Main Menu");
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
                if (choice != 3)
                {
                    if (choice == 1)
                    {
                        
                    }
                    else if (choice == 2)
                    {
                        
                    }
                    else if (choice == 3)
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
        

        static void SafeEntryMenu() // Menu to allow user to navigate through the functions of SafeEntry
        { 
            Console.WriteLine("***************************************************************");
            Console.WriteLine("*                                                             *");
            Console.WriteLine("*                          SafeEntry                          *");
            Console.WriteLine("*                                                             *");
            Console.WriteLine("***************************************************************");
            Console.WriteLine("");
            Console.WriteLine("========== Menu Options ==========");
            Console.WriteLine("(1) Assign/Replace your TraceTogetherToken ");
            Console.WriteLine("(2) List all Business Locations");
            Console.WriteLine("(3) Edit all Business Locations");
            Console.WriteLine("(4) Check-In ");
            Console.WriteLine("(5) Check-Out ");
            Console.WriteLine("(6) Back to Main Menu ");
            /*Console.WriteLine("Please Enter your name: ");
            string SafeEntryName = Convert.ToString(Console.ReadLine());
            SearchName(personList, Name);*/
        }

        static void TravelEntryMenu()
        {
            Console.WriteLine("***************************************************************");
            Console.WriteLine("*                                                             *");
            Console.WriteLine("*                         TravelEntry                         *");
            Console.WriteLine("*                                                             *");
            Console.WriteLine("***************************************************************");
            Console.WriteLine("");
            Console.WriteLine("========== Menu Options ==========");
        }
        
        //Reading of Person.csv file using System.IO
        static void IncludePerson(List<Person> pList, List<SHNFacility> shnList)
        {
            // reading person csv file after headings, from the second line onwards according to interpretation from csv file (without headings of attributes)
            string[] csvLines = File.ReadAllLines("Person.csv");
            for (int i=1; i<csvLines.Length; i++)
            {
                string[] properties = csvLines[i].Split(','); 
                if (properties[0] == "resident")  // When the attribute under the heading "type" is a resident
                {
                    DateTime dateA = DateTime.ParseExact(properties[3], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    Resident resident = new Resident(properties[1], properties[2], dateA);
                    pList.Add(resident);
                    if (properties[6] != null)
                    {
                        
                        DateTime dateB = DateTime.ParseExact(properties[8], "dd-MMM-yy", CultureInfo.InvariantCulture);
                        resident.Token = new TraceTogetherToken(properties[6], properties[7], dateB);
                    }
                    if (properties[9] != null)
                    {
                        
                        DateTime dateC = DateTime.ParseExact(properties[11], "dd/MM/yyyy HH:mm tt", CultureInfo.InvariantCulture);
                        TravelEntry TE = new TravelEntry(properties[9], properties[10], dateC);
                        resident.AddTravelEntry(TE);
                        DateTime dateD = DateTime.ParseExact(properties[12], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        TE.ShnEndDate = dateD;
                        TE.IsPaid = Convert.ToBoolean(properties[13]);
                        
                       
                        if (properties[14] != null)
                        {
                            TE.AssignSHNFacility(SearchFacility(shnList, properties[14]));
                        }
                    }
                }
                else if (properties[0] == "visitor")  // When the attribute under the heading "type" is a visitor
                {
                    Visitor visitor = new Visitor(properties[1], properties[4], properties[5]);
                    pList.Add(visitor);
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
        static SHNFacility SearchFacility(List<SHNFacility> shnList, string n)
        {
            foreach (SHNFacility s in shnList)
            {
                if (n == s.FacilityName)
                {
                    return s;
                }
            }
            return null;
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
    