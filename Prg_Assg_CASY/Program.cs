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
            foreach(SHNFacility facility in shnfacilityList)
            {
                facility.FacilityVacancy = facility.FacilityCapacity;
            }
            //Basic Feature 1 - Loading of Person and Business Location Data
            //Creation of list to store csv file 
            List<Person> personList = new List<Person>();
            List<BusinessLocation> businessLocationList = new List<BusinessLocation>();
            //IncludePerson(personList, shnfacilityList);
            IncludeBusinessLocation(businessLocationList);
            IncludePerson(personList, shnfacilityList);

            //Loading of the different Menus (MainMenu, GeneralMenu, SafeEntry, TravelEntry) 
            // Load MainMenu page
            MainMenu(personList,businessLocationList, shnfacilityList);
        }

 //Creation of Menus  (MainMenu, GeneralMenu, SafeEntry, TravelEntry) 
        // Creation of the MainMenu for users to navigate through other functions 
        static void MainMenu(List<Person> personList, List<BusinessLocation> businessLocationList, List<SHNFacility> shnFacilityList) 
        {
            bool display = true;
            while (display == true)
            {
                int choice = 50; //dummy value
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
                try
                {
                    Console.Write("Options: ");
                    choice = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine();
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Invalid option selected!");
                    Console.Write("Exception details: ");
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Choose from either Options 1, 2, 3, or 4...");
                    Console.WriteLine();
                }
                if (choice != 4)
                {
                    if (choice == 1)
                    {
                        GeneralMenu(personList, businessLocationList, shnFacilityList);
                    }
                    else if (choice == 2)
                    {
                        SafeEntryMenu(personList,businessLocationList, shnFacilityList);
                    }
                    else if (choice == 3)
                    {
                        TravelEntryMenu(personList,businessLocationList, shnFacilityList);
                    }
                }
                else
                {
                    display = false;
                    Console.WriteLine("Thank you! Bye...");
                }
            }
        }
// General Menu and Methods 
        static void GeneralMenu(List<Person> personList, List<BusinessLocation> businessLocationList, List<SHNFacility> shnFacilityList)
        {
            bool displaygeneral = true;
            while (displaygeneral == true)
            {
                int choice = 50;// dummy value
                Console.WriteLine();
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
                try
                {
                    Console.Write("Options: ");
                    choice = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine();
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Invalid option selected!");
                    Console.Write("Exception details: ");
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Choose from either Options 1, 2, 3, 4 or 5...");
                    Console.WriteLine();
                }
                if (choice != 3)
                {
                    if (choice == 1)
                    {
                        DisplayAllVisitors(personList);
                    }
                    else if (choice == 2)
                    {
                        DisplayPersonDetails(personList);
                    }
                }
                else
                {
                    displaygeneral = false;
                    MainMenu(personList, businessLocationList, shnFacilityList);
                    Task.Delay(1500).Wait();
                }
            }
        }
 
        // option 1 of GeneralMenu to display all the visitors 
        static void DisplayAllVisitors(List<Person> personList)
        {
            Console.WriteLine("--------------------------- List of Visitors ---------------------------");
            for (int i = 0; i < personList.Count; i++)
            {
                if (personList[i] is Visitor)
                {
                    Console.WriteLine(personList[i]);
                }
            }
            Task.Delay(1500).Wait();
        }
        // option 2 of GeneralMenu to ask for person to enter name to  diaplay person details 
        static void DisplayPersonDetails(List<Person> personList)
        {
            bool isFound = false;
            Console.Write("Enter Name of person you are searching for: "); // Asking for user's input for name to be checked. 
            string searchedName = Console.ReadLine();
            foreach (Person p in personList)
            {
                if (p.Name.ToLower() == searchedName.ToLower()) // When correct Name is being input by the user 
                {
                    Console.WriteLine("");
                    Console.WriteLine("-------------------------- Detail of Person --------------------------");
                    Console.WriteLine(p); // To print the name, address and Date last travelled 
                    Console.WriteLine();
                    Console.WriteLine("------------------------Travel Entry Details--------------------------");
                    foreach(TravelEntry TE in p.TravelEntryList) 
                    {
                        if (string.IsNullOrEmpty(TE.LastCountyOfEmbarkation))// When there is no travel records found 
                        {
                            Console.WriteLine("No Travel Entry Record Found...");
                        }
                        else // When there is travel entry record found, travel records would print 
                        {
                            Console.WriteLine(TE);
                            //Console.WriteLine("{0,10}  {1,10}  {2,10}  {3,10}  {4,10}  {5,10}", TE.LastCountyOfEmbarkation, TE.EntryMode, TE.EntryDate, TE.ShnEndDate, TE.IsPaid, TE.ShnStay);
                        }
                    }
                    Console.WriteLine("");
                    Console.WriteLine("-------------------------Safe Entry Details---------------------------");
                    if (p.SafeEntryList.Count == 0) // When there is no SafeEntry Check-In records available 
                    {
                        Console.WriteLine("No Safe Entry Check-In Record Found...");
                    }
                    else // When there is Safeentry Check=In records available 
                    {
                        foreach (SafeEntry SE in p.SafeEntryList)
                        {
                            Console.WriteLine(SE); // To Print out the Check-In location details and time 
                            Console.WriteLine("-----------------------------------------");
                        }
                    }
                    isFound = true;
                    if (p is Resident) //When Person found in list is a resident 
                    {
                        Console.WriteLine();
                        //Console.WriteLine("--------------------------Safe Entry Details--------------------------");
                        //if (string.IsNullOrEmpty(((Resident)p).SafeEntryList.
                        Console.WriteLine("----------------------TraceTogether Token Details---------------------");
                        if (string.IsNullOrEmpty(((Resident)p).Token.SerialNo))
                        {
                            Console.WriteLine("No Trace Together Token Data Found..."); // When Resident do not have a TraceTogetherToken 
                        }
                        else
                        {
                            Console.WriteLine("------------ Trace Together Token information ------------");
                            Console.WriteLine(((Resident)p).Token.ToString()); // When Resident has a TraceTogetherToken 
                        }
                    }
                    Task.Delay(1500).Wait();
                }
            }
            if (isFound == false) // When an invalid name was being input by the user
            {
                Console.WriteLine("Name of person '" + searchedName + "' could not be found. Please enter a valid name...");
                Task.Delay(1500).Wait();
            }
        }
