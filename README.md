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
-  country
-  state
-  county
-  city
-  country_code
-  state_code
