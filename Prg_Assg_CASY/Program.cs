using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

//============================================================
// Student Number : S10205100, S10203193
// Student Name : Seo Shin Youn, Phua Cheng Ann
// Module Group : T09 //============================================================

namespace Prg_Assg_CASY
{
    class Program
    {
        static void Main(string[] args)
        {
            // Calling SHNFacility class API (Basic Feature 2)
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

            //Creation of list to store csv file 
            List<Person> personList = new List<Person>();
            List<BusinessLocation> businessLocationList = new List<BusinessLocation>();
            IncludePerson(personList, shnfacilityList);
            IncludeBusinessLocation(businessLocationList);
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

        //Reading of person csv file using system.IO 
        static void IncludePerson(List<Person> pList, List<SHNFacility> shnList)
        {
            // reading person location csv file, from the second line onwards according to interpretation from csv file (without headings of attributes)
            string[] csvLines = File.ReadAllLines("Person.csv");
            for (int i = 1; i < csvLines.Length; i++)
            {
                string[] attributes = csvLines[i].Split(',');  //splitting the different attributes into its own individual table 
                if (attributes[0] == "resident") // When the attribute under the heading "type" is a resident 
                {
                    Resident r = new Resident(attributes[1], attributes[2], DateTime.ParseExact(attributes[3], "dd/MM/yyyy", null)); // last left country date using ParseExact to convert the string in csv to a datetime in program 
                    pList.Add(r);
                    if (attributes[6] != null) //When token serial number is present 
                    {
                        r.Token = new TraceTogetherToken(attributes[6], attributes[7], DateTime.ParseExact(attributes[8], "d-MMM-yyyy", null)); // Name, address, Tokenexpiry date using ParseExact to convert the string in csv to a datetime in program 
                    }
                    if (attributes[9] != null) //When Travel location is present 
                    {
                        TravelEntry t = new TravelEntry(attributes[9], attributes[10], DateTime.ParseExact(attributes[11], "dd/MM/yyyy H:mm", null)); // TokenExpiry , TravelEntryMode, TravelEntry date and time (in hours and minutes) using ParseExact to convert the string in csv to a datetime in program 
                        t.ShnEndDate = DateTime.ParseExact(attributes[12], "dd/MM/yyyy H:mm", null); //SHN end date and time (in hours and minutes) using ParseExact to convert the string in csv to a datetime in program 
                        if (attributes[13] != null)
                        {
                            t.IsPaid = Convert.ToBoolean(attributes[13]);
                        }
                        if (attributes[14] != null)
                        {
                            t.AssignSHNFacility(SearchFacility(shnList, attributes[14]));
                        }
                    }
                }
                else if (attributes[0] == "visitor") // When the attribute under the heading "type" is a visitor  
                {
                    pList.Add(new Visitor(attributes[1], attributes[4], attributes[5]));

                }
            }
        }

        static void LoadSHNFacility()
        {

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

        static void MainMenu()
        {
            // Creation of the MainMenu for users to navigate through other functions 
            List<Person> PersonList = new List<Person>();
            Console.WriteLine("***************************************************************");
            Console.WriteLine("*                                                             *");
            Console.WriteLine("*                 COVID-19 Monitoring System                  *");
            Console.WriteLine("*                                                             *");
            Console.WriteLine("***************************************************************");
            Console.WriteLine("");
            Console.WriteLine("==========Main Menu==========");
            Console.WriteLine("(1) General ");
            Console.WriteLine("(2) SafeEntry  ");
            Console.WriteLine("(3) TravelEntry ");
            Console.WriteLine("============================ ");
            Console.WriteLine("Options: ");
            int opt = Convert.ToChar(Console.ReadLine());
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
