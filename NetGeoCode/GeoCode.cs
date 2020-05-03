using System;
using Newtonsoft.Json;
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

    internal class Geometry
    {
        dynamic bounds;
        public Dictionary<string, double> location;
    }
    internal class ApiResult
    {
        public string formatted_address;
        public string place_id;
        public Geometry geometry;
        public dynamic types;
        public List<Address> address_components;
    }
    internal class ApiResponse
    {
        public string status;
        public List<ApiResult> results;
        public string error_message;
    }

    public class Location
    {
        public string country;
        public string state;
        public string county;
        public string city;
        public string country_code;
        public string state_code;
        public double lng;
        public double lat;
        public string status;
        public string zip;
        public Location() { }
        public Location(string country, string state, string county, string city, string country_code, string state_code, double lng, double lat)
        {
            this.country = country;
            this.state = state;
            this.county = county;
            this.city = city;
            this.country_code = country_code;
            this.state_code = state_code;
            this.lng = lng;
            this.lat = lat;
            this.status = "";
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

        public Location GetLocationFromAddress(string address)
        {
            /**
             * address is in the format: street_number,street_name,city,state,country
             */
            string url = string.Format(@"https://maps.googleapis.com/maps/api/geocode/json?address={0}&key={1}", address, googleApiKey);
            return GetLocation(url);
        }

        public Location GetLocationFromZipCode(string postal_code)
        {
            string url = string.Format(@"https://maps.googleapis.com/maps/api/geocode/json?components=postal_code:{0}&key={1}", postal_code, googleApiKey);
            return GetLocation(url);
        }
        private Location GetLocation(string url)
        {
            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream data = response.GetResponseStream();
            StreamReader reader = new StreamReader(data);

            // json-formatted string from maps api
            string jsonText = reader.ReadToEnd();
            response.Close();
            ApiResponse res = JsonConvert.DeserializeObject<ApiResponse>(jsonText);
            if (res.status == "OVER_DAILY_LIMIT")
            {
                throw new OverDailyLimitException();
            }
            else if (res.status == "OVER_QUERY_LIMIT")
            {
                throw new OverQueryLimitException();
            }
            else if (res.status == "REQUEST_DENIED")
            {
                throw new RequestDeniedException(res.error_message);
            }
            else if (res.status == "INVALID_REQUEST")
            {
                throw new InvalidRequestException(res.error_message);
            }
            else if (res.status != "OK" && res.status != "ZERO_RESULTS")
            {
                throw new Exception("Unknown Error");
            }

            Location loc = new Location();
            if (res.results.Count > 0)
            {
                loc.lng = res.results[0].geometry.location["lng"];
                loc.lat = res.results[0].geometry.location["lat"];
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
                    else if(comp.types[0] == "postal_code")
                    {
                        loc.zip = comp.short_name;
                    }
                }
            }
            else
            {
                throw new NoResultException("No results.");
            }
            loc.status = res.status;
            return loc;
        }
    }

    [Serializable]
    public class OverDailyLimitException : Exception
    {
        public OverDailyLimitException() { }

        public OverDailyLimitException(string msg) : base(msg)
        {
        }
    }

    [Serializable]
    public class OverQueryLimitException : Exception
    {
        public OverQueryLimitException() { }

        public OverQueryLimitException(string msg) : base(msg)
        {
        }
    }

    [Serializable]
    public class RequestDeniedException : Exception
    {
        public RequestDeniedException() { }

        public RequestDeniedException(string msg) : base(msg)
        {
        }
    }

    [Serializable]
    public class InvalidRequestException : Exception
    {
        public InvalidRequestException() { }

        public InvalidRequestException(string msg) : base(msg)
        {
        }
    }

    [Serializable]
    public class NoResultException : Exception
    {
        public NoResultException(string msg) : base(msg)
        {
        }
    }
}
