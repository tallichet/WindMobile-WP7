using System;
using System.Collections.Generic;
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
            //if (Seriliazers.ContainsKey(typeof(T)) == false)
            //{
            //    Seriliazers.Add(typeof(T), new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(T)));
            //}
            //var serializer = Seriliazers[typeof(T)];
            //return (T)serializer.ReadObject(s);
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
