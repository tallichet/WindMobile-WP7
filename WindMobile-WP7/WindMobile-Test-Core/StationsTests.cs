using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ch.Epyx.WindMobile.Core.Test
{
    [TestClass]
    public class StationsTests
    {
        [TestMethod]
        public async void TestLists()
        {
            var networkService = new Core.Service.NetworkService();

            var stations = await networkService.ListStations(10);

            Assert.IsNotNull(stations, "no stations retuned");
            Assert.AreEqual(10, stations.Count, "limit not respected");
            Assert.AreEqual("Demo Endesa", stations[0].ShortName, "not the correct short name");
        }
    }
}
