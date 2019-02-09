using ConsoleClient.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;

namespace ConsoleClient
{
    internal class BaseRequests
    {
        public static string APP_PATH;

        // Creating http-client with token 
        protected static HttpClient CreateClient(string accessToken = "")
        {
            var client = new HttpClient();
            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            }
            return client;
        }

        protected static void ErrorCheck(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = response.Content.ReadAsStringAsync().Result;

                // Deserialize received JSON-оbject
                Dictionary<string, string> error =
                    JsonConvert.DeserializeObject<Dictionary<string, string>>(errorMessage);

                throw new AppException($"{response.StatusCode.ToString()} {error?["message"]}");
            }
        }
    }
}
