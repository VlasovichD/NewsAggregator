using ConsoleClient.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace ConsoleClient
{
    internal class UserRequests : BaseRequests
    {
        // Registration
        public static string Register(UserModel user)
        {
            using (var client = new HttpClient())
            {
                var responseTask =
                    client.PostAsJsonAsync(APP_PATH + "/api/users/registration", user);

                var responseMessage = TryGetResult(responseTask);

                var result = responseMessage.Content.ReadAsStringAsync().Result;

                // Deserialize received JSON-оbject
                UserModel userInfo = JsonConvert.DeserializeObject<UserModel>(result);

                return userInfo.Id.ToString();
            }
        }

        // Authenticate and return user info (with token)
        public static Dictionary<string, string> Authenticate(string userName, string password)
        {
            using (var client = new HttpClient())
            {

                var authData = new { Username = userName, Password = password };

                var responseTask =
                    client.PostAsJsonAsync(APP_PATH + "/api/users/authentication", authData);

                var responseMessage = TryGetResult(responseTask);

                var result = responseMessage.Content.ReadAsStringAsync().Result;

                // Deserialize received JSON-оbject
                Dictionary<string, string> userInfo =
                    JsonConvert.DeserializeObject<Dictionary<string, string>>(result);

                return userInfo;
            }
        }



        // GET api/users/info
        public static UserModel GetUserInfo(string token)
        {
            using (var client = CreateClient(token))
            {
                var responseTask =
                    client.GetAsync(APP_PATH + "/api/users/info");

                var responseMessage = TryGetResult(responseTask);

                var result = responseMessage.Content.ReadAsStringAsync().Result;

                // Deserialize received JSON-оbject
                UserModel userInfo = JsonConvert.DeserializeObject<UserModel>(result);

                return userInfo;
            }
        }

        // PUT api/users
        public static string UpdateUser(string token, UserModel user)
        {
            using (var client = CreateClient(token))
            {
                var responseTask =
                    client.PutAsJsonAsync(APP_PATH + "/api/users", user);

                var responseMessage = TryGetResult(responseTask);

                return responseMessage.StatusCode.ToString();
            }
        }
    }
}
