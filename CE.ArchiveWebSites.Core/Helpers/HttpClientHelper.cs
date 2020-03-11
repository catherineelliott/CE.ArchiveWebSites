using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using CE.ArchiveWebSites.Core.Models;

namespace CE.ArchiveWebSites.Core.Helpers
{
    public static class HttpClientHelper
    {
        public static async Task<ApiResponse<T>> GetFromLMARApi<T>(string url)
        {
            ApiResponse<T> responseObject = new ApiResponse<T>();
            
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(url);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    responseObject.ResponseHeaders = response.Headers;
                    var jsonString = await response.Content.ReadAsStringAsync();
                    responseObject.ResponseData = JsonConvert.DeserializeObject<T>(jsonString);
                }
            }
            return responseObject;
        }
    }
}
