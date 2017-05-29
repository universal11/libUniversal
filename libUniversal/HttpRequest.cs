using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace libUniversal
{
    public class HttpRequest
    {
        public static HttpResponseMessage Post(string url, string data)
        {
            HttpClient client = new HttpClient();
            return client.PostAsync(url, new StringContent(data)).Result;
        }

        public static HttpResponseMessage Get(string url)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = null;
            try
            {
                response = client.GetAsync(url).Result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Exception: {ex.Message}");
            }
            return response;
        }
    }
}