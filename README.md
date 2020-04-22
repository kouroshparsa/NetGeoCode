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
            var loc = geo.GetLocation("YOU ZIPCODE");
            Console.WriteLine(loc.ToString());
        }
    }
}
```

Public methods:
GetLocation(string postal_code)
returns a Location object with properties:
- country - string
- state - string
- county - string
- city - string
- country_code - string
- state_code - string
- lng - double - longitude
- lat - double - latitude