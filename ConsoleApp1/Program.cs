using System;
using NetGeoCode;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            GeoCode geo = new GeoCode("YOUR KEY");
            var loc = geo.GetLocationFromZipCode("YOUR ZIPCODE");
            Console.WriteLine(loc.ToString());
            Console.ReadKey();
        }
    }
}
