/* 
The MIT License (MIT)

Copyright (c) 2017 Wolfgang Almeida <wolfgang.almeida@yahoo.com>

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace IPSharpNS
{
    public class IPSharp
    {
        // Member Variables
        // ================
        
        // INSERT YOUR GOOGLE API KEY HERE:
        // --------------------------------
        private string GoogleAPIKey = "";

        // Results Variables
        // -----------------
        private string IP { get; set; }
        private string Hostname { get; set; }
        private string City { get; set; }
        private string Region { get; set; }
        private string Country { get; set; }
        private string Postal { get; set; }
        private string Loc { get; set; }
        private string Org { get; set; }
        private string GoogleGeoLoc { get; set; }

        // Common Variables
        // ----------------
        private string MyIP = null;

        // Default Constructor
        // ===================
        public IPSharp(): this(null) { }

        // Custom Constructor
        // ==================
        public IPSharp(string SelectedIP)
        {
            MyIP = SelectedIP;

            string Response = GetIPInformation();
            SettingValues(Response);
            GetIPLocation();
        }

        // Member Methods
        // ==============

        // Public Methods:
        // ---------------

        // Displaying results for the user
        // -------------------------------
        public void DisplayResults()
        {
            string Announcer = string.Format("Informações sobre o IP '{0}':", IP);

            Console.WriteLine(Announcer);
            Console.WriteLine(string.Concat(Enumerable.Repeat("-", Announcer.Length)));

            Console.WriteLine("Endereço IP: {0}", IP);
            Console.WriteLine("Hostname: {0}", Hostname);
            Console.WriteLine("Cidade: {0}", City);
            Console.WriteLine("Região: {0}", Region);
            Console.WriteLine("País: {0}", Country);
            Console.WriteLine("Cód. Postal: {0}", Postal);
            Console.WriteLine("Latitude/longitude: {0}", Loc);
            Console.WriteLine("Endereço (estimativa): {0}", GoogleGeoLoc);
            Console.WriteLine("Organização: {0}", Org);
        }

        // Private Methods:
        // ----------------

        // Getting the IP information for further processing
        //--------------------------------------------------
        private string GetIPInformation()
        {
            string IPInfoAPIURL;
            string Response = "";

            if (MyIP == null) {
                IPInfoAPIURL = "http://ipinfo.io/json";
            }
            else {
                IPInfoAPIURL = string.Format("http://ipinfo.io/{0}/json", MyIP);
            }

            try {
                WebClient client = new WebClient();
                Response = client.DownloadString(IPInfoAPIURL);
            }
            catch (Exception) {
                Console.WriteLine("[IPSharp] Erro! Não foi possível verificar o Endereço IP! Saindo...");
                Environment.Exit(2);
            }

            return Response;
        }

        // Setting up values
        // -----------------
        private void SettingValues(string Response)
        {
            JObject Data = (JObject)JsonConvert.DeserializeObject(Response);
            Dictionary<string, string> DataDict = Data.ToObject<Dictionary<string, string>>();
            string Value = "";
            string[] PossibleKeys = new string[8] { "ip", "hostname", "city", "region", "country", "postal", "loc", "org"};

            for (int i = 0; i < PossibleKeys.Length; i++) {
                if (DataDict.ContainsKey(PossibleKeys[i])) {
                    Value = (string)Data[PossibleKeys[i]];
                }
                else {
                    Value = "Indisponível";
                }

                switch (PossibleKeys[i])
                {
                    case "ip": IP = Value; break;
                    case "hostname": Hostname = Value; break;
                    case "city": City = Value; break;
                    case "region": Region = Value; break;
                    case "country": Country = Value; break;
                    case "postal": Postal = Value; break;
                    case "loc": Loc = Value; break;
                    case "org": Org = Value; break;
                    default: break;
                }
            }
        }

        // Getting possible location from the Loc
        // --------------------------------------
        private void GetIPLocation()
        {
            if (Loc != "Indisponível") {
                string Response = "";
                string URLAPIGoogleMaps = string.Format("https://maps.googleapis.com/maps/api/geocode/json?latlng={0}&key={1}", 
                    Loc, GoogleAPIKey);

                try {
                    WebClient client = new WebClient();
                    Response = client.DownloadString(URLAPIGoogleMaps);

                    JObject Data = (JObject)JsonConvert.DeserializeObject(Response);
                    if ((string)Data["status"] == "OK") {
                        JArray msg = (JArray)Data.SelectToken("results");
                        foreach (JToken i in msg)
                        {
                            GoogleGeoLoc = (string)i["formatted_address"];
                            break;
                        }
                    }
                    else if ((string)Data["status"] == "OVER_QUERY_LIMIT") {
                        GoogleGeoLoc = "Limite de verificação excedido!";
                    }
                }
                catch (Exception) {
                    GoogleGeoLoc = "Indisponível";
                }
            }
            else {
                GoogleGeoLoc = "Indisponível";
            }
        } 
    }
}
