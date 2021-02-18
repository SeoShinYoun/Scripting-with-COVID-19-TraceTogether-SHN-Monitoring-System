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
                client.BaseAddress = new Uri("https://covidmonitoringapiprg2.azurewebsites.net"); // Sending HTTP GET request to Web API
                Task<HttpResponseMessage> responseTask = client.GetAsync("/facility"); // Sending HTTP GET request to Web API
                responseTask.Wait();
                HttpResponseMessage result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    Task<string> readTask = result.Content.ReadAsStringAsync(); // Receiving HTTP Response from Web API
                    readTask.Wait();
                    string data = readTask.Result;
                    shnfacilityList = JsonConvert.DeserializeObject<List<SHNFacility>>(data); //Converting the JSON text string back to a List of SHNFacility Objects
                }
            }
            foreach (SHNFacility facility in shnfacilityList)
            {
                facility.FacilityVacancy = facility.FacilityCapacity; // Making Facility Vacancy to be equal to Facility Capacity at the start of the Program
            }
            //Basic Feature 1 - Loading of Person and Business Location Data
            //Creation of list to store csv file 
            List<Person> personList = new List<Person>(); 
            List<BusinessLocation> businessLocationList = new List<BusinessLocation>();
            //IncludePerson(personList, shnfacilityList);
            IncludeBusinessLocation(businessLocationList); //Calling IncludeBusinessLocation Method to Load BusinessLocation.CSV and Populate List
            IncludePerson(personList, shnfacilityList); // Calling IncludePerson Method to Load Person.CSV and Populate List

            //Loading of the different Menus (MainMenu, GeneralMenu, SafeEntry, TravelEntry) 
            // Load MainMenu page
            MainMenu(personList, businessLocationList, shnfacilityList);
        }

        //Creation of Menus  (MainMenu, GeneralMenu, SafeEntry, TravelEntry) 
        // Creation of the MainMenu for users to navigate through other functions as well as methods for the advanced features 
        static void MainMenu(List<Person> personList, List<BusinessLocation> businessLocationList, List<SHNFacility> shnFacilityList)
        {
            bool display = true;
            while (display == true)
            {
                int choice = 50; //dummy value
                Console.WriteLine();
                Console.WriteLine("***************************************************************");// Format of the Main Menu 
                Console.WriteLine("*                                                             *");
                Console.WriteLine("*                 COVID-19 Monitoring System                  *");
                Console.WriteLine("*                                                             *");
                Console.WriteLine("***************************************************************");
                Console.WriteLine("==========Main Menu==========");
                Console.WriteLine("(1) General "); // A GeneralMenu method is created to display a sepoerate menu 
                Console.WriteLine("(2) SafeEntry/TraceTogether"); // A SafeEntryMenu method is created to display a seperate menu 
                Console.WriteLine("(3) TravelEntry"); // A TravelntryMenu method is created to display a seperate menu 
                Console.WriteLine("(4) Contact Tracing Reporting"); //Advance feature 3.1  
                Console.WriteLine("(5) SHN Status Reporting"); //Advance feature 3.2
                Console.WriteLine("(6) Exit");// Exit the application 
                Console.WriteLine("=============================");
                //Exception Handling for Option
                try
                {
                    Console.Write("Options: ");
                    choice = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine();
                }
                catch (FormatException ex)
                {
                    Console.WriteLine();
                    Console.WriteLine("Invalid option selected!");
                    Console.Write("Exception details: ");
                    Console.WriteLine(ex.Message); //Print out Error Message
                }
                if (choice != 6)
                {
                    if (choice == 1)
                    {
                        GeneralMenu(personList, businessLocationList, shnFacilityList); // Calling General Menu For User to select General Features from the Basic Feature
                    }
                    else if (choice == 2)
                    {
                        SafeEntryMenu(personList, businessLocationList, shnFacilityList); //Calling SafeEntryMenu For Users to Select SafeEntry/TraceTogether Options from the Basic Feature
                    }
                    else if (choice == 3)
                    {
                        TravelEntryMenu(personList, businessLocationList, shnFacilityList); //Calling TravelEntryMenu For Users to Select TravelEntry Options from the Basic Feature
                    }
                    else if (choice == 4)
                    {
                        ContactTracingReporting(personList, businessLocationList, shnFacilityList); //Calling ContracTracingReporting For Users to generate CSV for People who checked in and out of a Business Location during a period of Time From the Advanced Feature
                    }
                    else if (choice == 5)
                    {
                        ShnStatusReporting(personList, businessLocationList, shnFacilityList); // Calling SHNStatusReporting For Users to Generate CSV for Travellers serving their SHN From the Advanced Feature
                    }
                    else
                    {
                        Console.WriteLine("Choose from either Options 1, 2, 3, 4, 5, or 6...");
                    }
                }
                else
                {
                    display = false; //break from while loop by making condition false
                    Console.WriteLine("Thank you! Bye..."); //Exit From Program
                }
            }
        }

        //Advanced Feature 3.1 - Contact Tracing Reporting
        static void ContactTracingReporting(List<Person> pList, List<BusinessLocation> bList, List<SHNFacility> shnFacilityList)
        {
            DateTime formattedDate; //Format Date from user input
            int choice = 50; //Dummy value
            bool display = true;
            while (display == true)
            { 
                Console.WriteLine();
                //Display Contract Tracing Reporting Menu
                Console.WriteLine("***************************************************************");
                Console.WriteLine("*                                                             *");
                Console.WriteLine("*                 Contact Tracing Reporting                   *");
                Console.WriteLine("*                                                             *");
                Console.WriteLine("***************************************************************");
                Console.WriteLine("Would you like to generate a Contact Tracing Report?");
                Console.WriteLine("(1) Yes");
                Console.WriteLine("(2) No");
                Console.Write("Options: ");
                //Exception Handling
                try
                {
                    choice = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine();
                }
                catch (FormatException ex)
                {
                    Console.WriteLine();
                    Console.WriteLine("Invalid option selected!");
                    Console.Write("Exception details: ");
                    Console.WriteLine(ex.Message);
                }
                if (choice == 1) //When User Chooses to Generate A Contract Tracing Report
                {
                    Console.Write("Please Select A Date To Generate The Report For (yyyy/mm/dd): "); //Specify format of date
                    string dateChosen = Console.ReadLine(); //Read Date inputted by user
                    //Check if Date is in the correct format as Inputted by User
                    if (DateTime.TryParse(dateChosen, out formattedDate))
                    {
                        String.Format("{0:yyy/MM/dd}", formattedDate);
                        Console.WriteLine("Date successfully obtained!"); 
                    }
                    else //If Date input is not as expected
                    {
                        Console.WriteLine();
                        Console.Write("Invalid...");
                        Console.WriteLine("Please Enter Date in Format of yyyy/mm/dd ");
                        Console.WriteLine("If date is in correct format, ensure that it is a valid date (e.g. 2021/14/52 - Invalid)");
                        ContactTracingReporting(pList, bList, shnFacilityList); //Return back to contract Tracing reporting Menu
                        Console.WriteLine();
                    }
                    int hour; 
                    int minute;
                    int seconds;
                    while (true)
                    {
                        //Exception Handling for Hour Input 
                        try
                        {
                            Console.Write("Enter Hour of Day from 0 to 23 (24-Hour Format): "); //Prompt User to Enter Hour of 24-Hour Time Format to Generate Contract Tracing Report
                            hour = Convert.ToInt32(Console.ReadLine());
                            if (hour < 0 || hour > 23) //Check if Hour entered by user is between 0 and 23 - 0 being 12 AM and 23 being 11 PM
                            {
                                Console.WriteLine("Selected hour is out of range... Please Select Between 0 and 23..."); //Display Invalid Message
                            }
                            else
                            {
                                break; //break from loop if Hour entered is valid
                            }
                        }
                        catch (FormatException ex)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Invalid option selected!");
                            Console.Write("Exception details: ");
                            Console.WriteLine(ex.Message);
                        }
                        
                    }
                    while (true)
                    {
                        //Exception Handling
                        try
                        {
                            Console.Write("Enter Minute of Day 0 to 59: "); //Prompt User to Enter Minute of 24-Hour Time Format to Generate Contract Tracing Report
                            minute = Convert.ToInt32(Console.ReadLine());

                            Console.Write("Enter Seconds of Day 0 to 59: "); //Prompt User to Enter Second of 24-Hour Time Format to Generate Contract Tracing Report
                            seconds = Convert.ToInt32(Console.ReadLine());
                            if ((minute < 0 || minute > 59)) //Ensure that Minute is between 0 Minute and 59 Minute
                            {
                                Console.WriteLine("Selected Minute is out of range... Please Select Between 0 and 60..."); //Display Out of range Invalid Message
                            }
                            if ((seconds < 0 || seconds > 59)) //Ensure that Second is between 0 Seconds and 59 seconds
                            {
                                Console.WriteLine("Selected Second is out of range... Please Select Between 0 and 60..."); //Display Out of range invalid Message
                            }
                            if ((minute >= 0 && minute <= 59) && (seconds >= 0 && seconds <= 59)) // Check if Both Hour and Minute is Valid and Break From Loop if true
                            {
                                break;
                            }
                        }
                        catch (FormatException ex)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Invalid option selected!");
                            Console.Write("Exception details: ");
                            Console.WriteLine(ex.Message);
                        }
                    }
                    DateTime date = new DateTime(formattedDate.Year, formattedDate.Month, formattedDate.Day, hour, minute, seconds); //Create A DateTime object to store Date Entered by user with Exact Hour, Minute and Seconds
                    Console.WriteLine("Date Object Successfully Created!..."); //Validation Message
                    Console.WriteLine();
                    Console.WriteLine("Enter Valid Business Location Name: "); //Prompt User to Input Business Name to Generate Contract Tracing Report For
                    string checkName = Console.ReadLine();
                    bool isFound = false;
                    foreach (BusinessLocation businessLocation in bList)
                    {
                        if (businessLocation.BusinessName.ToUpper() == checkName.ToUpper()) //Check if name entered by user is in business location list of names
                        {
                            Console.WriteLine("Report has been Created into 'ContactTracingReport.csv' File!");
                            isFound = true;
                            using (StreamWriter sw = new StreamWriter("ContactTracingReport.csv", false)) //Using StreamWriter to write data to ContactTracingReport.csv
                            {
                                sw.WriteLine(businessLocation.BusinessName); //Add business location name as  title to the csv report
                                string headings = "Name of Person(s)" + "," + "Check-In" + "," + "Check-Out"; //Add Headings of csv Report
                                sw.WriteLine(headings); 
                                string dataInput;
                                foreach (Person p in pList) //Loop through person list 
                                {
                                    foreach (SafeEntry SE in p.SafeEntryList) //Loop through safeentry list to find safeentry reocrd
                                    {
                                        if (SE.Location.BusinessName == businessLocation.BusinessName) //Check if Location Name with safeentry record is found in business location list
                                        {
                                            if (SE.CheckIn <= date) // Check that Date Input by user is after the Safeentry Check in timing so that it is within the period
                                            {
                                                if (date <= SE.CheckOut) //Check that Date Input by user is before the Safeentry Check out timing so that it is within the period
                                                {
                                                    dataInput = p.Name + "," + SE.CheckIn + "," + SE.CheckOut; 
                                                    sw.WriteLine(dataInput); //Write data to csv file if date Input by user is between Check in DateTime and Checkout DateTime
                                                }
                                                else if (SE.CheckOut == Convert.ToDateTime("1212/02/20")) // Dummy Value of Date "1212/02/20" to temporarily store Checkout property when user checks in. If DateTime = 1212/02/20, User hasnt Checked out
                                                {
                                                    dataInput = p.Name + "," + SE.CheckIn + "," + "NA"; //NA entered into checkout property if user has not checked out (i.e. 1212/02/20)
                                                    sw.WriteLine(dataInput);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (isFound == false)
                    {
                        Console.WriteLine("Business Name Searched Could not be found...");
                    }
                }
                else if (choice == 2) //If user chooses not to generate Contract Tracing Report
                {
                    display = false;
                    Console.WriteLine("Returning back to main menu");
                    Task.Delay(1500).Wait();
                    MainMenu(pList, bList, shnFacilityList);
                }
                else
                {
                    Console.WriteLine("Invalid Option! Please try again...");
                }
                
            }
        }

        //Advanced Feature 3.2 - SHN Status Reporting 
        static void ShnStatusReporting(List<Person> pList, List<BusinessLocation> bList, List<SHNFacility> shnList)
        {
            DateTime formattedDate = DateTime.Now; // dummy value
            bool shnStatusReporting = true;
            int choice = 50;// dummy value
            while(shnStatusReporting == true)
            {
                //Print out SHN Status Reporting Menu
                Console.WriteLine("***************************************************************");
                Console.WriteLine("*                                                             *");
                Console.WriteLine("*                    SHN Status Reporting                     *");
                Console.WriteLine("*                                                             *");
                Console.WriteLine("***************************************************************");
                Console.WriteLine("Would you like to generate a SHN Status Report?");
                Console.WriteLine("(1) Yes");
                Console.WriteLine("(2) No");
                //Exception handling
                try
                {
                    Console.Write("Options: ");
                    choice = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine();
                }
                catch (FormatException ex)
                {
                    Console.WriteLine();
                    Console.WriteLine("Invalid option selected!");
                    Console.Write("Exception details: ");
                    Console.WriteLine(ex.Message);
                }
                if (choice == 1)
                {
                    Console.Write("Please Select A Date To Generate The Report For (yyyy/mm/dd): "); //Prompt user to enter the day to generate Travel Entry Records with SHN for that date
                    string dateChosen = Console.ReadLine(); 
                    // Check if Date Input by user is in the correct format of yyyy/MM/dd
                    if (DateTime.TryParse(dateChosen, out formattedDate))
                    {
                        String.Format("{0:yyy/MM/dd}", formattedDate);
                        Console.WriteLine("Date successfully obtained!"); //Print validation message when date is obtained successfully
                        break;
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.Write("Invalid...");
                        Console.WriteLine("Please Enter Date in Format of yyyy/mm/dd "); //Prompt validation message
                        Console.WriteLine("If date is in correct format, ensure that it is a valid date (e.g. 2021/14/52 - Invalid)"); //Prompt user to enter Year, Month and Day is not out of range 
                        Console.WriteLine();
                    }
                }
                else if (choice == 2) // If user chooses not to generate SHN Status Reprot
                {
                    shnStatusReporting = false;
                    Console.WriteLine("returning to main menu...");
                    Task.Delay(1000).Wait();
                    MainMenu(pList, bList, shnList); //Return Back to Main Menu
                }
                else
                {
                    Console.WriteLine("Invalid Option...Please Select From Either Option 1 or 2..."); // Check that option inputted by user is either 1 or 2 
                    Console.WriteLine();
                }
            }
            string headings = "Name of Traveller" + "," + "SHN End Date" + "," + "SHN Location"; //headings for Excel report to display the name, shn end date and location of SHN
            using (StreamWriter sw = new StreamWriter("SHNStatusReport.csv", false))
            {
                sw.WriteLine("SHN Status Reporting Date: " + formattedDate.ToString("dd/MM/yyyy")); //Title of CSV Report of the Date Inputted By user 
                sw.WriteLine(headings); //Write Headings to csv report
                string data = null;
                int countRecord = 0; //Count number of travel entry records with SHN registered during that date inputted by user, Default being 0
                for (int i = 1; i < pList.Count; i++) //Loop through personList
                {
                    if (pList[i].TravelEntryList.Count != 0) //Check if Person's Travel list is not empty
                    {
                        foreach (TravelEntry TE in pList[i].TravelEntryList) //loop through Travel entry list of Person
                        {
                            if ((TE.EntryDate.Date <= formattedDate) && (formattedDate <= TE.ShnEndDate.Date)) //Check if formatted date is in between the entry date and shn End Date
                            {
                                if (TE.ShnStay == null) //Check if SHN Stay is empty when user comes from Macao SAR, Vietnam or New Zealand
                                {
                                    if (TE.ShnEndDate > TE.EntryDate)  //Check if SHN Stay is empty when user comes from Macao SAR, as shn end date would be same as entry date for travellers coming form Vietnmae or New Zealand
                                    {
                                        data = pList[i].Name + "," + TE.ShnEndDate.ToString("dd/MM/yyyy HH:mm") + "," + "Own Accommodation"; //data to be inputted to csv, with "Own Accommodation" as third column for travellers who came from Macao SAR
                                        sw.WriteLine(data);
                                        countRecord += 1; //increase count record by 1 when data is written to csv
                                    }
                                }
                                else
                                {
                                    data = pList[i].Name + "," + TE.ShnEndDate.ToString("dd/MM/yyyy HH:mm") + "," + TE.ShnStay.FacilityName; //if person currently has shn Stay in a facility
                                    sw.WriteLine(data);
                                    countRecord += 1; //increase count record by 1 when data is written to csv
                                }
                            }
                        }
                    }
                }
                Console.WriteLine(countRecord + " Record(s) successfully added to SHN Status Report..."); //Display number of records successfully written to csv
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
                Console.WriteLine("(1) List all Visitors"); //Display all visitor and details
                Console.WriteLine("(2) List Person Details"); //Display specific person details from name entered
                Console.WriteLine("(3) Back to Main Menu"); // Return back to Main Menu
                Console.WriteLine("=============================");
                try
                {
                    Console.Write("Options: ");
                    choice = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine();
                }
                catch (FormatException ex)
                {
                    Console.WriteLine();
                    Console.WriteLine("Invalid option selected!");
                    Console.Write("Exception details: ");
                    Console.WriteLine(ex.Message);
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
                    else
                    {
                        Console.WriteLine("Choose from either Options 1, 2, or 3...");
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
            Console.WriteLine("--------------------------- List of Visitors ---------------------------"); //Listing of all visitors
            for (int i = 0; i < personList.Count; i++)
            {
                if (personList[i] is Visitor) //Check if object in person list is also a visitor object
                {
                    Console.WriteLine(personList[i]);
                }
            }
            Task.Delay(1500).Wait(); //Delay output for users to view longer duration
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
                    if (p.TravelEntryList.Count == 0) //When there is no travel entry record found
                    {
                        Console.WriteLine("No Travel Entry Record Found...");
                    }
                    else
                    {
                        for (int i = 0; i < p.TravelEntryList.Count; i++)
                        {
                            Console.WriteLine("Record " + "#" + Convert.ToInt32(i + 1) + ":");
                            Console.WriteLine(p.TravelEntryList[i]);
                            Console.WriteLine();
                            //Console.WriteLine("{0,10}  {1,10}  {2,10}  {3,10}  {4,10}  {5,10}", TE.LastCountyOfEmbarkation, TE.EntryMode, TE.EntryDate, TE.ShnEndDate, TE.IsPaid, TE.ShnStay);
                        }
                    }
                    
                    Console.WriteLine("");
                    Console.WriteLine("-------------------------Safe Entry Details---------------------------");
                    if (p.SafeEntryList.Count == 0) // When there is no SafeEntry Check-In records available 
                    {
                        Console.WriteLine("No Safe Entry Check-In Record Found...");
                        Console.WriteLine();
                    }
                    else // When there is Safeentry Check=In records available 
                    {
                        foreach (SafeEntry SE in p.SafeEntryList) //Loop through Safeentry List
                        {
                            if (SE.CheckOut == Convert.ToDateTime("1212/02/20")) // DateTime set as "1212/02/20" when user checked in, means that user has not checked out yet
                            {
                                Console.WriteLine(SE); //Display Safeentry Details without check out details
                            }
                            else
                            {
                                Console.WriteLine(SE);
                                Console.WriteLine("Check Out Time: " + SE.CheckOut); //Display Safentry Details with checkout when user has checked out
                            }
                            Console.WriteLine("-----------------------------------------");
                            Console.WriteLine();
                        }
                    }
                    isFound = true;
                    if (p is Resident) //When Person found in list is a resident 
                    {
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
            Console.Write("Enter your name: ");
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
                            while (true)
                            {
                                Console.WriteLine("===============================================");
                                Console.WriteLine("There was no Trace Together Token Data Found...");
                                Console.WriteLine("===============================================");
                                Console.WriteLine("Would you like to be assigned a token? ");
                                Console.WriteLine("(1) Yes");
                                Console.WriteLine("(2) No");
                                Console.Write("Option: ");
                                try
                                {
                                    string option = Console.ReadLine(); // store option of either (1) Yes or (2) NO 
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
                                    else
                                    {
                                        Console.WriteLine("Invalid Option... Please Select from either Option 1 or 2...");
                                    }
                                }
                                catch(FormatException ex)
                                {
                                    Console.WriteLine("Invalid option selected!");
                                    Console.Write("Exception details: ");
                                    Console.WriteLine(ex.Message);
                                    Console.WriteLine("Choose from either Options 1 or 2..");
                                    Console.WriteLine();
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
                                Console.Write("Option: ");
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
                                else if (ReplaceOption is "3")//When user chooses (3) Go Back
                                {
                                    SafeEntryMenu(personList, businessLocationList, shnFacilityList);
                                }
                                else
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("Choose from either Options 1, 2 or 3...");
                                    Task.Delay(1500).Wait();
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
            Console.Write("Option: ");
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
            else
            {
                Console.WriteLine();
                Console.WriteLine("Choose from either Options 1, 2, 3, 4, 5, or 6...");
            }
        }

        // option 4 of SafeEntry Menu to Check-In 
        static void CheckIn(List<Person> personList, List<BusinessLocation> businessLocationList, List<SHNFacility> shnFacilityList)
        {
            bool isFound = false;
            Console.Write("Enter your name: ");
            string SEName = Console.ReadLine();
            foreach (Person p in personList)
            {
                if (p.Name.ToLower() == SEName.ToLower()) // To check if correct Name is being input by the user 
                {
                    isFound = true;
                    //if ((p.SafeEntryList.Count >= 1)) // When there is no check in data to be displayed 
                    //{
                    //    Console.WriteLine("");
                    //    Console.WriteLine("===========================================================================================");
                    //    Console.WriteLine("Please Check Out from previous location, before you are able to Check-In to a new loaction!");
                    //    Console.WriteLine("===========================================================================================");
                    //    SafeEntryMenu(personList, businessLocationList, shnFacilityList); // Navigate user back to Safe Entry Menu after diplaying message to tell user that he needs to checkout from the previous location before he can check in again 
                    //}
                    
                    Console.WriteLine("--------------------------- All Business Locations---------------------------");
                    for (int i = 0; i < businessLocationList.Count; i++) // To loop and get index 
                    {
                        Console.WriteLine(i + 1 + ".................................");
                        Console.WriteLine(businessLocationList[i]);// To list business locatiopns for user to choose from 
                        Console.WriteLine("");
                    }
                    Console.WriteLine();
                    Console.WriteLine("Please enter the number tagged to the business location...");
                    Console.WriteLine("==========================================================");
                    while (true)
                    {
                        try
                        {
                            Console.Write("Business Location to Check In: ");
                            int SEBLOption = Convert.ToInt32(Console.ReadLine()); // To store the users choice of shop from 1 to 4 
                            if (SEBLOption >= 5 || SEBLOption <= 0)
                            {
                                Console.WriteLine("Invalid Option!...");
                                CheckIn(personList, businessLocationList, shnFacilityList);
                            }
                            //SEBLOption = SEBLOption - 1; // To get index of the business locations 
                            Task.Delay(1500).Wait();
                            if (businessLocationList[SEBLOption-1].VisitorsNow < businessLocationList[SEBLOption-1].MaximumCapacity)// When the number of visitors in the location is not at maximum 
                            {
                                SafeEntry CheckIn = new SafeEntry(DateTime.Now, businessLocationList[SEBLOption-1]);
                                CheckIn.CheckOut = Convert.ToDateTime("1212/02/20"); //Store Checkout DateTime to "1212/02/20" when user checked in but has not checked out
                                businessLocationList[SEBLOption-1].VisitorsNow = businessLocationList[SEBLOption-1].VisitorsNow + 1; // Visitor now would add 1 
                                p.AddSafeEntry(CheckIn); // To update check in data for the business locations 
                                Console.WriteLine("");
                                Console.WriteLine("=============== Checked-In ==============");
                                Console.WriteLine(CheckIn); // To display the new check in data information with the updated number of visitors 
                                Console.WriteLine("-----------------------------------------");
                                Console.WriteLine("=========================================");
                                break;
                            }
                            else // Whem the number of visitors at the location is at maximum 
                            {
                                Console.WriteLine("----------------------------------------------------------------------");
                                Console.WriteLine("Business Location has reached Maximum Capacity. Try again in a while! ");
                                Console.WriteLine("----------------------------------------------------------------------");
                                SafeEntryMenu(personList, businessLocationList, shnFacilityList);
                            }
                        }
                        catch (FormatException ex)
                        {
                            Console.WriteLine("Invalid option selected!");
                            Console.Write("Exception details: ");
                            Console.WriteLine(ex.Message);
                            Console.WriteLine("Choose from either Options 1, 2, 3, 4...");
                            Console.WriteLine();
                        }
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
            Console.Write("Enter your name: ");
            string SEName = Console.ReadLine(); // Stores user input name 
            foreach (Person p in personList)
            {
                if (p.Name.ToLower() == SEName.ToLower()) // To check if correct Name is being input by the user 
                {
                    isFound = true;
                    List<SafeEntry> notCheckedOut = new List<SafeEntry>(); //List of SafeEntry objects that have not been checked out
                    foreach (SafeEntry SE in p.SafeEntryList)
                    {
                        if (SE.CheckOut == Convert.ToDateTime("1212/02/20")) //Check if DateTime is not checked out which is represented by "1212/02/20"
                        {
                            notCheckedOut.Add(SE); //Add Safeentry Objects that have not been checked out into a list
                        }
                    }
                    //DisplayAllBusinessLocation(businessLocationList);
                    if ((notCheckedOut.Count == 0) || (notCheckedOut == null)) // When there is no check in data to be displayed 
                    {
                        Console.WriteLine("");
                        Console.WriteLine("==================================");
                        Console.WriteLine("No Location available to check out");
                        Console.WriteLine("==================================");// Navigate user back to Sae Entry Menu after diplaying message to tell user that there is no location to check in 
                    }
                    else if ((notCheckedOut != null) || (notCheckedOut.Count != 0)) // When there is data in checkin in safentry cal
                    {
                        Console.WriteLine("------------------- Business Location Not checked out -------------------"); //Display Business Locations which have not been checked out
                        Console.WriteLine(".................................");
                        for (int i = 0;  i< notCheckedOut.Count; i++)
                        {
                            Console.WriteLine("Record #" + Convert.ToInt32(i + 1)); //Numbering of records
                            Console.WriteLine(notCheckedOut[i]); //Display not checked out Safeentry objects
                            Console.WriteLine();
                        }
                        while (true)
                        {
                            try
                            {
                                Console.Write("Please Select Record Number to Check Out From: "); //Prompt user to enter record to check out from 
                                int choice = Convert.ToInt32(Console.ReadLine());
                                if ((choice >= 1) && (choice <= notCheckedOut.Count))
                                {
                                    foreach (BusinessLocation b in businessLocationList) // When the checkin location matches the name in business location list 
                                    {
                                        if (notCheckedOut[choice-1].Location.BusinessName == b.BusinessName) // If the check In location in safentrylist tallies with the location in businesslocationlist 
                                        {
                                            b.VisitorsNow = b.VisitorsNow - 1; // deduct one from the number of visitors in the business location
                                            Console.WriteLine("");
                                            Console.WriteLine("=============== Checked-Out ==============");
                                            foreach (SafeEntry SE in p.SafeEntryList)
                                            {
                                                if (SE.CheckIn == notCheckedOut[choice - 1].CheckIn) //Check if not Safentry object equals to selected object from the notCheckedOut List
                                                {
                                                    Console.WriteLine(SE);
                                                    Console.WriteLine(SE.PerformCheckOut()); //Check out with DateTime.Now
                                                }
                                            }
                                            //Console.WriteLine(notCheckedOut[choice-1]); // To tell users the new information of the business and to confirm that the number of vistors is deducted
                                            //Console.WriteLine(notCheckedOut[choice-1].PerformCheckOut());//Show the checkin and checkout timing
                                            Console.WriteLine("------------------------------------------");
                                            Console.WriteLine("==========================================");
                                            Task.Delay(1500).Wait();
                                            SafeEntryMenu(personList, businessLocationList, shnFacilityList); // Navigate user back to the SafeEntry Menu after updated business location is displayed 
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("Invalid Input...Please select from Integer Option from 1 to " + notCheckedOut.Count); //Prompt user to select an option from 1 to ... when user does not entered a valid input
                                }
                            }
                            catch (FormatException ex) //Check if option selected by user is not a number
                            {
                                Console.WriteLine("Invalid option selected!");
                                Console.Write("Exception details: ");
                                Console.WriteLine(ex.Message);
                                Console.WriteLine("Choose from a Numbered Record (e.g. 1)...");
                                Console.WriteLine();
                            }
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
            bool displayTravelEntry = true;  // display set to true 
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
                    choice = Convert.ToInt32(Console.ReadLine()); //Check if User entered valid option and not letters
                    Console.WriteLine();
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Invalid option selected!");
                    Console.Write("Exception details: "); //Invalid Option Messages
                    Console.WriteLine(ex.Message);
                }
                if (choice != 5) // If user does not brack Back to Menu...
                {
                    if (choice == 1)
                    {
                        ListAllSHNFacility(shnFacilityList); //Call method to list all shn facilities in the shnFacilityList
                    }
                    else if (choice == 2)
                    {
                        CreateVisitor(personList); //Call Method to create visitor object
                    }
                    else if (choice == 3)
                    {
                        CreateTravelEntryRecord(personList, shnFacilityList, businessLocationList); // Create new Travel Entry record for people who previously had no travel entry record, shn has ended, and shn is Paid For
                    }
                    else if (choice == 4)
                    {
                        CalculateSHNCharges(personList);
                    }
                    else
                    {
                        Console.WriteLine("Choose from either Options 1, 2, 3, 4, or 5..."); //Display Invalid Error message and guides users to choose from options 1-5
                    }
                }
                else
                {
                    displayTravelEntry = false;
                    Task.Delay(1500).Wait(); //Delay program
                    MainMenu(personList,businessLocationList, shnFacilityList); // Call back to main menu if user decides to click on option 5 to exit
                }

            }

        }
        
        // Method for Option 1 of TravelEntry Menu 
        static void ListAllSHNFacility(List<SHNFacility> shnFacilityList)
        {   // Listing of all SHN Facilities including Facility Name, Capacity, Vacancy, Distance From Air/Sea/Land Checkpoints
            Console.WriteLine();
            Console.WriteLine("=================================================== SHN Facilities ======================================================");
            Console.WriteLine("{0,-15}   {1,-8}   {2,-8}   {3,-28}   {3,-28}   {4,-29}", "Facility Name", "Capacity", "Vacancy", "Distance From Air Checkpoint", "From Sea Checkpoint", "From Land Checkpoint");
            foreach (SHNFacility facility in shnFacilityList)
            {
                Console.WriteLine("{0,-15}   {1,-8}   {2,-8}   {3,-28}   {3,-28}   {4,-29}", facility.FacilityName,facility.FacilityCapacity, facility.FacilityVacancy, facility.DistFromAirCheckpoint, facility.DistFromSeaCheckpoint, facility.DistFromLandCheckpoint);
            }
            Console.WriteLine("=========================================================================================================================");
            Console.WriteLine();
            Task.Delay(1500).Wait();
        }

        // Method for Option 2 of TravelEntry Menu
        static void CreateVisitor(List<Person> pList)
        {
            bool alreadyExist = false;
            Console.Write("Please Enter The Visitor's Name: "); //Obtain Visitor Name
            string name = Console.ReadLine(); 
            name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(name); //Modify Name to Uppercase for first character and every character after spacing
            foreach (Person p in pList)
            {
                if (p.Name == name)
                {
                    alreadyExist = true;
                    Console.WriteLine("Person with Name, " + name + " already exists...");
                    break;
                }
            }
            if (alreadyExist == false)
            {
                Console.Write("Please Enter Your Passport Number: "); //Obtain Passport Number
                string passportNo = Console.ReadLine().ToUpper(); //Modify PassportNo to all uppercases
                char passportNoLastChar = passportNo[passportNo.Length - 1];
                string passportSubstring = passportNo.Substring(1, passportNo.Length - 2);
                if (passportNo.StartsWith("A") && (passportSubstring.Length == 7 || passportSubstring.Length == 8) && int.TryParse(passportSubstring, out _) && char.IsLetter(passportNoLastChar))
                {
                    Console.Write("Please Enter Your Nationality: "); // Obtain Nationality of Visior
                    string nationality = Console.ReadLine();
                    nationality = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(nationality); // Modify Nationality to Uppercase for first character and every character after spacing
                    Visitor visitor = new Visitor(name, passportNo, nationality);  //Creating Visitor Object
                    Console.WriteLine("Visitor Object Successfully Created...");
                    pList.Add(visitor);
                }
                else
                {
                    Console.WriteLine("Passport Number Must Start with 'A', have a String Length of 7/8 Numeric Characters in Between and Must End with an Alphabetical Character...");
                    Console.WriteLine("e.g. A1234567E");
                }
            }
        }

        // Method for Option 3 of TravelEntry Menu
        //Creation of Travel Entry Record
        static void CreateTravelEntryRecord(List<Person> personList, List<SHNFacility> shnFacilityList, List<BusinessLocation> businessLocationList)
        {
            Console.Write("Enter Name To Be Searched: "); 
            string searchedName = Console.ReadLine(); //Obtain name string
            bool isFound = false;  //def
            for (int i = 0; i<personList.Count; i++)
            {
                if (personList[i].Name.ToLower() == searchedName.ToLower())
                {
                    Console.WriteLine("Successfully Found Name To Be Searched!");
                    isFound = true;
                    Console.WriteLine();
                    Console.Write("Enter " + personList[i].Name +"'s Last Country of Embarkation: "); //Creating Travel Entry Object of personList[i]
                    string lastCountryOfEmbarkation = Console.ReadLine();
                    lastCountryOfEmbarkation = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lastCountryOfEmbarkation); // Change last country of embarkation first character to capital case for every word
                    Console.Write("Enter " + personList[i].Name + "'s Mode of Entry: "); // Prompt user for mode of travel 
                    string entryMode = Console.ReadLine(); 
                    entryMode = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(entryMode); //Change Entry Mode first character to Capital Case for every word
                    while (entryMode != "Air" && entryMode != "Land" && entryMode != "Sea") // Check if entryMode is either Air, Land or Sea for validation purposes.
                    {
                        Console.WriteLine();
                        Console.WriteLine("Please enter a valid Mode of Entry... Choose From Either: Land, Air, or Sea...");
                        Console.Write("Enter " + personList[i].Name + "'s Mode of Entry: ");
                        entryMode = Console.ReadLine();
                        entryMode = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(entryMode); //Change mode of entry to title cases.
                    }
                    TravelEntry TE = new TravelEntry(lastCountryOfEmbarkation, entryMode, DateTime.Now); // Create new Travel Entry object with current date as entry date.
                    TE.CalculateSHNDuration(); //Method to calculate duration of SHN
                    if ((lastCountryOfEmbarkation != "New Zealand") && (lastCountryOfEmbarkation != "Vietnam") && (lastCountryOfEmbarkation.ToLower() != "macao sar")) //Check if last country of embarkation is one of the following countries.
                    {
                        Console.WriteLine("====================================");
                        for (int x = 0; x < shnFacilityList.Count; x++)
                        {
                            Console.WriteLine("Option " + Convert.ToInt32(x + 1) + ":"); //Printing of facilities for booking
                            Console.WriteLine(shnFacilityList[x]);
                            Console.WriteLine("====================================");
                        }
                        while (true)
                        {
                            int choice = 50;//dummy value
                            try
                            {
                                Console.Write("From the Options above...\nPlease Select A SHN Facility To Be Assigned To: ");
                                choice = Convert.ToInt32(Console.ReadLine()) - 1; // Choose index of list to select SHN Facility to be assigned to 
                            }
                            catch (FormatException ex)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Invalid option selected!"); //Print invalid message if user enters wong information.
                                Console.Write("Exception details: ");
                                Console.WriteLine(ex.Message);
                            }
                            if (choice >= 0 && choice < shnFacilityList.Count)
                            {
                                if (shnFacilityList[choice].IsAvailable() == false)
                                {
                                    Console.WriteLine("The Facility is not available due to vacancy constraints..."); //inform users that the chosen facility is not available for banking.
                                    continue;
                                }
                                TE.AssignSHNFacility(shnFacilityList[choice]);
                                shnFacilityList[choice].FacilityVacancy = shnFacilityList[choice].FacilityVacancy - 1; //Reduce vacancy of SHN facility after faculty is booked by customers
                                break;
                            }
                            else
                            {
                                Console.WriteLine();
                                Console.WriteLine("Invalid option selected. Select a given numbered option..."); //prompts users to enter a correct input.
                                Console.WriteLine();
                            }
                        }
                    }
                    else if (lastCountryOfEmbarkation.ToLower() == "macao sar")
                    {
                        Console.WriteLine("The Person Identified Is Only Required To Serve SHN At Own Accommodation..."); //Prints message that user would be serbing SHN at their own accommodations 
                    }
                    else
                    {
                        Console.WriteLine("The person identified is not required to serve SHN..."); //Informs that the person Identifid would not be required to serve SHN
                    }
                    TE.IsPaid = false; //While Loops
                    personList[i].AddTravelEntry(TE); // Adds Travel entry object a person object to a personList.
                    Console.WriteLine();
                    Console.WriteLine("Travel Entry Successfully Recorded for " + personList[i].Name + "!"); //Display Success in adding record. 
                }
            }
            if (isFound == false)
            {
                Console.WriteLine("Searched Name could not be found..."); // Display error message if name could not be found in person List to begin with
            }
        }
        //Method for Option 4 of TravelEntry Menu
        static void CalculateSHNCharges(List<Person> personList) //Method to Calculate SHN Charges and make payments
        {
            Console.Write("Please enter the name to be searched: ");
            string searchedName = Console.ReadLine(); //search for name
            bool isFound = false; //Check if Person can be found in list and update accordingly
            double cost;
            List<TravelEntry> notEndedAndUnpaid = new List<TravelEntry>();
            for (int i = 0; i < personList.Count; i++)
            {
                if (searchedName.ToLower() == personList[i].Name.ToLower()) //Check if Searched Name is in Person List
                {
                    isFound = true;
                    Console.WriteLine("Name Searched Successfully!.... "); 
                    Console.WriteLine();
                    DateTime presentDate = DateTime.Now; //Date to check against SHN Ended Date if Stay has ended
                    foreach(TravelEntry TE in personList[i].TravelEntryList) //Loop Through List of TravelEntry Records in Travel Entry List
                    {
                        if (TE.ShnEndDate <= presentDate && TE.IsPaid == false) //Check if SHN has ended and is unpaid
                        {
                            notEndedAndUnpaid.Add(TE); //add to list of Travel Entry Records which have not ended and is unpaid
                        }
                    }
                    if (notEndedAndUnpaid.Count == 0) //Check if There is no travel entry record that has ended or is unpaid
                    {
                        //Check if person has travel entry record, if no person would not need to pay for SHN Charges
                        Console.WriteLine("No Travel Entry Record Found to have Ended and is Unpaid...");
                        Console.WriteLine("The Person Identified Does Not Need to Pay SHN Charges...");
                    }
                    else
                    {
                        int choice = 100; //dummy value
                        Console.WriteLine("================================================");
                        Console.WriteLine("==  TravelEntry with SHN ended and is unpaid  =="); //Menu to Display Travel Entry Records for payment 
                        Console.WriteLine("================================================");
                        for (int index = 0; index < notEndedAndUnpaid.Count; index++) 
                        {
                            Console.WriteLine("Record #" + Convert.ToInt32(index + 1)); //Labelling Travel entry records by numbering them
                            Console.WriteLine(notEndedAndUnpaid[index]); //Display Travel Entry Records Properties
                            Console.WriteLine("SHN End Date: " + notEndedAndUnpaid[index].ShnEndDate); //Display SHN End Date of Unpaid and SHN Ended Travel Entry Record
                            if (notEndedAndUnpaid[index].EntryDate != notEndedAndUnpaid[index].ShnEndDate) //Check for Travel Entry Records with the Exception of Vietnam or New Zealand
                            {
                                if (string.IsNullOrEmpty(Convert.ToString(notEndedAndUnpaid[index].ShnStay)) == false) //Check if Travel Entry Record has SHN Stay
                                {
                                    Console.WriteLine("SHN Stay: " + notEndedAndUnpaid[index].ShnStay); //For Other countries besides Macao SAR and Vietnam or New Zealand
                                }
                                else
                                {
                                    Console.WriteLine("SHN Stay: Own Accommodation"); //For display of Macao SAR
                                }
                            }
                            else if (notEndedAndUnpaid[index].EntryDate == notEndedAndUnpaid[index].ShnEndDate)
                            {
                                Console.WriteLine("SHN Stay: Not Available"); //Display SHN Stay is not available if from vietnam or new zealand
                            }
                            Console.WriteLine("Payment Made: " + notEndedAndUnpaid[index].IsPaid); //Display if payment has been made or not
                            Console.WriteLine();
                        }
                        //Exception handling
                        try
                        {
                            Console.Write("Please Select A Record That You Would Like To Pay For: ");
                            choice = Convert.ToInt32(Console.ReadLine());
                        }
                        catch (FormatException ex)
                        {
                            Console.WriteLine("Invalid option selected!");
                            Console.Write("Exception details: "); //Invalid Option Messages
                            Console.WriteLine(ex.Message);
                        }
                        if (choice >= 1 && choice <= notEndedAndUnpaid.Count) //Check if Choice selected by user is within range
                        {
                            cost = personList[i].CalculateSHNCharges(notEndedAndUnpaid[choice - 1]); //Calculate SHN Charges from Visitor/Resident Class. Calling Transportation cost in visitor class.
                            while (true)
                            {

                                Console.Write("Would you like to pay " + cost.ToString("$0.00") + " from Record #" + choice + "? [Y/N]: ");
                                string option = Console.ReadLine();
                                if (option.ToUpper() == "Y") //If user wants to proceed with payment display the following message
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("Ok! Processing Payment...");
                                    Task.Delay(1500).Wait();
                                    Console.WriteLine("Payment Successfully Made!");
                                    notEndedAndUnpaid[choice - 1].IsPaid = true; // Change boolean value of IsPaid to true
                                    break;
                                }
                                else if (option.ToUpper() == "N") //If user wants to proceed without payment, display the following message
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("Proceeding without Payment...");
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("Invalid Option... Please Select [Y]/[N]"); //Prompt user to click of Y or N if an invalid option is made
                                    Console.WriteLine();
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Option Selected is out of range...");
                        }
                    }
                }
            }
            if (isFound == false) //If searched name could not be found display the following message
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
                    if (string.IsNullOrEmpty(properties[9]) == false)
                    {
                        DateTime travelEntryDate;
                        travelEntryDate = FormatTravelDate(properties[11]);
                        TravelEntry TE = new TravelEntry(properties[9], properties[10], travelEntryDate);
                        DateTime travelShnEndDate;
                        travelShnEndDate = FormatTravelDate(properties[12]);
                        TE.ShnEndDate = travelShnEndDate;
                        ValidatePayment(properties[13]);
                        resident.AddTravelEntry(TE);

                        if (properties[13] != null)
                        {
                            string boolValue = properties[13];
                            TE.IsPaid = ValidatePayment(boolValue);
                        }
                        if (properties[14] != null)
                        {
                            TE.AssignSHNFacility(SearchFacility(shnList, properties[14]));
                        }
                    }
                }
                else if (properties[0] == "visitor")  // When the attribute under the heading "type" is a visitor
                {
                    Visitor visitor = new Visitor(properties[1], properties[4], properties[5]);
                    TravelEntry TE = new TravelEntry();
                    pList.Add(visitor);
                    if (string.IsNullOrEmpty(properties[9]) == false)
                    {
                        TE.LastCountyOfEmbarkation = properties[9];
                        TE.EntryMode = properties[10];
                        DateTime travelEntryDate;
                        DateTime travelShnEndDate;
                        travelEntryDate = FormatTravelDate(properties[11]);
                        travelShnEndDate = FormatTravelDate(properties[12]);
                        TE.EntryDate = travelEntryDate;
                        TE.ShnEndDate = travelShnEndDate;
                        visitor.AddTravelEntry(TE);
                    }
                    if (string.IsNullOrEmpty(properties[13]) == false)
                    {
                        string boolValue = properties[13];
                        TE.IsPaid = ValidatePayment(boolValue);
                    }
                    if (string.IsNullOrEmpty(properties[14]) == false)
                    {
                        TE.AssignSHNFacility(SearchFacility(shnList, properties[14]));
                    }
                }
            }
        }
        static DateTime FormatTravelDate(string travelDate) // FormatTravelDate() is used to check the datetime format of excel cells and make it a proper DateTime format
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
        static bool ValidatePayment(string boolValue) //Check if payment has been made 
        {
            if (boolValue == "TRUE")
            {
                return true; //return true if payment has been made
            }
            else
            {
                return false;  //return false if payment has not been made
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
        static SHNFacility SearchFacility(List<SHNFacility> shnList, string n) // Search for SHN facility when prompted through a foreach loop
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
