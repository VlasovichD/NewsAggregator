using ConsoleClient.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;

namespace ConsoleClient
{
    internal class CollectionRequests : BaseRequests
    {
         // POST api/collections
        public static string CreateCollection(string token, CollectionModel collection)
        {
            using (var client = CreateClient(token))
            {
                var response =
                    client.PostAsJsonAsync(APP_PATH + "/api/collections", collection).Result;

                ErrorCheck(response);

                var result = response.Content.ReadAsStringAsync().Result;

                // Deserialize received JSON-оbject
                CollectionModel collectionInfo = JsonConvert.DeserializeObject<CollectionModel>(result);

                return collectionInfo.Id.ToString();
            }
        }

        // GET api/collections
        public static IList<CollectionModel> GetAllCollections(string token)
        {
            using (var client = CreateClient(token))
            {
                var response =
                    client.GetAsync(APP_PATH + "/api/collections").Result;

                ErrorCheck(response);

                var result = response.Content.ReadAsStringAsync().Result;

                // Deserialize received JSON-оbject
                List<CollectionModel> collections =
                    JsonConvert.DeserializeObject<List<CollectionModel>>(result);

                return collections;
            }
        }

        // GET api/collections/{collectionId}
        public static CollectionModel GetCollectionById(string token, int collectionId)
        {
            using (var client = CreateClient(token))
            {
                var response =
                    client.GetAsync(APP_PATH + "/api/collections/" + collectionId).Result;

                ErrorCheck(response);

                var result = response.Content.ReadAsStringAsync().Result;

                // Deserialize received JSON-оbject
                CollectionModel collection = JsonConvert.DeserializeObject<CollectionModel>(result);

                return collection;
            }
        }

        // PUT api/collections/{collectionId}
        public static string UpdateCollection(string token, CollectionModel collection)
        {
            using (var client = CreateClient(token))
            {
                var response =
                    client.PutAsJsonAsync(APP_PATH + "/api/collections/" + collection.Id, collection).Result;

                ErrorCheck(response);

                return response.StatusCode.ToString();
            }
        }

        // DELETE api/collections/{collectionId}
        public static string DeleteCollection(string token, int collectionId)
        {
            using (var client = CreateClient(token))
            {
                var response =
                    client.DeleteAsync(APP_PATH + "/api/collections/" + collectionId).Result;

                ErrorCheck(response);

                return response.StatusCode.ToString();
            }
        }
    }
}
