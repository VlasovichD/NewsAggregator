using ConsoleClient.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;

namespace ConsoleClient
{
    internal class AdminRequests : BaseRequests
    {
        // Registration of new Admin
        // POST api/admin/users/regadmin
        public static string RegisterAdmin(string token, UserModel user)
        {
            using (var client = CreateClient(token))
            {
                var responseTask =
                    client.PostAsJsonAsync(APP_PATH + "/api/admin/users/regadmin", user);

                var responseMessage = TryGetResult(responseTask);

                var result = responseMessage.Content.ReadAsStringAsync().Result;

                // Deserialize received JSON-оbject
                UserModel userInfo = JsonConvert.DeserializeObject<UserModel>(result);

                return userInfo.Id.ToString();
            }
        }

        // GET api/admin/users
        public static IList<UserModel> GetAllUsers(string token)
        {
            using (var client = CreateClient(token))
            {
                var responseTask =
                    client.GetAsync(APP_PATH + "/api/admin/users");

                var responseMessage = TryGetResult(responseTask);

                var result = responseMessage.Content.ReadAsStringAsync().Result;

                // Deserialize received JSON-оbject
                List<UserModel> users =
                    JsonConvert.DeserializeObject<List<UserModel>>(result);

                return users;
            }
        }

        // GET api/admin/users/{collectionId}
        public static UserModel GetUserById(string token, int userId)
        {
            using (var client = CreateClient(token))
            {
                var responseTask =
                    client.GetAsync(APP_PATH + "/api/admin/users/" + userId);

                var responseMessage = TryGetResult(responseTask);

                var result = responseMessage.Content.ReadAsStringAsync().Result;

                // Deserialize received JSON-оbject
                UserModel user = JsonConvert.DeserializeObject<UserModel>(result);

                return user;
            }
        }

        // DELETE api/admin/users/{collectionId}
        public static string DeleteUser(string token, int userId)
        {
            using (var client = CreateClient(token))
            {
                var responseTask =
                    client.DeleteAsync(APP_PATH + "/api/admin/users/" + userId);

                var responseMessage = TryGetResult(responseTask);

                return responseMessage.StatusCode.ToString();
            }
        }

        // GET api/values 
        public static string GetServerInfo(string token)
        {
            using (var client = CreateClient(token))
            {
                var responseTask =
                    client.GetAsync(APP_PATH + "/api/values");

                var responseMessage = TryGetResult(responseTask);

                return responseMessage.Content.ReadAsStringAsync().Result;
            }
        }
    }
}
