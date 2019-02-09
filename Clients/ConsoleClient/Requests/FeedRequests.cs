using CodeHollow.FeedReader;
using ConsoleClient.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;

namespace ConsoleClient
{
    internal class FeedRequests : BaseRequests
    {
        // POST api/collections/{collectionId}/feeds
        public static string AddFeedToCollection(string token, FeedModel feed)
        {
            using (var client = CreateClient(token))
            {
                var response =
                    client.PostAsJsonAsync(APP_PATH + "/api/collections/" + feed.CollectionId + "/feeds", feed).Result;

                ErrorCheck(response);

                var result = response.Content.ReadAsStringAsync().Result;

                // Deserialize received JSON-оbject
                FeedModel feedInfo = JsonConvert.DeserializeObject<FeedModel>(result);

                return feedInfo.Id.ToString();
            }
        }

        // GET api/collections/{collectionId}/feeds
        public static IList<FeedModel> GetAllFeedsFromCollection(string token, int collectionId)
        {
            using (var client = CreateClient(token))
            {
                var response =
                    client.GetAsync(APP_PATH + "/api/collections/" + collectionId + "/feeds").Result;

                ErrorCheck(response);

                var result = response.Content.ReadAsStringAsync().Result;

                // Deserialize received JSON-оbject
                List<FeedModel> feeds = JsonConvert.DeserializeObject<List<FeedModel>>(result);

                return feeds;
            }
        }

        // GET api/collections/{collectionId}/feeds/{feedId}
        public static FeedModel GetFeedById(string token, int collectionId, int feedId)
        {
            using (var client = CreateClient(token))
            {
                var response =
                    client.GetAsync(APP_PATH + "/api/collections/" + collectionId + "/feeds/" + feedId).Result;

                ErrorCheck(response);

                var result = response.Content.ReadAsStringAsync().Result;

                // Deserialize received JSON-оbject
                FeedModel feed = JsonConvert.DeserializeObject<FeedModel>(result);

                return feed;
            }
        }

        // PUT api/collections/{collectionId}/feeds/{feedId}
        public static string UpdateFeedInCollection(string token, FeedModel feed)
        {
            using (var client = CreateClient(token))
            {
                var response =
                    client.PutAsJsonAsync(APP_PATH + "/api/collections/" + feed.CollectionId + "/feeds/" + feed.Id, feed).Result;

                ErrorCheck(response);

                return response.StatusCode.ToString();
            }
        }

        // DELETE api/collections/{collectionId}/feeds/{feedId}
        public static string RemoveFeedFromCollection(string token, int collectionId, int feedId)
        {
            using (var client = CreateClient(token))
            {
                var response =
                    client.DeleteAsync(APP_PATH + "/api/collections/" + collectionId + "/feeds/" + feedId).Result;

                ErrorCheck(response);

                return response.StatusCode.ToString();
            }
        }

        // GET api/collections/{collectionId}/feeds/content
        public static IEnumerable<FeedContentModel> ReadAllFeedsFromCollection(string token, int collectionId)
        {
            using (var client = CreateClient(token))
            {
                var response =
                    client.GetAsync(APP_PATH + "/api/collections/" + collectionId + "/feeds/content").Result;

                ErrorCheck(response);

                var result = response.Content.ReadAsStringAsync().Result;

                // Deserialize received JSON-оbject
                List<FeedContentModel> feeds = JsonConvert.DeserializeObject<List<FeedContentModel>>(result);

                return feeds;
            }
        }

        // GET api/collections/{collectionId}/feeds/{feedId}/content
        public static FeedContentModel ReadFeedById(string token, int collectionId, int feedId)
        {
            using (var client = CreateClient(token))
            {
                var response =
                    client.GetAsync(APP_PATH + "/api/collections/" + collectionId + "/feeds/" + feedId + "/content").Result;

                ErrorCheck(response);

                var result = response.Content.ReadAsStringAsync().Result;

                // Deserialize received JSON-оbject
                FeedContentModel feed = JsonConvert.DeserializeObject<FeedContentModel>(result);

                return feed;
            }
        }

        // GET api/collections/{collectionId}/feeds/url?url=
        public static FeedContentModel ReadFeedByUrl(string token, string url)
        {
            using (var client = CreateClient(token))
            {
                var response =
                    client.GetAsync(APP_PATH + "/api/collections/0/feeds/url?=" + url).Result;

                ErrorCheck(response);

                var result = response.Content.ReadAsStringAsync().Result;

                // Deserialize received JSON-оbject
                FeedContentModel feed = JsonConvert.DeserializeObject<FeedContentModel>(result);

                return feed;
            }
        }
    }
}
