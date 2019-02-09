using CodeHollow.FeedReader;
using FeedAggregator.BLL.Infrastructure;
using FeedAggregator.DAL.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedAggregator.BLL.Services
{
    public class FeedReadingService : IFeedReadingService
    {
        private readonly IUnitOfWork _database;
        private IMemoryCache cache;

        public FeedReadingService(IUnitOfWork uow, IMemoryCache memoryCache)
        {
            _database = uow;
            cache = memoryCache;
        }

        public IEnumerable<Feed> ReadAll(int collectionId, int userId)
        {
            var collection = _database.Collections.GetById(collectionId);

            if (collection == null || collection.UserId != userId)
                throw new ValidationException("Collection not found");

            var feeds = _database.Feeds.GetAll(f => f.CollectionId == collectionId).ToList();

            if (feeds == null)
                throw new ValidationException("List of feeds is empty");
            
            List<Feed> readFeeds = new List<Feed>();

            foreach (var feed in feeds)
            {
                try
                {
                    var reader = ReadFeedAsync(feed.Url);
                    readFeeds.Add(reader.Result);
                }
                catch (Exception ex)
                {
                    readFeeds.Add(new Feed { Title = ex.Message});
                }
            }

            return readFeeds;
        }

        public Feed ReadById(int collectionId, int feedId, int userId)
        {
            var feed = _database.Feeds.GetById(feedId);

            if (feed == null || feed.CollectionId != collectionId)
                throw new ValidationException("Feed not found");

            var collection = _database.Collections.GetById(collectionId);

            if (collection == null || collection.UserId != userId)
                throw new ValidationException("Feed not found");

            return ReadFeedAsync(feed.Url).Result;
        }

        public Feed ReadByUrl(string url)
        {
            return ReadFeedAsync(url).Result;
        }

        private async Task<Feed> ReadFeedAsync(string url)
        {
            if (!cache.TryGetValue(url, out Feed feed))
            {
                feed = await FeedReader.ReadAsync(url);
                if (feed != null)
                {
                    cache.Set(url, feed,
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
                }
            }
            return feed;
        }
    }
}
