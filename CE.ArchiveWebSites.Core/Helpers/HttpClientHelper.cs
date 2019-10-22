using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CE.ArchiveWebSites.Core.Helpers
{
    public static class HttpClientHelper
    {
        public static async Task<T> GetFromLMARApi<T>(string url)
        {
            T reponseObject = default;
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(url);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    reponseObject = JsonConvert.DeserializeObject<T>(jsonString);
                }
            }
            return reponseObject;
        }
    }
}
