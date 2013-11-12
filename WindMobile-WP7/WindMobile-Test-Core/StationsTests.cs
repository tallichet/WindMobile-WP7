using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Ch.Epyx.WindMobile.Core.Test
{
    [TestClass]
    public class StationsTests
    {
        [TestMethod]
        public async Task TestLists()
        {
            var networkService = new Core.Service.NetworkService();

            var stations = await networkService.ListStations(10);

            Assert.IsNotNull(stations, "no stations retuned");
            Assert.AreEqual(10, stations.Count, "limit not respected");
            Assert.AreEqual("Demo Endesa", stations[0].ShortName, "not the correct short name");
        }

        [TestMethod]
        public async Task TestSearch()
        {
            var networkService = new Core.Service.NetworkService();

            var stations = await networkService.SearchStations("dole");

            Assert.IsNotNull(stations, "no stations retuned");
            Assert.AreEqual(1, stations.Count, "found more than 'la dole'");
            Assert.AreEqual("La Dôle", stations[0].ShortName, "not the correct short name");
        }

        [TestMethod]
        public async Task TestTextSearchStation()
        {
            var networkService = new Core.Service.NetworkService();

            var stations = await networkService.TextSearchStations("sommet");

            Assert.IsNotNull(stations, "no stations retuned");
            Assert.AreEqual(3, stations.Count, "found too much stations");
            Assert.AreEqual("Sommet Puy de Dome", stations[0].ShortName, "not the correct short name");
        }

        [TestMethod]
        public async Task TestGeoSearchStations()
        {
            var networkService = new Core.Service.NetworkService();

            var stations = await networkService.GeoSearchStations(46.78, 6.63, 20000);

            Assert.IsNotNull(stations, "no stations retuned");
            Assert.AreEqual("Yverdon", stations[0].ShortName, "not the correct short name");
        } 
        
        [TestMethod]
        public async Task TestGetStationById()
        {
            var networkService = new Core.Service.NetworkService();

            var station = await networkService.GetStation("jdc-1001");

            Assert.IsNotNull(station, "no stations retuned");
            Assert.AreEqual("jdc-1001", station.ID, "not the correct ID");            
        }

        [TestMethod]
        public async Task TestGetStationDataJDC()
        {
            var networkService = new Core.Service.NetworkService();

            var stationData = await networkService.GetStationData("jdc-1001", TimeSpan.FromHours(1));

            Assert.IsNotNull(stationData, "no stations retuned");
            Assert.IsTrue(stationData.Count > 0, "no data for this station");
        }

        [TestMethod]
        public async Task TestGetStationDataFFVL()
        {
            var networkService = new Core.Service.NetworkService();

            var stationData = await networkService.GetStationData("ffvl-46", TimeSpan.FromHours(1));

            Assert.IsNotNull(stationData, "no stations retuned");
            Assert.IsTrue(stationData.Count > 0, "no data for this station");
        }
    }
}
