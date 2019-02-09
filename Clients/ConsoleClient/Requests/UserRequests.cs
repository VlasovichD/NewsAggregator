using ConsoleClient.Models;
using Newtonsoft.Json;
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
                var response =
                    client.PostAsJsonAsync(APP_PATH + "/api/users/registration", user).Result;

                ErrorCheck(response);

                var result = response.Content.ReadAsStringAsync().Result;

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

                var response =
                    client.PostAsJsonAsync(APP_PATH + "/api/users/authentication", authData).Result;

                var result = response.Content.ReadAsStringAsync().Result;

                ErrorCheck(response);

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
                var response =
                    client.GetAsync(APP_PATH + "/api/users/info").Result;

                ErrorCheck(response);

                var result = response.Content.ReadAsStringAsync().Result;

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
                var response =
                    client.PutAsJsonAsync(APP_PATH + "/api/users", user).Result;

                ErrorCheck(response);

                return response.StatusCode.ToString();
            }
        }
    }
}
