using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ch.Epyx.WindMobile.Core.Service
{
    public class NetworkService : INetworkService
    {
        private Uri BacklogManApiBaseUri = new Uri("http://rtwind.mobi/api/2/");
        private HttpClient client;
        
        
        public Task<List<Core.Model.Station>> ListStations(int limit = 100)
        {
            var uri = new Uri(BacklogManApiBaseUri, "stations/?limit=" + limit);
            return DownloadDocument<List<Core.Model.Station>>(uri);
        }

        public Task<List<Core.Model.Station>> SearchStations(string searchCriteria)
        {
            var uri = new Uri(BacklogManApiBaseUri, "stations/?search=" + searchCriteria);
            return DownloadDocument<List<Core.Model.Station>>(uri);
        }

        public Task<List<Core.Model.Station>> GeoSearchStations(Model.Location location, long distanceInMeters)
        {
            var uri = new Uri(BacklogManApiBaseUri, string.Format("stations/?lat={0}&lon={1}&distance={2}", 
                location.Latitude.ToString(CultureInfo.InvariantCulture),
                location.Longitude.ToString(CultureInfo.InvariantCulture), 
                distanceInMeters));
            return DownloadDocument<List<Core.Model.Station>>(uri);
        }
        public async Task<List<Core.Model.Station>> TextSearchStations(string searchCriteria)
        {
            var uri = new Uri(BacklogManApiBaseUri, "stations/?word=" + searchCriteria);
            return (await DownloadDocument<List<Core.Model.TextSearchResult>>(uri)).Select(r => r.Station).ToList();
        }

        public Task<Core.Model.Station> GetStation(string stationId)
        {
            var uri = new Uri(BacklogManApiBaseUri, "stations/" + stationId + "/");
            return DownloadDocument<Core.Model.Station>(uri);
        }

        public Task<List<Core.Model.StationData>> GetStationData(string stationId, TimeSpan duration)
        {
            var uri = new Uri(BacklogManApiBaseUri, "stations/" + stationId + "/historic/?duration=" + (long)duration.TotalSeconds);
            return DownloadDocument<List<Core.Model.StationData>>(uri);
        }

        #region Network methods
        protected HttpClient Client
        {
            get
            {
                if (client == null)
                {
                    client = new HttpClient();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));                    
                }
                return client;
            }
        }

        private async Task<T> DownloadDocument<T>(Uri url)
        {
            var s = await Client.GetStringAsync(url);
            return Helper.Deserialize<T>(s);
        }

        private async Task<R> PostOrPutData<T, R>(Uri uri, T objectToSend, bool post = true)
        {
            var content = new StringContent(
                Helper.Serialize<T>(objectToSend),
                Encoding.UTF8,
                "application/json");

            HttpResponseMessage response;

            if (post)
            {
                response = await Client.PostAsync(uri, content);
            }
            else
            {
                response = await Client.PutAsync(uri, content);
            }

            if (response.IsSuccessStatusCode == false)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                throw new Exception("Unknwon error");
            }

            var json = await response.Content.ReadAsStringAsync();
            return Helper.Deserialize<R>(json);
        }

        private async Task Delete(Uri uri)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, uri);

            var response = await Client.SendAsync(request);

            if (response.IsSuccessStatusCode == false)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                throw new Exception("Unknwon error");
            }
        }


        #endregion
    }
}
