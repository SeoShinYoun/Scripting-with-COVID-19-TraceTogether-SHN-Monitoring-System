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


            // Creation of the MainMenu for users to navigate through other functions 
            List<Person> PersonList = new List<Person>();
            Console.WriteLine("");
            char opt = Convert.ToChar(Console.ReadLine());

            //Read Person csv file 
            using (StreamReader sr = new StreamReader("Person.csv"))
            {
                string s = sr.ReadLine();
                while ((s = sr.ReadLine()) != null)
                {
                    string[] attribute = s.Split(",");

                    if (attribute[0] == "resident")
                    {
                        Resident r = new Resident(attribute[1], attribute[2], Convert.ToDateTime(attribute[3]));
                        PersonList.Add(r);
                        foreach (char c in attribute[8])
                        {
                            if (c == '-')
                            {
                                var  = Convert.ToDateTime(attribute[8]).Date; 


                            }
                        }

                    }
                }
            }

        }

    }
}