// Safe Entry Menu and Methods 
        static void SafeEntryMenu(List<Person> personList, List<BusinessLocation> businessLocationList, List<SHNFacility> shnFacilityList) // Menu to allow user to navigate through the functions of SafeEntry
        {
            bool displaySafeEntry = true;
            while (displaySafeEntry == true)
            {
                int choice = 50; //dummy value
                Console.WriteLine();
                Console.WriteLine("***************************************************************");
                Console.WriteLine("*                                                             *");
                Console.WriteLine("*                          SafeEntry                          *");
                Console.WriteLine("*                                                             *");
                Console.WriteLine("***************************************************************");
                Console.WriteLine("========== Menu Options ==========");
                Console.WriteLine("(1) Assign/Replace your TraceTogetherToken ");
                Console.WriteLine("(2) List all Business Locations");
                Console.WriteLine("(3) Edit Business Location capacity");
                Console.WriteLine("(4) Check-In ");
                Console.WriteLine("(5) Check-Out ");
                Console.WriteLine("(6) Back to Main Menu ");
                Console.WriteLine("==================================");
                try
                {
                    Console.Write("Options: ");
                    choice = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine();
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Invalid option selected!");
                    Console.Write("Exception details: ");
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Choose from either Options 1, 2, 3, 4, 5 or 6..");
                    Console.WriteLine();
                }
                if (choice != 6)
                {
                    if (choice == 1) 
                    {
                        AssignReplaceToken(personList,businessLocationList, shnFacilityList);
                    }
                    else if (choice == 2)
                    {
                        DisplayAllBusinessLocation(businessLocationList);
                    }
                    else if (choice == 3)
                    {
                        EditBusinessCapacity(personList, businessLocationList, shnFacilityList);
                    }
                    else if (choice == 4)
                    {
                        CheckIn(personList, businessLocationList, shnFacilityList);
                    }
                    else if (choice == 5)
                    {
                        CheckOut(personList, businessLocationList, shnFacilityList);
                    }
                    else
                    {
                        Console.WriteLine("Choose from either Options 1, 2, 3, 4, 5, or 6...");
                    }
                }
                else
                {
                    displaySafeEntry = false;
                    Task.Delay(1500).Wait();
                    MainMenu(personList,businessLocationList, shnFacilityList);
                }
            }
        }

        static void AssignReplaceToken(List<Person> personList, List<BusinessLocation> businessLocationList, List<SHNFacility> shnFacilityList)
        {
            bool isFound = false;
            Console.WriteLine("Enter your name: ");
            string ttName = Console.ReadLine(); // To store user input name 
            foreach (Person r in personList)
            {
                if (r.Name.ToLower() == ttName.ToLower()) // When correct Name is being input by the user 
                {
                    isFound = true;
                    if (r is Resident) //When Person found in list is a resident 
                    {
                        Resident resident = (Resident)r;
                        if (string.IsNullOrEmpty(((Resident)r).Token.SerialNo))// When resident does not have an existing Trace Together Token 
                        {
                            Console.WriteLine("===============================================");
                            Console.WriteLine("There was no Trace Together Token Data Found...");
                            Console.WriteLine("===============================================");
                            Console.WriteLine("Would you like to be assigned a token? ");
                            Console.WriteLine("(1) Yes");
                            Console.WriteLine("(2) No");
                            Console.WriteLine("Option: ");
                            string option = Console.ReadLine(); // store option of either (1) Yes or (2) NO 
                            while (true)
                            {
                                if (option is "1") //When user chooses option (1) Yes 
                                {
                                    resident.Token.ReplaceToken(resident.Token.SerialNo, resident.Token.CollectionLocation); //To make a new token for the resident 
                                    SafeEntryMenu(personList, businessLocationList, shnFacilityList);// Navigate the user back to the SafeEntry Menu after token is being assigned
                                    //MainMenu(personList, businessLocationList); // Navigate the user back to the Main Menu after token is being assigned 
                                }
                                else if (option is "2") //When User Chooses option (2) No 
                                {
                                    SafeEntryMenu(personList, businessLocationList, shnFacilityList); // To go back to the Safe Entry Menu 
                                }
                            }
                        }
                        else // When resident already has an existing Trace Togethger Token 
                        {
                            while (true)
                            {
                                Console.WriteLine("====================================================");
                                Console.WriteLine("Hi " + ttName + " ! Your Trace Together Token Data was Found!");
                                Console.WriteLine("====================================================");
                                Console.WriteLine(((Resident)r).Token.ToString()); // Print out existing Trace together Token details (Old Trace together Token detail) 
                                Console.WriteLine("====================================================");
                                Console.WriteLine("(1) Check for eligibilty to replace token");
                                Console.WriteLine("(2) Replace Token");
                                Console.WriteLine("(3) Go Back");
                                Console.WriteLine("Option: ");
                                string ReplaceOption = Console.ReadLine();  // store option of either (1) Check for eligibility to replace token or (2) Replace Token or (3) Go Back
                                if (ReplaceOption is "1") // When user chooses to (1) check for eligibility of their token
                                {
                                    resident.Token.IsEligibleForReplacement();

                                }
                                else if (ReplaceOption is "2")// When user chooses to (2) replace token 
                                {
                                    if (resident.Token.IsEligibleForReplacement() == true) // When resident with existing token is elligible to replace their Trace Together token
                                    {

                                        resident.Token.ReplaceToken(resident.Token.SerialNo, resident.Token.CollectionLocation);
                                        SafeEntryMenu(personList, businessLocationList, shnFacilityList);// Navigate the user back to the SafeEntry Menu after new token is assigned 
                                        //MainMenu(personList,businessLocationList);// Brings user back to the Safe Entry menu 
                                    }
                                    else // When resident with existing token is unable to replace the Trace Together Token 
                                    {
                                        SafeEntryMenu(personList, businessLocationList, shnFacilityList); //Navigate the user back to the SafeEntry Menu after message to tell user that their token cannot be replaced is desplayed
                                    }
                                }
                                else //When user chooses (3) Go Back
                                {
                                    SafeEntryMenu(personList, businessLocationList, shnFacilityList);
                                }
                            }
                        }
                    }
                    else // When user is not a resident and is a Visitor 
                    {
                        Console.WriteLine("Sorry " + ttName + " You are not a Resident in Singapore. You will not have or be assigned a Trace Together Token. "); // A visitor is not entitled to 
                    }
                }
            }
            if (isFound == false)// When user types in the wrong name 
            {
                Console.WriteLine("Name of person '" + ttName + "' could not be found. Please enter a valid name..."); // When an invalid name is being input by the user 
                Task.Delay(1500).Wait();
            }
        }

        // Option 2 of SafeEntry Menu to Display all Business Locations 
        static void DisplayAllBusinessLocation(List<BusinessLocation> businessLocationList)
        {
            Console.WriteLine("--------------------------- All Business Locations---------------------------");
            for (int i = 0; i < businessLocationList.Count; i++)
            {
                Console.WriteLine(i + 1 + ".................................");
                Console.WriteLine(businessLocationList[i]); // To List all of the business locations 
                Console.WriteLine("");
            }
            Task.Delay(1500).Wait();
        }

        // Option 3 of SafeEntry Menu to Edit Business Location Capacity
        static void EditBusinessCapacity(List<Person> personList, List<BusinessLocation> businessLocationList, List<SHNFacility> shnFacilityList)
        {
            Console.WriteLine("--------------------------- All Business Locations---------------------------");
            for (int i = 0; i < businessLocationList.Count; i++) // To loop and get index 
            {
                Console.WriteLine(i + 1 + ".................................");
                Console.WriteLine(businessLocationList[i]); // To diplay all of the business locations 
                Console.WriteLine("");
            }
            Console.WriteLine("=========================================================================");
            Console.WriteLine("Do You want to edit the Maximum Capacity of selected Business Location(s)");
            Console.WriteLine("(1) Yes");
            Console.WriteLine("(2) No");
            Console.WriteLine("=========================================================================");
            Console.WriteLine("Option: ");
            string option = Console.ReadLine(); // To store user option of (1) Yes & (2) No
            if (option == "1") //When the user chooses (1) Yes to edit maximum capacity of business location 
            {
                Console.WriteLine("=================================");
                Console.WriteLine("Enter Business Location to edit: ");
                Console.WriteLine("=================================");
                int BLOption = Convert.ToInt32(Console.ReadLine()); // To store the users choice of shop from 1 to 4 
                BLOption = BLOption - 1; // To get index of the business locations 
                Console.WriteLine("========================================================");
                Console.WriteLine("Edit New maximum capacity of " + businessLocationList[BLOption].BusinessName + ":" );
                Console.WriteLine("========================================================");
                int BLMaxCapacity = Convert.ToInt32(Console.ReadLine()); // To store users option of new maximum capacity 
                businessLocationList[BLOption].MaximumCapacity = BLMaxCapacity; // To change the business location max capacity 
                Console.WriteLine(businessLocationList[BLOption].ToString()); // To update the new business location max capacity 
            }
            else if (option == "2") //When user chooses (2) No to not edit maximum capacity of business location 
            {
                SafeEntryMenu(personList, businessLocationList, shnFacilityList); // To navigate the user back to the Safe Entry Menu 
            }
        }

        // option 4 of SafeEntry Menu to Check-In 
        static void CheckIn(List<Person> personList, List<BusinessLocation> businessLocationList, List<SHNFacility> shnFacilityList)
        {
            bool isFound = false;
            Console.WriteLine("Enter your name: ");
            string SEName = Console.ReadLine();
            foreach (Person p in personList)
            {
                if (p.Name.ToLower() == SEName.ToLower()) // To check if correct Name is being input by the user 
                {
                    isFound = true;
                    if ((p.SafeEntryList.Count >= 1)) // When there is no check in data to be displayed 
                    {
                        Console.WriteLine("");
                        Console.WriteLine("========================================");
                        Console.WriteLine("Please Check Out from previous location!");
                        Console.WriteLine("========================================");
                        SafeEntryMenu(personList, businessLocationList, shnFacilityList); // Navigate user back to Safe Entry Menu after diplaying message to tell user that he needs to checkout from the previous location before he can check in again 
                    }
                    
                    Console.WriteLine("--------------------------- All Business Locations---------------------------");
                    for (int i = 0; i < businessLocationList.Count; i++) // To loop and get index 
                    {
                        Console.WriteLine(i + 1 + ".................................");
                        Console.WriteLine(businessLocationList[i]);// To list business locatiopns for user to choose from 
                        Console.WriteLine("");
                    }
                    Console.WriteLine("=========================================");
                    Console.WriteLine("Business Location to Check In: ");
                    int SEBLOption = Convert.ToInt32(Console.ReadLine()); // To store the users choice of shop from 1 to 4 
                    SEBLOption = SEBLOption - 1; // To get index of the business locations 
                    Task.Delay(1500).Wait();
                    if (businessLocationList[SEBLOption].VisitorsNow < businessLocationList[SEBLOption].MaximumCapacity)// When the number of visitors in the loaction is not at amximum 
                    {
                        SafeEntry CheckIn = new SafeEntry(DateTime.Now, businessLocationList[SEBLOption]);
                        businessLocationList[SEBLOption].VisitorsNow = businessLocationList[SEBLOption].VisitorsNow + 1; // Visitor now would add 1 
                        p.AddSafeEntry(CheckIn); // To update check in data for the business locations 
                        Console.WriteLine("");
                        Console.WriteLine("=============== Checked-In ==============");
                        Console.WriteLine(CheckIn); // To display the new check in data information with the updated number of visitors 
                        Console.WriteLine("-----------------------------------------");
                        Console.WriteLine("=========================================");
                    }
                    else // Whem the number of visitors at the locatiopn is at maximum 
                    {
                        Console.WriteLine("----------------------------------------------------------------------");
                        Console.WriteLine("Business Location has reached Maximum Capacity. Try again in a while! ");
                        Console.WriteLine("----------------------------------------------------------------------");
                    }
                }

            }
            if (isFound == false)// When an invalid name was being input by the user 
            {
                Console.WriteLine("Name of person '" + SEName + "' could not be found. Please enter a valid name...");
                Task.Delay(1500).Wait();
            }

        }

        // option 5 of SafeEntry Menu to Check-Out
        static void CheckOut(List<Person> personList, List<BusinessLocation> businessLocationList, List<SHNFacility> shnFacilityList)
        {
            bool isFound = false;
            Console.WriteLine("Enter your name: ");
            string SEName = Console.ReadLine(); // Stores user input name 
            foreach (Person p in personList)
            {
                if (p.Name.ToLower() == SEName.ToLower()) // To check if correct Name is being input by the user 
                {
                    isFound = true;
                    //DisplayAllBusinessLocation(businessLocationList);
                    if ((p.SafeEntryList.Count == 0) || (p.SafeEntryList == null)) // When there is no check in data to be displayed 
                    {
                        Console.WriteLine("");
                        Console.WriteLine("==================================");
                        Console.WriteLine("No Location available to check out");
                        Console.WriteLine("==================================");// Navigate user back to Sae Entry Menu after diplaying message to tell user that there is no location to check in 
                    }
                    else if ((p.SafeEntryList != null) || (p.SafeEntryList.Count != 0)) // When there is data in checkin in safentry cal
                    {
                        Console.WriteLine("------------------- Business Location Not checked out -------------------");
                        Console.WriteLine(".................................");
                        Console.WriteLine(p.SafeEntryList[0]); // To list the location that the user has yet to check out 
                        Console.WriteLine("");
                        Console.WriteLine("=========================================");
                        Console.WriteLine("To Check out from this business location: ");
                        Console.WriteLine("(1) Yes ");
                        Console.WriteLine("(2) No ");
                        Console.WriteLine("option: ");
                        string option = Console.ReadLine();// To store options of (1) Yes & (2) No 
                        if (option == "1") //When the user chooses (1) Yes to checkout from business location 
                        {
                            foreach (BusinessLocation b in businessLocationList) // When the checkin location matches the name in business location list 
                            {
                               if (p.SafeEntryList[0].Location.BusinessName == b.BusinessName) // If the check In location in safentrylist tallies with the location in businesslocationlist 
                                {
                                    b.VisitorsNow = b.VisitorsNow - 1; // deduct one from the number of visitors in the business location
                                    Console.WriteLine("");
                                    Console.WriteLine("=============== Checked-Out ==============");
                                    Console.WriteLine(p.SafeEntryList[0]); // To tell users the new information of the business and to confirm that the number of vistors is deducted
                                    Console.WriteLine(p.SafeEntryList[0].PerformCheckOut());//Show the checkin and checkout timing 
                                    Console.WriteLine("------------------------------------------");
                                    Console.WriteLine("==========================================");
                                    p.SafeEntryList.RemoveAt(0); //To remove the checkin data to tell the user that there is no check in data to check out 
                                    Task.Delay(1500).Wait();
                                    SafeEntryMenu(personList, businessLocationList, shnFacilityList); // Navigate user back to the SafeEntry Menu after updated business location is displayed 
                                }
                            }
                        }
                        else if (option == "2") //When the user chooses(2) No to not check out from business location 
                        {
                            SafeEntryMenu(personList, businessLocationList, shnFacilityList); // Navigate user back to the SafeEntry Menu after updated business location is displayed 
                        }
                    }
                }
            }
            if (isFound == false) // When an invalid name was being input by the user 
            {
                Console.WriteLine("Name of person '" + SEName + "' could not be found. Please enter a valid name...");
                Task.Delay(1500).Wait();
            }
        }

