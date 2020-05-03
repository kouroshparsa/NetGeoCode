using System;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetGeoCode;

namespace NetGeoCodeTests
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void GetLocationFromZipCodeTest()
        {
            GeoCode geo = new GeoCode(ConfigurationManager.AppSettings["GoogleApiKey"]);
            var loc = geo.GetLocationFromZipCode("95116");
            Assert.AreEqual("US", loc.country_code);
            Assert.AreEqual("United States", loc.country);
            Assert.AreEqual("CA", loc.state_code);
            Assert.AreEqual("San Jose", loc.city);
            Assert.AreEqual(-121.8505679, loc.lng);
            Assert.AreEqual(37.3558627, loc.lat);
        }

        [TestMethod]
        [ExpectedException(typeof(RequestDeniedException))]
        public void BadKeyTest()
        {
            GeoCode geo = new GeoCode("BAD KEY");
            var loc = geo.GetLocationFromZipCode("95116");
        }

        [TestMethod]
        public void getLocationFromAddress()
        {
            string address = "225 N Jackson Ave, San Jose, CA, USA";
            GeoCode geo = new GeoCode(ConfigurationManager.AppSettings["GoogleApiKey"]);
            var loc = geo.GetLocationFromAddress(address);
            Assert.AreEqual("US", loc.country_code);
            Assert.AreEqual("United States", loc.country);
            Assert.AreEqual("CA", loc.state_code);
            Assert.AreEqual("San Jose", loc.city);
            Assert.AreEqual(-121.8489667, loc.lng);
            Assert.AreEqual(37.3622057, loc.lat);
            Assert.AreEqual("95116", loc.zip);
        }

    }
}
