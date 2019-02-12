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
                var responseTask =
                    client.PostAsJsonAsync(APP_PATH + "/api/collections", collection);

                var responseMessage = TryGetResult(responseTask);

                var result = responseMessage.Content.ReadAsStringAsync().Result;

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
                var responseTask =
                    client.GetAsync(APP_PATH + "/api/collections");

                var responseMessage = TryGetResult(responseTask);

                var result = responseMessage.Content.ReadAsStringAsync().Result;

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
                var responseTask =
                    client.GetAsync(APP_PATH + "/api/collections/" + collectionId);

                var responseMessage = TryGetResult(responseTask);

                var result = responseMessage.Content.ReadAsStringAsync().Result;

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
                var responseTask =
                    client.PutAsJsonAsync(APP_PATH + "/api/collections/" + collection.Id, collection);

                var responseMessage = TryGetResult(responseTask);

                return responseMessage.StatusCode.ToString();
            }
        }

        // DELETE api/collections/{collectionId}
        public static string DeleteCollection(string token, int collectionId)
        {
            using (var client = CreateClient(token))
            {
                var responseTask =
                    client.DeleteAsync(APP_PATH + "/api/collections/" + collectionId);

                var responseMessage = TryGetResult(responseTask);

                return responseMessage.StatusCode.ToString();
            }
        }
    }
}