// Travel Entry Menu and methods 
        static void TravelEntryMenu(List<Person> personList, List<BusinessLocation> businessLocationList, List<SHNFacility> shnFacilityList)
        {
            bool displayTravelEntry = true;
            while (displayTravelEntry == true)
            {
                int choice = 50; //dummy value
                Console.WriteLine();
                Console.WriteLine("***************************************************************");
                Console.WriteLine("*                                                             *");
                Console.WriteLine("*                         TravelEntry                         *");
                Console.WriteLine("*                                                             *");
                Console.WriteLine("***************************************************************");
                Console.WriteLine("========== Menu Options ==========");
                Console.WriteLine("(1) List all SHN Facilities");
                Console.WriteLine("(2) Create Visitor");
                Console.WriteLine("(3) Create TravelEntry Record");
                Console.WriteLine("(4) Calculate SHN Charges");
                Console.WriteLine("(5) Back to Main Menu");
                Console.WriteLine("==================================");
                try
                {
                    Console.Write("Options: ");
                    choice = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine();
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Invalid option selected!");
                    Console.Write("Exception details: ");
                    Console.WriteLine(ex.Message);
                }
                if (choice != 5)
                {
                    if (choice == 1)
                    {
                        ListAllSHNFacility(shnFacilityList);
                    }
                    else if (choice == 2)
                    {
                        CreateVisitor();
                    }
                    else if (choice == 3)
                    {
                        CreateTravelEntryRecord(personList, shnFacilityList);
                    }
                    else if (choice == 4)
                    {
                        CalculateSHNCharges(personList);
                    }
                    else
                    {
                        Console.WriteLine("Choose from either Options 1, 2, 3, 4, or 5...");
                    }
                }
                else
                {
                    displayTravelEntry = false;
                    Task.Delay(1500).Wait();
                    MainMenu(personList,businessLocationList, shnFacilityList);
                }

            }

        }
        
        // Method for Option 1 of TravelEntry Menu 
        static void ListAllSHNFacility(List<SHNFacility> shnFacilityList)
        {
            Console.WriteLine("{0,-15}   {1,-8}   {2,-8}   {3,-28}   {3,-28}   {4,-29}", "Facility Name", "Capacity", "Vacancy", "Distance From Air Checkpoint", "From Sea Checkpoint", "From Land Checkpoint");
            foreach (SHNFacility facility in shnFacilityList)
            {
                Console.WriteLine("{0,-15}   {1,-8}   {2,-8}   {3,-28}   {3,-28}   {4,-29}", facility.FacilityName,facility.FacilityCapacity, facility.FacilityVacancy, facility.DistFromAirCheckpoint, facility.DistFromSeaCheckpoint, facility.DistFromLandCheckpoint);
            }
        }
        // Method for Option 2 of TravelEntry Menu
        static void CreateVisitor()
        {
            Console.Write("Please Enter Your Name: ");
            string name = Console.ReadLine();
            Console.Write("Please Enter Your Passport Number: ");
            string passportNo = Console.ReadLine();
            Console.Write("Please Enter Your Nationality: ");
            string nationality = Console.ReadLine();
            Visitor visitor = new Visitor(name, passportNo, nationality);
            if (visitor != null)
            {
                Console.WriteLine();
                Console.WriteLine("Visitor Object Successfully Created!");
                Console.WriteLine(visitor);
                Task.Delay(2000).Wait();
            }
            else
            {
                Console.WriteLine("Visitor Object Could Not Be Created...");
            }
        }

        // Method for Option 3 of TravelEntry Menu
        static void CreateTravelEntryRecord(List<Person> personList, List<SHNFacility> shnFacilityList)
        {
            Console.Write("Enter Name To Be Searched: ");
            string searchedName = Console.ReadLine();
            bool isFound = false;
            for (int i = 0; i<personList.Count; i++)
            {
                if (personList[i].Name.ToLower() == searchedName.ToLower())
                {
                    Console.WriteLine("Successfully Found Name To Be Searched!");
                    isFound = true;
                    Console.WriteLine();
                    Console.Write("Enter " + personList[i].Name +"'s Last Country of Embarkation: ");
                    string lastCountryOfEmbarkation = Console.ReadLine();
                    lastCountryOfEmbarkation = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lastCountryOfEmbarkation);
                    Console.Write("Enter " + personList[i].Name + "'s Mode of Entry: ");
                    string entryMode = Console.ReadLine();
                    entryMode = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(entryMode);
                    while (entryMode != "Air" && entryMode != "Land" && entryMode != "Sea")
                    {
                        Console.WriteLine();
                        Console.WriteLine("Please enter a valid Mode of Entry... Choose From Either: Land, Air, or Sea...");
                        Console.Write("Enter " + personList[i].Name + "'s Mode of Entry: ");
                        entryMode = Console.ReadLine();
                        entryMode = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(entryMode);
                    }
                    TravelEntry TE = new TravelEntry(lastCountryOfEmbarkation, entryMode, DateTime.Now);
                    TE.CalculateSHNDuration();
                    if ((lastCountryOfEmbarkation.ToLower() != "New Zealand") && (lastCountryOfEmbarkation != "Vietnam") && (lastCountryOfEmbarkation.ToLower() != "macao sar"))
                    {
                        Console.WriteLine("====================================");
                        for (int x = 0; x < shnFacilityList.Count; x++)
                        {
                            Console.WriteLine("Option " + Convert.ToInt32(x+1) + ":");
                            Console.WriteLine(shnFacilityList[x]);
                            Console.WriteLine("====================================");
                        }
                        while (true)
                        {
                            int choice = 50;//dummy value
                            try
                            {
                                Console.Write("From the Options above...\nPlease Select A SHN Facility To Be Assigned To: ");
                                choice = Convert.ToInt32(Console.ReadLine()) - 1;
                            }   
                            catch (FormatException ex)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Invalid option selected!");
                                Console.Write("Exception details: ");
                                Console.WriteLine(ex.Message);
                            }
                            if (choice >= 0 && choice < shnFacilityList.Count)
                            {
                                if (shnFacilityList[choice].IsAvailable() == false)
                                {
                                    Console.WriteLine("The Facility is not available due to vacancy constraints...");
                                    continue;
                                }
                                TE.AssignSHNFacility(shnFacilityList[choice]);
                                shnFacilityList[choice].FacilityVacancy = shnFacilityList[choice].FacilityVacancy - 1;
                                
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Invalid option selected. Select a given numbered option...");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("The person identified is not required to serve SHN...");
                    }
                    personList[i].AddTravelEntry(TE);
                    Console.WriteLine();
                    Console.WriteLine("Travel Entry Successfully Recorded for " + personList[i].Name + "!");
                }
            }
            if (isFound == false)
            {
                Console.WriteLine("Searched Name could not be found...");
            }
        }
        //Method for Option 4 of TravelEntry Menu
        static void CalculateSHNCharges(List<Person> personList)
        {
            Console.Write("Please enter the name to be searched: ");
            string searchedName = Console.ReadLine();
            bool isFound = false;
            double cost;
            for (int i = 0; i < personList.Count; i++)
            {
                if (searchedName.ToLower() == personList[i].Name.ToLower())
                {
                    isFound = true;
                    Console.WriteLine("Name Searched Successfully!.... ");
                    DateTime presentDate = DateTime.Now;
     
                    foreach(TravelEntry TE in personList[i].TravelEntryList)
                    {
                        if (string.IsNullOrEmpty(TE.LastCountyOfEmbarkation) == false)
                        {
                            if (TE.ShnEndDate <= presentDate && TE.IsPaid == false)
                            {
                                cost = personList[i].CalculateSHNCharges();
                                Console.WriteLine(TE);
                                if (TE.ShnStay != null)
                                {
                                    Console.Write("Would you like to pay $" + cost.ToString("0.00") + " for your Swab Test, Transportaion and SDF Charges?\n[Y]/[N]: ");
                                }
                                else
                                {
                                    Console.Write("Would you like to pay $" + cost.ToString("0.00") + " for your Swab Test and Transportation?\n[Y]/[N]: ");
                                }
                                string choice = Console.ReadLine();
                                while (true)
                                {
                                    if (choice.ToUpper() == "Y")
                                    {
                                        Console.WriteLine("Ok! Processing Payment...");
                                        TE.IsPaid = true;
                                        break;
                                    }
                                    else if (choice.ToUpper() == "N")
                                    {
                                        Console.WriteLine("Ok! Proceeding without Payment...");
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid Option... Please Select [Y]/[N]");
                                    }
                                }
                            }
                            else if (TE.IsPaid == true)
                            {
                                Console.WriteLine("Payment has already been made...");
                            }
                            else if (TE.ShnEndDate > presentDate)
                            {
                                Console.WriteLine("Please Refrain From Paying...");
                                Console.WriteLine("Your SHN Has Not Ended Yet...");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No Travel Entry Record Found...");
                            Console.WriteLine("The Person Identified Does Not Need to Pay SHN Charges...");
                        }
                    }
                }
            }
            if (isFound == false)
            {
                Console.WriteLine("Name could not be found...");
            }
        }

 //Reading of CSV files         
        //Reading of Person.csv file using System.IO
        static void IncludePerson(List<Person> pList, List<SHNFacility> shnList)
        {
            // reading person csv file after headings, from the second line onwards according to interpretation from csv file (without headings of attributes)
            string[] csvLines = File.ReadAllLines("Person.csv");
            for (int i = 1; i < csvLines.Length; i++)
            {
                string[] properties = csvLines[i].Split(',');
                if (properties[0] == "resident")  // When the attribute under the heading "type" is a resident
                {
                    DateTime dateA = DateTime.ParseExact(properties[3], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    Resident resident = new Resident(properties[1], properties[2], dateA);
                    pList.Add(resident);
                    if (properties[6] != null)
                    {
                        string date = properties[8];
                        DateTime expirydate;
                        if (DateTime.TryParseExact(date, "dd-MMM-yy", CultureInfo.InvariantCulture,
                        DateTimeStyles.None, out expirydate))
                        {
                            expirydate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                        }
                        /*DateTime dateB = DateTime.ParseExact(properties[8], "dd-MMM-yy", CultureInfo.InvariantCulture);*/
                        resident.Token = new TraceTogetherToken(properties[6], properties[7], expirydate);
                    }
                    if (properties[9] != null)
                    {
                        DateTime travelEntryDate;
                        travelEntryDate = FormatTravelDate(properties[11]);
                        TravelEntry TE = new TravelEntry(properties[9], properties[10], travelEntryDate);
                        DateTime travelShnEndDate;
                        travelShnEndDate = FormatTravelDate(properties[12]);
                        TE.ShnEndDate = travelShnEndDate;
                        ValidatePayment(properties[13]);

                        if (properties[13] != null)
                        {
                            string boolValue = properties[13];
                            TE.IsPaid = ValidatePayment(boolValue);
                        }
                        if (properties[14] != null)
                        {
                            TE.AssignSHNFacility(SearchFacility(shnList, properties[14]));
                        }
                        resident.AddTravelEntry(TE);
                    }
                }
                else if (properties[0] == "visitor")  // When the attribute under the heading "type" is a visitor
                {
                    Visitor visitor = new Visitor(properties[1], properties[4], properties[5]);
                    TravelEntry TE = new TravelEntry();
                    pList.Add(visitor);
                    if (properties[9] != null)
                    {
                        TE.LastCountyOfEmbarkation = properties[9];
                        TE.EntryMode = properties[10];
                        DateTime travelEntryDate;
                        DateTime travelShnEndDate;
                        travelEntryDate = FormatTravelDate(properties[11]);
                        travelShnEndDate = FormatTravelDate(properties[12]);
                        TE.EntryDate = travelEntryDate;
                        TE.ShnEndDate = travelShnEndDate;
                    }
                    if (properties[13] != null)
                    {
                        string boolValue = properties[13];
                        TE.IsPaid = ValidatePayment(boolValue);
                    }
                    if (properties[14] != null)
                    {
                        TE.AssignSHNFacility(SearchFacility(shnList, properties[14]));
                    }
                    visitor.AddTravelEntry(TE);
                }
            }
        }
        static DateTime FormatTravelDate(string travelDate)
        {
            DateTime formattedDate;
            if (DateTime.TryParseExact(travelDate, "dd/MM/yyyy HH:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out formattedDate))
            {
                formattedDate = DateTime.ParseExact(travelDate, "dd/MM/yyyy HH:mm tt", CultureInfo.InvariantCulture);
            }
            else if (DateTime.TryParseExact(travelDate, "dd/MM/yyyy hh:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out formattedDate))
            {
                formattedDate = DateTime.ParseExact(travelDate, "dd/MM/yyyy hh:mm", CultureInfo.InvariantCulture);
            }
            return formattedDate;
        }
        static bool ValidatePayment(string boolValue)
        {
            if (boolValue == "TRUE")
            {
                return true;
            }
            else
            {
                return false;
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

// End of Program         



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


    