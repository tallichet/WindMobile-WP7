using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch.Epyx.WindMobile.Core.Service
{
    public interface INetworkService
    {
        Task<List<Core.Model.Station>> ListStations(int limit = 100);
        Task<List<Core.Model.Station>> SearchStations(string searchCriteria);
        Task<List<Core.Model.Station>> GeoSearchStations(double latitude, double longitude, long distanceInMeters);
        Task<List<Core.Model.Station>> TextSearchStations(string searchCriteria);
        Task<Core.Model.Station> GetStation(string stationId);
        Task<List<Core.Model.StationData>> GetStationData(string stationId, TimeSpan duration);
        
    }
}
