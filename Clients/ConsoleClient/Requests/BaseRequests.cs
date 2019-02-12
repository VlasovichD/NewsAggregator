using ConsoleClient.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

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

        protected static HttpResponseMessage TryGetResult(Task<HttpResponseMessage> responseTask)
        {
            try
            {
                var responseMessage = responseTask.Result;

                if (!responseMessage.IsSuccessStatusCode)
                {
                    // Deserialize received JSON-оbject
                    Dictionary<string, string> error =
                        JsonConvert.DeserializeObject<Dictionary<string, string>>(responseMessage.Content.ReadAsStringAsync().Result);

                    throw new AppException($"{responseMessage.StatusCode.ToString()} {error?["message"]}");
                }

                return responseMessage;
            }
            catch (Exception e)
            {
                throw new AppException($"{e.Message}");
            }
        }
    }
}
