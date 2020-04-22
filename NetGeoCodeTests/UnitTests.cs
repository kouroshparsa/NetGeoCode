using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetGeoCode;

namespace NetGeoCodeTests
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void GetLocationTest()
        {
            GeoCode geo = new GeoCode("YOUR KEY");
            var loc = geo.GetLocation("95116");
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
            var loc = geo.GetLocation("95116");
        }


    }
}
