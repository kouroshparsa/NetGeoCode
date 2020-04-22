using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetGeoCode;

namespace NetGeoCodeTests
{
    [TestClass]
    public class UnitTest1
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
        }
        public void TestMethod1()
        {
        }


    }
}
