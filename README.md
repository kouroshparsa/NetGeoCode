# NetGeoCode
.Net package to use Google geocode api

Example:
```
using NetGeoCode;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            GeoCode geo = new GeoCode("YOUR KEY");
            string address = "225 N Jackson Ave, San Jose, CA, USA";
            var loc = geo.GetLocationFromAddress(address);
            Console.WriteLine(loc.lat + "," + loc.lng + " zip code=" + loc.zip);
			
            var loc = geo.GetLocationFromAddress(loc.zip);
            Console.WriteLine(loc.lat + "," + loc.lng);
        }
    }
}
```

Public methods:
GetLocationFromAddress(string address)  *address* is in the format: street_number,street_name,city,state,country
GetLocationFromZipCode(string postal_code)

returns a Location object with properties:
- country - string
- state - string
- county - string
- city - string
- country_code - string
- state_code - string
- lng - double - longitude
- lat - double - latitude
- zip - string
