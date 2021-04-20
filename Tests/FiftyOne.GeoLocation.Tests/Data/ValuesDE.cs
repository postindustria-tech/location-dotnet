using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FiftyOne.GeoLocation.Tests.Data
{
    /// <summary>
    /// Test values using cloud Geo-Location engine using the DigitalElement 
    /// GeoLocation Provider.
    /// </summary>
    [TestClass]
    public class ValuesDE
    {
        private IWrapper Wrapper;

        [TestInitialize]
        public void Init()
        {
            Wrapper = new WrapperDigitalElement();
        }

        [TestMethod]
        public void Values_Cloud_DE_ValueTypes()
        {
            ValueTests.ValueTypes(Wrapper);
        }

        [TestMethod]
        public void Values_Cloud_DE_AvailableProperties()
        {
            ValueTests.AvailableProperties(Wrapper);
        }

        [TestMethod]
        public void Values_Cloud_DE_TypedGetters()
        {
            ValueTests.TypedGetters(Wrapper);
        }
    }
}
