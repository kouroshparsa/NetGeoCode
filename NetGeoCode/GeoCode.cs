﻿using System;
using Newtonsoft.Json;
using System.Web;
using System.Collections.Generic;
using System.Net;
using System.IO;

namespace NetGeoCode
{
    internal class Address
    {
        public string long_name;
        public string short_name;
        public string[] types;
    }
    internal class ApiResult
    {
        public string formatted_address;
        public string place_id;
        dynamic geometry;
        public dynamic types;
        public List<Address> address_components;
    }
    internal class ApiResponse
    {
        public string status;
        public List<ApiResult> results;
    }

    public class Location
    {
        public string country;
        public string state;
        public string county;
        public string city;
        public string country_code;
        public string state_code;
        public Location() { }
        public Location(string country, string state, string county, string city, string country_code, string state_code)
        {
            this.country = country;
            this.state = state;
            this.county = county;
            this.city = city;
            this.country_code = country_code;
            this.state_code = state_code;
         }

        public override string ToString()
        {
            return string.Format("country={0}, state={1}, city={2}", this.country, this.state, this.city);
        }
}
    public class GeoCode
    {
        private string googleApiKey;
        public GeoCode(string googleApiKey)
        {
            this.googleApiKey = googleApiKey;
        }
        public Location GetLocation(string postal_code)
        {
            string url = string.Format(@"https://maps.googleapis.com/maps/api/geocode/json?components=postal_code:{0}&key={1}", postal_code, googleApiKey);

            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream data = response.GetResponseStream();
            StreamReader reader = new StreamReader(data);

            // json-formatted string from maps api
            string jsonText = reader.ReadToEnd();
            response.Close();
            ApiResponse res = JsonConvert.DeserializeObject<ApiResponse>(jsonText);
            Location loc = new Location();
            foreach (var comp in res.results[0].address_components)
            {
                if (comp.types[0] == "country")
                {
                    loc.country = comp.long_name;
                    loc.country_code = comp.short_name;
                }
                else if (comp.types[0] == "locality")
                {
                    loc.city = comp.long_name;
                }
                else if (comp.types[0] == "administrative_area_level_1")
                {
                    loc.state = comp.long_name; // or province
                    loc.state_code = comp.short_name;
                }
                else if (comp.types[0] == "administrative_area_level_2")
                {
                    loc.county = comp.long_name;
                }
            }

            return loc;
        }
    }
}

