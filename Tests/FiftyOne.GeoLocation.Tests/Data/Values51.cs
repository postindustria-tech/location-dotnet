using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FiftyOne.GeoLocation.Tests.Data
{
    /// <summary>
    /// Test values using cloud Geo-Location engine using the 51Degrees 
    /// GeoLocation Provider.
    /// </summary>
    [TestClass]
    public class Values51
    {
        private IWrapper Wrapper;

        [TestInitialize]
        public void Init() {
            Wrapper = new Wrapper51Degrees();
        }

        [TestMethod]
        public void Values_Cloud_51_ValueTypes()
        {
            ValueTests.ValueTypes(Wrapper);
        }

        [TestMethod]
        public void Values_Cloud_51_AvailableProperties()
        {
            ValueTests.AvailableProperties(Wrapper);
        }

        [TestMethod]
        public void Values_Cloud_51_TypedGetters()
        {
            ValueTests.TypedGetters(Wrapper);
        }
    }
}
